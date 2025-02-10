using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TBoGV.Core.Interface
{
	internal interface ITexture
	{
		void Draw(SpriteBatch spriteBatch, Texture2D sprite);
		void Load(ContentManager content);
		Vector2 GetSize(Texture2D sprite);
	}
}
