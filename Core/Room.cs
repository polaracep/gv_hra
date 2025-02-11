using Microsoft.Xna.Framework;

namespace TBoGV;
public abstract class Room
{
    public Vector2 Dimensions { get; protected set; }
    protected Tile[,] RoomMap;

    public Room(Vector2 dimensions)
    {
        this.Dimensions = dimensions;
        this.GenerateRoom();
    }

    protected abstract void GenerateRoom();
    public virtual Tile GetTile(Vector2 coords)
    {
        if (float.IsNaN(coords.X) || float.IsNaN(coords.Y))
            return new TileWall(WallTypes.BASIC);
        if (coords.X >= Dimensions.X * Tile.GetSize().X || coords.Y >= Dimensions.Y * Tile.GetSize().Y || coords.X < 0 || coords.Y < 0)
            return new TileWall(WallTypes.BASIC);
        return RoomMap[(int)(coords.X / Tile.GetSize().X), (int)(coords.Y / Tile.GetSize().Y)];
    }
}