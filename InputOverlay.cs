using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Mod.InputOverlay;

public class InputOverlay
{
    private static float MARGIN = 10f;
    private static float PADDING = 5f;
    private static float SIZE = 80f;
    private static Color ACTIVE_COLOR = Color.DarkSlateBlue * 0.8f;
    private static Color INACTIVE_COLOR = Color.DarkSlateGray * 0.8f;


    private static PixelFont Font => Fonts.Get(Dialog.Languages["english"].FontFace);
    
    private static PixelFontSize FontSize => Font.Get(BaseSize);
    
    private static float BaseSize => Dialog.Languages["english"].FontFaceSize;
    
    private static float LineHeight => FontSize.LineHeight;
    
    private static float SCALE = 0.5f;
    
    private static Vector2 Measure(string text) => FontSize.Measure(text) * SCALE;
    
    public static void Load() {
        On.Celeste.Level.Render += LevelOnRender;
    }
    
    public static void Unload() {
        On.Celeste.Level.Render -= LevelOnRender;
    }

    private static void LevelOnRender(On.Celeste.Level.orig_Render orig, Level self)
    {
        orig(self);
        
        if (!InputOverlayModule.Settings.ShowOverlay)
            return;
        
        Draw.SpriteBatch.Begin();
        RenderKey(MInput.Keyboard.Check(Input.Grab.Binding.Keyboard[0]), "Grab", 0, 0);
        RenderKey(MInput.Keyboard.Check(Input.CrouchDash.Binding.Keyboard[0]), "Demo", 1, 0);
        RenderKey(MInput.Keyboard.Check(Input.Dash.Binding.Keyboard[0]), "Dash", 2, 0);
        
        RenderKey(MInput.Keyboard.Check(Input.MenuRight.Binding.Keyboard[0]), "Right", 3, 0);
        RenderKey(MInput.Keyboard.Check(Input.MenuDown.Binding.Keyboard[0]), "Down", 4, 0);
        RenderKey(MInput.Keyboard.Check(Input.MenuLeft.Binding.Keyboard[0]), "Left", 5, 0);
        RenderKey(MInput.Keyboard.Check(Input.MenuUp.Binding.Keyboard[0]), "Up", 4, 1);
        
        RenderKey(MInput.Keyboard.Check(Input.Talk.Binding.Keyboard[0]), "Talk", 3, 1);
        RenderKey(MInput.Keyboard.Check(Input.Pause.Binding.Keyboard[0]), "Menu", 5, 1);

        for (var x = 0; x < Input.Jump.Binding.Keyboard.Count; x++)
            RenderKey(MInput.Keyboard.Check(Input.Jump.Binding.Keyboard[x]), "Jump", x, 1);
        Draw.SpriteBatch.End();
    }

    private static void RenderKey(bool isActive, string name, int x, int y)
    {
        var xPos = Engine.ViewWidth - MARGIN - x * (PADDING + SIZE) - SIZE;
        var yPos = Engine.ViewHeight - MARGIN - y * (PADDING + SIZE) - SIZE;

        Draw.Rect(xPos, yPos, SIZE, SIZE, isActive ? ACTIVE_COLOR : INACTIVE_COLOR);
        var bounds = Measure(name);
        Font.Draw(
            BaseSize,
            name,
            new Vector2(xPos + (SIZE - bounds.X) / 2, yPos + (SIZE - bounds.Y) / 2),
            Vector2.Zero,
            Vector2.One * SCALE,
            Color.White
        );
    }
}