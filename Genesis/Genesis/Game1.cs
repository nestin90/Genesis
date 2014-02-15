using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genesis
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        int tileCount = 1;
        int cursorRow = 0;
        int cursorCol = 0;
        bool playerCtrl = true;
        Texture2D tileBase;
        Texture2D tileLand;
        Texture2D tileWater;
        Texture2D cursorTexture;
        KeyboardState currentState;
        KeyboardState prevState;
        GamePadState currentPad;
        GamePadState currentPad2;
        GamePadState prevPad;
        GamePadState prevPad2;
        Cursor cursor;
        Tile [,] board;
        //KeyboardState oldState;
        //GamePadState oldPadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1440;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            
            board = new Tile [12,12];

            cursor = new Cursor();

            //oldState = Keyboard.GetState(PlayerIndex.One);
            //oldPadState = GamePad.GetState(PlayerIndex.One);

            base.Initialize();  
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileWater = Content.Load<Texture2D>("tile_water");
            tileLand = Content.Load<Texture2D>("tile_land");
            tileBase = Content.Load<Texture2D>("wasteland_tile");

            Animation cursorAnimation = new Animation();
            cursorTexture = Content.Load<Texture2D>("cursors_64x64");
            cursorAnimation.Initialize(cursorTexture, new Vector2(0, 64), 64, 64, 12, 30, Color.White, 1f, true);
            cursor.Initialize(cursorAnimation, new Vector2(336, 130));
            font = Content.Load<SpriteFont>("gameFont");
            InitializeBoard();
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Console.WriteLine(cursorTexture);
        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //// Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
          
            KeyboardState keyState = Keyboard.GetState();
            
            GamePadState gState = GamePad.GetState(PlayerIndex.One);
            currentPad = gState;
            
            
            GamePadState gState2 = GamePad.GetState(PlayerIndex.Two);
            currentPad2 = gState2;

            currentState = keyState;     

            #region Keyboard Control
            #region Player One
            if (playerCtrl == true)
            {
                if (currentState.IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left))
                {
                    if (cursorRow <= 11 && cursorRow > 0)
                    {
                        cursorRow--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right))
                {
                    if (cursorRow < 11 && cursorRow >= 0)
                    {
                        cursorRow++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up))
                {
                    if (cursorCol <= 11 && cursorCol > 0)
                    {
                        cursorCol--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down))
                {
                    if (cursorCol < 11 && cursorCol >= 0)
                    {
                        cursorCol++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                {
                    board[cursorRow, cursorCol].tileValue++;
                    Console.WriteLine("Land: " + board[cursorRow, cursorCol].tileValue);
                    Console.WriteLine("Turn: " + tileCount);
                    Console.WriteLine("Player: One");

                    if (tileCount == 3)
                    {
                        tileCount = 0;
                        playerCtrl = false;
                    }
                    if (board[cursorRow, cursorCol].tileValue > 0)
                    {
                        board[cursorRow, cursorCol].Texture = tileLand;
                    }
                    else if (board[cursorRow, cursorCol].tileValue < 0)
                        board[cursorRow, cursorCol].Texture = tileWater;
                    else
                        board[cursorRow, cursorCol].Texture = tileBase;
                    tileCount++;
                }
            }
            #endregion

            #region Player Two

            if (playerCtrl == false)
            {

                if (currentState.IsKeyDown(Keys.A) && prevState.IsKeyUp(Keys.A))
                {
                    if (cursorRow <= 11 && cursorRow > 0)
                    {
                        cursorRow--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.D) && prevState.IsKeyUp(Keys.D))
                {
                    if (cursorRow < 11 && cursorRow >= 0)
                    {
                        cursorRow++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
                {
                    if (cursorCol <= 11 && cursorCol > 0)
                    {
                        cursorCol--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S))
                {
                    if (cursorCol < 11 && cursorCol >= 0)
                    {
                        cursorCol++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentState.IsKeyDown(Keys.J) && prevState.IsKeyUp(Keys.J))
                {
                    board[cursorRow, cursorCol].tileValue--;
                    Console.WriteLine("Water: " + board[cursorRow, cursorCol].tileValue);
                    Console.WriteLine("Turn: " + tileCount);
                    Console.WriteLine("Player: Two");

                    if (tileCount == 3)
                    {
                        tileCount = 0;
                        playerCtrl = true;
                    }
                    if (board[cursorRow, cursorCol].tileValue < 0)
                    {
                        board[cursorRow, cursorCol].Texture = tileWater;
                    }
                    else if (board[cursorRow, cursorCol].tileValue > 0)
                        board[cursorRow, cursorCol].Texture = tileLand;
                    else
                        board[cursorRow, cursorCol].Texture = tileBase;
                    tileCount++;
                }
            }

            #endregion
            #endregion

            #region Gamepad Control
            #region Player One
            if (playerCtrl == true)
            {
                if (currentPad.IsButtonDown(Buttons.DPadLeft) && prevPad.IsButtonUp(Buttons.DPadLeft))
                {
                    if (cursorRow <= 11 && cursorRow > 0)
                    {
                        cursorRow--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad.IsButtonDown(Buttons.DPadRight) && prevPad.IsButtonUp(Buttons.DPadRight))
                {
                    if (cursorRow < 11 && cursorRow >= 0)
                    {
                        cursorRow++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad.IsButtonDown(Buttons.DPadUp) && prevPad.IsButtonUp(Buttons.DPadUp))
                {
                    if (cursorCol <= 11 && cursorCol > 0)
                    {
                        cursorCol--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad.IsButtonDown(Buttons.DPadDown) && prevPad.IsButtonUp(Buttons.DPadDown))
                {
                    if (cursorCol < 11 && cursorCol >= 0)
                    {
                        cursorCol++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad.IsButtonDown(Buttons.A) && prevPad.IsButtonUp(Buttons.A))
                {
                    board[cursorRow, cursorCol].tileValue++;
                    Console.WriteLine("Land: " + board[cursorRow, cursorCol].tileValue);
                    Console.WriteLine("Turn: " + tileCount);
                    Console.WriteLine("Player: One");

                    if (tileCount == 3)
                    {
                        tileCount = 0;
                        playerCtrl = false;
                    }
                    if (board[cursorRow, cursorCol].tileValue > 0)
                    {
                        board[cursorRow, cursorCol].Texture = tileLand;
                    }
                    else if (board[cursorRow, cursorCol].tileValue < 0)
                        board[cursorRow, cursorCol].Texture = tileWater;
                    else
                        board[cursorRow, cursorCol].Texture = tileBase;
                    tileCount++;
                }
            }
            #endregion

            #region Player Two
            if (playerCtrl == false)
            {
                if (currentPad2.IsButtonDown(Buttons.DPadLeft) && prevPad2.IsButtonUp(Buttons.DPadLeft))
                {
                    if (cursorRow <= 11 && cursorRow > 0)
                    {
                        cursorRow--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad2.IsButtonDown(Buttons.DPadRight) && prevPad2.IsButtonUp(Buttons.DPadRight))
                {
                    if (cursorRow < 11 && cursorRow >= 0)
                    {
                        cursorRow++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad2.IsButtonDown(Buttons.DPadUp) && prevPad2.IsButtonUp(Buttons.DPadUp))
                {
                    if (cursorCol <= 11 && cursorCol > 0)
                    {
                        cursorCol--;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad2.IsButtonDown(Buttons.DPadDown) && prevPad2.IsButtonUp(Buttons.DPadDown))
                {
                    if (cursorCol < 11 && cursorCol >= 0)
                    {
                        cursorCol++;
                        cursor.Position = board[cursorRow, cursorCol].Position;
                    }
                }

                if (currentPad2.IsButtonDown(Buttons.A) && prevPad2.IsButtonUp(Buttons.A))
                {
                    board[cursorRow, cursorCol].tileValue--;
                    Console.WriteLine("Land: " + board[cursorRow, cursorCol].tileValue);
                    Console.WriteLine("Turn: " + tileCount);
                    Console.WriteLine("Player: Two");

                    if (tileCount == 3)
                    {
                        tileCount = 0;
                        playerCtrl = true;
                    }
                    if (board[cursorRow, cursorCol].tileValue < 0)
                    {
                        board[cursorRow, cursorCol].Texture = tileWater;
                    }
                    else if (board[cursorRow, cursorCol].tileValue > 0)
                        board[cursorRow, cursorCol].Texture = tileLand;
                    else
                        board[cursorRow, cursorCol].Texture = tileBase;
                    tileCount++;
                }
            }
            #endregion
            #endregion


            prevState = keyState;
            prevPad = gState;
            prevPad2 = gState2;
            base.Update(gameTime);
        }

        public void InitializeBoard()
        {
            int rows = 12;
            int col = 12;
            int centerBoardX = (rows * 64)/2;
            int centerBoardY = (col * 64)/2;
            int centerScreenX = (GraphicsDevice.Viewport.Width / 2) - centerBoardX;
            int centerScreenY = (GraphicsDevice.Viewport.Height / 2) - centerBoardY;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Tile tile = new Tile();
                    Vector2 position = new Vector2(i * (tileBase.Width) + centerScreenX, j * (tileBase.Height) + (centerScreenY+64));
                    tile.Initialize(tileBase, position, 0);
                    board[i, j] = tile;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    spriteBatch.Draw(board[i,j].Texture,board[i,j].Position, null, Color.White, 0f, new Vector2(0, 64), 1, SpriteEffects.None, 0f);
                    //board[i].Draw(spriteBatch);
                }
            }

            cursor.Draw(spriteBatch);

            if (playerCtrl == true)
            {
                spriteBatch.DrawString(font, "Player: One", new
                Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y),
                Color.White);
            }

            else if (playerCtrl == false)
            {
                spriteBatch.DrawString(font, "Player: Two", new
                Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y),
                Color.White);
            }
            spriteBatch.DrawString(font, "Tiles Left: " + tileCount, new
            Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y +
            30), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
