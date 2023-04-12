using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class TextParticle : Particle
    {
        private Color ParticleColor;
        private string Text;
        public TextParticle(Vector2 pos,string text,Color col, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(0,-1);
            Text = text;
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

            Delta.Y /= 1.05f;
        }
        public override void Draw(SpriteBatch sb)
        {
            if (TimeSinceCreation % 0.25f > 0.125f) 
            {
                sb.DrawString(SceneMan.Pico8, Text, Pos, Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                sb.DrawString(SceneMan.Pico8, Text, new Vector2(Pos.X, Pos.Y + 1), new Color(64,64,64,255), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.15f);

            }
            else
            {
                sb.DrawString(SceneMan.Pico8, Text, Pos, ParticleColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                sb.DrawString(SceneMan.Pico8, Text, new Vector2(Pos.X, Pos.Y + 1), new Color(ParticleColor.R / 2, ParticleColor.G / 2, ParticleColor.B/2, 255), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.15f);
            }
        }

    }
}
