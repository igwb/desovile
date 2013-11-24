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
using Menus.MenuComponents;

namespace Menus
{
    public class Menu
    {
        private Dictionary<string, MenuComponent> components = new Dictionary<string, MenuComponent>();

        public MenuComponent this[string name]
        {
            get { return components[name]; }
            set { components[name] = value; }
        }

        public Dictionary<string, MenuComponent> getDictionary()
        {
            return components;
        }

        public void update(GameTime gameTime)
        {
            foreach (MenuComponent component in components.Values)
            {
                component.update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (MenuComponent component in components.Values)
            {
                component.draw(spriteBatch);
            }
        }
    }
}
