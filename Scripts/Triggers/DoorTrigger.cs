using Godot;
using System;

public partial class DoorTrigger : Area3D
{
    [Export]
    public Node3D target;

    public override void _Ready()
    {
        BodyEntered += DoorTrigger_BodyEntered;
        BodyExited += DoorTrigger_BodyEntered;
    }

    private void DoorTrigger_BodyEntered(Node3D body)
    {
        if (body != PlayerControl.inst) return;

        ((IntObject)target).Activate(body);
    }
}
