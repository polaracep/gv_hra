using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TBoGV
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private Vector2 _position;
        private Viewport _viewport;
        private int RoomWidth, RoomHeight, Offset;

        public Camera(Viewport viewport, int roomWidth, int roomHeight)
        {
            _viewport = viewport;
            RoomWidth = roomWidth;
            RoomHeight = roomHeight;
            Offset = 100;
        }

        public void Update(Vector2 targetPosition)
        {
            // Zaměř se na hráče, ale s omezením na velikost místnosti
            _position = targetPosition - new Vector2(_viewport.Width / 2, _viewport.Height / 2);

            // Omez pohyb kamery tak, aby nešla mimo hranice místnosti
            _position.X = MathHelper.Clamp(_position.X, -Offset, RoomWidth + Offset - _viewport.Width);
            _position.Y = MathHelper.Clamp(_position.Y, -Offset, RoomHeight + Offset - _viewport.Height);
            Transform = Matrix.CreateTranslation(new Vector3(-_position, 0));
        }
    }
}
