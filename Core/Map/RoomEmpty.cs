using Microsoft.Xna.Framework;

namespace TBoGV;

public class RoomEmpty : Room, IDraw
{
    public RoomEmpty(Vector2 pos, Directions dir, Player p) : base(new Vector2(13, 17), pos, dir, p) { }
    public RoomEmpty(Vector2 dimensions, Vector2 pos, Player p) : base(dimensions, pos, p) { }
    public RoomEmpty(Vector2 dimensions, Player p) : base(dimensions, Vector2.One, p) { }
    //public RoomEmpty(Player p) :


    public override void GenerateRoom()
    {
        base.GenerateRoomBase();
        this.AddTile(new TileHeal(), new Vector2(5, 5));
        this.AddTile(new TileDoor(DoorTypes.BASIC), new Vector2(0, 4));
        this.GenerateEnemies();

        player.Position = this.GetTilePos(Vector2.One);
    }

    protected override void GenerateEnemies()
    {
        for (int i = 1; i <= 5; i++)
        {
            this.AddEnemy(new EnemyZdena(new Vector2(Tile.GetSize().X * i, Tile.GetSize().Y)));
        }
    }

}