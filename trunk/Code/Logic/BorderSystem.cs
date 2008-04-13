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
    public class BorderSystem
    {
        public BorderTile[] top;
        public BorderTile[] bottom;
        public BorderTile[] left;
        public BorderTile[] right;

        public BorderSystem()
        {
            top = new BorderTile[16];
            bottom = new BorderTile[16];
            left = new BorderTile[10];
            right = new BorderTile[10];
        }

        public void Initialize()
        {
            for(int i = 0; i < top.Length; i++)
            {
                top[i] = new BorderTile();
                bottom[i] = new BorderTile();
                
                top[i].Initialize(new Vector2(50 + i * 100, 50), BorderType.CLCR);
                top[i].sprite.Origin = new Vector2(top[i].sprite.Size.X / 2, top[i].sprite.Size.Y / 2);
                top[i].sprite.Rotation = (float)Math.PI;

                bottom[i].Initialize(new Vector2(50 + i * 100, 1150), BorderType.CLCR);
                bottom[i].sprite.Origin = new Vector2(bottom[i].sprite.Size.X / 2, bottom[i].sprite.Size.Y / 2);
                bottom[i].sprite.Rotation = 0f;
            }

            for (int i = 0; i < left.Length; i++)
            {
                left[i] = new BorderTile();
                right[i] = new BorderTile();

                left[i].Initialize(new Vector2(50, 150 + (i * 100)), BorderType.CLCR);
                left[i].sprite.Origin = new Vector2(left[i].sprite.Size.X / 2, left[i].sprite.Size.Y / 2);
                left[i].sprite.Rotation = (float)Math.PI / 2;

                right[i].Initialize(new Vector2(1550, 150 + (i * 100)), BorderType.CLCR);
                right[i].sprite.Origin = new Vector2(right[i].sprite.Size.X / 2, right[i].sprite.Size.Y / 2);
                right[i].sprite.Rotation = 3 * (float)Math.PI / 2;
            }
        }

        /// <summary>
        /// Generates a new level.
        /// </summary>
        /// <param name="difficultyFactor"></param>
        public void Regenerate(float difficultyFactor)
        {

        }

        public void Draw()
        {
        }
    }
}
