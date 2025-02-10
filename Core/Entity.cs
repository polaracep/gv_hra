using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBoGV.Core.Interface;

namespace TBoGV.Core
{
	abstract class Entity:iTexture
	{
		Texture2D Sprite;
		string SpriteName;
		public Vector2 Position;
		public Vector2 Size;
		public Entity(string spriteName)
		{
			this.SpriteName = spriteName;
		}
		public void Load(ContentManager content)
		{
			Sprite = content.Load<Texture2D>(SpriteName);
			Size = GetSize();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Sprite, Position, Color.White);
		}
		public Vector2 GetSize()
		{
			return new Vector2(Sprite.Width, Sprite.Height);
		}
	}
}
