using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class FlameParticle : Particle
    {
        private Color ParticleColor;
        private Animation FlameAnimation;
        public FlameParticle(Vector2 pos, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2((float)((SceneMan.rand.NextDouble()-0.5)/2),0);
            ParticleColor = new Color(1f, 0f, 0f, 1f);
            FlameAnimation = new Animation(SceneMan.Textures["FlameParticle"],8,5,false);
            TimeSinceCreation = 0 - (float)SceneMan.rand.NextDouble()/4;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            FlameAnimation.Update(GT);
            if (TimeSinceCreation < 0.1f)
            {
                ParticleColor = new Color(1f, 0f, 0f, 1f);
            }
            else if (TimeSinceCreation < 0.2f)
            {
                ParticleColor = new Color(1f, 1f, 0f, 1f);
            }
            else if (TimeSinceCreation < 0.3f)
            {
                ParticleColor = new Color(1f, 0.5f, 0f, 1f);
            }
            else if (TimeSinceCreation < 0.5f)
            {
                ParticleColor = new Color(0.5f, 0.5f, 0.5f, 1f);
            }

            if (TimeSinceCreation > 0.5f)
            {
                Pos.Y = 700;
            }

            Delta.Y /= 1.02f;
        }
        public override void Draw(SpriteBatch sb)
        {
            FlameAnimation.Draw(sb,Pos,0.6f,ParticleColor,SpriteEffects.None,SceneMan);
        }

    }
}
