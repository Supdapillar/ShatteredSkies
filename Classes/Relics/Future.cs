using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Future : Relic
    {
        //local variables
        public List<FutureClone> Clones = new List<FutureClone>();
        public double[] WarpDelays = new double[2] { 0f, 0f };
        public Future(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesPlayerOnHit = true;
            ModifiesPlayerUpdate = true;
            ModifiesPlayerDraw = true;
            if (PowerLevel > 1)
            {
                ModifiesAllyConstructor = true;
                ModifiesAllyOnHit = true;
                ModifiesAllyUpdate = true;
                ModifiesAllyDraw = true;
            }
            ConnectedPlayer.LocalRelics.Add(new Future(PowerLevel, SceneMan, ConnectedPlayer, true));
            for (int i = 0; i < ConnectedPlayer.LocalRelics.Count; i++)
            {
                if (ConnectedPlayer.LocalRelics[i] is Future)
                {
                    ConnectedPlayer.LocalRelics[i].Clones.Add(new FutureClone(new Vector2(144, 100), SceneMan, false, ConnectedPlayer));
                    if (PowerLevel > 1)//gives the players a past clone as well
                    {
                        ConnectedPlayer.LocalRelics[i].Clones.Add(new FutureClone(new Vector2(144, 100), SceneMan, true, ConnectedPlayer));
                    }
                }
            }

            //makes sure all premade allies get the relic
            if (PowerLevel > 1)
            {
                foreach (Ally al in SceneMan.Allies)
                {
                    al.LocalRelics.Add(new Future(PowerLevel, SceneMan, ConnectedPlayer, true));
                    for (int i = 0; i < al.LocalRelics.Count; i++)
                    {
                        if (al.LocalRelics[i] is Future)
                        {
                            al.LocalRelics[i].Clones.Add(new FutureClone(new Vector2(144, 100), SceneMan, al.WidthHeight, ConnectedPlayer));
                        }
                    }
                }
            }
        }
        //Relic Vars
        private Vector2 OldPos;

        //local constructor
        public Future(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

        }
        public override void ModPlayOnHit(Player play, EnemyBullet Ebull)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Future)
                {
                    if (PowerLevel > 1)
                    {
                        if (play.LocalRelics[i].WarpDelays[0] <= 0f)//future check
                        {
                            OldPos = play.Pos;
                            play.Health += (int)Ebull.Damage;
                            play.Pos = play.LocalRelics[i].Clones[0].Pos;
                            play.LocalRelics[i].Clones[0].Pos = OldPos;
                            play.LocalRelics[i].Clones[0].Delta = new Vector2(0, 0);
                            play.LocalRelics[i].WarpDelays[0] = 10f;
                            foreach (EnemyBullet Ebul in SceneMan.EnemyBullets)
                            {
                                if (Math.Sqrt(Math.Pow(play.Pos.X - Ebul.Pos.X, 2) + Math.Pow(play.Pos.Y - Ebul.Pos.Y, 2)) < 50)
                                {
                                    Ebul.Health = 0;
                                }
                            }
                            SceneMan.Particles.Add(new CryoParticle(new Vector2(play.Pos.X+play.AllCores[play.CurrentShipParts[0]].Width/2 - 25, play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2 - 25), SceneMan));
                            break;
                        }
                        else if (play.LocalRelics[i].WarpDelays[1] <= 0f)//past check
                        {
                            play.LocalRelics[i].Clones[1].PastPositions.Clear();
                            OldPos = play.Pos;
                            play.Health += (int)Ebull.Damage;
                            play.Pos = play.LocalRelics[i].Clones[1].Pos;
                            play.LocalRelics[i].WarpDelays[1] = 10f;
                            foreach (EnemyBullet Ebul in SceneMan.EnemyBullets)
                            {
                                if (Math.Sqrt(Math.Pow(play.Pos.X - Ebul.Pos.X, 2) + Math.Pow(play.Pos.Y - Ebul.Pos.Y, 2)) < 50)
                                {
                                    Ebul.Health = 0;
                                }
                            }
                            SceneMan.Particles.Add(new CryoParticle(new Vector2(play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Width / 2 - 25, play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2 - 25), SceneMan));
                            break;
                        }
                    }
                    else
                    {
                        if (play.LocalRelics[i].WarpDelays[0] <= 0f)//future check
                        {
                            OldPos = play.Pos;
                            play.Health += (int)Ebull.Damage;
                            play.Pos = play.LocalRelics[i].Clones[0].Pos;
                            play.LocalRelics[i].Clones[0].Pos = OldPos;
                            play.LocalRelics[i].Clones[0].Delta = new Vector2(0, 0);
                            play.LocalRelics[i].WarpDelays[0] = 10f;
                            foreach (EnemyBullet Ebul in SceneMan.EnemyBullets)
                            {
                                if (Math.Sqrt(Math.Pow(play.Pos.X - Ebul.Pos.X, 2) + Math.Pow(play.Pos.Y - Ebul.Pos.Y, 2)) < 50)
                                {
                                    Ebul.Health = 0;
                                }
                            }
                            SceneMan.Particles.Add(new CryoParticle(new Vector2(play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Width / 2 - 25, play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2 - 25), SceneMan));
                            break;
                        }
                    }

                    play.LocalRelics[i].Clones[0].Pos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(115, 140));
                    play.LocalRelics[i].Clones[0].Delta = new Vector2(0, 0);
                }
            }
        }
        public override void ModPlayUpdate(Player play, GameTime GT)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Future)
                {
                    foreach (FutureClone clone in play.LocalRelics[i].Clones)
                    {
                        if (clone.IsPast == true)
                        {
                            clone.PastPositions.Insert(0, new Tuple<Vector2, float>(play.Pos, (float)GT.ElapsedGameTime.TotalSeconds));
                            if (play.LocalRelics[i].WarpDelays[1] > 0) // past
                            {
                                play.LocalRelics[i].WarpDelays[1] -= GT.ElapsedGameTime.TotalSeconds;
                            }
                            else
                            {
                                play.LocalRelics[i].WarpDelays[1] = 0;
                            }
                        }
                        else
                        {
                            if (play.LocalRelics[i].WarpDelays[0] > 0) // future
                            {
                                play.LocalRelics[i].WarpDelays[0] -= GT.ElapsedGameTime.TotalSeconds;
                            }
                            else
                            {
                                play.LocalRelics[i].WarpDelays[0] = 0;
                            }
                        }
                        clone.Update(GT);
                    }
                }
            }
        }
        public override void ModPlayDraw(Player play, SpriteBatch sb)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Future)
                {
                    foreach (FutureClone clone in play.LocalRelics[i].Clones)
                    {
                        clone.Draw(sb);
                    }
                    //UI Stuff
                    if (SceneMan.game.Scene == 2)
                    {
                        if (PowerLevel > 1)
                        {
                            //past
                            sb.DrawString(SceneMan.Pico8, play.LocalRelics[i].WarpDelays[0].ToString(), new Vector2(0, 14), Color.Blue); //Endless timer
                            sb.Draw(SceneMan.Textures["FutureUI"], new Rectangle(1, 48, 22, 5), new Rectangle(0, 0, 22, 5), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(2, 49, 20 - ((int)Math.Floor(play.LocalRelics[i].WarpDelays[1] * 2)), 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors2[8], 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(2, 50, 20 - ((int)Math.Floor(play.LocalRelics[i].WarpDelays[1] * 2)), 1), new Rectangle(0, 0, 1, 1), new Color(128, 0, 128, 255), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                        }
                        //future
                        sb.Draw(SceneMan.Textures["FutureUI"], new Rectangle(1, 44, 22, 5), new Rectangle(0, 0, 22, 5), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.02f);
                        sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(2, 45, 20-((int)Math.Floor(play.LocalRelics[i].WarpDelays[0]*2)), 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors1[8], 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                        sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(2, 46, 20-((int)Math.Floor(play.LocalRelics[i].WarpDelays[0]*2)), 1), new Rectangle(0, 0, 1, 1), new Color(0, 128, 128, 255), 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                    }
                }
            }
        }

        //tier 2 stuff
        public override void ModAllyCons(Ally Al)
        {
            //adds future to ally
            if (!Al.LocalRelics.OfType<Future>().Any())
            {
                if (Al.CreatedBy == ConnectedPlayer)
                {
                    Al.LocalRelics.Add(new Future(0, SceneMan, ConnectedPlayer, true));
                    for (int i = 0; i < Al.LocalRelics.Count; i++)
                    {
                        if (Al.LocalRelics[i] is Future)
                        {
                            Al.LocalRelics[i].Clones.Add(new FutureClone(new Vector2(144, 100), SceneMan, Al.WidthHeight, ConnectedPlayer));
                        }
                    }
                }
            }
        }
        public override void ModAllyOnHit(Ally Al, EnemyBullet Ebull)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Future)
                {
                    if (Al.LocalRelics[i].WarpDelays[0] <= 0f)//future check
                    {
                        OldPos = Al.Pos;
                        Al.Health += (int)Ebull.Damage;
                        Al.Pos = Al.LocalRelics[i].Clones[0].Pos;
                        Al.LocalRelics[i].Clones[0].Pos = OldPos;
                        Al.LocalRelics[i].Clones[0].Delta = new Vector2(0, 0);
                        Al.LocalRelics[i].WarpDelays[0] = 20f;
                        foreach(EnemyBullet Ebul in SceneMan.EnemyBullets)
                        {
                            if (Math.Sqrt(Math.Pow(Al.Pos.X - Ebul.Pos.X, 2) + Math.Pow(Al.Pos.Y - Ebul.Pos.Y, 2)) < 25)
                            {
                                Ebul.Health = 0;
                            }
                        }
                        SceneMan.Particles.Add(new CryoParticle(new Vector2(Al.Pos.X+Al.WidthHeight.X/2 - 12.5f, Al.Pos.Y + Al.WidthHeight.Y / 2 - 12.5f), SceneMan)
                        {
                            Width = 25
                        });
                        break;
                    }

                    Al.LocalRelics[i].Clones[0].Pos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(115, 140));
                    Al.LocalRelics[i].Clones[0].Delta = new Vector2(0, 0);
                }
            }
        }
        public override void ModAllyUpdate(Ally Al, GameTime GT)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Future)
                {
                    Al.LocalRelics[i].WarpDelays[1] -= GT.ElapsedGameTime.TotalSeconds;
                    Al.LocalRelics[i].Clones[0].Update(GT);
                }
            }
        }

        public override void ModAllyDraw(Ally Al, SpriteBatch sb)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Future)
                {
                    if (Al.LocalRelics[i].WarpDelays[0] <= 0f)
                    {
                      sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Al.Pos.X, (int)Al.Pos.Y - 4, ((int)Al.Health * 2) + 1, 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors1[8], 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
