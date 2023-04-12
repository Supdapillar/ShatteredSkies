using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class PlasmaParticle : Particle
    {
        private double Angle;
        public PlasmaParticle(Vector2 pos,double angle,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Angle = angle;
        }

        public override void Update(GameTime GT)
        {
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation > 5.5f)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["PlasmaIndicator"], new Vector2((int)Pos.X, (int)Pos.Y), null,
                Color.White*((0.5f - TimeSinceCreation)*4),
                (float)(Angle-((Math.PI/2)+Math.PI)), new Vector2(2f, 12f),1f, SpriteEffects.None, 0f);;
        }

    }
}
