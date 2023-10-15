using Godot;
using System;

public partial class DamageTrigger : Area3D
{
    [Export]
    public int damage;

    [Export]
    public float time;

    double cnt;

    public override void _PhysicsProcess(double delta)
    {
        if (OverlapsBody(PlayerControl.inst))
        {
            cnt += delta;
            if (cnt >= time)
            {
                cnt = 0;
                MainScript.inst.Damage(damage);
            }
        }
        else cnt = 0;
    }
}
