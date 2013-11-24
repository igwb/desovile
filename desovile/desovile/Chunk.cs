using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desovile {

    class Chunk {
        private static int SIZE = 11, FIELD_SIZE = 45;

        private Point pos;

        private Texture2D wall;

        //Sides the chunk is open on
        private bool top, right, left, bottom;

        private Field[,] fields;

        public Chunk(bool openTop, bool openRight, bool openLeft, bool openBottom, Point pos) {

            this.top = openTop;
            this.right = openRight;
            this.bottom = openBottom;
            this.left = openLeft;

            this.pos = pos;
        }

        public void setOpenTop(bool openTop) {

            this.top = openTop;
        }

        public void setOpenRight(bool openRight) {

            this.right = openRight;
        }

        public void setOpenBottom(bool openBottom) {

            this.bottom = openBottom;
        }

        public void setOpenLeft(bool openLeft) {

            this.left = openLeft;
        }

        public bool getOpenTop() {

            return this.top;
        }

        public bool getOpenRight() {

            return this.right;
        }

        public bool getOpenBottom() {

            return this.bottom;
        }

        public bool getOpenLeft() {

            return this.left;
        }

        public Point getPosition() {

            return pos;
        }

        public void initializeGraphics(Texture2D wall) {

            this.wall = wall;
        }


        public void initialize() {

            bool passable;

            fields = new Field[SIZE, SIZE];

            for (int x = 0; x < SIZE; x++) {

                for (int y = 0; y < SIZE; y++) {

                    passable = !((!getOpenTop() && y == 0) | (!getOpenRight() && x == SIZE) | (!getOpenBottom() && y == SIZE) | (!getOpenLeft() && x == 0));
                    
                    fields[x, y] = new Field(this, new Rectangle(x * FIELD_SIZE, y * FIELD_SIZE, FIELD_SIZE, FIELD_SIZE), passable);
                    fields[x, y].initializeGraphics(wall);
                }
            }

        }

        public void draw(SpriteBatch spriteBatch, Point position) {

            foreach (Field item in fields) {

                
                item.draw(spriteBatch, position);
            }
        }
    }
}
