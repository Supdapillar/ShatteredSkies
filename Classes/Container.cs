using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Container
    {
        //This class will be a data type that holds the info of what an enemy is holding

        public List<Enemy> StoredEnemies = new List<Enemy>();
        public List<EnemyBullet> StoredEnemyBullets = new List<EnemyBullet>();
        public List<Bullet> StoredBullets = new List<Bullet>();
        public Container()
        {
        }
    }
}
