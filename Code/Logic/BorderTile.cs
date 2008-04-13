using System;
using System.Collections.Generic;
using System.Text;
using F2D.Graphics;
using F2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Scatter.Logic
{
    public class BorderTile
    {
        public Sprite sprite;
        public BorderType borderType;

        public BorderTile()
        {
            sprite = new Sprite();
        }

        public void Initialize(Vector2 position, BorderType borderType)
        {
            sprite.Initialize("BorderTile", position);
            this.borderType = borderType;
            this.ResetType(this.borderType);
            sprite.physicsBody.IsStatic = true;
        }

        public void ResetType(BorderType borderType)
        {
            this.borderType = borderType;
            string fn = @"Content\Graphics\tiles\";
            fn += borderType.ToString();
            sprite.LoadContent(Director.content, fn);
            sprite.physicsBody.IsStatic = true;
        }

        public void Draw()
        {
            
        }
    }
}
