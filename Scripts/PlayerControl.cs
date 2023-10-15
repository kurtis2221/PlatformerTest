using Godot;
using System;

public partial class PlayerControl : CharacterBody3D
{
    public static PlayerControl inst;

    [Export]
    public Node3D camera;
    [Export]
    public CollisionShape3D player_shape;
    [Export]
    public ShapeCast3D player_cast;

    public int zone_type;
    public bool on_floor;
    public bool crouching;
    bool reset_height = false;

    const float height_normal = 2.0f;
    const float height_crouch = 1.0f;

    const float walk_speed = 5.0f;
    const float run_speed = 8.0f;
    const float jump_velocity = 4.5f;
    const float water_velocity = -1.0f;
    const float water_jump_velocity = 4.5f;

    float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    CapsuleShape3D player_col;
    Vector3 cam_normal;
    Vector3 cam_crouch;

    public override void _Ready()
    {
        inst = this;
        player_col = (CapsuleShape3D)player_shape.Shape;
        cam_normal = camera.Position;
        cam_crouch = cam_normal;
        cam_crouch.Y -= (height_normal - height_crouch) / 2;
        player_cast.Position -= cam_crouch;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (reset_height)
        {
            player_col.Height = height_normal;
            reset_height = false;
        }
        Vector3 velocity = Velocity;
        float speed = Input.IsActionPressed("Run") ? run_speed : walk_speed;

        bool in_water = zone_type != 0;
        on_floor = IsOnFloor();

        if (in_water) velocity.Y = zone_type == 1 ? water_velocity : 0f;
        else if (!on_floor) velocity.Y -= gravity * (float)delta;

        if (Input.IsActionPressed("Jump"))
        {
            if (in_water) velocity.Y = water_jump_velocity;
            else if (on_floor) velocity.Y = jump_velocity;
        }

        if (Input.IsActionPressed("Crouch"))
        {
            if (in_water) velocity.Y = -water_jump_velocity;
            else if (!crouching)
            {
                camera.Position = cam_crouch;
                //Sniper crouching bug feature if commented
                //Position += cam_crouch;
                player_col.Height = height_crouch;
                crouching = true;
            }
        }
        else if (crouching)
        {
            if (player_cast.CollisionResult.Count <= 1)
            {
                camera.Position = cam_normal;
                Position -= cam_crouch;
                reset_height = true;
                crouching = false;
            }
        }

        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Backward");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        velocity.X = direction.X * speed;
        velocity.Z = direction.Z * speed;
        if (in_water)
        {
            direction = (camera.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
            velocity.Y += direction.Y * speed;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
