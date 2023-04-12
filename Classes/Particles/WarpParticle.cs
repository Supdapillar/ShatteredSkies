using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class WarpParticle : Particle
    {
        private Animation StarAnimation;
        public WarpParticle(Vector2 pos ,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2((float)(SceneMan.rand.NextDouble()-0.5)/8, (float)((SceneMan.rand.NextDouble()-0.5) / 8));
            StarAnimation = new Animation(SceneMan.Textures["WarpParticle"],13);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)(GT.ElapsedGameTime.TotalSeconds * 1.5f); ;
            StarAnimation.Update((int)(TimeSinceCreation*12));
            if (TimeSinceCreation >= 0.5)
            {
                Pos.Y = 7000;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            StarAnimation.Draw(sb, Pos, 0.7f, Color.White,SpriteEffects.None,SceneMan); ;
        }

    }
}
