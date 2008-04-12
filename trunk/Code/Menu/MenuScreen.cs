using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using F2D.Graphics.Gui;
using F2D.Core;
using F2D.Input;
using Microsoft.Xna.Framework.Input;

namespace Scatter.Menu
{
    abstract class MenuScreen : GameScreen
    {

        #region Input

        public bool InputMenuUp
        {
            get
            {
                return Director.input.IsNewKeyPress(Keys.Up);
            }
        }

        public bool InputMenuDown
        {
            get
            {
                return Director.input.IsNewKeyPress(Keys.Down);
            }
        }

        public bool InputMenuSelect
        {
            get
            {
                return Director.input.IsNewKeyPress(Keys.Space) ||
                       Director.input.IsNewKeyPress(Keys.Enter);
            }
        }

        public bool InputMenuCancel
        {
            get
            {
                return Director.input.IsNewKeyPress(Keys.Escape);
            }
        }

        #endregion

        List<MenuEntry> menuEntries = new List<MenuEntry>();
        int selectedEntry = 0;
        string menuTitle;

        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        public MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (this.InputMenuUp)
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }
            // Move to the next menu entry?
            if (this.InputMenuDown)
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            // Accept or cancel the menu?
            if (this.InputMenuSelect)
            {
                OnSelectEntry(selectedEntry);
            }
            else if (this.InputMenuCancel)
            {
                OnCancel();
            }

            //Override keyboard input with Mouse: it gets priority
            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntries[i].Button.Update();

                if (MenuEntries[i].Button.CurState == F2D.Graphics.Gui.Button.State.Hover)
                {
                    selectedEntry = i;
                }

                else if (MenuEntries[i].Button.CurState == F2D.Graphics.Gui.Button.State.Depressed)
                {
                    selectedEntry = i;
                    OnSelectEntry(selectedEntry);
                }
            }
        }

        protected virtual void OnSelectEntry(int entryIndex)
        {
            menuEntries[selectedEntry].OnSelectEntry();
        }

        protected virtual void OnCancel()
        {
            ExitScreen();
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            this.UnloadContent();
            OnCancel();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 position = new Vector2(1100, 250);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];
                bool isSelected = IsActive && (i == selectedEntry);
                position.Y += menuEntry.GetHeight(this);
                menuEntry.Draw(this, position, isSelected, gameTime);
            }
        }
    }
}
