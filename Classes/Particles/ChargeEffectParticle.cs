using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ChargeEffectParticle : Particle
    {
        public bool CanMakeParticles = true;
        public ChargeEffectParticle(Vector2 pos,Vector2 delta,float lifeSpan, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = delta;
            TimeSinceCreation = lifeSpan;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation -= (float)GT.ElapsedGameTime.TotalSeconds;
            if (CanMakeParticles)
            {
                SceneMan.Particles.Add(new ChargeEffectParticle(Pos, new Vector2(0, 0), TimeSinceCreation - 0.1f, SceneMan)
                {
                    CanMakeParticles = false,
                });
            }
            if (TimeSinceCreation < 0)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 4), null, new Color(1f*(TimeSinceCreation*2), 1f * (TimeSinceCreation * 2), 1f * (TimeSinceCreation * 2), 1f * (TimeSinceCreation * 2)), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

    }
}
