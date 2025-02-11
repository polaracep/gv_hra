using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Tile
{
    protected Vector2 screenPos;
    public bool DoCollision { get; protected set; } = false;

    // Vsechny tiles50x50.
    protected static Vector2 tileSize = new Vector2(50, 50);

    protected Tile(bool collide)
    {
        DoCollision = collide;
    }

    public abstract Texture2D getTexture();

    public static Vector2 GetSize()
    {
        return tileSize;
    }
}

/*
 * Tady varil ChatGPT o3
 */
// Generic base class that gives each concrete type its own static sprite list.
public abstract class Tile<T> : Tile where T : Tile<T>
{
    protected static List<Texture2D> sprites = new List<Texture2D>();
    protected int spriteId;

    protected Tile(bool collide) : base(collide) { }

    public override Texture2D getTexture()
    {
        return sprites[spriteId];
    }

    // Optional: You can add helper methods to manage sprites for type T.
    public static void AddSprite(Texture2D sprite)
    {
        sprites.Add(sprite);
    }
}

public class TileFloor : Tile<TileFloor>, ITexture
{
    public TileFloor(FloorTypes floor) : base(false)
    {
        this.spriteId = (int)floor;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.getTexture(), this.screenPos, Color.White);
    }
    public static void Load(ContentManager content)
    {
        TileFloor.sprites.Add(content.Load<Texture2D>("tile"));
    }

}

public class TileWall : Tile<TileWall>, ITexture
{
    public TileWall(WallTypes wall) : base(true)
    {
        this.spriteId = (int)wall;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.getTexture(), this.screenPos, Color.White);
    }
    public static void Load(ContentManager content)
    {
        TileWall.sprites.Add(content.Load<Texture2D>("wall"));
    }
}

public class TileDoor : Tile<TileDoor>
{
    public TileDoor() : base(true)
    {
    }
}