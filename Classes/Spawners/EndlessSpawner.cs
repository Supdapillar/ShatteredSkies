using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EndlessSpawner
    {
        public SceneManager SceneMan;
        //spawn rules
        public List<SpawnRule> SpawnRules;
        public List<SpawnRule> RelicSpawnRules;
        //endless stuff
        public double EndlessPoints = 0;
        public double EndlessWaveDelay = 0;
        public double SpawnPoolTimer;
        public double EndlessPointSpeed = 0.25;
        public double RelicTimer;


        //Defualt with or without looping
        public EndlessSpawner(SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            SpawnPoolTimer = SceneMan.rand.Next(15, 25);
            RelicTimer = SceneMan.rand.Next(20, 40);
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
                new TargetRule(Sceneman),
                new HealingRule(Sceneman),
                new CurseRule(Sceneman),
                new DefenseRule(Sceneman),
                new InfernoRule(Sceneman),
            };
        }
        public void Update(GameTime GT)
        {
            SceneMan.GameTime += GT.ElapsedGameTime.TotalSeconds;
            EndlessPoints += GT.ElapsedGameTime.TotalSeconds * EndlessPointSpeed;
            EndlessWaveDelay -= GT.ElapsedGameTime.TotalSeconds;
            EndlessPointSpeed += 0.02 * GT.ElapsedGameTime.TotalSeconds;
            SpawnPoolTimer -= GT.ElapsedGameTime.TotalSeconds;
            RelicTimer -= GT.ElapsedGameTime.TotalSeconds;
            List<int> SmallList = new List<int>();
            List<int> MedList = new List<int>();
            List<int> LargeList = new List<int>();
            //spawning
            if (EndlessWaveDelay <= 0)
            {
                //making the spawnpool stuff
                foreach (SpawnRule rule in SpawnRules)
                {
                    if (rule.InPool)
                    {
                        switch (rule.Size)
                        {
                            case 0:
                                SmallList.Add(rule.SpawnId);
                                break;
                            case 1:
                                MedList.Add(rule.SpawnId);
                                break;
                            case 2:
                                LargeList.Add(rule.SpawnId);
                                break;
                        }
                    }
                }

                //spawning
                double CheepestCost = 99999;
                foreach (SpawnRule rule in SpawnRules)
                {
                    if (rule.InPool)
                    {
                        if (rule.PointCost < CheepestCost) { CheepestCost = rule.PointCost; }
                    }
                }

                while (EndlessPoints > CheepestCost)
                {   //Small
                    if (SceneMan.rand.Next(0,5)==0) 
                    {
                        if (SmallList.Count > 0)
                        {
                            int randomid = SceneMan.rand.Next(0, SmallList.Count);
                            foreach (SpawnRule rule in SpawnRules)
                            {
                                if (EndlessPoints - rule.PointCost >= 0)
                                {
                                    rule.EndlessSpawn(SmallList[randomid], new Vector2(SceneMan.rand.Next(0, 288), SceneMan.rand.Next(-10, 5)));
                                }
                            }
                        }
                    }
                    //Medium
                    else if (SceneMan.rand.Next(0, 10) == 0)
                    {
                        if (MedList.Count > 0)
                        {
                            int randomid = SceneMan.rand.Next(0, MedList.Count);
                            foreach (SpawnRule rule in SpawnRules)
                            {
                                if (EndlessPoints - rule.PointCost >= 0)
                                {
                                    if (EndlessPoints - rule.PointCost >= 0)
                                    {
                                        rule.EndlessSpawn(MedList[randomid], new Vector2(SceneMan.rand.Next(0, 288), SceneMan.rand.Next(-10, 5)));
                                    }
                                }
                            }
                        }
                    }
                    //Large
                    else if (SceneMan.rand.Next(0, 25) == 0)
                    {
                        if (LargeList.Count > 0)
                        {
                            int randomid = SceneMan.rand.Next(0, LargeList.Count);
                            foreach (SpawnRule rule in SpawnRules)
                            {
                                if (EndlessPoints - rule.PointCost >= 0)
                                {
                                    if (EndlessPoints - rule.PointCost >= 0)
                                    {
                                        rule.EndlessSpawn(LargeList[randomid], new Vector2(SceneMan.rand.Next(0, 288), SceneMan.rand.Next(-10, 5)));
                                    }
                                }
                            }
                        }
                    }
                }

                EndlessWaveDelay = SceneMan.rand.Next(3, 12);
            }
            //adding enemies 2 the pool
            if (SpawnPoolTimer < 0)
            {
                SpawnPoolTimer += SceneMan.rand.Next(15, 25);
                int RandomEnemy = SceneMan.rand.Next(0, SpawnRules.Count);
                if ((SceneMan.rand.NextDouble() * 5 > SpawnRules[RandomEnemy].Size)&& !SpawnRules[RandomEnemy].InPool)
                {
                    SpawnRules[RandomEnemy].InPool = true;
                }
                else
                {
                    SpawnPoolTimer = 0;
                }
            }
            //adding relics 2 1-3 enemies
            if (RelicTimer < 0)
            {
                RelicTimer += SceneMan.rand.Next(15, 25);
                int randomRelic = SceneMan.rand.Next(0,RelicSpawnRules.Count);
                foreach (SpawnRule Rrule in RelicSpawnRules)
                {
                    Rrule.EndlessSpawn(randomRelic,new Vector2());
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(SceneMan.Pico8, "PTS "+EndlessPoints.ToString(), new Vector2(0, 130), Color.Azure, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            sb.DrawString(SceneMan.Pico8, "SPT "+SpawnPoolTimer.ToString(), new Vector2(0, 136), Color.HotPink, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            sb.DrawString(SceneMan.Pico8, "EWD "+EndlessWaveDelay.ToString(), new Vector2(0, 142), Color.Lime, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            sb.DrawString(SceneMan.Pico8, "EPS " + EndlessPointSpeed.ToString(), new Vector2(0, 148), Color.CornflowerBlue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            sb.DrawString(SceneMan.Pico8, "RT " + RelicTimer.ToString(), new Vector2(0, 154), Color.Orange, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            int EIP = 0;
            foreach (SpawnRule rule in SpawnRules)
            {
                if (rule.InPool)
                {
                    EIP += 1;
                }
            }
            sb.DrawString(SceneMan.Pico8, "EP " + EIP.ToString(), new Vector2(0, 124), Color.Orange, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
        
        
        }
    }
}
