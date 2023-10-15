using Godot;
using System;

public partial class ActivateTrigger : Area3D
{
    [Export]
    public Node3D target;

    [Export]
    public bool once;

    public override void _Ready()
    {
        BodyEntered += ActivateTrigger_BodyEntered;
    }

    private void ActivateTrigger_BodyEntered(Node3D body)
    {
        if (body != PlayerControl.inst) return;

        ((IntObject)target).Activate(body);
        if (once) QueueFree();
    }
}
