using Godot;
using System;

public partial class WeaponScript : Node3D
{
    [Export]
    public RayCast3D ray;

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("Fire"))
        {
            Explosive tmp = ray.GetCollider() as Explosive;
            if (tmp == null) return;
            tmp.Activate();
        }
    }
}
