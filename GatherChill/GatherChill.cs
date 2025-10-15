using ECommons.Automation.NeoTaskManager;
using ECommons.Configuration;
using ECommons.Logging;
using GatherChill.GatheringInfo;
using GatherChill.IPC;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Handlers;
using GatherChill.Ui;
using GatherChill.Yaml;
using GatherChill.Yaml.ConfigFiles;

namespace GatherChill;

public sealed class GatherChill : IDalamudPlugin
{
    public string Name => "GatherChill";
    internal static GatherChill P = null!;
    public static Config C => P.config;
    private Config config;

    private static GatherIndex? GatherIndexConfig;
    public static GatherIndex GIndex => GatherIndexConfig ??= LoadConfig<GatherIndex>();
    public static RouteData RD => P.routeData;
    private RouteData routeData = null!;  // Make this private instance field

    private static T LoadConfig<T>() where T : IYamlConfig, new()
    {
        var path = typeof(T).GetProperty("ConfigPath")!.GetValue(null)!.ToString()!;
        var config = YamlConfig.Load<T>(path);

        if (config == null)
        {
            PluginLog.Warning($"[{typeof(T).Name}] Config was null. Creating new default.");
            config = new T();
            YamlConfig.Save(config, path);
        }

        PluginLog.Information($"[{typeof(T).Name}] Loaded from {path}");
        return config;
    }

    // Window's that I use, base window to the settings... need these to actually show shit 
    internal WindowSystem windowSystem;
    internal MainWindow mainWindow;
    internal SettingsWindow settingWindow;
    internal DebugWindow debugWindow;
    internal RouteEditorWindow routeEditorWindow;

    // Taskmanager from Ecommons
    internal TaskManager taskManager;

    // Internal IPC's that I use for... well plugins. 
    internal LifestreamIPC lifestream;
    internal NavmeshIPC navmesh;
    internal PandoraIPC pandora;

    public GatherChill(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, P, ECommons.Module.DalamudReflector, ECommons.Module.ObjectFunctions);
        new ECommons.Schedulers.TickScheduler(Load);
    }

    public void Load()
    {
        EzConfig.Migrate<Config>();
        config = EzConfig.Init<Config>();

        //IPC's that are used
        taskManager = new();
        lifestream = new();
        navmesh = new();
        pandora = new();

        // all the windows
        windowSystem = new();
        mainWindow = new();
        settingWindow = new();
        debugWindow = new();
        routeEditorWindow = new();

        taskManager = new(new(abortOnTimeout: true, timeLimitMS: 20000, showDebug: true));
        Svc.PluginInterface.UiBuilder.Draw += windowSystem.Draw;
        Svc.PluginInterface.UiBuilder.OpenMainUi += () =>
        {
            mainWindow.IsOpen = true;
        };
        Svc.PluginInterface.UiBuilder.OpenConfigUi += () =>
        {
            settingWindow.IsOpen = true;
        };
        EzCmd.Add("/gatherchill", OnCommand, """
            Open plugin interface
            """);
        EzCmd.Add("/icegather", OnCommand);
        Svc.Framework.Update += Tick;

        RouteInfoCreator();
        RouteConfigManager.EnsureRouteConfigsExist();

        routeData = new RouteData();  // lowercase
        routeData.LoadAllRoutes();
        PluginLog.Information($"Route data loaded: {routeData.GatheringInfo.Count} expansions");

        // Log details
        foreach (var (expId, zones) in routeData.GatheringInfo)
        {
            PluginLog.Information($"  Expansion {expId}: {zones.Count} zones");
        }
    }

    private void Tick(object _)
    {
        if (SchedulerMain.AreWeTicking && Svc.ClientState.LocalPlayer != null)
        {
            SchedulerMain.Tick();
        }
        GenericManager.Tick();
        TextAdvancedManager.Tick();
        YesAlreadyManager.Tick();
    }

    public void Dispose()
    {
        Safe(() => Svc.Framework.Update -= Tick);
        Safe(() => Svc.PluginInterface.UiBuilder.Draw -= windowSystem.Draw);
        ECommonsMain.Dispose();
        Safe(TextAdvancedManager.UnlockTA);
        Safe(YesAlreadyManager.Unlock);

        Safe(() => routeData?.GatheringInfo.Clear());
        Safe(() => routeData = null!);
    }

    private void OnCommand(string command, string args)
    {
        var subcommands = args.Split(' ');

        if (subcommands.Length == 0 || args == "")
        {
            mainWindow.IsOpen = !mainWindow.IsOpen;
            return;
        }

        var firstArg = subcommands[0];

        if (firstArg.ToLower() == "d" || firstArg.ToLower() == "debug")
        {
            debugWindow.IsOpen = true;
            return;
        }
        else if (firstArg.ToLower() == "s" || firstArg.ToLower() == "settings")
        {
            settingWindow.IsOpen = true;
            return;
        }
        // Add this new command
        else if (firstArg.ToLower() == "r" || firstArg.ToLower() == "routes")
        {
            routeEditorWindow.IsOpen = true;
            return;
        }
    }
}
