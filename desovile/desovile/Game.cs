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

namespace desovile
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapGenerator map;


        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // A movement speed for the player
        int playerMoveSpeed = 4;

        private static int MAP_SIZE = 15;

        Point activeChunk;

        private Player player;

        private Texture2D trbl, rbl, tbl, trl, trb, wall;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 750;
            graphics.ApplyChanges();

            map = new MapGenerator();
            map.generateMap(MAP_SIZE, MAP_SIZE, true, 1337, 20);

            activeChunk = new Point(0, 0);

            player = new Player();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D trbl, rbl, tbl, trl, trb;
            trbl = Content.Load<Texture2D>("Graphics/TRBL");
            rbl = Content.Load<Texture2D>("Graphics/RBL");
            tbl = Content.Load<Texture2D>("Graphics/TBL");
            trl = Content.Load<Texture2D>("Graphics/TRL");
            trb = Content.Load<Texture2D>("Graphics/TRB");
            wall = Content.Load<Texture2D>("Graphics/wall");
            map.initializeGraphics(trbl,rbl,tbl,trl,trb, wall);

            player.initialize(Content.Load<Texture2D>("Graphics/player"), new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2), map.getChunk(new Point(7, 7)));
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            updatePlayer(gameTime);
            updateActiveChunk(gameTime);

            base.Update(gameTime);
        }


        private void updatePlayer(GameTime gameTime) {

            if (currentKeyboardState.IsKeyDown(Keys.Left)) {
                player.movePlayer(Player.direction.left, playerMoveSpeed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up)) {
                player.movePlayer(Player.direction.up, playerMoveSpeed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right)) {
                player.movePlayer(Player.direction.right, playerMoveSpeed);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down)) {
                player.movePlayer(Player.direction.down, playerMoveSpeed);
            }
        }

        private void updateActiveChunk(GameTime gameTime) {

            Chunk active = map.getChunk(activeChunk);
            Rectangle chunkBounds = new Rectangle(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - map.getChunk(activeChunk).getBounds().Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - map.getChunk(activeChunk).getBounds().Height / 2, active.getBounds().Width, active.getBounds().Height);

            if (player.getBounds().Center.X - 10 <= chunkBounds.X && activeChunk.X - 1 >= 0) {
                activeChunk = new Point(activeChunk.X - 1, activeChunk.Y);
                player.setPosition(new Vector2(chunkBounds.X + chunkBounds.Width - 50, player.getBounds().Y));
            }

            if (player.getBounds().Center.X + 10 >= chunkBounds.X + chunkBounds.Width && activeChunk.X + 1 <= MAP_SIZE - 1) {
                activeChunk = new Point(activeChunk.X + 1, activeChunk.Y);
                player.setPosition(new Vector2(chunkBounds.X + 50, player.getBounds().Y));
            }

            if (player.getBounds().Center.Y - 10 <= chunkBounds.Y  && activeChunk.Y - 1 >= 0) {
                activeChunk = new Point(activeChunk.X, activeChunk.Y - 1);
                player.setPosition(new Vector2(player.getBounds().X, chunkBounds.Y + chunkBounds.Height - 50));
            }

            if (player.getBounds().Center.Y + 10 >= chunkBounds.Y + chunkBounds.Height && activeChunk.Y + 1 <= MAP_SIZE - 1) {
                activeChunk = new Point(activeChunk.X, activeChunk.Y + 1);
                player.setPosition(new Vector2(player.getBounds().X, chunkBounds.Y + 50));
            }

            System.Diagnostics.Debug.WriteLine("Active: " + activeChunk);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            map.getChunk(activeChunk).setPos(new Point(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.TitleSafeArea.Width / 2 - map.getChunk(activeChunk).getBounds().Width / 2 , GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2 - map.getChunk(activeChunk).getBounds().Height / 2));
            map.getChunk(activeChunk).draw(spriteBatch);
            map.draw(spriteBatch);

            player.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
