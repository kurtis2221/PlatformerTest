using Godot;
using System;

public partial class Explosive : Node3D
{
    static PackedScene expl = GD.Load<PackedScene>("res://Prefabs/Explosion.tscn");

    public void Activate()
    {
        Node3D tmp = (Node3D)expl.Instantiate();
        tmp.Position = GlobalPosition;
        GetTree().CurrentScene.AddChild(tmp);
        QueueFree();
    }
}
