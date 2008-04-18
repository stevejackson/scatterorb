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
        Director director;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            director = new Director(this, graphics);
            Grid.Initialize(400, new Vector2(1600, 1200), 2);

            Camera.Initialize(new Vector2(0, 0), new Vector2(1600, 1200));

            director.Initialize();

            Components.Add(director);
        
            director.AddScreen(new Background());
            director.AddScreen(new MainMenu());

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

            director.Update(gameTime);
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
