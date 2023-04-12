using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class LevelSpawner
    {
        public SceneManager SceneMan;
        public List<SpawnRule> SpawnRules;
        public List<SpawnRule> RelicSpawnRules;

        public List<EnemyRelic> ERelicList = new List<EnemyRelic>();

        //Defualt with or without looping
        public LevelSpawner(SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            SpawnRules = new List<SpawnRule>()
            {
                new BasicEnemyRule(Sceneman),
                new BouncerRule(Sceneman),
                new CurlicueRule(Sceneman),
                new ExploderRule(Sceneman),
                new FlamethrowerEnemyRule(Sceneman),
                new GuardianRule(Sceneman),
                new JinxRule(Sceneman),
                new KamikazeRule(Sceneman),
                new LazermancerRule(Sceneman),
                new MeteorologistRule(Sceneman)
            };
            RelicSpawnRules = new List<SpawnRule>()
            {
                new WarpRule(Sceneman),
            };
        }
        public void Update(GameTime GT)
        {
            int CurrentLevel = SceneMan.CurrentLevel;
            List<Level> Levels = SceneMan.Levels;

            CheckForEnemies(CurrentLevel, Levels);
            while (SceneMan.Enemies.Count < 1)
            {
                SceneMan.GameTime -= 1;
                CheckForEnemies(CurrentLevel, Levels);
            }
        }

        private void CheckForEnemies(int CurrentLevel, List<Level> Levels)
        {
            //Level stuff
            for (int w = 0; w < Levels[CurrentLevel].Layers[0].width; w++)
            {
                foreach (Layer lay in Levels[CurrentLevel].Layers)
                {
                    int TileId = lay.data[w + (int)Math.Floor(SceneMan.GameTime) * lay.width];
                    int TileIdOffset = Levels[CurrentLevel].TileDatas.First(r => r.source == lay.name + ".tsx").firstgid;
                    switch (lay.name)
                    {
                        case "Enemies":
                            foreach (SpawnRule rule in SpawnRules)
                            {
                                if (rule.Spawn(TileId - (TileIdOffset - 1), new Vector2(288 / lay.width * w + 144 / lay.width - 4, SceneMan.rand.Next(0, 0))))
                                {
                                    lay.data[w + (int)Math.Floor(SceneMan.GameTime) * lay.width] = 0;
                                    SceneMan.Enemies[^1].EnemyRelics.AddRange(ERelicList);
                                    ERelicList.Clear();
                                }
                            }
                            break;
                        case "Relics":
                            foreach (SpawnRule rule in RelicSpawnRules)
                            {
                                rule.Spawn(TileId - (TileIdOffset - 1), new Vector2());
                                lay.data[w + (int)Math.Floor(SceneMan.GameTime) * lay.width] = 0;
                            }
                            break;
                    }
                }
            }
        }
    }
}
