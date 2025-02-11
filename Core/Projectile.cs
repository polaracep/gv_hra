using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace TBoGV;

internal class Projectile : Entity, IDraw
{
	public const string SpriteName = "projectile";
	public Texture2D Sprite { get; protected set; }
	public bool ShotByPlayer;
	public int Damage { get; set; }

	public Projectile(Vector2 position, Vector2 direction, int damage)
	{
		Sprite = TextureManager.GetTexture(SpriteName);
		Size = new Vector2(7, 7);
		Position = position - Size / 2;
		Direction = direction;
		MovementSpeed = 5;
		Damage = damage;
	}
	public void Update()
	{
		Position += Direction * MovementSpeed;
	}
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
	}

}

