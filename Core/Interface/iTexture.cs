using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;

namespace TBoGV;
interface ITexture
{
	void Draw(SpriteBatch spriteBatch);
	static void Load(ContentManager content)
	{
		throw new NotImplementedException("ITexture::Load");
	}
}
