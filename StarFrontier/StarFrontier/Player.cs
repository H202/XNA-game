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
        public static Point FrameSize = new Point(45,45);
        public static Point CurrentFrame = new Point(0, 0);
        public static Point SpriteSheetSize = new Point(4, 2);
        public static Vector2 direction;

        public static void RotatePlayer()
        {
            PlayerRotation = (float)Math.Atan2(Game1.Distance.Y,Game1.Distance.X);
        }
        public static void PlayerPositionUpdate()
        {
            
        }
    }
}
