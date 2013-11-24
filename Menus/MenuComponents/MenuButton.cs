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

        private Point buttonLabelPosition;
        public Point LabelPosition
        {
            get { return buttonLabelPosition; }
            set { buttonLabelPosition = value; }

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
                Vector2 labelPos = new Vector2(buttonLabelPosition.X + canvas.X, buttonLabelPosition.Y + canvas.Y);
                spriteBatch.DrawString(buttonLabelFont, buttonLabelText, labelPos, buttonLabelColor);
            }
        }
    }
}
