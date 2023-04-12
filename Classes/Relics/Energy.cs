using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Energy : Relic
    {
        //Local Storage Variables
        public Enemy StoredEnemy;
        public List<Enemy> StoredEnemys = new List<Enemy>();
        //t2
        public bool IsWaitingConnection = false;


        //Global Variables
        public Energy(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyConstructor = true;
            ModifiesEnemyUpdate = true;
            ModifiesEnemyOnHit = true;
            ModifiesEnemyDraw = true;
            //makes sure all premade enemies get the relic
            foreach (Enemy ene in SceneMan.Enemies)
            {
                for (int i = 0; i < ene.LocalRelics.Count; i++)
                {
                    if (ene.LocalRelics[i] is Energy)
                    {
                        break;
                    }
                }
                ene.LocalRelics.Add(new Energy(PowerLevel, SceneMan, ConnectedPlayer, true));
            }
        }
        //for the local constructor
        public Energy(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

        }


        public override void ModEneCons(Enemy ene)
        {
            ene.LocalRelics.Add(new Energy(0, SceneMan, ConnectedPlayer, true));
            for (int i = 0; i < ene.LocalRelics.Count; i++)
            {
                if (ene.LocalRelics[i] is Energy)
                {
                    if (SceneMan.rand.Next(0, 4) == 0)
                    {
                        if (SceneMan.Enemies.Count > 1)
                        {
                            while (ene.LocalRelics[i].StoredEnemy == ene || ene.LocalRelics[i].StoredEnemy is null)
                            {
                                ene.LocalRelics[i].StoredEnemy = SceneMan.Enemies[SceneMan.rand.Next(0, SceneMan.Enemies.Count)];
                            }
                            ene.LocalRelics[i].StoredEnemys.Add(ene.LocalRelics[i].StoredEnemy);
                            //force the stored enemy to store the base enemy back
                            for (int x = 0; x < ene.LocalRelics[i].StoredEnemy.LocalRelics.Count; x++)
                            {
                                if (ene.LocalRelics[i].StoredEnemy.LocalRelics[x] is Energy)
                                {
                                    ene.LocalRelics[i].StoredEnemy.LocalRelics[x].StoredEnemys.Add(ene);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            for (int i = 0; i < ene.LocalRelics.Count; i++)
            {
                if (ene.LocalRelics[i] is Energy)
                {
                    for (int x = 0; x < ene.LocalRelics[i].StoredEnemys.Count; x++)
                    {
                        if (ene.LocalRelics[i].StoredEnemys[x].Health <= 0)
                        {
                            ene.LocalRelics[i].StoredEnemys.Remove(ene.LocalRelics[i].StoredEnemys[x]);
                        }
                    }
                }
            }
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            for (int i = 0; i < ene.LocalRelics.Count; i++)
            {
                if (ene.LocalRelics[i] is Energy)
                {
                    foreach (Enemy Eene in ene.LocalRelics[i].StoredEnemys)
                    {
                        for (int x = 0; x < SceneMan.rand.Next((int)(25f *bul.ProcChance), (int)(50f * bul.ProcChance)); x++)
                        {
                            SceneMan.Particles.Add(new ColoredParticle(new Vector2(Eene.Pos.X + (Eene.WidthHeight.X / 2), Eene.Pos.Y + (Eene.WidthHeight.Y / 2)), new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) * 2, (float)(SceneMan.rand.NextDouble() - 0.5f) * 2), SceneMan, new Color(255, 255, 0), true, 1));
                            SceneMan.Particles.Add(new ColoredParticle(new Vector2(ene.Pos.X + (ene.WidthHeight.X / 2), ene.Pos.Y + (ene.WidthHeight.Y / 2)), new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) * 2, (float)(SceneMan.rand.NextDouble() - 0.5f)*2), SceneMan, new Color(255, 255, 0),true,1));
                        }
                        Eene.Health -= bul.Damage;
                    }
                    //the connection mechanic
                    if (PowerLevel > 1)
                    {
                        if (bul.ShotBy is Player)
                        {
                            if (bul.ShotBy == ConnectedPlayer)
                            {
                                ene.LocalRelics[i].IsWaitingConnection = true;
                                foreach (Enemy ene2 in SceneMan.Enemies)//check to see if another enemy is awaiting connection
                                {
                                    for (int y = 0; y < ene2.LocalRelics.Count; y++)
                                    {
                                        if (ene2.LocalRelics[y] is Energy)
                                        {
                                            if (ene2.LocalRelics[y].IsWaitingConnection && (ene2 != ene))
                                            {
                                                ene.LocalRelics[i].StoredEnemy = ene2;
                                                ene.LocalRelics[i].StoredEnemys.Add(ene.LocalRelics[i].StoredEnemy);
                                                ene.LocalRelics[i].IsWaitingConnection = false;
                                                //force the stored enemy to store the base enemy back
                                                for (int x = 0; x < ene2.LocalRelics.Count; x++)
                                                {
                                                    if (ene2.LocalRelics[x] is Energy)
                                                    {
                                                        ene2.LocalRelics[x].StoredEnemys.Add(ene);
                                                        ene2.LocalRelics[x].IsWaitingConnection = false;
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
            }
        }

        public override void ModEneDraw(Enemy ene, SpriteBatch sb)
        {
            for (int i = 0; i < ene.LocalRelics.Count; i++)
            {
                if (ene.LocalRelics[i] is Energy)
                {
                    if (ene.LocalRelics[i].StoredEnemys != null)
                    {
                        foreach (Enemy Eene in ene.LocalRelics[i].StoredEnemys)
                        {
                            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(ene.Pos.X + (ene.WidthHeight.X / 2)), (int)(ene.Pos.Y + (ene.WidthHeight.Y / 2)), (int)Helper.GetDistance(ene.Pos, Eene.Pos), 1), new Rectangle(0, 0, 1, 1), new Color(0.5f, 0.5f, 0f, 0.5f), (float)Math.Atan2(Eene.Pos.Y + (Eene.WidthHeight.Y / 2) - (ene.Pos.Y + (ene.WidthHeight.Y / 2)), Eene.Pos.X + (Eene.WidthHeight.X / 2) - (ene.Pos.X + (ene.WidthHeight.X / 2))), new Vector2(0, 0), SpriteEffects.None, 0.6f);
                        }
                    }
                }
            }
        }
    }
}