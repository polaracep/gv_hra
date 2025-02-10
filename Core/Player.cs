using Microsoft.Xna.Framework;

namespace TBoGV.Core
{
	internal class Player : Entity
	{
		public Player(string spriteName, Vector2 postition) : base(spriteName) 
		{ 
			Position = Vector2.Zero;
			SetPosition(postition);
		}
		public void SetPosition(Vector2 postition)
		{
			Position = postition;
		}
	}
}
