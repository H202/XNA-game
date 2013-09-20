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
    public class Player
    {
        Texture2D PlayerCurrentSprite;
        public static Vector2 PlayerPos = new Vector2(250,250);
        public static float PlayerRotation = 0; 
        //for Walking animation
        public static Point FrameSize = new Point(48,48);
        public static Point CurrentFrame = new Point(0, 0);
        public static Point SpriteSheetSize = new Point(4, 2);
        public static Vector2 direction;
        public static int TimeElapsed = 0; // is used to regulate the speed of the walking animation
        public static void RotatePlayer()
        {
            PlayerRotation = (float)Math.Atan2(Game1.Distance.Y,Game1.Distance.X);
        }
        public static Vector2 PlayerPositionUpdate(KeyboardState Keyboardinput ,GameTime gameTime )
        {
            if (Keyboardinput.IsKeyDown(Keys.W))
            {
                if ((Mouse.GetState().X >= PlayerPos.X + 1 || Mouse.GetState().X <= PlayerPos.X - 1) && (Mouse.GetState().Y >= PlayerPos.Y + 1 || Mouse.GetState().Y <= PlayerPos.Y - 1))
                {
                    direction = new Vector2((float)Math.Cos(PlayerRotation), (float)Math.Sin(Player.PlayerRotation));
                    PlayerPos -= direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
                    TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (TimeElapsed > 100)
                    {
                        if ((CurrentFrame.X <= 4) && (CurrentFrame.Y == 0))
                            CurrentFrame.X = CurrentFrame.X + 1;
                        if (CurrentFrame.X >= 4 && CurrentFrame.Y == 0)
                        {
                            CurrentFrame.X = 0;
                            CurrentFrame.Y = 1;
                        }
                        if ((CurrentFrame.X <= 4) && (CurrentFrame.Y == 1))
                        {
                            CurrentFrame.X = CurrentFrame.X + 1;
                        }
                        if ((CurrentFrame.X >= 4) && (CurrentFrame.Y == 1))
                        {
                            CurrentFrame.X = 0;
                            CurrentFrame.Y = 0;
                        }
                        TimeElapsed = 0;
                    }
                }
                else
                {
                    CurrentFrame.X = 0;
                    CurrentFrame.Y = 0;
                }
            }
            if (Keyboardinput.IsKeyUp(Keys.W))
            {
                CurrentFrame.X = 0;
                CurrentFrame.Y = 0;
            }
            if (Keyboardinput.IsKeyDown(Keys.S))
            {
                direction = new Vector2((float)Math.Cos(PlayerRotation), (float)Math.Sin(Player.PlayerRotation));
                PlayerPos += direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6 );
            }
            if (Keyboardinput.IsKeyDown(Keys.D))//player supposed to move right from mouse direction. NOT WORKING YET
            {
                direction = new Vector2((float)Math.Cos(PlayerRotation + 1.57f), (float)Math.Sin(Player.PlayerRotation));
                PlayerPos += direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
            }
            if (Keyboardinput.IsKeyDown(Keys.A))//player supposed to move left from mouse direction. NOT WORKING YET
            {
                direction = new Vector2((float)Math.Cos(PlayerRotation + 1.57f), (float)Math.Sin(Player.PlayerRotation));
                PlayerPos += direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
            }
            return PlayerPos;
        }
    }
}
