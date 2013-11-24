using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace desovile
{
    
    class Chunk
    {
        Point pos;

        //Sides the chunk is open on
        bool top, right, left, bottom;

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
    
    }
}
