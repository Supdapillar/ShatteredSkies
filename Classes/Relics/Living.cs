using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Living : Relic
    {
        //Local Storage Variables
        public List<Enemy> FloweredEnemies = new List<Enemy>();

        public Living(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesBulletUpdate = true;
        }
        public override void ModBulUpdate(Bullet bul, GameTime GT)
        {
            //create thorns
            if (bul.ShotBy is Ally)
            {
                if (bul.ShotBy.CreatedBy == ConnectedPlayer)
                {
                    if (bul.ProcChance > 0f & bul.ShotBy.CreatedBy == ConnectedPlayer)
                    {
                        if (SceneMan.rand.Next(0, (int)(10 / bul.ProcChance)) == 0)
                        {
                            SceneMan.Bullets.Add(new LivingThorn(0, new Vector2(SceneMan.rand.Next((int)bul.Pos.X, (int)(bul.Pos.X + bul.WidthHeight.X)), bul.Pos.Y), SceneMan, ConnectedPlayer));
                        }
                        //create the ooga booga flowers
                        if (PowerLevel > 1)
                        {
                            if (bul.Pos.Y > 0)
                            {
                                if (SceneMan.rand.Next(0, (int)(500 / bul.ProcChance)) == 0)
                                {
                                    if (bul.ProcChance > 0f)
                                    {
                                        SceneMan.Bullets.Add(new LivingFlower(0, new Vector2(SceneMan.rand.Next((int)bul.Pos.X, (int)(bul.Pos.X + bul.WidthHeight.X)), bul.Pos.Y), SceneMan, ConnectedPlayer));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
