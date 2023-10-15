using Godot;
using System;

public partial class Teleport : Node3D, IntObject
{
    public void Activate(Node3D act)
    {
        act.GlobalPosition = GlobalPosition;
    }
}
