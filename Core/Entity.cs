using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TBoGV;
abstract class Entity : ITexture
{
	protected static Texture2D sprite;
	protected string spriteName;
	protected Vector2 position;
	protected Vector2 size;
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
