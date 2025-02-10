using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TBoGV.Core
{
	abstract internal class Enemy : Entity
	{
		public abstract void SetPosition(Vector2 postition);
		public abstract void Draw(SpriteBatch spriteBatch);


	}
}
