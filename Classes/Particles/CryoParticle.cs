using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class CryoParticle : Particle
    {
        public float Width = 50;
        public CryoParticle(Vector2 pos,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(SceneMan.rand.Next(0, 0), (float)SceneMan.rand.Next(1, 5) / 200);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation > 0.5f)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["CryoCircle"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)Width, (int)Width), null, new Color(0, 255 - (byte)(TimeSinceCreation * 510), 255 - (byte)(TimeSinceCreation * 510), 100), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

    }
}
