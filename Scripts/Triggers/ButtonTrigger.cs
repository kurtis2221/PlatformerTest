using Godot;
using System;

public partial class ButtonTrigger : Node3D, IntObject
{
    [Export]
    public Node3D target;

    [Export]
    Vector3 dest;

    [Export]
    bool is_open;

    [Export]
    float speed;

    Vector3 door_close, door_open;
    bool moving;

    public override void _Ready()
    {
        door_close = GlobalPosition;
        door_open = door_close + dest;
        is_open = !is_open;
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (is_open)
        {
            GlobalPosition = GlobalPosition.MoveToward(door_close, speed);
            if (GlobalPosition.DistanceSquaredTo(door_close) < 0.0001f)
            {
                GlobalPosition = door_close;
                moving = false;
                SetPhysicsProcess(false);
            }
        }
        else
        {
            GlobalPosition = GlobalPosition.MoveToward(door_open, speed);
            if (GlobalPosition.DistanceSquaredTo(door_open) < 0.0001f)
            {
                GlobalPosition = door_open;
                moving = false;
                SetPhysicsProcess(false);
            }
        }
    }



    public void Activate(Node3D act)
    {
        if (moving) return;
        SetPhysicsProcess(true);
        moving = true;
        is_open = !is_open;
        ((IntObject)target).Activate(act);
    }
}

