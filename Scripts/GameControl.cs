using Godot;
using System;

public partial class GameControl : Node3D
{
    public override void _Ready()
    {
        Window win = GetWindow();
        win.GrabFocus();
#if DEBUG
        win.Mode = Window.ModeEnum.Windowed;
        win.Size = new Vector2I(1280, 720);
#endif
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GetTree().Quit();
        }
    }
}
