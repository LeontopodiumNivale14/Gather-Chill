using ECommons.Automation.NeoTaskManager;
using ECommons.Configuration;
using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.GrandCompany;
using GatherChill.IPC;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Handlers;
using GatherChill.Ui;
using GatherChill.Utilities.Replay;
using GatherChill.Utilities.Tools;
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
    internal GatheringOverlay gatherOverlay;
    internal CalculatorWindow calculatorWindow;
    internal ReplayViewerWindow replayWindow;

    // Taskmanager from Ecommons
    internal TaskManager TaskManager;
    internal TaskManager navTask;

    // Internal IPC's that I use for... well plugins. 
    internal LifestreamIPC lifestream;
    internal NavmeshIPC navmesh;
    internal PandoraIPC pandora;

    // IPC that other plugins call into (RelicTracker, ...)
    internal GatherChillProvider ipcProvider;

    internal GcProvisioning Gc;

    // putting this here to initialize all the routes, instead of having to go a roundbout way of accessing it...
    internal GatheringRouteLoader routeEditor;

    public GatherChill(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, P, ECommons.Module.DalamudReflector, ECommons.Module.ObjectFunctions);
        new ECommons.Schedulers.TickScheduler(Load);
        PctService.Initialize(pi);
    }

    public void Load()
    {
        EzConfig.Migrate<Config>();
        config = EzConfig.Init<Config>();
        if (C.User_AutoProfile.Migrate())
            EzConfig.Save();
        C.MigrateCordials();

        //IPC's that are used
        TaskManager = new(TaskConfig);
        navTask = new(TaskConfig);
        lifestream = new();
        navmesh = new();
        pandora = new();
        Gc = new();

        routeEditor = new();
        routeEditor.LoadAllRoutes();
        if (C.LoadExternalRoutes)
            routeEditor.LoadRoutesFromDirectory(C.SaveLocation);

        // all the windows
        windowSystem = new();
        mainWindow = new();
        settingWindow = new();
        debugWindow = new();
        routeWindow = new();
        gatherOverlay = new();
        calculatorWindow = new();
        replayWindow = new();

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
            /gatherchill start - Starts running list
            /gatherchill stop - Stops plugin
            /gatherchill overlay - opens the overlay
            /forager - other option
            /chill - other OTHER option
            """);
        EzCmd.Add("/forager", OnCommand);
        EzCmd.Add("/chill", OnCommand);
        Svc.Framework.Update += Tick;
        Svc.PluginInterface.UiBuilder.Draw += OnDraw;

        ExcelHelper.Init();
        UpdateSheetInfo();

        // After the sheets load, so CanGather / AddToGatherList know every gatherable
        ipcProvider = new();
    }

    private void Tick(object _)
    {
        Gc.Update();
        if (Player.Available)
        {
            SchedulerMain.Tick();
        }
        Pandora_Handler.Tick();
        TextAdvancedManager.Tick();
        YesAlreadyManager.Tick();
        Gather_Helper.RemoveTimedRoutes();
        GatherJobs.Tick();
        ReplayRecorder.Tick();
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
        Safe(() => Gc?.Dispose());
        Safe(() => ReplayRecorder.EndSession("Plugin unloaded", sync: true));
        ECommonsMain.Dispose();
        Safe(TextAdvancedManager.UnlockTA);
        Safe(YesAlreadyManager.Unlock);
        PctService.Dispose();
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
        else if (firstArg.ToLower() == "start")
        {
            Gather_Helper.State = Enums.IceState.Start;
            return;
        }
        else if (firstArg.ToLower() == "stop")
        {
            SchedulerMain.DisablePlugin();
            return;
        }
        else if (firstArg.ToLower() == "overlay")
        {
            gatherOverlay.IsOpen = true;
            return;
        }
        else if (firstArg.ToLower() == "replay" || firstArg.ToLower() == "replays")
        {
            replayWindow.IsOpen = true;
            return;
        }
    }
}