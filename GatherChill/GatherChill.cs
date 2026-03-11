using ECommons.Automation.NeoTaskManager;
using ECommons.Configuration;
using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.IPC;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Handlers;
using GatherChill.Ui;
using Pictomancy;

namespace GatherChill;

public sealed class GatherChill : IDalamudPlugin
{
    public string Name => "GatherChill";
    internal static GatherChill P = null!;
    public static Config C => P.config;
    private Config config;

    // Window's that I use, base window to the settings... need these to actually show shit 
    internal WindowSystem windowSystem;
    internal MainWindow mainWindow;
    internal SettingsWindow settingWindow;
    internal DebugWindow debugWindow;
    internal RouteEditor_Window routeWindow;

    // Taskmanager from Ecommons
    internal TaskManager taskManager;

    // Internal IPC's that I use for... well plugins. 
    internal LifestreamIPC lifestream;
    internal NavmeshIPC navmesh;
    internal PandoraIPC pandora;

    // putting this here to initialize all the routes, instead of having to go a roundbout way of accessing it...
    internal GatheringRouteLoader routeEditor;

    public GatherChill(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, P, ECommons.Module.DalamudReflector, ECommons.Module.ObjectFunctions);
        new ECommons.Schedulers.TickScheduler(Load);
        PictoService.Initialize(pi);
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

        routeEditor = new();
        routeEditor.LoadAllRoutes();

        // all the windows
        windowSystem = new();
        mainWindow = new();
        settingWindow = new();
        debugWindow = new();
        routeWindow = new();

        taskManager = new(new(abortOnTimeout: true, timeLimitMS: 20000, showDebug: false));
        Svc.PluginInterface.UiBuilder.Draw += windowSystem.Draw;
        Svc.PluginInterface.UiBuilder.OpenMainUi += () =>
        {
            mainWindow.IsOpen = true;
        };
        Svc.PluginInterface.UiBuilder.OpenConfigUi += () =>
        {
            debugWindow.IsOpen = true;
        };
        EzCmd.Add("/gatherchill", OnCommand, """
            Open plugin interface
            """);
        EzCmd.Add("/icegather", OnCommand);
        Svc.Framework.Update += Tick;
        Svc.PluginInterface.UiBuilder.Draw += OnDraw;

        UpdateSheetInfo();
    }

    private void Tick(object _)
    {
        if (Player.Available)
        {
            SchedulerMain.Tick();
        }
        GenericManager.Tick();
        TextAdvancedManager.Tick();
        YesAlreadyManager.Tick();
    }

    public void OnDraw()
    {
        if (Player.Available)
        {
            PictoManager.DrawPicto();
        }
    }

    public void Dispose()
    {
        Safe(() => Svc.Framework.Update -= Tick);
        Safe(() => Svc.PluginInterface.UiBuilder.Draw -= windowSystem.Draw);
        Safe(() => Svc.PluginInterface.UiBuilder.Draw -= OnDraw);
        ECommonsMain.Dispose();
        Safe(TextAdvancedManager.UnlockTA);
        Safe(YesAlreadyManager.Unlock);
        PictoService.Dispose();
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
        else if (firstArg.ToLower() == "r" || firstArg.ToLower() == "routes")
        {
            routeWindow.IsOpen = true;
            return;
        }
    }
}