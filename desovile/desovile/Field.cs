using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desovile {
    class Field {

        bool passable;

        private Texture2D wall;

        private Chunk chunk;
        private Rectangle bounds;

        public Field(Chunk chunk, Rectangle bounds, bool passable) {

            this.chunk = chunk;
            this.bounds = bounds;
            this.passable = passable;
        }

        public void initializeGraphics(Texture2D wall) {

            this.wall = wall;
        }

        public void draw(SpriteBatch spriteBatch, Point position) {

            if (!passable) {
                spriteBatch.Draw(wall,new Rectangle(bounds.X + position.X, bounds.Y + position.Y, bounds.Width, bounds.Height),Color.White);
                
            }

        }

    }
}
