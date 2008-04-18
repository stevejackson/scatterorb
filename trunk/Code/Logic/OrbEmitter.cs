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
    class OrbEmitter
    {
        public double minTime;
        public double maxTime;
        public double lastShot;
        public Vector2 position;

        List<Orb> orbs = new List<Orb>();

        public OrbEmitter()
        {
            minTime = 5f;
            maxTime = 10f;
            lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
            position = new Vector2();
        }

        public void Update()
        {
            if (Director.GameTime.TotalGameTime.TotalSeconds - lastShot > minTime)
            {
                lastShot = Director.GameTime.TotalGameTime.TotalSeconds;
                this.Shoot();
            }

            foreach (Orb o in orbs)
            {
                o.Update();
            }
        }

        public void Shoot()
        {
            Orb orb = new Orb();
            orb.Initialize();
            orb.LoadContent();
            orbs.Add(orb);
        }
    }
}