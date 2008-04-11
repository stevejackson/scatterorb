using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using F2D.Core;

namespace Scatter.Menu
{
    class MenuEntry
    {
        string text;
        float selectionFade;

        F2D.Graphics.Gui.Button button;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public F2D.Graphics.Gui.Button Button
        {
            get { return button; }
        }

        public event EventHandler<EventArgs> Selected;

        protected internal virtual void OnSelectEntry()
        {
            if (Selected != null)
                Selected(this, EventArgs.Empty);
        }

        public MenuEntry(string text)
        {
            this.text = text;
        }

        public MenuEntry(string text, string buttonLocation)
        {
            this.text = text;
            this.button = new F2D.Graphics.Gui.Button();
            this.button.Initialize(buttonLocation, new Vector2(0, 0));
        }

        public void LoadContent(ContentManager content)
        {
            this.button.LoadContent(content);
        }

        public void UnloadContent()
        {
            this.button.UnloadContent();
        }

        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);

            this.button.Update();
        }


        public virtual void Draw(MenuScreen screen, Vector2 position,
                                 bool isSelected, GameTime gameTime)
        {
            this.button.CurState = isSelected ? F2D.Graphics.Gui.Button.State.Hover : F2D.Graphics.Gui.Button.State.Idle;
            this.button.Position = position;
        }


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return (int)this.button.Size.Y;
        }
    }
}
