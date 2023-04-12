using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Relic
    {
        //order
        ///  Const
        ///  Update
        ///  OnHit
        ///  OnDeath
        ///  Draw
        //Bullet
        public bool ModifiesBulletConstructor = false;
        public bool ModifiesBulletUpdate = false;
        public bool ModifiesBulletOnDeath = false;
        public bool ModifiesBulletDraw = false;
        //Enemy
        public bool ModifiesEnemyConstructor = false;
        public bool ModifiesEnemyUpdate = false;
        public bool ModifiesEnemyOnHit = false;
        public bool ModifiesEnemyOnDeath = false;
        public bool ModifiesEnemyDraw = false;
        //Enemy Bullet
        public bool ModifiesEnemyBulletUpdate = false;
        //Ally
        public bool ModifiesAllyConstructor = false;
        public bool ModifiesAllyUpdate = false;
        public bool ModifiesAllyOnHit = false;
        public bool ModifiesAllyOnDeath = false;
        public bool ModifiesAllyDraw = false;
        //Player
        public bool ModifiesPlayerUpdate = false;
        public bool ModifiesPlayerOnHit = false;
        public bool ModifiesPlayerDraw = false;
        //Scenemanager
        public bool ModifiesDraw = false;

        public Player ConnectedPlayer;

        public int PowerLevel;

        public SceneManager SceneMan;

        public Relic(int power, SceneManager sceneman, Player play)
        {
        }
        //////////////// Bullet ///////////////
        public virtual void ModBulCons(Bullet bul){}
        public virtual void ModBulUpdate(Bullet bul, GameTime GT) { }
        public virtual void ModBulOnDeath(Bullet bul) { }
        public virtual void ModBulDraw(Bullet bul, SpriteBatch sb) { }
        //////////////// Enemy ////////////////
        public virtual void ModEneCons(Enemy ene) {}
        public virtual void ModEneUpdate(Enemy ene, GameTime GT){}
        public virtual void ModEneOnHit(Enemy ene, Bullet bul) { }
        public virtual void ModEneOnDeath(Enemy ene){}
        public virtual void ModEneDraw(Enemy ene, SpriteBatch sb) { }
        ///////////// Enemy Bullet //////////// 
        public virtual void ModEneBulUpdate(EnemyBullet ebull, GameTime GT){}
        public virtual void ModEneBulDraw(EnemyBullet ebull, SpriteBatch sb) { }
        /////////////// Allies ///////////////
        public virtual void ModAllyCons(Ally Al) { }
        public virtual void ModAllyUpdate(Ally Al, GameTime GT) { }
        public virtual void ModAllyOnHit(Ally Al, EnemyBullet Ebull) { }
        public virtual void ModAllyOnDeath(Ally Al) { }
        public virtual void ModAllyDraw(Ally Al, SpriteBatch sb) { }
        /////////////// Player ////////////////
        public virtual void ModPlayUpdate(Player play, GameTime GT) { }
        public virtual void ModPlayOnHit(Player play, EnemyBullet Ebull) { }
        public virtual void ModPlayDraw(Player play, SpriteBatch sb) { }
        ////////// Scene Manager //////////////
        public virtual void ModDraw(SpriteBatch sb) { }
    }
}
