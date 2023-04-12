using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Particle
    {
        public Vector2 Pos;
        public Vector2 Delta = new Vector2(0, 0);

        public float TimeSinceCreation = 0;
        public bool DeleteWhenOutside = true;

        public SceneManager SceneMan;

        public Particle(Vector2 pos,SceneManager sceneman)
        {
            SceneMan = sceneman;
        }

        public virtual void Update(GameTime GT)
        {

        }
        public virtual void Draw(SpriteBatch sb)
        {

        }

    }
}
