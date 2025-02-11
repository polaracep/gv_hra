using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public class RoomEmpty : Room, IDraw
{
    public RoomEmpty(Vector2 dimensions, Player p) : base(dimensions, p) { }
    public RoomEmpty(Player p) : base(new Vector2(13, 17), p) { }

    protected override void GenerateRoom()
    {
        RoomMap = new Tile[(int)Dimensions.X, (int)Dimensions.Y];
        for (int i = 1; i < Dimensions.X - 1; i++)
            for (var j = 1; j < Dimensions.Y - 1; j++)
                RoomMap[i, j] = new TileFloor(FloorTypes.BASIC);

        for (int i = 0; i < Dimensions.X; i++)
        {
            RoomMap[i, 0] = new TileWall(WallTypes.BASIC);
            RoomMap[i, (int)Dimensions.Y - 1] = new TileWall(WallTypes.BASIC);
        }

        for (int i = 0; i < Dimensions.Y; i++)
        {
            RoomMap[0, i] = new TileWall(WallTypes.BASIC);
            RoomMap[(int)Dimensions.X - 1, i] = new TileWall(WallTypes.BASIC);
        }

        RoomMap[0, 4] = new TileDoor(DoorTypes.BASIC);
    }

}