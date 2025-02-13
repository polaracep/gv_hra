using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TBoGV;

internal class Coin : Item, IDraw
{
	static Texture2D Sprite;
	public Vector2 Position;
	public Vector2 Size;
	public Coin()
	{
		Sprite = TextureManager.GetTexture("coin");
		Size = new Vector2(30, 30);
		Small = true;
	}
	public override void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
	}

}
