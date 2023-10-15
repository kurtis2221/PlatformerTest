using Godot;
using System;

public partial class HeadBob : Node3D
{
    [Export]
    public Node3D player;

    const float move_dist = 0.005f;
    const float move_dist2 = 0.01f;

    const float move_speed = 0.01f;
    const float move_speed2 = 0.02f;
    const float move_limit = 0.1f;

    Vector3 normal_pos;
    Vector3 old_pos;

    Vector3 current_pos;

    bool rev;

    public override void _Ready()
    {
        current_pos = Position;
    }

    public override void _Process(double delta)
    {
        Vector3 pos = player.Position;
        float dist = pos.DistanceSquaredTo(old_pos);
        if (PlayerControl.inst.on_floor && dist >= move_dist)
        {
            float speed = dist >= move_dist2 ? move_speed2 : move_speed;
            if (rev)
            {
                current_pos.Y += speed;
                if (current_pos.Y >= move_limit)
                {
                    rev = false;
                }
            }
            else
            {
                current_pos.Y -= speed;
                if (current_pos.Y <= -move_limit)
                {
                    rev = true;
                }
            }
            Position = current_pos;
        }
        else
        {
            Position = Position.MoveToward(normal_pos, move_speed);
        }
        old_pos = pos;
    }
}
