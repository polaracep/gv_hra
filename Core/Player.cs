using Microsoft.Xna.Framework;

namespace TBoGV.Core
{
	internal class Player : Entity
	{
		public Player(string spriteName, Vector2 pos) : base(spriteName)
		{
			this.position = Vector2.Zero;
			SetPosition(pos);
		}
		public void SetPosition(Vector2 pos)
		{
			this.position = pos;
		}
	}
}
