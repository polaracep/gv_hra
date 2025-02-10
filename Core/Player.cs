using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TBoGV.Core.Interface;

namespace TBoGV.Core
{
	internal class Player : Entity, IRecieveDmg, IDealDmg
	{
		static Texture2D Sprite;
		static string SpriteName = "vitek-nobg";
		int Level {  get; set; }
		int Xp {  get; set; }
		public int AttackSpeed { get; set; }
		public int AttackDmg { get; set; }
		public DateTime LastAttackTime { get; set; }

        public List<Projectile> Projectiles { get; set; }
		public Player(Vector2 postition)
		{ 
			Position = Vector2.Zero;
            Size = new Vector2(50, 50);
            SetPosition(postition);
			Hp = 3;
			MovementSpeed = 4;
			Projectiles = new List<Projectile>();
			AttackSpeed = 2;
		}
		public void SetPosition(Vector2 postition)
		{
			Position = postition;
		}
		public override void Update(KeyboardState keyboardState, MouseState mouseState)
		{
			if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
			{
				Position.X -= MovementSpeed;
			}
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                Position.X += MovementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                Position.Y -= MovementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                Position.Y += MovementSpeed;
            }

            Vector2 direction = new Vector2(mouseState.X, mouseState.Y) - Position - Size/2;

            if (direction != Vector2.Zero)
                direction.Normalize(); // Normalize to get unit direction vector
				Direction = direction;

            if (ReadyToAttack() && mouseState.LeftButton == ButtonState.Pressed)
				Projectiles.Add(Attack());
        }
		public override void Load(ContentManager content)
		{
			Sprite = content.Load<Texture2D>(SpriteName);
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch, Sprite);
		}
		public bool ReadyToAttack() 
		{
            return (DateTime.UtcNow - LastAttackTime).TotalMilliseconds >= AttackSpeed;
        }
		public Projectile Attack()
		{
            LastAttackTime = DateTime.UtcNow;
            return new Projectile(Position + Size/2,Direction);
		}
		public void RecieveDmg()
		{

		}
	}
}
