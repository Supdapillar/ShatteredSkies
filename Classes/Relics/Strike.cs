using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Strike : Relic
    {
        //Local Relic Variables
        public bool IsLaunched = false;
        public Enemy HitBy;
        public Strike(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnHit = true;
            ModifiesEnemyUpdate = true;
            //makes sure all enemies get relic
            foreach (Enemy ene in SceneMan.Enemies)
            {
                ene.LocalRelics.Add(new Strike(PowerLevel, SceneMan, ConnectedPlayer, true));
            }
        }
        //Local Relic
        public Strike(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            if (SceneMan.rand.NextDouble() >= 0 * bul.ProcChance)
            {
                for (int i = 0; i < ene.LocalRelics.Count; i++)
                {
                    if (ene.LocalRelics[i] is Strike)
                    {
                        ene.LocalRelics[i].IsLaunched = true;

                        double ClosestDistance = 500;
                        double ClosestAngle = (Math.PI * 2) * SceneMan.rand.NextDouble();
                        Vector2 Enemy1Vec = Helper.CenterActor(ene.Pos, ene.WidthHeight);
                        foreach (Enemy Cene in SceneMan.Enemies)
                        {
                            if (Cene != ene)
                            {
                                Vector2 Enemy2Vec = Helper.CenterActor(Cene.Pos, Cene.WidthHeight);

                                double Distance = Helper.GetDistance(Enemy1Vec, Enemy2Vec);
                                if (Distance < ClosestDistance)
                                {
                                    ClosestDistance = Distance;
                                    ClosestAngle = Helper.GetRadiansOfTwoPoints(Enemy1Vec, Enemy2Vec);
                                }
                            }
                        }
                        ene.Delta.X += (float)(Math.Cos(ClosestAngle) * 4);
                        ene.Delta.Y += (float)(Math.Sin(ClosestAngle) * 4);
                    }
                }
            }
        }
        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            for (int i = 0; i < ene.LocalRelics.Count; i++)
            {
                if (ene.LocalRelics[i] is Strike)
                {
                    if (ene.LocalRelics[i].IsLaunched)
                    {
                        SceneMan.Particles.Add(new EnemyStrikeParticle(ene.Pos, SceneMan, SceneMan.RelicsColors1[12], ene));
                        if (ene.Delta.Length() < 3f)
                        {
                            ene.LocalRelics[i].IsLaunched = false;
                        }
                        ////// Collision ///////
                        // Left wall
                        if (ene.Pos.X < 0) 
                        {
                            ene.Health -= Math.Abs(ene.Delta.X/2);
                            ene.Delta.X = 0;
                        }
                        // Right wall
                        if (ene.Pos.X+ene.WidthHeight.X > 288)
                        {
                            ene.Health -= Math.Abs(ene.Delta.X / 2);
                            ene.Delta.X = 0;
                        }
                        // Up wall
                        if (ene.Pos.Y < 0) 
                        {
                            ene.Health -= Math.Abs(ene.Delta.Y / 2);
                            ene.Delta.Y = 0;
                        }
                        // Down wall
                        if (ene.Pos.Y + ene.WidthHeight.Y > 162) 
                        {
                            ene.Health -= Math.Abs(ene.Delta.Y / 2);
                            ene.Delta.Y = 0;
                        }
                        //Other enemy collision
                        foreach (Enemy Cene in SceneMan.Enemies)
                        {
                            if ((Cene != HitBy) && (Cene != ene))
                            {
                                if (Helper.BoxCollision((int)ene.Pos.X, (int)ene.Pos.Y, (int)ene.WidthHeight.X, (int)ene.WidthHeight.Y, (int)Cene.Pos.X, (int)Cene.Pos.Y, (int)Cene.WidthHeight.X, (int)Cene.WidthHeight.Y))
                                {
                                    Vector2 ene1Vec = Helper.CenterActor(ene.Pos, ene.WidthHeight);
                                    Vector2 ene2Vec = Helper.CenterActor(Cene.Pos, Cene.WidthHeight);
                                    double PushAngle = Helper.GetRadiansOfTwoPoints(ene2Vec, ene1Vec);
                                    Cene.Delta.X += (float)(Math.Cos(PushAngle) * ene.Delta.X);
                                    Cene.Delta.Y += (float)(Math.Sin(PushAngle) * ene.Delta.Y);

                                    for (int g = 0; g < Cene.LocalRelics.Count; g++)
                                    {
                                        if (Cene.LocalRelics[g] is Strike)
                                        {
                                            Cene.LocalRelics[g].IsLaunched = true;
                                            Cene.LocalRelics[g].HitBy = ene;
                                        }
                                    }

                                    ene.Health -= Math.Abs(ene.Delta.X / 2);
                                    ene.Health -= Math.Abs(ene.Delta.Y / 2);
                                    ene.Delta *= 0;
                                    ene.LocalRelics[i].IsLaunched = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
