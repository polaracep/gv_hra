using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Tile
{
    protected Texture2D texture;
    protected bool doCollision = false;

    public Tile(Texture2D tileTexture, bool collide)
    {
        this.texture = tileTexture;
        this.doCollision = collide;
    }

    public Texture2D getTexture()
    {
        return texture;
    }

    public bool doesCollision()
    {
        return doCollision;
    }
}

public class TileFloor : Tile
{
    public TileFloor(Texture2D tileTexture) : base(tileTexture, false) { }

}

/*
public class TileWall : Tile
{
    public TileWall() : base() { }
}
*/

/*
public class TileChest : Tile
{

    public TileChest()
}
*/