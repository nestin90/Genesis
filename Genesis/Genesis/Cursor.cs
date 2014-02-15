using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genesis
{
    class Cursor 
    {
        public Animation CursorAnimation;
        public Vector2 Position;
        public bool Active;

        public int Width
        {
            get { return CursorAnimation.FrameWidth; }
        }

        public int Height
        {
            get { return CursorAnimation.FrameHeight; }
        }

        public void Initialize(Animation animation, Vector2 position)
        {
            CursorAnimation = animation;
            Position = position;
            Active = true;
            Console.WriteLine("Initialized" + Position + CursorAnimation);

        }

        public void Update(GameTime gameTime)
        {
            CursorAnimation.Position = Position;
            CursorAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CursorAnimation.Draw(spriteBatch);
        }
    }
}
