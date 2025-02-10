using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TBoGV;
internal interface ITexture
{
	void Draw(SpriteBatch spriteBatch, Texture2D sprite);
	static void Load(ContentManager content) => throw new NotImplementedException();
	Vector2 GetSize(Texture2D sprite);
}