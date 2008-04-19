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
using F2D.Input;

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

        public void Update(Sprite paddle)
        {
            //this.sprite.physicsBody.AngularVelocity = 0f;
            bool attract = false;
            bool repel = false;

            if (Director.Rat.LState == Rat.State.Down)
                attract = true;

            else if (Director.Rat.RState == Rat.State.Down)
                repel = true;

            if (attract)
            {
                //get the distance between the 2 objects from edge-to-edge (not center-to-center)
                float dist = Vector2.Distance(paddle.Position, this.sprite.Position);
                dist -= paddle.Size.X;
                dist -= (this.sprite.Size.X / 8f);

                //get a vector in the proper direction            
                Vector2 diff = new Vector2();
                diff = paddle.Position - this.sprite.Position;

                /* calculate the power of the pull
                 * C/(d^p)
                 * C = power constant
                 * d = distance
                 * p = 1 - linear movement
                 *     2 - fast as it gets closer
                 *     3 - insanely fast as it gets closer.. 
                 */
                float power = 1000000f / ((float)Math.Pow(dist, 1.25f));

                //retain the direction, but give it proper power
                diff.Normalize();
                diff *= power;

                this.sprite.physicsBody.ApplyForce(diff);
            }
            else if (repel)
            {
                //get the distance between the 2 objects from edge-to-edge (not center-to-center)
                float dist = Vector2.Distance(paddle.Position, this.sprite.Position);
                dist -= paddle.Size.X;
                dist -= (this.sprite.Size.X / 8f);

                //get a vector in the proper direction            
                Vector2 diff = new Vector2();
                diff = paddle.Position - this.sprite.Position;

                /* calculate the power of the pull
                 * C/(d^p)
                 * C = power constant
                 * d = distance
                 * p = 1 - linear movement
                 *     2 - fast as it gets closer
                 *     3 - insanely fast as it gets closer.. 
                 */
                float power = 1000000f / ((float)Math.Pow(dist, 1.25f));

                //retain the direction, but give it proper power
                diff.Normalize();
                diff *= power;

                this.sprite.physicsBody.ApplyForce(-diff);
            }
        }

        public void UnloadContent()
        {
            sprite.UnloadContent();
        }
    }
}
