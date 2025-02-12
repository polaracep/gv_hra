using System.Net.NetworkInformation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public class RoomEmpty : Room, IDraw
{
    public RoomEmpty(Vector2 dimensions, Player p) : base(dimensions, p) { }
    public RoomEmpty(Player p) : base(new Vector2(13, 17), p) { }

    protected override void GenerateRoom()
    {
        base.GenerateRoomBase();
        this.AddTile(new TileHeal(), new Vector2(5, 5));
        this.AddTile(new TileDoor(DoorTypes.BASIC), new Vector2(0, 4));
        this.GenerateEnemies();

        player.Position = this.GetTilePos(Vector2.One);
    }

    protected void GenerateEnemies()
    {
        for (int i = 1; i <= 5; i++)
        {
            this.AddEnemy(new EnemyZdena(new Vector2(Tile.GetSize().X * i, Tile.GetSize().Y)));
        }
    }

}