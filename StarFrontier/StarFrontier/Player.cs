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
        public  Vector2 PlayerPos { get; set; }
        public  float PlayerRotation = 0; 
        //for Walking animation
        public  Point FrameSize = new Point(48,48);
        public  Point CurrentFrame = new Point(0, 0);
        public  Point SpriteSheetSize = new Point(4, 2);
        public  Vector2 direction;
        public  int TimeElapsed2 = 0;// is used to regulate the speed of the backward walking animation
        public  int TimeElapsed = 0;// is used to regulate the speed of the forward walking animation
        public  Vector2 SideStrafe; // initial player position saved here when player starts side strafing.
        public  Vector2 CurrentMousePosition;
        public  bool IsSideStrafing = false;
        public  void RotatePlayer()
        {
            PlayerRotation = (float)Math.Atan2(Game1.Distance.Y,Game1.Distance.X);
        }
        public  Vector2 PlayerPositionUpdate(KeyboardState Keyboardinput ,GameTime gameTime )
        {
            if (Keyboardinput.IsKeyDown(Keys.W))
            {
               
                if ((Mouse.GetState().X >= PlayerPos.X + 1 || Mouse.GetState().X <= PlayerPos.X - 1) && (Mouse.GetState().Y >= PlayerPos.Y + 1 || Mouse.GetState().Y <= PlayerPos.Y - 1)) // prevents walking bug "spiral of death"
                {
                    direction = new Vector2((float)Math.Cos(PlayerRotation), (float)Math.Sin(this.PlayerRotation));
                    PlayerPos -= direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
                    TimeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (TimeElapsed > 100)// controls walking animation speed
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
                else // resets walking animation frame if player stops due to proximity with mouse pointer.
                {
                    CurrentFrame.X = 0;
                    CurrentFrame.Y = 0;
                }
            }
            if (Keyboardinput.IsKeyUp(Keys.W) && Keyboardinput.IsKeyUp(Keys.S))//resets walking animation frame when player standing still
            {
                CurrentFrame.X = 0;
                CurrentFrame.Y = 0;
            }
            if (Keyboardinput.IsKeyDown(Keys.S))
            {
                
                direction = new Vector2((float)Math.Cos(PlayerRotation), (float)Math.Sin(this.PlayerRotation));
                    PlayerPos += direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
                    TimeElapsed2 += gameTime.ElapsedGameTime.Milliseconds;
                    if (TimeElapsed2 > 100)// controls walking animation speed
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
                        TimeElapsed2 = 0;
                    }
            }
            if (Keyboardinput.IsKeyDown(Keys.D))//player supposed to move right from mouse direction. NOT WORKING YET
            {
                if (!IsSideStrafing)
                {
 
                    SideStrafe = new Vector2((float)Math.Cos(PlayerRotation + 1.5707f), (float)Math.Sin(this.PlayerRotation + 1.5707f));
                    IsSideStrafing = true;
                }
                PlayerPos -= SideStrafe * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
            }
            if (Keyboardinput.IsKeyUp(Keys.D) && Keyboardinput.IsKeyUp(Keys.A))//player supposed to move right from mouse direction. NOT WORKING YET
            {
                if ((CurrentMousePosition.X != Mouse.GetState().X && CurrentMousePosition.Y != Mouse.GetState().Y))
                {
                    CurrentMousePosition.X = Mouse.GetState().X;
                    CurrentMousePosition.Y = Mouse.GetState().Y;
                    IsSideStrafing = false;
                }
            }
            if (Keyboardinput.IsKeyDown(Keys.A))//player supposed to move left from mouse direction. NOT WORKING YET
            {
                if (!IsSideStrafing || (CurrentMousePosition.X != Mouse.GetState().X && CurrentMousePosition.Y != Mouse.GetState().Y))
                {
                    SideStrafe = new Vector2((float)Math.Cos(PlayerRotation + 1.5707f), (float)Math.Sin(this.PlayerRotation + 1.5707f));
                    IsSideStrafing = true;
                }
                PlayerPos += SideStrafe * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
            }
            return PlayerPos;
        }
    }
}
