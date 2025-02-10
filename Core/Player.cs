using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TBoGV.Core
{
	internal class Player : Entity
	{
		static Texture2D Sprite;
		static string SpriteName = "vitek-nobg";
		public Player(Vector2 postition) : base() 
		{ 
			Position = Vector2.Zero;
			Size = new Vector2(50,50);
			SetPosition(postition);
		}
		public void SetPosition(Vector2 postition)
		{
			Position = postition;
		}
		public override void Update(KeyboardState keyboardState, MouseState mouseState)
		{

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
