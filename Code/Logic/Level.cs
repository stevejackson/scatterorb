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

        #region Game related input

        public bool InputMoveLeft
        {
            get { return Director.input.IsNewKeyPress(Keys.A); }
        }

        public bool InputMoveRight
        {
            get { return Director.input.IsNewKeyPress(Keys.D); }
        }

        public bool InputMoveUp
        {
            get { return Director.input.IsNewKeyPress(Keys.W); }
        }

        public bool InputMoveDown
        {
            get { return Director.input.IsNewKeyPress(Keys.S); }
        }

        #endregion

        #region Game content
        /*
         * game content
         */

        F2D.Graphics.Sprite paddle;

        #endregion

        private BorderSystem borderSys;
  
        public Level()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            borderSys = new BorderSystem();
            borderSys.Initialize();
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Director.Game.Services);

            paddle = new F2D.Graphics.Sprite();

            paddle.Initialize("Paddle", new Vector2(Director.Game.Window.ClientBounds.Width / 2, Director.Game.Window.ClientBounds.Height / 2),0.5f);
            paddle.LoadContent(this.content, @"Content\Graphics\paddle");

            Director.Rat.setVisible();
        }

        public override void UnloadContent()
        {
            paddle.UnloadContent();

            Director.Rat.setVisible();
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);


        }

        public override void HandleInput(InputState input)
        {
            /* debug input */
            if (input.IsNewKeyPress(Keys.C))
            {
                Director.RenderCells = !Director.RenderCells;
            }

            /* Game input */
            for (int i = 0; i < InputState.MaxInputs; i++)
            {
                if (input.CurrentKeyboardStates[i].IsKeyDown(Keys.A))
                    paddle.Rotation -= 0.015f;

                if (input.CurrentKeyboardStates[i].IsKeyDown(Keys.D))
                    paddle.Rotation += 0.015f;
            }
            
            paddle.Position = Director.Rat.Position;
            //paddle.Rotation = TurnToFace(paddle.Position, Director.Rat.Position, paddle.Rotation, 180f);

        }

        private static float TurnToFace(Vector2 position, Vector2 faceThis,
                                        float currentAngle, float turnSpeed)
        {
            // consider this diagram:
            //         C 
            //        /|
            //      /  |
            //    /    | y
            //  / o    |
            // S--------
            //     x
            // 
            // where S is the position of the spot light, C is the position of the cat,
            // and "o" is the angle that the spot light should be facing in order to 
            // point at the cat. we need to know what o is. using trig, we know that
            //      tan(theta)       = opposite / adjacent
            //      tan(o)           = y / x
            // if we take the arctan of both sides of this equation...
            //      arctan( tan(o) ) = arctan( y / x )
            //      o                = arctan( y / x )
            // so, we can use x and y to find o, our "desiredAngle."
            // x and y are just the differences in position between the two objects.
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;

            // we'll use the Atan2 function. Atan will calculates the arc tangent of 
            // y / x for us, and has the added benefit that it will use the signs of x
            // and y to determine what cartesian quadrant to put the result in.
            // http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
            float desiredAngle = (float)Math.Atan2(y, x);

            // so now we know where we WANT to be facing, and where we ARE facing...
            // if we weren't constrained by turnSpeed, this would be easy: we'd just 
            // return desiredAngle.
            // instead, we have to calculate how much we WANT to turn, and then make
            // sure that's not more than turnSpeed.

            // first, figure out how much we want to turn, using WrapAngle to get our
            // result from -Pi to Pi ( -180 degrees to 180 degrees )
            float difference = WrapAngle(desiredAngle - currentAngle);

            // clamp that between -turnSpeed and turnSpeed.
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

            // so, the closest we can get to our target is currentAngle + difference.
            // return that, using WrapAngle again.
            return WrapAngle(currentAngle + difference);
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// </summary>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
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
