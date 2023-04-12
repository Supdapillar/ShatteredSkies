using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class UpwardsParticle : Particle
    {
        private Color ParticleColor;
        public UpwardsParticle(Vector2 pos, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(SceneMan.rand.Next(0, 0), (float)SceneMan.rand.Next(4, 60) / 10);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ParticleColor.R = (byte)(15 + (37 * Math.Ceiling(Delta.Y)));
            ParticleColor.G = (byte)(15 + (37 * Math.Ceiling(Delta.Y)));
            ParticleColor.B = (byte)(15 + (37 * Math.Ceiling(Delta.Y)));
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, (int)Math.Ceiling(Delta.Y * 1.5)), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, (float)(1f - (Delta.Y / 3.5)));
        }

    }
}
