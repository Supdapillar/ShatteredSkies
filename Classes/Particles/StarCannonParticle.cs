using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class StarCannonParticle : Particle
    {
        private Color ParticleColor;
        private Animation StarAnimation;
        public StarCannonParticle(Vector2 pos,Color col ,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            ParticleColor = col;
            Delta = new Vector2((float)(SceneMan.rand.NextDouble()-0.5)/2, (float)(SceneMan.rand.NextDouble()/2+0.25f));
            StarAnimation = new Animation(SceneMan.Textures["StarCannonParticle"],5);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)(GT.ElapsedGameTime.TotalSeconds * 1.5f); ;
            StarAnimation.Update((int)(TimeSinceCreation*4));
            if (TimeSinceCreation >= 1)
            {
                Pos.Y = 7000;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            StarAnimation.Draw(sb, Pos, 0.7f, ParticleColor,SpriteEffects.None,SceneMan); ;
        }

    }
}
