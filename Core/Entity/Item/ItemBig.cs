using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TBoGV;

internal class ItemBig : Item, IInteractable
{
    static Texture2D Sprite;
    public Vector2 Position;
    public Vector2 Size;
    public ItemBig(ItemTypes type, Vector2 position) 
    {
        string spriteName;
        switch (type)
        {
            case ItemTypes.GOLD:
                spriteName = "coin";
                break;
            case ItemTypes.PERVITIN:
                spriteName = "heal";
                break;
            default:
                spriteName = "whoopsies";
                break;
        }
        Sprite = TextureManager.GetTexture(spriteName);
        Size = new Vector2(50, 50);
        Position = position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
    }

    public void Interact(Entity e, Room r)
    {
        throw new NotImplementedException();
    }
}

public enum ItemTypes : int
{
    GOLD = 0,
    PERVITIN = 1,
}
