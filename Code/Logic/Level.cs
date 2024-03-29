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
            paddle.Initialize(
                new Vector2(Director.Game.Window.ClientBounds.Width / 2, Director.Game.Window.ClientBounds.Height / 2),
                "Circle",
                50f);
            paddle.LoadContent(this.content, @"Content\Graphics\paddle");
            paddle.PhysicsGeometry.RestitutionCoefficient = 0f;
            //Director.Rat.setVisible();

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            paddle.UnloadContent();

            Director.Rat.setVisible();
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            borderSys.Update(paddle);
            Farseer.Physics.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void HandleInput(InputState input)
        {

            /* debug input */
            if (input.IsNewKeyPress(Keys.C))
            {
                Director.RenderCells = !Director.RenderCells;
            }

            /* Game input */            

                paddle.physicsBody.LinearVelocity = Vector2.Zero;
                paddle.physicsBody.AngularVelocity = 0f;
           
                if (input.CurrentKeyboardState.IsKeyDown(Keys.A))
                    paddle.physicsBody.LinearVelocity = new Vector2(-200, paddle.physicsBody.LinearVelocity.Y);
                
                if (input.CurrentKeyboardState.IsKeyDown(Keys.D))                
                    paddle.physicsBody.LinearVelocity = new Vector2(200, paddle.physicsBody.LinearVelocity.Y);
                                
                if (input.CurrentKeyboardState.IsKeyDown(Keys.W))                
                    paddle.physicsBody.LinearVelocity = new Vector2(paddle.physicsBody.LinearVelocity.X, -200);
                
                if (input.CurrentKeyboardState.IsKeyDown(Keys.S))                
                    paddle.physicsBody.LinearVelocity = new Vector2(paddle.physicsBody.LinearVelocity.X, 200);   

                if (input.CurrentKeyboardState.IsKeyDown(Keys.Left))
                    paddle.physicsBody.AngularVelocity = -2f;

                if (input.CurrentKeyboardState.IsKeyDown(Keys.Right))
                    paddle.physicsBody.AngularVelocity = 2f;

            
            paddle.Origin = new Vector2(paddle.Size.X / 2, paddle.Size.Y / 2);
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
