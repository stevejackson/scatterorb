using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Core;
using F2D;
using F2D.Graphics;
using F2D.Graphics.Gui;
using F2D.Input;
using F2D.Math;
using Scatter.Logic;
using Scatter;
using Scatter.Menu;

namespace Scatter.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Director screenManager;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            screenManager = new Director(this, graphics);
            Grid.Initialize(200, new Vector2(1600, 1200), 1);

            Camera.Initialize(new Vector2(0, 0), new Vector2(1600, 1200));

            screenManager.Initialize();

            Components.Add(screenManager);

            screenManager.AddScreen(new Background());
            screenManager.AddScreen(new MainMenu());
            screenManager.AddScreen(new Scatter.Logic.Level());

            this.IsFixedTimeStep = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
            Content.Unload();

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Grid.ParentCell = new Vector2Int(1, 1);

            screenManager.Update(gameTime);
            base.Update(gameTime);

            Window.Title = Director.FramesPerSecond.ToString();

        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Viewport = Director.ClearViewport;

            Director.graphicsDevice.Clear(Color.Blue);

            Director.graphicsDevice.Viewport = Director.SceneViewport;

            Director.graphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
