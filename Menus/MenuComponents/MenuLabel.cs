using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Menus.MenuComponents
{
    public class MenuLabel : MenuComponent
    {
        private string labelText;

        /// <summary>
        /// The displayed text of the label.
        /// </summary>
        public string Text
        {
            get { return labelText; }
            set { labelText = value; }
        }

        private SpriteFont labelFont;

        /// <summary>
        /// The font used for the label.
        /// </summary>
        public SpriteFont Font
        {
            get { return labelFont; }
            set { labelFont = value;  }
        }

        private Vector2 labelPosition;

        public Point Position
        {
            get { return new Point((int)labelPosition.X, (int)labelPosition.Y); }
            set { labelPosition.X = value.X; labelPosition.Y = value.Y; }

        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (labelFont != null && labelText != null && color != null)
            {
                spriteBatch.DrawString(labelFont, labelText, labelPosition, Tint); 
            }
        }
    }
}
