using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBoGV.Core.Interface;
using Microsoft.Xna.Framework.Input;

namespace TBoGV.Core
{
	abstract class Entity:ITexture
	{
		string SpriteName;
		public Vector2 Position;
		public Vector2 Direction;
		public Vector2 Size;
		public int MovementSpeed;
		public int Hp {  get; set; }
		public Entity() { }
		public virtual void Load(ContentManager content) { }
		public void Draw(SpriteBatch spriteBatch, Texture2D sprite)
		{
			spriteBatch.Draw(sprite, new Rectangle(Convert.ToInt32(Position.X),Convert.ToInt32(Position.Y), Convert.ToInt32(Size.X), Convert.ToInt32(Size.Y)),Color.White);
		}
		public Vector2 GetSize(Texture2D sprite)
		{
			return new Vector2(sprite.Width, sprite.Height);
		}
		public virtual void Update(KeyboardState keyboardState, MouseState mouseState) { }
	}
}
