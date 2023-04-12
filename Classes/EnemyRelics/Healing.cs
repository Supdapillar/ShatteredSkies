using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Healing : EnemyRelic
    {
        private List<Enemy> HealingEnemies = new List<Enemy>();
        public Healing(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(0,200,0,255);
            HealthIncrease = 1.4f;
            EndlessCostIncrease = 2.3;
        }

        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            if (HealthIncrease != 1)
            {
                ene.Health *= HealthIncrease;
                ene.MaxHealth *= HealthIncrease;
                HealthIncrease = 1;
            }
            //adds enemies 2 heal
            if (HealingEnemies.Count < 3)
            {
                foreach (Enemy ene2 in SceneMan.Enemies)
                {
                    if (ene2 != ene)
                    {
                        if (HealingEnemies.Count < 3)
                        {
                            if (Helper.GetDistance(new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2), new Vector2(ene2.Pos.X + ene2.WidthHeight.X / 2, ene2.Pos.Y + ene2.WidthHeight.Y / 2)) < 75)
                            {
                                HealingEnemies.Add(ene2);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            //removes enemise out of range and heals  
            for(int i = 0; i < HealingEnemies.Count; i++)
            {
                //heal
                if (HealingEnemies[i].Health + (float)GT.ElapsedGameTime.TotalSeconds * (ene.Size + 1) < HealingEnemies[i].MaxHealth)
                {
                    if (SceneMan.rand.Next(0, 21) ==0)
                    {
                        SceneMan.Particles.Add(new HealingParticle(new Vector2(SceneMan.rand.Next((int)(HealingEnemies[i].Pos.X), (int)(HealingEnemies[i].Pos.X+ HealingEnemies[i].WidthHeight.X)), SceneMan.rand.Next((int)(HealingEnemies[i].Pos.Y), (int)(HealingEnemies[i].Pos.Y + HealingEnemies[i].WidthHeight.Y))), Color, SceneMan));
                    }
                    HealingEnemies[i].Health += (float)GT.ElapsedGameTime.TotalSeconds * (ene.Size+1);
                }
                else
                {
                    HealingEnemies[i].Health = HealingEnemies[i].MaxHealth;
                }

                //remove
                if ((!(Helper.GetDistance(new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2), new Vector2(HealingEnemies[i].Pos.X + HealingEnemies[i].WidthHeight.X / 2, HealingEnemies[i].Pos.Y + HealingEnemies[i].WidthHeight.Y / 2)) < 75) )|| !SceneMan.Enemies.Contains(HealingEnemies[i]))
                {
                    HealingEnemies.Remove(HealingEnemies[i]);
                }
            }
        }
        
        public override void ModEneDraw(Enemy ene, SpriteBatch sb)
        {
            foreach (Enemy ene2 in HealingEnemies)
            {
                MonoGame.Extended.ShapeExtensions.DrawLine(sb, new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2), new Vector2(ene2.Pos.X + ene2.WidthHeight.X / 2, ene2.Pos.Y + ene2.WidthHeight.Y / 2), Color, 1, 0.6f);
            }
        }
    }
}
