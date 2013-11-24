using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desovile {

    class Chunk {
        private static int SIZE = 11, FIELD_SIZE = 45;

        private bool initialized;

        private Rectangle bounds;
        private Point chunkCoords;

        private Texture2D wall;

        //Sides the chunk is open on
        private bool top, right, left, bottom;

        private Field[,] fields;

        public Chunk(bool openTop, bool openRight, bool openLeft, bool openBottom, Point pos) {

            this.top = openTop;
            this.right = openRight;
            this.bottom = openBottom;
            this.left = openLeft;

            chunkCoords = pos;

            setPos(new Point(0,0));
        }

        public void setOpenTop(bool openTop) {

            initialized = false;

            this.top = openTop;
        }

        public void setOpenRight(bool openRight) {

            initialized = false;

            this.right = openRight;
        }

        public void setOpenBottom(bool openBottom) {

            initialized = false;

            this.bottom = openBottom;
        }

        public void setOpenLeft(bool openLeft) {

            initialized = false;

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

            return chunkCoords;
        }

        public void initialize(Texture2D wall) {

            bool passable;

            this.wall = wall;

            fields = new Field[SIZE, SIZE];

            for (int x = 0; x < SIZE; x++) {

                for (int y = 0; y < SIZE; y++) {

                    passable = !((!getOpenTop() && y == 0) | (!getOpenRight() && x == SIZE - 1) | (!getOpenBottom() && y == SIZE - 1) | (!getOpenLeft() && x == 0));
                    
                    fields[x, y] = new Field(this, new Rectangle(x * FIELD_SIZE, y * FIELD_SIZE, FIELD_SIZE, FIELD_SIZE), passable);
                    fields[x, y].initializeGraphics(wall);
                }
            }

            initialized = true;
        }

        public Rectangle getBounds() {

            return bounds;
        }

        public void setPos(Point pos) {

            this.bounds = new Rectangle(pos.X, pos.Y, SIZE * FIELD_SIZE, SIZE * FIELD_SIZE);
        }

        public bool isInitilaized() {

            return initialized;
        }

        public void draw(SpriteBatch spriteBatch) {

            foreach (Field item in fields) {

                item.draw(spriteBatch, new Point(bounds.X, bounds.Y));
            }
        }
    }
}
