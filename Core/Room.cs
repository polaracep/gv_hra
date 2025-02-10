using Microsoft.Xna.Framework;

namespace TBoGV;
public class Room
{
    private Vector2 dim;
    private Tile[,] roomMap;

    public Room(Vector2 dimensions)
    {
        this.dim = dimensions;
        this.roomMap = new Tile[(int)dimensions.X, (int)dimensions.Y];
    }

    public Vector2 getDimensions()
    {
        return dim;
    }

}