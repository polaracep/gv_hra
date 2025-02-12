using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TBoGV;

internal class Heart : IDraw
{
    public bool Broken;
    static Texture2D SpriteFull;
    static Texture2D SpriteBroken;
    public Vector2 Position;
    public Vector2 Size;
    public Heart()
    {
        SpriteFull = TextureManager.GetTexture("admiration");
        SpriteBroken = TextureManager.GetTexture("taunt");
        Broken = false;
        Size = new Vector2(25, 40);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(!Broken ? SpriteFull : SpriteBroken, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
    }
}

