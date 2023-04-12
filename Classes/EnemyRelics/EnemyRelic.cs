using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EnemyRelic
    {
        //order
        ///  Const
        ///  Update
        ///  OnHit
        ///  OnDeath
        ///  Draw

        public Color Color;

        public SceneManager SceneMan;

        public float HealthIncrease;
        public double EndlessCostIncrease = 1;

        public EnemyRelic(SceneManager sceneman)
        {
        }
        //////////////// Enemy ////////////////
        public virtual void ModEneCons(Enemy ene) {}
        public virtual void ModEneUpdate(Enemy ene, GameTime GT){}
        public virtual void ModEneOnHit(Enemy ene, Bullet bul) { }
        public virtual void ModEneOnDeath(Enemy ene){}
        public virtual void ModEneDraw(Enemy ene, SpriteBatch sb) { }
        ///////////// Enemy Bullet //////////// 
        public virtual void ModEneBulCons(EnemyBullet Ebull) { }
        public virtual void ModEneBulUpdate(EnemyBullet Ebull, GameTime GT) { }
        public virtual void ModEneBulOnDeath(EnemyBullet Ebull) { }
        public virtual void ModEneBulDraw(EnemyBullet Ebull, SpriteBatch sb) { }
    }
}
