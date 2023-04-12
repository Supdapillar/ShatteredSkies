using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    /*public class Particle
    {
        public Vector2 Pos;
        public Vector2 Delta;

        public int RandomNumber;
        public int Type;
        public float TimeSinceCreation = 0;

        public Texture2D ParticlePic;

        public void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            switch (Type)
            {
            case 2:
                if (TimeSinceCreation > 0.5f)
                {
                    Pos.X = 600;
                }
            break;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            switch (Type)
            {
            case 0: //Particles that make it look like your moving upwards
                ParticleColor.R = (byte) (15 + (37 * Math.Ceiling(Delta.Y)));
                ParticleColor.G = (byte) (15 + (37 * Math.Ceiling(Delta.Y)));
                ParticleColor.B = (byte) (15 + (37 * Math.Ceiling(Delta.Y)));
                //sb.Draw(ParticlePic, new Vector2(Convert.ToSingle(Math.Ceiling(Pos.X)), Convert.ToSingle(Math.Ceiling(Pos.Y) - i)), ParticleColor);
                sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, (int)Math.Ceiling(Delta.Y * 1.5)), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, (float)(1f -(Delta.Y/3.5)));
                    break;
            case 1: //Star particles that blink
                    //gets a random good looking star
                    switch (RandomNumber)
                    {
                        case 0://red
                            ParticleColor.R = 128;
                            ParticleColor.G = 0;
                            ParticleColor.B = 0;
                            break;

                        case 1://yellow
                            ParticleColor.R = 128;
                            ParticleColor.G = 128;
                            ParticleColor.B = 0;
                            break;

                        case 2://blue
                            ParticleColor.R = 0;
                            ParticleColor.G = 0;
                            ParticleColor.B = 128;
                            break;

                        case 3://white
                            ParticleColor.R = 128;
                            ParticleColor.G = 128;
                            ParticleColor.B = 128;
                            break;
                    }

                if (TimeSinceCreation % 1 > 0.5)
                {
                // sb.Draw(ParticlePic, new Vector2(Convert.ToSingle(Math.Ceiling(Pos.X)), Convert.ToSingle(Math.Ceiling(Pos.Y))), ParticleColor);
                sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                } 
                else 
                {
                    sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X-1), (int)Math.Ceiling(Pos.Y), 3, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                    sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y-1), 1, 3), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                    sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 1, 1), null, Color.Black, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
                }
                break;
                case 2://player snap
                    sb.Draw(ParticlePic, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), ParticlePic.Width, ParticlePic.Height), null, new Color(255 - (byte)(TimeSinceCreation * 510), 255 - (byte)(TimeSinceCreation * 510), 255-(byte)(TimeSinceCreation* 510), 100), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    break;
            }
        }

    }
*/}
