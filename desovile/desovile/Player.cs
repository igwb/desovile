using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace desovile {
    class Player {

        private Texture2D player;
        private Vector2 position;
        private Chunk chunk;

        private bool active;

        public enum direction {

            up,
            right,
            left,
            down
        };

        public void initialize(Texture2D texture, Vector2 position, Chunk chunk) {

            player = texture;
            this.position = position;
            this.chunk = chunk;

        }

        public void draw(SpriteBatch spriteBatch) {


            spriteBatch.Draw(player, position, Color.White);

        }

        public bool movePlayer(direction dir, int distance) {

            Vector2 newPos;

            switch (dir) {
                case direction.up:
                    newPos = new Vector2(position.X, position.Y - distance);
                    break;
                case direction.right:
                    newPos = new Vector2(position.X + distance, position.Y);
                    break;
                case direction.left:
                    newPos = new Vector2(position.X - distance, position.Y);
                    break;
                case direction.down:
                    newPos = new Vector2(position.X, position.Y + distance);
                    break;
                default:
                    return false;
            }

            position = newPos;
            return true;
        }
    }
}
