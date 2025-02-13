using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBoGV.Core.Entity.Item;

namespace TBoGV;

public class Player : Entity, IRecieveDmg, IDealDmg, IDraw
{
	static Texture2D Sprite;
	public int Level { get; set; }
	public int Xp { get; set; }
	public int AttackSpeed { get; set; }
	public int AttackDmg { get; set; }
	public DateTime LastAttackTime { get; set; }
	public DateTime LastRecievedDmgTime { get; set; }
	public int InvulnerabilityFrame = 1000;
	public List<Projectile> Projectiles { get; set; }
	public List<ItemContainer> ItemContainers;
    public int selectedItemIndex = 0;
	private int PrevScrollWheelValue;
    public int ItemCapacity { get; set; }
	public int Hp { get; set; }
	public int MaxHp { get; set; }
	public int Coins { get; set; }
	public Player(Vector2 position)
	{
		Position = position;
		Size = new Vector2(50, 50);
		Hp = MaxHp = 9;
		MovementSpeed = 4;
		Projectiles = new List<Projectile>();
		ItemContainer kontak = new();
        kontak.Item = new ItemDoping(position);
        ItemContainers = new List<ItemContainer>() {new ItemContainer(), new ItemContainer() , new ItemContainer(), kontak};
		
		AttackSpeed = 100;
		AttackDmg = 1;
		Sprite = TextureManager.GetTexture("vitek-nobg");
		Coins = 1;
		ItemCapacity = 3;
	}

	public Player() : this(Vector2.One) { }

	Vector2 InteractionPoint = Vector2.Zero;
	public void Update(KeyboardState keyboardState, MouseState mouseState, Matrix transform, Room room)
	{
		int dx = 0, dy = 0;

		InteractionPoint = Position + (Direction * 50) + Size / 2;

		if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
		{
			dx -= MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
		{
			dx += MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
		{
			dy -= MovementSpeed;
		}
		if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
		{
			dy += MovementSpeed;
		}
		if (mouseState.RightButton == ButtonState.Pressed)
		{
			Tile t = room.GetTileInteractable(InteractionPoint);
			if (t != null)
			{
				IInteractable tile = (IInteractable)t;
				tile.Interact(this, room);
			}
		}
		UpdateContainers(mouseState);
        /* === */

        Vector2 newPosition = Position;
		if (dx != 0)
		{
			newPosition.X += dx;
			if (!room.ShouldCollideAt(newPosition) && !room.ShouldCollideAt(newPosition + Size) && !room.ShouldCollideAt(new Vector2(newPosition.X + Size.X, newPosition.Y)) && !room.ShouldCollideAt(new Vector2(newPosition.X, newPosition.Y + Size.Y)))
				Position.X = newPosition.X;
		}
		newPosition = Position;
		// Try moving on the Y-axis next
		if (dy != 0)
		{
			newPosition.Y += dy;
			if (!room.ShouldCollideAt(newPosition) && !room.ShouldCollideAt(newPosition + Size) && !room.ShouldCollideAt(new Vector2(newPosition.X + Size.X, newPosition.Y)) && !room.ShouldCollideAt(new Vector2(newPosition.X, newPosition.Y + Size.Y)))
				Position.Y = newPosition.Y;
		}
		Vector2 screenMousePos = new Vector2(mouseState.X, mouseState.Y);
		Vector2 worldMousePos = Vector2.Transform(screenMousePos, Matrix.Invert(transform));

		Vector2 direction = worldMousePos - Position - Size / 2;

		if (!float.IsNaN(direction.X) && !float.IsNaN(direction.Y))
		{
			direction.Normalize(); // Normalize to get unit direction vector
			Direction = direction;
		}
		if (ReadyToAttack() && mouseState.LeftButton == ButtonState.Pressed)
			Projectiles.Add(Attack());
	}

	public void UpdateContainers(MouseState mouseState)
	{
        int scrollDelta = PrevScrollWheelValue - mouseState.ScrollWheelValue;
        if (scrollDelta > 0)
        {
            selectedItemIndex = (selectedItemIndex + 1) % ItemContainers.Count;
            SetActiveItemContainer();
        }
        else if (scrollDelta < 0)
        {
            selectedItemIndex = (selectedItemIndex - 1 + ItemContainers.Count) % ItemContainers.Count;
            SetActiveItemContainer();
        }
		PrevScrollWheelValue = mouseState.ScrollWheelValue;
    }
    private void SetActiveItemContainer()
    {
        for (int i = 0; i < ItemContainers.Count; i++)
        {
            ItemContainers[i].Selected = (i == selectedItemIndex);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Sprite,
			new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)),
			(DateTime.UtcNow - LastRecievedDmgTime).TotalMilliseconds >= InvulnerabilityFrame ? Color.White : Color.DarkRed);
		spriteBatch.Draw(TextureManager.GetTexture("projectile"), InteractionPoint, Color.White);
	}
	public bool ReadyToAttack()
	{
		return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
	}
	public Projectile Attack()
	{
		LastAttackTime = DateTime.UtcNow;
		Projectile projectile = new ProjectilePee(Position + Size / 2, Direction, AttackDmg);
		projectile.ShotByPlayer = true;

		return projectile;
	}
	public void RecieveDmg(int damage)
	{
		if ((DateTime.UtcNow - LastRecievedDmgTime).TotalMilliseconds >= InvulnerabilityFrame)
		{
			Hp -= damage;

			LastRecievedDmgTime = DateTime.UtcNow;
		}

	}
	public void Kill(int xpGain)
	{
		Xp += xpGain;
		if(Xp >= XpForLevel())
		{
			LevelUp();
		}
	}
	public int XpForLevel()
	{
		return 5 + Level*2;
	}
	private void LevelUp()
	{
		Level += 1;
		Xp = 0;
	}
	public void Heal(uint healAmount)
	{
		if (Hp < MaxHp)
		{
			Hp += (int)healAmount;
		}
	}

}

