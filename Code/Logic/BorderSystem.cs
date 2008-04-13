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

                top[i].Initialize(new Vector2(i * 100, 0), BorderType.CLCR);
                bottom[i].Initialize(new Vector2(i * 100, 1100), BorderType.CLCR);
            }

            for (int i = 0; i < left.Length; i++)
            {
                left[i] = new BorderTile();
                right[i] = new BorderTile();

                left[i].Initialize(new Vector2(0, 100 + (i * 100)), BorderType.CLCR);
                right[i].Initialize(new Vector2(1500, 100 + (i * 100)), BorderType.CLCR);
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
