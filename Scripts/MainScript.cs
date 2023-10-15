using Godot;
using System;

public partial class MainScript : Node3D
{
    public static MainScript inst;

    [Export]
    public Color cross_color_int;

    Color cross_color_normal;

    [Export]
    public RayCast3D ray;

    [Export]
    public RayCast3D ray_ins;

    [Export]
    public TextureRect gui_cross;

    [Export]
    public Label gui_health;

    [Export]
    public int health = 100;

    public override void _Ready()
    {
        inst = this;
        cross_color_normal = gui_cross.Modulate;
    }

    public override void _PhysicsProcess(double delta)
    {
        Inspect();
        if (Input.IsActionJustPressed("Use"))
        {
            ButtonTrigger tmp = ray.GetCollider() as ButtonTrigger;
            if (tmp == null) return;
            tmp.Activate(null);
        }
    }

    void Inspect()
    {
        GodotObject tmp = ray_ins.GetCollider();
        if (tmp is Explosive || tmp is ButtonTrigger)
            gui_cross.Modulate = cross_color_int;
        else
            gui_cross.Modulate = cross_color_normal;
    }

    public void Damage(int dmg)
    {
        if (health < 0) return;
        health -= dmg;
        gui_health.Text = health.ToString();
    }
}
