using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class TargetParticle : Particle
    {
        private Animation TargetAnimation;
        public TargetParticle(Vector2 pos ,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            TargetAnimation = new Animation(SceneMan.Textures["TargetParticle"],16,11,false);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)(GT.ElapsedGameTime.TotalSeconds); ;
            TargetAnimation.Update(GT);
            if (TimeSinceCreation >= 0.35)
            {
                Pos.Y = 7000;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            TargetAnimation.Draw(sb, Pos, 0.7f, Color.White,SpriteEffects.None,SceneMan); ;
        }

    }
}
