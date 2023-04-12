using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class HealingParticle : Particle
    {
        private Color ParticleColor;
        public HealingParticle(Vector2 pos,Color col, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2((float)(SceneMan.rand.NextDouble()-0.5f)/4,-1+(float)(sceneman.rand.NextDouble()/2));
            ParticleColor = col;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;

            if (TimeSinceCreation > 1)
            {
                Pos.Y = 700;
            }

            Delta.Y /= 1.03f;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y-1), 1, 1), null, ParticleColor*(1-TimeSinceCreation), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X-1), (int)(Pos.Y), 1, 1), null, ParticleColor*(1-TimeSinceCreation), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 1, 1), null, ParticleColor*(1-TimeSinceCreation), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X+1), (int)(Pos.Y), 1, 1), null, ParticleColor*(1-TimeSinceCreation), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y+1), 1, 1), null, ParticleColor*(1-TimeSinceCreation), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X-1), (int)(Pos.Y+1), 1, 1), null, (ParticleColor * (1 - TimeSinceCreation)*0.5f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y+2), 1, 1), null, ParticleColor * (1 - TimeSinceCreation) * 0.5f, 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X+1), (int)(Pos.Y + 1), 1, 1), null, ParticleColor * (1 - TimeSinceCreation) * 0.5f, 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
        }

    }
}
