using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Tile : ITexture
{
    protected static Texture2D sprite;
    protected static string spriteName;
    protected bool doCollision = false;
    protected Vector2 screenPos;

    // Vsechny tiles jsou 50x50
    protected static Vector2 tileSize = new Vector2(50, 50);

    public Tile(String spriteName, bool collide)
    {
        Tile.spriteName = spriteName;
        this.doCollision = collide;
    }

    // You have to provide the sprite in the constructor 
    protected Tile(bool collide)
    {
        this.doCollision = collide;
    }

    public Texture2D getTexture()
    {
        return sprite;
    }

    public bool doesCollision()
    {
        return doCollision;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, screenPos, Color.White);
    }

    public static void Load(ContentManager content)
    {
        sprite = content.Load<Texture2D>(spriteName);
    }

    public Vector2 GetSize()
    {
        return tileSize;
    }
}

public enum FloorTypes
{
    BASIC
}
public enum WallTypes
{
    BASIC
}

public class TileFloor : Tile
{
    public TileFloor(FloorTypes floor) : base(false)
    {
        switch (floor)
        {
            case FloorTypes.BASIC:
                TileFloor.spriteName = "tile";
                break;
        }
    }
}

public class TileWall : Tile
{
    public TileWall(WallTypes wall) : base(true)
    {
        switch (wall)
        {
            case WallTypes.BASIC:
                TileWall.spriteName = "wall";
                break;
        }
    }
}

/*
public class TileChest : Tile
{

    public TileChest()
}
*/