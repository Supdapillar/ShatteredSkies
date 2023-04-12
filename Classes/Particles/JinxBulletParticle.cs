using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class JinxBulletParticle : Particle
    {
        private Double StartingAngle;
        public JinxBulletParticle(Vector2 pos, SceneManager sceneman, double startingangle) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(0,0);
            StartingAngle = startingangle;
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation > 1) Pos.Y = 600;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["JinxBullet"], new Vector2((int)Pos.X, (int)Pos.Y), new Rectangle(0, 0, 5, 5), new Color(0.5f - TimeSinceCreation/2, 0, 0, 0.5f - TimeSinceCreation/2), (float)((Math.PI * TimeSinceCreation)+ StartingAngle), new Vector2(2.5f, 2.5f), 1f/(((TimeSinceCreation*2) + 1)), SpriteEffects.None, 0.4f);
        }

    }
}
