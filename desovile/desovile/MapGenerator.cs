using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desovile {
    class MapGenerator {

        int height, width, cutWalls;


        Texture2D trbl, rbl, tbl, trl, trb;

        Chunk[,] chunks;

        public void generateMap(int width, int height,bool useSeed, int seed, int cutWalls) {

            Random r;

            if (useSeed) {
                r = new Random(seed);
            }
            else {
                r = new Random();
            }

            this.width = width;
            this.height = height;

            chunks = new Chunk[width, height];

            Point active, next;
            List<Point> unvisited = new List<Point>();
            List<Point> adjicent = new List<Point>();
            Stack<Point> stack = new Stack<Point>();

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    chunks[x, y] = new Chunk(false, false, false, false, new Point(x, y));
                    unvisited.Add(new Point(x, y));
                }
            }

            active = new Point(width / 2, height / 2);
            unvisited.Remove(active);

            while (unvisited.Count > 0) {

                adjicent.Clear();
                foreach (Point item in getChunksAdjicent(active)) {
                    if (unvisited.Contains(item)) {
                        adjicent.Add(item);
                    }
                }

                if (adjicent.Count > 0) {

                    next = adjicent.ElementAt(r.Next(0, adjicent.Count));

                    connectChunk(chunks[active.X, active.Y], chunks[next.X, next.Y]);

                    stack.Push(active);
                    unvisited.Remove(next);
                    active = next;
                }
                else if (stack.Count > 0) {

                    active = stack.Pop();
                }
                else {
                    active = unvisited.ElementAt(r.Next(0, unvisited.Count));
                    unvisited.Remove(active);
                }
            }
            
            List<Point> possible = new List<Point>();

            for (int x = 1; x < width - 1; x++) {
                for (int y = 1; y < height - 1; y++) {
                    possible.Add(new Point(x, y));
                }
            }

            int i = 0;
            while(i < cutWalls) {

                active = possible.ElementAt(r.Next(0, possible.Count));


                adjicent.Clear();
                foreach (Point item in getChunksAdjicent(active)) {
                    
                    if(!hasDirectConnection(chunks[active.X,active.Y],chunks[item.X,item.Y])) {
                        adjicent.Add(item);
                    }
                }

                if (adjicent.Count > 0) {
                    next = adjicent.ElementAt(r.Next(0, adjicent.Count));
                    connectChunk(chunks[active.X, active.Y], chunks[next.X, next.Y]);

                    
                    possible.Remove(next);
                    i++;
                }
                    possible.Remove(active);
            }
            
        }


        private void connectChunk(Chunk chunk1, Chunk chunk2) {
           
            if (chunk1.getPosition().X < chunk2.getPosition().X) {
                chunks[chunk1.getPosition().X, chunk1.getPosition().Y].setOpenRight(true);
                chunks[chunk2.getPosition().X, chunk2.getPosition().Y].setOpenLeft(true);
            }

            if (chunk1.getPosition().X > chunk2.getPosition().X) {
                chunks[chunk1.getPosition().X, chunk1.getPosition().Y].setOpenLeft(true);
                chunks[chunk2.getPosition().X, chunk2.getPosition().Y].setOpenRight(true);
            }

            if (chunk1.getPosition().Y < chunk2.getPosition().Y) {
                chunks[chunk1.getPosition().X, chunk1.getPosition().Y].setOpenBottom(true);
                chunks[chunk2.getPosition().X, chunk2.getPosition().Y].setOpenTop(true);
            }

            if (chunk1.getPosition().Y > chunk2.getPosition().Y) {
                chunks[chunk1.getPosition().X, chunk1.getPosition().Y].setOpenTop(true);
                chunks[chunk2.getPosition().X, chunk2.getPosition().Y].setOpenBottom(true);
            }
        }

        private bool hasDirectConnection(Chunk chunk1, Chunk chunk2) {

            if (chunk1.getPosition().X + 1 == chunk2.getPosition().X) {

                return chunk1.getOpenRight() && chunk2.getOpenLeft();
            }

            if (chunk1.getPosition().X - 1 == chunk2.getPosition().X) {

                return chunk1.getOpenLeft() && chunk2.getOpenRight();
            }

            if (chunk1.getPosition().Y + 1 == chunk2.getPosition().Y) {

                return chunk1.getOpenBottom() && chunk2.getOpenTop();
            }

            if (chunk1.getPosition().Y - 1 == chunk2.getPosition().Y) {

                return chunk1.getOpenTop() && chunk2.getOpenBottom();
            }

            return false;
        }
        
        private List<Point> getChunksAdjicent(Point pos) {
          
            List<Point> adjicent = new List<Point>();

            adjicent = new List<Point>();

            if (pos.Y - 1 >= 0) {
                adjicent.Add(new Point(pos.X, pos.Y - 1));
            }


            if (pos.X + 1 <= width) {
                adjicent.Add(new Point(pos.X + 1, pos.Y));
            }


            if (pos.Y + 1 <= height) {
                adjicent.Add(new Point(pos.X, pos.Y + 1));
            }


            if (pos.X - 1 >= 0) {
                adjicent.Add(new Point(pos.X - 1, pos.Y));
            }

            return adjicent;
        }

        public Chunk getChunk(Point pos) {

            return chunks[pos.X, pos.Y];
        }

        public void initializeGraphics(Texture2D trbl, Texture2D rbl, Texture2D tbl, Texture2D trl, Texture2D trb) {
            
            this.trbl = trbl;
            this.rbl = rbl;
            this.tbl = tbl;
            this.trl = trl;
            this.trb = trb;
        }

        public void draw(SpriteBatch spriteBatch) {

            System.Diagnostics.Debug.WriteLine("----------");

            foreach (Chunk item in chunks) {
                /*
                spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
spriteBatch.Draw(tbl, new Vector2(40 + item.getPosition().X * tbl.Bounds.Height, 40 + item.getPosition().Y * tbl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(tbl.Bounds.Width / 2, tbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(trl, new Vector2(40 + item.getPosition().X * trl.Bounds.Height, 40 + item.getPosition().Y * trl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(trl.Bounds.Width / 2, trl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(trb, new Vector2(40 + item.getPosition().X * trb.Bounds.Height, 40 + item.getPosition().Y * trb.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(trb.Bounds.Width / 2, trb.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                */
                System.Diagnostics.Debug.WriteLine(item.getPosition() + " -- " + item.getOpenTop() + "; " + item.getOpenRight() + "; " + item.getOpenBottom() + "; " + item.getOpenLeft());

                if (!item.getOpenTop()) {

                    spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    //spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                }

                if (!item.getOpenRight()) {
                    
                    spriteBatch.Draw(tbl, new Vector2(40 + item.getPosition().X * tbl.Bounds.Height, 40 + item.getPosition().Y * tbl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(tbl.Bounds.Width / 2, tbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    //spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 90.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                }

                if (!item.getOpenBottom()) {
                    spriteBatch.Draw(trl, new Vector2(40 + item.getPosition().X * trl.Bounds.Height, 40 + item.getPosition().Y * trl.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(trl.Bounds.Width / 2, trl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    //spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 180.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                }

                if (!item.getOpenLeft()) {

                    spriteBatch.Draw(trb, new Vector2(40 + item.getPosition().X * trb.Bounds.Height, 40 + item.getPosition().Y * trb.Bounds.Width), null, Color.White, (float)Math.PI * 0.0f / 180.0f, new Vector2(trb.Bounds.Width / 2, trb.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                    //spriteBatch.Draw(rbl, new Vector2(40 + item.getPosition().X * rbl.Bounds.Height, 40 + item.getPosition().Y * rbl.Bounds.Width), null, Color.White, (float)Math.PI * 270.0f / 180.0f, new Vector2(rbl.Bounds.Width / 2, rbl.Bounds.Height / 2), 1f, SpriteEffects.None, 0.0f);
                }
               
            }

        }
    }
}
