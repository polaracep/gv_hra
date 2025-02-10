using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public enum FloorTypes
{
    BASIC
}
public enum WallTypes
{
    BASIC
}

public abstract class Tile : ITexture
{
    protected static Dictionary<String, Texture2D> sprites;
    protected static string spriteName;
    protected bool doCollision = false;
    protected Vector2 screenPos;

    // Vsechny tiles jsou 50x50
    protected static Vector2 tileSize = new Vector2(50, 50);

    // Use in case of only one sprite type
    protected Tile(String name, bool collide)
    {
        spriteName = name;
        this.doCollision = collide;
    }

    // Use in case of more sprites -> you have to provide the sprite in the child constructor 
    protected Tile(bool collide)
    {
        this.doCollision = collide;
    }

    public Texture2D getTexture()
    {
        return (Texture2D)sprites.Values.Take(1);
    }

    public bool doesCollision()
    {
        return doCollision;
    }

    public Vector2 GetSize()
    {
        return tileSize;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprites[0], screenPos, Color.White);
    }
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
    public static void Load(ContentManager content)
    {
        sprites[0] = content.Load<Texture2D>("tile");
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprites[0], screenPos, Color.White);
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