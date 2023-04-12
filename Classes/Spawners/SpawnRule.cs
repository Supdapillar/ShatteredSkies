using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class SpawnRule
    {
        public SceneManager SceneMan;
        
        //level spawner stuff
        public int SpawnId;

        //Basic Data
        public int Size = 0; //0small 1med 2large 3para 4boss

        //Endless spawner stuff
        public double PointCost = 1;
        public bool InPool = false;
        public List<EnemyRelic> RelicPool = new List<EnemyRelic>();



        public virtual bool Spawn(int id,Vector2 Pos)
        {
            return false;
        }

        public virtual bool EndlessSpawn(int id, Vector2 Pos)
        {
            return false;
        }
    }
}
