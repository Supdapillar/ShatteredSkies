using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class CollidersParticle : Particle
    {
        public CollidersParticle(Vector2 pos,SceneManager sceneman) : base(pos, sceneman)
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
            sb.Draw(SceneMan.Textures["Snap"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), SceneMan.Textures["Snap"].Width, SceneMan.Textures["Snap"].Height), null, new Color(255 - (byte)(TimeSinceCreation * 510), 255 - (byte)(TimeSinceCreation * 510), 255 - (byte)(TimeSinceCreation * 510), 100), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

        }

    }
}
