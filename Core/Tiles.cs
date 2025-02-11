using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV;

public abstract class Tile
{
    protected Vector2 screenPos;
    protected string SpriteName;
    public Texture2D Sprite { get; protected set; }
    public bool DoCollision { get; protected set; } = false;

    // Vsechny tiles50x50.
    protected static Vector2 tileSize = new Vector2(50, 50);

    protected Tile(bool collide)
    {
        DoCollision = collide;
    }

    public static Vector2 GetSize()
    {
        return tileSize;
    }
}

public class TileFloor : Tile, IDraw
{
    public TileFloor(FloorTypes floor) : base(false)
    {
        switch (floor)
        {
            case FloorTypes.BASIC:
                Console.WriteLine(TextureManager.GetTexture("tile"));
                Sprite = TextureManager.GetTexture("tile");
                break;
            default:
                throw new Exception();
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, this.screenPos, Color.White);
    }
}

public class TileWall : Tile, IDraw
{
    public TileWall(WallTypes wall) : base(true)
    {
        switch (wall)
        {
            case WallTypes.BASIC:
                Sprite = TextureManager.GetTexture("wall");
                break;
            default:
                throw new Exception();
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, this.screenPos, Color.White);
    }
}

public class TileDoor : Tile, IDraw, IInteractable
{
    public TileDoor(DoorTypes door) : base(true)
    {
        switch (door)
        {
            case DoorTypes.BASIC:
                Sprite = TextureManager.GetTexture("door");
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, this.screenPos, Color.White);
    }

    public void Interact(Entity e)
    {
        e.Position = Vector2.Zero;
    }
}