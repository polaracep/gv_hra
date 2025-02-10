using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace TBoGV.Core
{
	internal class Projectile : Entity
	{
		static Texture2D Sprite;
		static readonly string SpriteName = "tile";
		public bool ShotByPlayer;
		int Damage {  get; set; }

		public Projectile(Vector2 position, Vector2 direction, int damage) 
		{ 
			Size = new Vector2(25, 25);
			Position = position - Size/2;
			Direction = direction;
			MovementSpeed = 5;
			Damage = damage;
		}
		public void Update()
		{
			Position += Direction * MovementSpeed;
		}
		public override void Load(ContentManager content)
		{
			Sprite = content.Load<Texture2D>(SpriteName);
			if (Size.X == 0 && Size.Y == 0)
				Size = GetSize(Sprite);
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch, Sprite);
		}

	}
}
