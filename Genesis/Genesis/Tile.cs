using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Genesis
{
    class Tile
    {
        public Texture2D Texture;

        public Vector2 Position;

        public sbyte tileValue;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position, sbyte value)
        {
            Texture = texture;
            Position = position;
            tileValue = value;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
