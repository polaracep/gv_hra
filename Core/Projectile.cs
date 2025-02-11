using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace TBoGV;

internal class Projectile : Entity
{
	static Texture2D Sprite;
	static readonly string SpriteName = "projectile";
	public bool ShotByPlayer;
	public int Damage { get; set; }

	public Projectile(Vector2 position, Vector2 direction, int damage)
	{
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
	public static void Load(ContentManager content)
	{
		Sprite = content.Load<Texture2D>(SpriteName);
	}
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
	}

}

