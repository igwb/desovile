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
        private List<ComponentSet> components = new List<ComponentSet>();

        private bool active = true;
        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

        private MenuButton buttonTemplate = new MenuButton();
        public MenuButton ButtonTemplate
        {
            get { return buttonTemplate; }
            set { buttonTemplate = value; buttonTemplate.IsActive = false; }
        }

        private MenuLabel labelTemplate = new MenuLabel();
        public MenuLabel LabelTemplate
        {
            get { return labelTemplate; }
            set { labelTemplate = value; labelTemplate.IsActive = false; }
        }

        public MenuButton addButton(string name, string text, Rectangle canvas)
        {
            MenuButton button = new MenuButton();

            button.Font = buttonTemplate.Font;
            button.LabelColor = buttonTemplate.LabelColor;
            button.LabelPosition = buttonTemplate.LabelPosition;
            button.Texture = buttonTemplate.Texture;
            button.Tint = buttonTemplate.Tint;

            button.Text = text;
            button.Canvas = canvas;

            this[name] = button;

            return button;
        }

        public MenuLabel addLabel(string name, string text, Point position)
        {
            MenuLabel label = new MenuLabel();

            label.Font = labelTemplate.Font;
            label.Tint = labelTemplate.Tint;

            label.Text = text;
            label.Position = position;

            this[name] = label;

            return label;
        }

        public MenuComponent this[string name]
        {
            get 
            {
                if (getComponentSet(name) == null)
                    return null;
                else
                    return getComponentSet(name).Component;
            }
            set
            {
                if (getComponentSet(name) == null)
                    components.Add(new ComponentSet(name, value));
                else
                    getComponentSet(name).Component = value;
            }
        }

        public ComponentSet getComponentSet(string name)
        {
            foreach (ComponentSet component in components)
            {
                if (component.Name == name)
                    return component;
            }

            return null;
        }

        public List<ComponentSet> getComponentSets()
        {
            return components;
        }

        public void update(GameTime gameTime)
        {
            foreach (ComponentSet componentSet in components)
            {
                if (componentSet.Component.IsActive)
                    componentSet.Component.update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (ComponentSet componentSet in components)
            {
                if(componentSet.Component.IsActive)
                    componentSet.Component.draw(spriteBatch);
            }
        }

        public class ComponentSet
        {
            private string setName;
            public string Name
            {
                get { return setName; }
            }

            private MenuComponent setComponent;
            public MenuComponent Component
            {
                get { return setComponent; }
                set { setComponent = value; }
            }


            public ComponentSet(string name, MenuComponent component)
            {
                setName = name; setComponent = component;
            }
        }
    }
}
