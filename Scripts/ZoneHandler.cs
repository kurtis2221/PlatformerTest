using Godot;
using System;
using System.Collections.Generic;

public partial class ZoneHandler : Node3D
{
    class ZoneType
    {
        public Aabb area;
        public int type;
    }

    List<ZoneType> zones;

    public override void _Ready()
    {
        zones = new List<ZoneType>();
        foreach (Node node in FindChild("Water").GetChildren())
        {
            ZoneType tmp = new ZoneType();
            tmp.area = ((MeshInstance3D)node).GetAabb();
            tmp.area.Position += ((Node3D)node).GlobalPosition;
            tmp.type = 1;
            zones.Add(tmp);
        }
        foreach (Node node in FindChild("Ladder").GetChildren())
        {
            ZoneType tmp = new ZoneType();
            tmp.area = ((MeshInstance3D)node).GetAabb();
            tmp.area.Position += ((Node3D)node).GlobalPosition;
            tmp.type = 2;
            zones.Add(tmp);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        PlayerControl player = PlayerControl.inst;
        foreach (ZoneType zone in zones)
        {
            if (zone.area.HasPoint(player.Position))
            {
                player.zone_type = zone.type;
                return;
            }
        }
        player.zone_type = 0;
    }
}