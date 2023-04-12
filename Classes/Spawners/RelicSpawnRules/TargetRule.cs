using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class TargetRule : SpawnRule
    {
        //level spawner stuff
        //Endless spawner stuff

        public TargetRule(SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            SpawnId = 2;
        }
        public override bool Spawn(int id, Vector2 Pos)
        {
            if (SpawnId==id)
            {
                SceneMan.LevelSpawner.ERelicList.Add(new Target(SceneMan));
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
                    SceneMan.EndlessSpawner.SpawnRules[randEne].RelicPool.Add(new Target(SceneMan));
                }
            }
            return false;
        }
    }
}
