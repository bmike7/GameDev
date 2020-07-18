using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Camera2D
    {
        private readonly Viewport _viewport;
        private Vector2 _position;

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;
            _position = Vector2.Zero;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);
        }

        public void UpdatePosition(Vector2 playerPosition)
        {
            _position.X = playerPosition.X - _viewport.Width * 0.25f;
            // Example of updating camera before camera followed player
            // But removed those because it was obselete and KISS
            // if (Utility.IsKeyPressed(Keys.F3)) Rotation += .1f;
        }

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1);
        }
    }
}