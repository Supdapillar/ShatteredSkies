using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class DictionaryParticle : Particle
    {
        private Color ParticleColor;
        public DictionaryParticle(Vector2 pos, Color Col, SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            TimeSinceCreation = 0;
            ParticleColor = Col;
        }

        public override void Update(GameTime GT)
        {
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!(SceneMan.Buttons[SceneMan.menupos].Pos == Pos))
            {
                sb.Draw(SceneMan.Textures["DictionaryParticle"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 25, 26), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0.010f);
            }
        }
    }
}
