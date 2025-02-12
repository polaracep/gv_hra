using Microsoft.Xna.Framework;
using TBoGV;

public class RoomStart : Room, IDraw
{
    public RoomStart(Vector2 dimensions, Player p) : base(dimensions, p) { }

    protected override void GenerateRoom()
    {
        base.GenerateRoomBase();
    }
}