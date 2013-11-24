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

namespace Animation
{
    public class Animation2D
    {
        private Texture2D spriteTiles;

        private Rectangle tile, canvas;

        private int frame, maxFrame, minFrame, actualMax;

        private double frameTime = 100, elapsedTime = 0;

        private bool active = true, loop = false, play = false;

        protected Color color = Color.White;

        /// <summary>
        /// Tint of the texture;
        /// </summary>
        public Color Tint
        {
            get { return color; }
            set { color = value; }
        }


        /// <summary>
        /// Says if the component is active.
        /// </summary>
        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

        public int CurrentFrame
        {
            get { return frame; }
            set { frame = value > actualMax ? actualMax : value; }
        }

        public int MaxFrame
        {
            get { return maxFrame; }
            set { maxFrame = value < actualMax ? value > 0 ? value : 0 : actualMax; }
        }

        public int MinFrame
        {
            get { return minFrame; }
            set { minFrame = value > 0 ? value : 0; }
        }

        public double FrameDelay
        {
            get { return frameTime; }
            set { frameTime = value; }
        }

        public Rectangle TileSize
        {
            get { return tile; }
            set { tile = value; actualMax = getMaxFrames(); }
        }

        public Rectangle Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        public Texture2D Tiles
        {
            get { return spriteTiles; }
            set { spriteTiles = value; actualMax = getMaxFrames(); }
        }

        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }

        public bool Play
        {
            get { return play; }
            set { play = value; frame = frame > maxFrame && value ? frame = minFrame -1 : frame; }
        }

        private int getMaxFrames()
        {
            try
            {
                return (int)(Tiles.Width / TileSize.Width) * (int)(Tiles.Height / TileSize.Height);
            }
            catch
            {
                return 0;
            }
        }

        public void update(GameTime gameTime)
        {
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime && play)
            {                
                frame++;

                if (frame > maxFrame)
                {
                    if (!loop)
                    {
                        play = false;
                        frame--;
                    }
                    else
                    {
                        frame = minFrame;
                    }
                }

                elapsedTime = 0;
            }

            int x = 0, y = 0;

            for (x = tile.Width * frame; x > spriteTiles.Width - tile.Width; x -= spriteTiles.Width)
                y += tile.Height;


            if (play)
                System.Diagnostics.Debug.WriteLine(frame);

            tile = new Rectangle(x, y, tile.Width, tile.Height);

            
            if (tile.Bottom > spriteTiles.Height)
                tile.Height = tile.Bottom - spriteTiles.Height;

            if (tile.Right > spriteTiles.Width)
                tile.Width = tile.Right - spriteTiles.Width;
             
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTiles, canvas, tile, color);
        }
    }
}
