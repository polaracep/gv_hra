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

}