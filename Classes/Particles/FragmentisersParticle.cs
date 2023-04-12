using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class FragmentisersParticle : Particle
    {
        public FragmentisersParticle(Vector2 pos,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation > 0.05f)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["Snap"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), SceneMan.Textures["Snap"].Width/4, SceneMan.Textures["Snap"].Height/4), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}