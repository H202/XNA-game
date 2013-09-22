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
     public class BlueLaser
    {
        public const int Speed = 20;
        public Vector2 LaserPos;
        public float PlayerRotation;
        public Vector2 direction;
        public BlueLaser(Player SelectedPlayer,float Rotation)
        {
            LaserPos = SelectedPlayer.PlayerPos;
            PlayerRotation = Rotation;
        }
        public void LaserPositionUpdate(GameTime gameTime)
        {
            direction = new Vector2((float)Math.Cos(PlayerRotation), (float)Math.Sin(PlayerRotation));
                    LaserPos -= direction * 1 * (gameTime.ElapsedGameTime.Milliseconds / 6);
        }
    }
}
