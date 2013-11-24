using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Menus.MenuComponents
{
    public class MenuComponent
    {
        /// <summary>
        /// MenuComponent event
        /// </summary>
        public event EventHandler onClick, onHover, onMouseEnter, onMouseLeave, onDoubleClick, onMousePress, onMouseRelease;

        protected Rectangle canvas;

        /// <summary>
        /// Location and size of the component.
        /// </summary>
        public Rectangle Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        protected Texture2D texture;

        /// <summary>
        /// Texture of the component.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private bool active = true;

        /// <summary>
        /// Says if the component is active.
        /// </summary>
        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

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
        /// The default windows double click speed (500ms).
        /// </summary>
        public static double doubleClickSpeed = 500;

        /// <summary>
        /// update variable
        /// </summary>
        private bool mouseHovers = false, mousePressed = false, mouseReleased = true, canClick = false;

        /// <summary>
        /// update variable
        /// </summary>
        private double lastClick = -501;

        public virtual void update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (!mouseHovers && canvas.Contains(mouseState.X, mouseState.Y))
            {
                mouseHovers = true;

                if (onMouseEnter != null)
                {
                    onMouseEnter.Invoke(this, EventArgs.Empty); 
                }
            }
            else if (mouseHovers && !canvas.Contains(mouseState.X, mouseState.Y))
            {
                canClick = false;
                mouseHovers = false;

                if (onMouseLeave != null)
                {
                    onMouseLeave.Invoke(this, EventArgs.Empty); 
                }
            }

            if (mouseHovers && onHover != null)
            {
                onHover.Invoke(this, EventArgs.Empty);
            }

            if (mouseHovers && !mousePressed && mouseState.LeftButton == ButtonState.Pressed)
            {
                canClick = true;
                mousePressed = true;
                mouseReleased = false;

                if (onMousePress != null)
                {
                    onMousePress.Invoke(this, EventArgs.Empty); 
                }
            }
            else if (!mouseReleased && mouseState.LeftButton == ButtonState.Released)
            {
                mouseReleased = true;
                mousePressed = false;

                if (mouseHovers)
                {
                    if (onMouseRelease != null)
                    {
                        onMouseRelease.Invoke(this, EventArgs.Empty); 
                    }

                    if (canClick)
                    {
                        if (onClick != null)
                        {
                            onClick.Invoke(this, EventArgs.Empty); 
                        }

                        if ((lastClick + doubleClickSpeed) > gameTime.TotalGameTime.TotalMilliseconds && onDoubleClick != null)
                        {
                            onDoubleClick.Invoke(this, EventArgs.Empty);
                        }

                        lastClick = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
            }
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (canvas != null && texture != null && color != null)
            {
                spriteBatch.Draw(texture, canvas, color);
            }
        }
    }
}
