using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ColoredParticle : Particle
    {
        private Color ParticleColor;
        private bool IsFading = false;
        private float MaxLifeSpan;
        public ColoredParticle(Vector2 pos, Vector2 delta, SceneManager sceneman, Color col) : base(pos, sceneman)
        {
            ParticleColor = col;
            Pos = pos;
            SceneMan = sceneman;
            Delta = delta;
            TimeSinceCreation = 2;
        }
        public ColoredParticle(Vector2 pos, Vector2 delta, SceneManager sceneman, Color col, bool isFading, float LifeSpan) : base(pos, sceneman)
        {
            ParticleColor = col;
            Pos = pos;
            SceneMan = sceneman;
            Delta = delta;
            IsFading = isFading;
            TimeSinceCreation = LifeSpan;
            MaxLifeSpan = LifeSpan;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation -= (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation < 0)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (IsFading)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, new Color(ParticleColor.R/255f * (TimeSinceCreation / MaxLifeSpan), ParticleColor.G / 255f * (TimeSinceCreation / MaxLifeSpan), ParticleColor.B / 255f * (TimeSinceCreation / MaxLifeSpan), ParticleColor.A / 255f * (TimeSinceCreation / MaxLifeSpan)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            }
        }

    }
}
