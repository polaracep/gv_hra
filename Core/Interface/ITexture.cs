using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace TBoGV;
interface ITexture
{
	static void Draw(SpriteBatch spriteBatch) { }
	static void Load(ContentManager content) { }
	Vector2 GetSize();
}
