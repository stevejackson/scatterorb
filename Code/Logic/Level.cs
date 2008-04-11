using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Core;
using F2D.Input;

namespace Scatter.Logic
{
    class Level : GameScreen
    {
        ContentManager content;
        F2D.Graphics.Sprite[] sprites = new F2D.Graphics.Sprite[1000];
        F2D.Graphics.WorldImage wImg;
        F2D.Graphics.Gui.ScreenImage sImg;

        public Level()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Director.Game.Services);

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new F2D.Graphics.Sprite();
                sprites[i].Initialize("Asteroid", new Vector2(i, 200));
                sprites[i].LoadContent(this.content, @"Content\Graphics\Asteroid");
            }

            wImg = new F2D.Graphics.WorldImage();
            wImg.Initialize(new Vector2(1000, 200));
            wImg.LoadContent(this.content, @"Content\Graphics\Asteroid");

            sImg = new F2D.Graphics.Gui.ScreenImage();
            sImg.Initialize(new Vector2(1500, 1100));
            sImg.LoadContent(this.content, @"Content\Graphics\Asteroid");
        }

        public override void UnloadContent()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].UnloadContent();
            }

            sImg.UnloadContent();
            wImg.UnloadContent();

            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            /*if (input.MenuCancel)
            {
                OnCancel();
            }*/

            if (input.IsNewKeyPress(Keys.L))
            {
                Director.RenderCells = true;
            }
            if (input.IsNewKeyPress(Keys.K))
            {
                Director.RenderCells = false;
            }
        }

        public void OnCancel()
        {
            ExitScreen();
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle fullscreen = new Rectangle(0, 0, 1600, 1200);
            byte fade = TransitionAlpha;
        }

    }
}
