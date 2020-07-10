using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueSimulator.Classes.Mechanics
{
    public class Camera2D
    {
        private readonly Viewport _viewport;

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width * 0.5f, viewport.Height * 0.5f);
            Position = Vector2.Zero;
        }

        public void Update()
        {
            if (Utility.IsKeyPressed(Keys.F1))
            {
                var tempPos = Position;
                tempPos.X += 1;
                Position = tempPos;
            }
            if (Utility.IsKeyPressed(Keys.F2))
            {
                var tempPos = Position;
                tempPos.X -= 1;
                Position = tempPos;
            }
            if (Utility.IsKeyPressed(Keys.F3)) Rotation += .1f;
            if (Utility.IsKeyPressed(Keys.F4)) Rotation -= .1f;
            if (Utility.IsKeyPressed(Keys.F5)) Zoom += .1f;
            if (Utility.IsKeyPressed(Keys.F6)) Zoom -= .1f;
        }

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);
            }
        }

        public Vector2 Position { get; set; }
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