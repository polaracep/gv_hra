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

    public static readonly TileVoid NoTile = new TileVoid();

    protected Tile(bool collide)
    {
        DoCollision = collide;
    }

    public static Vector2 GetSize()
    {
        return tileSize;
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, this.screenPos, Color.White);
    }
}

public class TileVoid : Tile
{
    public TileVoid() : base(false) { }
}
public class TileFloor : Tile, IDraw
{
    public TileFloor(FloorTypes floor) : base(false)
    {
        switch (floor)
        {
            case FloorTypes.BASIC:
                Sprite = TextureManager.GetTexture("tile");
                break;
            default:
                throw new Exception();
        }
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

    public void Interact(Entity e, Room r)
    {
        // put player in the left-top corne
        e.Position = tileSize;
        r.ResetRoom();
    }
}

public class TileHeal : Tile, IDraw, IInteractable
{
    public TileHeal() : base(true)
    {
        this.Sprite = TextureManager.GetTexture("heal");
    }
    public void Interact(Entity e, Room r)
    {
        if (e is Player)
        {
            Player p = (Player)e;
            p.Heal(1);
        }
    }

}