using Godot;
using System;

public partial class PushTrigger : Area3D
{
    [Export]
    public Vector3 dest;

    [Export]
    public float speed;

    public override void _Ready()
    {
        BodyEntered += PushTrigger_BodyEntered;
    }

    private void PushTrigger_BodyEntered(Node3D body)
    {
        if (body != PlayerControl.inst) return;
        PlayerControl.inst.Velocity = dest * speed;
    }
}
