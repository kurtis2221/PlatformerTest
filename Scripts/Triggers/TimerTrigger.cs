using Godot;
using System;

public partial class TimerTrigger : Timer
{
    [Export]
    public Node3D target;

    public override void _Ready()
    {
        Timeout += TimerTrigger_Timeout;
    }

    private void TimerTrigger_Timeout()
    {
        ((IntObject)target).Activate(null);
    }
}
