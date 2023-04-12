using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class BasicEnemyRule : SpawnRule
    {
        //level spawner stuff
        //Endless spawner stuff

        public BasicEnemyRule(SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            SpawnId = 1;
            InPool = true;
        }
        public override bool Spawn(int id, Vector2 Pos)
        {
            if (SpawnId==id)
            {
                SceneMan.Enemies.Add(new BasicEnemy(Pos,SceneMan));
                return true;
            }
            return false;
        }

        public override bool EndlessSpawn(int id, Vector2 Pos)
        {
            if (SpawnId == id)
            {
                SceneMan.Enemies.Add(new BasicEnemy(Pos, SceneMan));
                if (SceneMan.rand.Next(0, 5) == 0)
                {
                    if (RelicPool.Count > 0)
                    {
                        int randRelic = SceneMan.rand.Next(0, RelicPool.Count);
                        if (SceneMan.EndlessSpawner.EndlessPoints - PointCost * RelicPool[randRelic].EndlessCostIncrease > 0)
                        {
                            SceneMan.Enemies[^1].EnemyRelics.Add(RelicPool[randRelic]);
                            SceneMan.EndlessSpawner.EndlessPoints -= PointCost * RelicPool[randRelic].EndlessCostIncrease;
                        }
                    }
                }
                else
                {
                    SceneMan.EndlessSpawner.EndlessPoints -= PointCost;
                }
                return true;
            }
            return false;
        }
    }
}
