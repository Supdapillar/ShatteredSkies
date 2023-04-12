using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ContaminatorParticle : Particle
    {
        private Color ParticleColor;
        int ParticleSize;
        double Speed;
        public ContaminatorAOE ConAOE;
        public ContaminatorParticle(Vector2 pos,ContaminatorAOE CAOE,SceneManager sceneman) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            ConAOE = CAOE;
            Speed = SceneMan.rand.NextDouble()/8+0.1;
            double Angle = SceneMan.rand.NextDouble()*2*Math.PI;

            Delta.X = (float)(Math.Cos(Angle)* Speed);
            Delta.Y = (float)(Math.Sin(Angle) * Speed);
            ParticleSize = SceneMan.rand.Next(0, 2);


            switch (SceneMan.rand.Next(0, 2))
            {
                case 0:
                    ParticleColor = Color.Red;
                    break;
                case 1:
                    ParticleColor = new Color(128,0,0,255);
                    break;
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation >= 2)
            {
                Pos.Y = 1000;
            }

            Vector2 CenterAOE = Helper.CenterActor(ConAOE.Pos, ConAOE.WidthHeight);


            //Deletion
            if (!SceneMan.EnemyBullets.Contains(ConAOE))
            {
                Pos.Y = 1000;
            }
            if (Helper.GetDistance(new Vector2(Pos.X+1, Pos.Y + 1), CenterAOE)>22.5)
            {
                Pos.Y = 1000;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (ParticleSize == 0)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 1, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X - 1), (int)(Pos.Y), 3, 1), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y - 1), 1, 3), null, ParticleColor, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 1, 1), null, Color.Black, 0f, new Vector2(0, 0), SpriteEffects.None, 0.99f);

            }
        }

    }
}
