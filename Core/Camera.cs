using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV
{
	public class Camera
	{
		public Matrix Transform { get; private set; }
		private Vector2 _position;
		private Viewport _viewport;

		public Camera(Viewport viewport)
		{
			_viewport = viewport;
		}

		public void Update(Vector2 targetPosition)
		{
			_position = targetPosition - new Vector2(_viewport.Width / 2, _viewport.Height / 2);
			Transform = Matrix.CreateTranslation(new Vector3(-_position, 0));
		}
	}
}
