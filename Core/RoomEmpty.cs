using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public class RoomEmpty : Room, ITexture
{
    public RoomEmpty() : base(new Vector2(14, 9)) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Dimensions.X; i++)
            for (var j = 0; j < Dimensions.Y; j++)
            {
                Tile t = RoomMap[i, j];
                spriteBatch.Draw(t.getTexture(), new Vector2(i * Tile.GetSize().X, j * Tile.GetSize().Y), Color.White);
            }

    }

	public override Tile GetTile(Vector2 coords)
	{
		if(coords.X >= Dimensions.X*Tile.GetSize().X || coords.Y >= Dimensions.Y * Tile.GetSize().Y || coords.X < 0 || coords.Y < 0)
		{ return new TileWall(WallTypes.BASIC); }
		return RoomMap[(int)(coords.X / Tile.GetSize().X), (int)(coords.Y / Tile.GetSize().Y)];
	}

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
    }
}