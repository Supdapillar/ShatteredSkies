using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class HealingRule : SpawnRule
    {
        //level spawner stuff
        //Endless spawner stuff

        public HealingRule(SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            SpawnId = 3;
        }
        public override bool Spawn(int id, Vector2 Pos)
        {
            if (SpawnId==id)
            {
                SceneMan.LevelSpawner.ERelicList.Add(new Healing(SceneMan));
                return true;
            }
            return false;
        }

        public override bool EndlessSpawn(int id, Vector2 Pos)
        {
            if (id == SpawnId)
            {
                int rand = SceneMan.rand.Next(1, 4);
                for (int i = 0; i < rand; i++)
                {
                    int randEne = SceneMan.rand.Next(0, SceneMan.EndlessSpawner.SpawnRules.Count);
                    SceneMan.EndlessSpawner.SpawnRules[randEne].RelicPool.Add(new Healing(SceneMan));
                }
            }
            return false;
        }
    }
}
