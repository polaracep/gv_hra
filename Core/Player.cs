using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TBoGV;

internal class Player : Entity, IRecieveDmg, IDealDmg, ITexture
{
	static Texture2D Sprite;
	static string SpriteName = "vitek-nobg";
	int Level { get; set; }
	int Xp { get; set; }
	public int AttackSpeed { get; set; }
	public int AttackDmg { get; set; }
	public DateTime LastAttackTime { get; set; }

	public List<Projectile> Projectiles { get; set; }
	public int Hp { get; set; }

	public Player(Vector2 position)
	{
		Position = position;
		Size = new Vector2(50, 50);
		Hp = 3;
		MovementSpeed = 4;
		Projectiles = new List<Projectile>();
		AttackSpeed = 2;
		AttackDmg = 1;
	}
	public void Update(KeyboardState keyboardState, MouseState mouseState, Matrix transform, Room room)
	{
		int dx = 0, dy = 0;
		if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
		{
			dx = -MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
		{
			dx = MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
		{
			dy = -MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
		{
			dy = MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.E))
		{
			room.GetTile(Position + (Direction * 10));
		}
		Vector2 newPosition = Position;
		if (dx != 0)
		{
			newPosition.X += dx;
			if (!room.GetTile(newPosition).DoCollision && !room.GetTile(newPosition + Size).DoCollision)
				Position.X = newPosition.X;
		}
		newPosition = Position;
		// Try moving on the Y-axis next
		if (dy != 0)
		{
			newPosition.Y += dy;
			if (!room.GetTile(newPosition).DoCollision && !room.GetTile(newPosition + Size).DoCollision && !room.GetTile(new Vector2(newPosition.X + Size.X, newPosition.Y)).DoCollision && !room.GetTile(new Vector2(newPosition.X, newPosition.Y + Size.Y)).DoCollision)
				Position.Y = newPosition.Y;
		}
		Vector2 screenMousePos = new Vector2(mouseState.X, mouseState.Y);
		Vector2 worldMousePos = Vector2.Transform(screenMousePos, Matrix.Invert(transform));

		Vector2 direction = worldMousePos - Position - Size / 2;

		if (direction != Vector2.Zero)
		{
			direction.Normalize(); // Normalize to get unit direction vector
			Direction = direction;
		}
		if (ReadyToAttack() && mouseState.LeftButton == ButtonState.Pressed)
			Projectiles.Add(Attack());
	}
	public static void Load(ContentManager content)
	{
		Sprite = content.Load<Texture2D>(SpriteName);
	}
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Sprite, new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)), Color.White);
	}
	public bool ReadyToAttack()
	{
		return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
	}
	public Projectile Attack()
	{
		LastAttackTime = DateTime.UtcNow;
		Projectile projectile = new Projectile(Position + Size / 2, Direction, AttackDmg);
		projectile.ShotByPlayer = true;

		return projectile;
	}
	public void RecieveDmg(int damage)
	{
		Hp -= damage;
	}
}

