using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Graphics;
using F2D.Core;

namespace Scatter.Logic
{
    public class Orb
    {
        public enum Color
        {
            Red,
            Blue,
            Green
        }

        public Sprite sprite;
        public Color color;

        public Orb()
        {
            sprite = new Sprite();
        }

        public void Initialize()
        {
            sprite.Initialize("Orb", new Vector2(0, 0));
        }

        public void LoadContent()
        {
            string fn = @"Content\Graphics\Orb";
            sprite.LoadContent(Director.content, fn);
            sprite.Layer = 0.1f;
            sprite.Origin = new Vector2(sprite.Size.X / 2, sprite.Size.Y / 2);
        }

        public void Update()
        {
            //this.sprite.Position += new Vector2(1f, 1f);
            this.sprite.physicsBody.ApplyForce(new Vector2(200f, 0f));
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
        }
    }
}
