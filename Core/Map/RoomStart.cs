using Microsoft.Xna.Framework;
using TBoGV;

public class RoomStart : Room, IDraw
{
    public RoomStart(Vector2 dimensions, Vector2 pos, Player p) : base(dimensions, pos, p) { }
    public RoomStart(Vector2 pos, Player p) : base(new Vector2(13, 17), pos, p) { }

    public override void GenerateRoom()
    {
        base.GenerateRoomBase();
        this.AddTile(new TileHeal(), this.Dimensions / 2);
        this.AddTile(new TileDoor(DoorTypes.BASIC), new Vector2(0, 4));
        this.GenerateEnemies();

        player.Position = this.GetTilePos(Vector2.One);
    }

    protected override void GenerateEnemies() { }

}