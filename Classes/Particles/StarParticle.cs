using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class StarParticle : Particle
    {
        private Color ParticleColor;
        public StarParticle(Vector2 pos,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(SceneMan.rand.Next(0, 0), (float)SceneMan.rand.Next(1, 5) / 200);
            switch(SceneMan.rand.Next(0,5))
            {
                case 0://Cyan star
                    ParticleColor = Color.DarkCyan;
                    break;
                case 1://red star
                    ParticleColor = new Color(0.25f,0,0);
                    break;
                case 2://white star
                    ParticleColor = Color.Gray;
                    break;
                case 3://yellow star
                    ParticleColor.R = 128;
                    ParticleColor.G = 128;
                    ParticleColor.B = 0;
                    break;
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
        }
        public override void Draw(SpriteBatch sb)
        {
            if (TimeSinceCreation % 1 > 0.5)
            {
                // sb.Draw(ParticlePic, new Vector2(Convert.ToSingle(Math.Ceiling(Pos.X)), Convert.ToSingle(Math.Ceiling(Pos.Y))), ParticleColor);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X - 1), (int)Math.Ceiling(Pos.Y), 3, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y - 1), 1, 3), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, Color.Black, 0f, new Vector2(0, 0), SpriteEffects.None, 0.99f);
            }
        }

    }
}
