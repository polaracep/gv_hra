using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public enum FloorTypes : uint
{
    BASIC = 0
}
public enum WallTypes : uint
{
    BASIC = 0
}

public abstract class Tile : ITexture
{
    protected static Texture2D[] sprites;
    protected uint spriteId;
    protected Vector2 screenPos;
    public bool doCollision { get; protected set; } = false;

    // Vsechny tiles jsou 50x50
    public static Vector2 tileSize { get; protected set; } = new Vector2(50, 50);

    // Use in case of more sprites -> you have to provide the sprite in the child constructor 
    protected Tile(bool collide)
    {
        this.doCollision = collide;
    }

    public Texture2D getTexture()
    {
        return sprites[spriteId];
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D sprite)
    {
        spriteBatch.Draw(this.getTexture(), this.screenPos, Color.White);
    }

    public Vector2 GetSize(Texture2D sprite)
    {
        return tileSize;
    }
}

public class TileFloor : Tile
{
    public TileFloor(FloorTypes floor) : base(false)
    {
        this.spriteId = (uint)floor;
    }
    public static void Load(ContentManager content)
    {
        TileFloor.sprites.Append(content.Load<Texture2D>("tile"));
    }

}

public class TileWall : Tile
{
    public TileWall(WallTypes wall) : base(true)
    {
        this.spriteId = (uint)wall;
    }
}

/*
public class TileChest : Tile
{

    public TileChest()
}
*/