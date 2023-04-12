using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShatteredSkies.Classes
{
    public class EssenceOrb : Bullet
    {
        private readonly Animation EssenceAnimation;
        private readonly Animation TimerAnimation;

        private readonly string EnemyName;

        public EssenceOrb(int subtype, Vector2 pos, SceneManager sceneman,dynamic shotby, string enemyName) : base(subtype, pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ProcChance = 0;
            EnemyName = enemyName.ToUpper();
            Health = 999;
            Damage = 0;

            ShotBy = shotby;

            WidthHeight = new Vector2(10, 11);
            Delta = new Vector2(0, 0.5f);

            EssenceAnimation = new Animation(SceneMan.Textures["EssenceOrb"], SceneMan.rand.Next(10, 15), 10, true);
            TimerAnimation = new Animation(SceneMan.Textures["EssenceTimer"], SceneMan.rand.Next(10, 15), 11, true);

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            //Collision With The player
            if (ShotBy.State.Triggers.Right <= 0.25f || EnemyName == "TIME+")
            {
                foreach (Player play in SceneMan.Players)
                {
                    if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)play.Pos.X, (int)play.Pos.Y, play.AllCores[play.CurrentShipParts[0]].Width, play.AllCores[play.CurrentShipParts[0]].Height))
                    {
                        Health = 0;
                        for (int i = 0; i < 33; i++)
                        {
                            SceneMan.Particles.Add(new ColoredParticle
                            (
                            new Vector2(Pos.X + WidthHeight.X / 2 - 1, Pos.Y + WidthHeight.Y / 2),
                            new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f), (float)(SceneMan.rand.NextDouble() - 0.5f)),
                            SceneMan,
                            SceneMan.RelicsColors1[9],
                            true,
                            1.5f

                            ));
                        }
                        for (int i = 0; i < play.LocalRelics.Count; i++)
                        {
                            if (play.LocalRelics[i] is Essence)
                            {
                                switch (EnemyName.ToUpper())
                                {
                                    case "BOUNCER":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "CURLICUE":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "EXPLODER":
                                        play.LocalRelics[i].MaxTime = 100;
                                        play.LocalRelics[i].Time = 100;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "GUARDIAN":
                                        play.LocalRelics[i].MaxTime = 10;
                                        play.LocalRelics[i].Time = 10;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        play.LocalRelics[i].ShotDirection = 0;
                                        play.LocalRelics[i].ChargeTime = 0;
                                        break;
                                    case "KAMIKAZE":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "METEOROLOGIST":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        play.LocalRelics[i].ChargeTime = 0;
                                        break;
                                    case "RETALIATOR":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "SCANNER":
                                        play.LocalRelics[i].MaxTime = 15;
                                        play.LocalRelics[i].Time = 15;
                                        play.LocalRelics[i].CurrentPower = EnemyName;
                                        break;
                                    case "TIME+":
                                        if (play.LocalRelics[i].Time + 5 > play.LocalRelics[i].MaxTime)
                                        {
                                            play.LocalRelics[i].Time = play.LocalRelics[i].MaxTime;
                                        }
                                        else
                                        {
                                            play.LocalRelics[i].Time += 5;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //Collision With Ally
            foreach (Ally Al in SceneMan.Allies)
            {
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)Al.Pos.X, (int)Al.Pos.Y, (int)Al.WidthHeight.X, (int)Al.WidthHeight.Y))
                {
                    Health = 0;
                    for (int i = 0; i < 33; i++)
                    {
                        SceneMan.Particles.Add(new ColoredParticle
                        (
                        new Vector2(Pos.X + WidthHeight.X / 2 - 1, Pos.Y + WidthHeight.Y / 2),
                        new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f), (float)(SceneMan.rand.NextDouble() - 0.5f)),
                        SceneMan,
                        SceneMan.RelicsColors1[9],
                        true,
                        1.5f

                        ));
                    }
                    for (int i = 0; i < Al.LocalRelics.Count; i++)
                    {
                        if (Al.LocalRelics[i] is Essence)
                        {
                            switch (EnemyName.ToUpper())
                            {
                                case "BOUNCER":
                                    Al.LocalRelics[i].MaxTime = 15;
                                    Al.LocalRelics[i].Time = 15;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "CURLICUE":
                                    Al.LocalRelics[i].MaxTime = 15;
                                    Al.LocalRelics[i].Time = 15;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "EXPLODER":
                                    Al.LocalRelics[i].MaxTime = 100;
                                    Al.LocalRelics[i].Time = 100;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "GUARDIAN":
                                    Al.LocalRelics[i].MaxTime = 100;
                                    Al.LocalRelics[i].Time = 100;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    Al.LocalRelics[i].ShotDirection = 0;
                                    Al.LocalRelics[i].ChargeTime = 0;
                                    break;
                                case "KAMIKAZE":
                                    Al.LocalRelics[i].MaxTime = 150;
                                    Al.LocalRelics[i].Time = 150;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "METEOROLOGIST":
                                    Al.LocalRelics[i].MaxTime = 15;
                                    Al.LocalRelics[i].Time = 15;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    Al.LocalRelics[i].ChargeTime = 0;
                                    break;
                                case "RETALIATOR":
                                    Al.LocalRelics[i].MaxTime = 45;
                                    Al.LocalRelics[i].Time = 45;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "SCANNER":
                                    Al.LocalRelics[i].MaxTime = 20;
                                    Al.LocalRelics[i].Time = 20;
                                    Al.LocalRelics[i].CurrentPower = EnemyName;
                                    break;
                                case "TIME+":
                                    if (Al.LocalRelics[i].Time + 5 > Al.LocalRelics[i].MaxTime)
                                    {
                                        Al.LocalRelics[i].Time = Al.LocalRelics[i].MaxTime;
                                    }
                                    else
                                    {
                                        Al.LocalRelics[i].Time += 5;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            EssenceAnimation.Update(GT);
            TimerAnimation.Update(GT);
            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }
            //kool hip particle
            if (SceneMan.rand.Next(0, 4) == 0)
            {
                SceneMan.Particles.Add(new ColoredParticle
                (
                    new Vector2(Pos.X + WidthHeight.X / 2 - 1, Pos.Y + WidthHeight.Y / 2),
                    new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) / 4, (float)((SceneMan.rand.NextDouble() / 2) - 0.5f)),
                    SceneMan,
                    SceneMan.RelicsColors1[9],
                    true,
                    1f

                ));
            }
            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (ShotBy.State.Triggers.Right <= 0.25f || EnemyName == "TIME+")
            {
                if (EnemyName == "TIME+")
                {
                    TimerAnimation.Draw(sb, Pos, 0.3f, SceneMan);
                }
                else
                {
                    EssenceAnimation.Draw(sb, Pos, 0.3f, SceneMan);

                }
                foreach (Player play in SceneMan.Players)
                {
                    if (Helper.GetDistance(Pos, play.Pos) < 50)
                    {
                        sb.DrawString(SceneMan.Pico8, EnemyName, new Vector2(Pos.X - ((EnemyName.Length / 2) * 3), Pos.Y - 6), new Color(255, 0, 128) * (float)((50 - Helper.GetDistance(Pos + WidthHeight, play.Pos)) / 40), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            //Relic Mod Nullet Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletDraw)
                {
                    rel.ModBulDraw(this, sb);
                }
            }
        }
    }
}
