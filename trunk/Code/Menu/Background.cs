using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Scatter.Core;
using Scatter.Logic;

namespace Scatter.Menu
{
    class Background : F2D.Core.GameScreen
    {
        Texture2D backgroundTexture;

        public Background()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            base.LoadContent();

           // backgroundTexture = content.Load<Texture2D>(@"Content\Graphics\menu\starfield");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle fullscreen = new Rectangle(0, 0, 1600, 1200);
            byte fade = TransitionAlpha;
        }

    }
}
