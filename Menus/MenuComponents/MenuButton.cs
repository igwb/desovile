using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Menus.MenuComponents
{
    public class MenuButton : MenuComponent
    {
        private string buttonLabelText;

        /// <summary>
        /// The displayed text of the buttonLabel.
        /// </summary>
        public string Text
        {
            get { return buttonLabelText; }
            set { buttonLabelText = value; }
        }

        private SpriteFont buttonLabelFont;

        /// <summary>
        /// The font used for the buttonLabel.
        /// </summary>
        public SpriteFont Font
        {
            get { return buttonLabelFont; }
            set { buttonLabelFont = value; }
        }

        private Vector2 buttonLabelPosition;

        public Point LabelPosition
        {
            get { return new Point((int)buttonLabelPosition.X - Canvas.X, (int)buttonLabelPosition.Y - Canvas.Y); }
            set { buttonLabelPosition.X = value.X + Canvas.X; buttonLabelPosition.Y = value.Y + Canvas.Y; }

        }

        private Color buttonLabelColor = Color.Black;

        /// <summary>
        /// Tint of the texture;
        /// </summary>
        public Color LabelColor
        {
            get { return buttonLabelColor; }
            set { buttonLabelColor = value; }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            if (buttonLabelFont != null && buttonLabelText != null && buttonLabelColor != null)
            {
                spriteBatch.DrawString(buttonLabelFont, buttonLabelText, buttonLabelPosition, buttonLabelColor);
            }
        }
    }
}
