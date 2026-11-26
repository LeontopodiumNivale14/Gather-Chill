using Dalamud.Interface.Windowing;

namespace GatherChill.Ui.Windows
{
    internal class ReplayViewerWindow : Window
    {
        private readonly ReplayViewer _viewer = new();

        public ReplayViewerWindow() : base("Replay Viewer###GatherChill_ReplayViewer")
        {
            P.windowSystem.AddWindow(this);

            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new(700, 400),
                MaximumSize = new(float.MaxValue, float.MaxValue),
            };
        }

        public override void OnOpen() => _viewer.Refresh();

        public override void Draw() => _viewer.Draw();
    }
}