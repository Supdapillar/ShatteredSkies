using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Warp : EnemyRelic
    {
        private double Cooldown;
        public Warp(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(255,0,255,255);
            HealthIncrease = 1.4f;
            EndlessCostIncrease = 2;
            Cooldown = 4;
        }


        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            if (HealthIncrease != 1)
            {
                ene.Health *= HealthIncrease;
                ene.MaxHealth *= HealthIncrease;
                HealthIncrease = 1;
            }
            Cooldown += GT.ElapsedGameTime.TotalSeconds;
            if (Cooldown > 4)
            {
                Cooldown = 4;
            }
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            if (Cooldown == 4)
            {
                Vector2 OldPos = ene.Pos;
                double ClosestPlayer = 0;

                ene.Health += bul.Damage;

                while (true)
                {
                    ene.Pos = OldPos;
                    double RandAngle = SceneMan.rand.NextDouble() * (Math.PI * 2);
                    ene.Delta *= 0;
                    ene.Pos.X += (float)(Math.Cos(RandAngle) * 50);
                    ene.Pos.Y += (float)(Math.Sin(RandAngle) * 50);


                    ClosestPlayer = 9999;
                    foreach (Player play in SceneMan.Players)
                    {
                        double Distance = Helper.GetDistance(Helper.CenterActor(ene.Pos, ene.WidthHeight), Helper.CenterPlayer(play));
                        if (Distance < ClosestPlayer)
                        {
                            ClosestPlayer = Distance;
                        }
                    }

                    if ((ene.Pos.X > 0 && (ene.Pos.X+ene.WidthHeight.X) < 288) && (ene.Pos.Y > 0 && (ene.Pos.Y + ene.WidthHeight.Y) < 162))
                    {
                        if (ClosestPlayer > 30)
                        {
                            int rand = SceneMan.rand.Next(20, 25);
                            for(int i = 0; i< rand; i++)
                            {
                                SceneMan.Particles.Add(new ColoredParticle(new Vector2(ene.Pos.X - (float)(Math.Cos(RandAngle) * (50*SceneMan.rand.NextDouble())), ene.Pos.Y - (float)(Math.Sin(RandAngle) * (50 * SceneMan.rand.NextDouble()))),
                                    new Vector2((float)(SceneMan.rand.NextDouble()-0.5), (float)(SceneMan.rand.NextDouble() - 0.5)), SceneMan, new Color(255, 0, 255), true, 0.5f));
                            }
                            SceneMan.Particles.Add(new WarpParticle(new Vector2(ene.Pos.X - (float)(Math.Cos(RandAngle) * 50), ene.Pos.Y - (float)(Math.Sin(RandAngle) * 50)), SceneMan) {TimeSinceCreation = 1/6f });
                            SceneMan.Particles.Add(new WarpParticle(new Vector2(ene.Pos.X - (float)(Math.Cos(RandAngle) * 35), ene.Pos.Y - (float)(Math.Sin(RandAngle) * 35)), SceneMan) { TimeSinceCreation = 1/12f });
                            SceneMan.Particles.Add(new WarpParticle(new Vector2(ene.Pos.X - (float)(Math.Cos(RandAngle) * 20), ene.Pos.Y - (float)(Math.Sin(RandAngle) * 20)), SceneMan));
                            Cooldown = 0;
                            break;
                        }
                    }
                }
            }
        }

        public override void ModEneDraw(Enemy ene, SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)ene.Pos.X, (int)ene.Pos.Y - 5, (int)((Cooldown/4)*ene.WidthHeight.X), 1), new Rectangle(0, 0, 1, 1), Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }





    }
}
