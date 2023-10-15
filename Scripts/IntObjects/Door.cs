using Godot;
using System;

public partial class Door : StaticBody3D, IntObject
{
    [Export]
    public Node3D dest;

    [Export]
    public bool is_open;

    [Export]
    public bool wait_door;

    [Export]
    public float speed;

    Vector3 door_close, door_open;
    bool moving;

    public override void _Ready()
    {
        door_close = GlobalPosition;
        door_open = dest.GlobalPosition;
        is_open = !is_open;
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (is_open)
        {
            GlobalPosition = GlobalPosition.MoveToward(door_close, speed);
            if (GlobalPosition.DistanceSquaredTo(door_close) < 0.01f)
            {
                GlobalPosition = door_close;
                moving = false;
                SetPhysicsProcess(false);
            }
        }
        else
        {
            GlobalPosition = GlobalPosition.MoveToward(door_open, speed);
            if (GlobalPosition.DistanceSquaredTo(door_open) < 0.01f)
            {
                GlobalPosition = door_open;
                moving = false;
                SetPhysicsProcess(false);
            }
        }
    }

    public void Activate(Node3D act)
    {
        if (wait_door && moving) return;
        SetPhysicsProcess(true);
        moving = true;
        is_open = !is_open;
    }
}
