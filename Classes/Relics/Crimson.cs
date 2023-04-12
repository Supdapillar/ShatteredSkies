using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Crimson : Relic
    {
        public Crimson(int power,SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesBulletConstructor = true;

            if (PowerLevel >= 2)
            {
                ModifiesEnemyOnDeath = true;
            }
        }
        public override void ModBulCons(Bullet bul)
        {
            if (bul.ShotBy is Ally) /////// Player Bullets
            {
                if (bul.ShotBy.CreatedBy == ConnectedPlayer) 
                {
                    if (PowerLevel >= 2)
                    {
                        if (bul.OnHitEffects.ContainsKey("Bleeding") == false)
                        {
                            bul.OnHitEffects.Add("Bleeding", bul.ProcChance);
                        }
                    }
                    else
                    {
                        if (!(bul is BloodBullet))
                        {
                            if (bul.OnHitEffects.ContainsKey("Bleeding") == false)
                            {
                                bul.OnHitEffects.Add("Bleeding", bul.ProcChance);
                            }
                        }
                    }
                }
            }

        }
        public override void ModEneOnDeath(Enemy ene)
        {
            if (ene.StatusEffects[1].EffectAmount >= 3)
            {
                if (ene.LastHitBy is Ally)
                {
                    if (ene.LastHitBy.CreatedBy == ConnectedPlayer)
                    {
                        SceneMan.Bullets.Add(new BloodBullet(0, new Vector2(ene.Pos.X + (ene.WidthHeight.X / 2), ene.Pos.Y + (ene.WidthHeight.Y / 2)), SceneMan, ConnectedPlayer));//up
                    }
                }
            }
        }
    }
}
