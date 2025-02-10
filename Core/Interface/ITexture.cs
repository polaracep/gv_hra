using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TBoGV;
internal interface ITexture
{
	void Draw(SpriteBatch spriteBatch);
	static abstract void Load(ContentManager content);
	Vector2 GetSize();
}