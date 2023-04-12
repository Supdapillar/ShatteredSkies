using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Plasma : Relic
    {
        private double RandomAngle;
        public Plasma(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            // PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnHit = true;
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            Enemy ShockOrigin = ene;
            bool CanShock = false;
            double ClosestDistance = 50;
            double Distance;

            while (true)
            {
                if (SceneMan.rand.NextDouble() > 0.80f)
                {
                    break;
                }
                SceneMan.Particles.Add(new PlasmaShockParticle(new Vector2(0, 0), ShockOrigin, SceneMan));
                ShockOrigin.Health -= 5;
                //checks for all enemies that are within radius     
                foreach (Enemy ene2 in SceneMan.Enemies)
                {
                    if (ene2 != ShockOrigin)
                    {
                        Distance = Helper.GetDistance(ShockOrigin.Pos, ene2.Pos);
                        if (Distance < 50)
                        {
                            if (Distance < ClosestDistance)
                            {
                                ClosestDistance = Distance;
                                ShockOrigin = ene2;
                                CanShock = true;
                            }
                        }
                    }
                }
                if (!CanShock)
                {
                    break;
                }
            }
        }
    }
}
