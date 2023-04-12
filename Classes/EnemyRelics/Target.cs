using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{

    public class Target : EnemyRelic
    {
        //local bullet variables
        public int Retargets = 3;
        public double Cooldown = 0;
        public Target(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(200,200,0,255);
            HealthIncrease = 1.75f;
            EndlessCostIncrease = 2.4;
        }

        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            if (HealthIncrease != 1)
            {
                ene.Health *= HealthIncrease;
                ene.MaxHealth *= HealthIncrease;
                HealthIncrease = 1;
            }
        }

        public override void ModEneBulCons(EnemyBullet Ebull)
        {
            Ebull.LocalEnemyRelics.Add(new Target(SceneMan));
        }

        public override void ModEneBulUpdate(EnemyBullet Ebull, GameTime GT)
        {
            for (int i = 0; i < Ebull.LocalEnemyRelics.Count; i++)
            {
                if (Ebull.LocalEnemyRelics[i] is Target)
                {
                    //delete

                    Ebull.LocalEnemyRelics[i].Cooldown -= GT.ElapsedGameTime.TotalSeconds;
                    if (Ebull.LocalEnemyRelics[i].Cooldown <= 0)
                    {
                        if (Ebull.LocalEnemyRelics[i].Retargets <= 0)
                        {
                            Ebull.Health = 0;
                            break;
                        }
                        Ebull.LocalEnemyRelics[i].Retargets -= 1;
                        Ebull.LocalEnemyRelics[i].Cooldown = 1 + SceneMan.rand.NextDouble();
                        Vector2 EbullCenter = Helper.CenterActor(Ebull.Pos, Ebull.WidthHeight);
                        SceneMan.Particles.Add(new TargetParticle(new Vector2(EbullCenter.X-5, EbullCenter.Y-6),SceneMan));

                        //Targeting
                        Player randPlay = SceneMan.Players[SceneMan.rand.Next(0, SceneMan.Players.Count)];
                        double Angle = Helper.GetRadiansOfTwoPoints(EbullCenter,Helper.CenterPlayer(randPlay));

                        int rand = SceneMan.rand.Next(20, 25);
                        for (int x = 0; x < rand; x++)
                        {
                            SceneMan.Particles.Add(new ColoredParticle(new Vector2(EbullCenter.X, EbullCenter.Y),
                                new Vector2(-(float)(Math.Cos(Angle)+(SceneMan.rand.NextDouble()-0.5)/2), -(float)(Math.Sin(Angle) + (SceneMan.rand.NextDouble() - 0.5) / 2)), SceneMan, new Color(255, 255, 0), true, 0.5f));
                        }

                        Ebull.Delta.X = (float)Math.Cos(Angle);
                        Ebull.Delta.Y = (float)Math.Sin(Angle);
                    }
                }
            }
        }
    }
}
