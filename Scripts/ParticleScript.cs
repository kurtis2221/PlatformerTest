using Godot;
using System;

public partial class ParticleScript : Node3D
{
    [Export]
    public float time;

    double cnt;

    public override void _PhysicsProcess(double delta)
    {
        cnt += delta;
        if (cnt >= time) QueueFree();
    }
}
