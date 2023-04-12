using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Inferno : EnemyRelic
    {
        public Inferno(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(255,128,0,255);
            HealthIncrease = 1.5f;
            EndlessCostIncrease = 3;
        }


        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            if (HealthIncrease != 1)
            {
                ene.Health *= HealthIncrease;
                ene.MaxHealth *= HealthIncrease;
                HealthIncrease = 1;
            }
            Vector2 CenteredEnemy = Helper.CenterActor(ene.Pos,ene.WidthHeight);
            SceneMan.EnemyBullets.Add(new EnemyFlame(CenteredEnemy, new Vector2((float)(SceneMan.rand.NextDouble() - 0.5), (float)(SceneMan.rand.NextDouble() - 0.75)), ene, SceneMan));
            SceneMan.EnemyBullets.Add(new EnemyFlame(CenteredEnemy, new Vector2((float)(SceneMan.rand.NextDouble() - 0.5), (float)(SceneMan.rand.NextDouble() - 0.75)), ene, SceneMan));
        }

        public override void ModEneBulUpdate(EnemyBullet Ebull, GameTime GT)
        {
            if (!(Ebull is EnemyFlame))
            {
                Vector2 centerBull = Helper.CenterActor(Ebull.Pos, Ebull.WidthHeight);
                SceneMan.EnemyBullets.Add(new EnemyFlame(centerBull, new Vector2((float)(SceneMan.rand.NextDouble() - 0.5)/2, (float)(SceneMan.rand.NextDouble() - 0.6)), Ebull.ShotBy, SceneMan));
            }
        }
    }
}
