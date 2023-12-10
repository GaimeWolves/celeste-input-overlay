using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.InputOverlay;

public class InputOverlayModule : EverestModule {
    public static InputOverlayModule Instance { get; private set; }

    public override Type SettingsType => typeof(InputOverlayModuleSettings);
    public static InputOverlayModuleSettings Settings => (InputOverlayModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(InputOverlayModuleSession);
    public static InputOverlayModuleSession Session => (InputOverlayModuleSession) Instance._Session;

    public InputOverlayModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(InputOverlayModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(InputOverlayModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        InputOverlay.Load();
    }

    public override void Unload() {
        InputOverlay.Unload();
    }
}