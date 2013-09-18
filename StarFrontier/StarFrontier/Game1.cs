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

namespace StarFrontier
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum gameState { Menu, InGame, GameOver };
        gameState CurrentGamestate = gameState.Menu;
        Vector2 MousePos; //tracks the mouse position on screen in order to display the pointer (r.g greenmousepointer)
        public static Vector2 Distance; // distance between mouse and sprite , used for rotation

        Texture2D GreenMousePointer;
        Texture2D Background_Menu;
        Texture2D beams;
        Texture2D SpriteSheet;
        public Player Mainplayer = new Player();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GreenMousePointer = Content.Load<Texture2D>(@"Textures\GreenMousePointer");
            Background_Menu = Content.Load<Texture2D>(@"Textures\Background_Menu");
            beams = Content.Load<Texture2D>(@"Textures\beams");
            SpriteSheet = Content.Load<Texture2D>(@"Textures\SpriteSheet");
            IsMouseVisible = true;
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MouseState Mouse1 = Mouse.GetState();
            // TODO: Add your update logic here
            MousePos.X = Mouse.GetState().X -15; //used for Mousepointer graphic
            MousePos.Y = Mouse.GetState().Y -15;
            Distance.X = Player.PlayerPos.X - Mouse1.X;
            Distance.Y = Player.PlayerPos.Y - Mouse1.Y;
            KeyboardState Keyboardinput = Keyboard.GetState();
            if (Keyboardinput.IsKeyDown(Keys.W))
            {
                Player.direction = new Vector2((float)Math.Cos(Player.PlayerRotation ), (float)Math.Sin(Player.PlayerRotation));
                Player.PlayerPos -= Player.direction * 1 * gameTime.ElapsedGameTime.Milliseconds;
                Window.Title = Player.PlayerPos.ToString();
            }
            if (Keyboardinput.IsKeyDown(Keys.S))
            {
                Player.direction = new Vector2((float)Math.Cos(Player.PlayerRotation), (float)Math.Sin(Player.PlayerRotation));
                Player.PlayerPos += Player.direction * 1 * gameTime.ElapsedGameTime.Milliseconds;
                Window.Title = Player.PlayerPos.ToString();
            }
            Player.RotatePlayer();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            switch (CurrentGamestate)//sets the background for current scene, e.g space || planet || blackhole
            {
                case gameState.Menu :
                    spriteBatch.Draw(Background_Menu, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                    break;
                   
            }
            spriteBatch.Draw(SpriteSheet, Player.PlayerPos, new Rectangle(Player.CurrentFrame.X * Player.FrameSize.X, Player.CurrentFrame.Y * Player.FrameSize.Y, Player.FrameSize.X, Player.FrameSize.Y), Color.White, Player.PlayerRotation + (float)1.57, new Vector2(23, 15), 1, SpriteEffects.None, 0);
            spriteBatch.Draw(GreenMousePointer, MousePos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
