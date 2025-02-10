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
	abstract class Entity : ITexture
	{
		Texture2D sprite;
		string spriteName;
		public Vector2 position;
		public Vector2 size;
		public Entity(string spriteName)
		{
			this.spriteName = spriteName;
		}
		public void Load(ContentManager content)
		{
			sprite = content.Load<Texture2D>(spriteName);
			size = GetSize();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(sprite, position, Color.White);
		}
		public Vector2 GetSize()
		{
			return new Vector2(sprite.Width, sprite.Height);
		}
	}
}
