using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Overseer : Enemy
    {

        public Vector2 GotoPos;
        private double GotoAngle;
        public bool LockedInPlace;
        public readonly List<Enemy> Minions = new List<Enemy>();
        private bool OnlyOneType;
        private int RandomType;

        public Overseer(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(14, 14);
            GotoPos = new Vector2(SceneMan.rand.Next(16, 288 - 58), SceneMan.rand.Next(10, 101));
            Health = 200f;
            Name = "Overseer";
            Enemy_init();
            GotoAngle = 0;
            LockedInPlace = false;
            if (SceneMan.rand.Next(0, 7) == 0)
            {
                OnlyOneType = true;
            }
            else
            {
                OnlyOneType = false;
            }
            RandomType = SceneMan.rand.Next(0, 4);
            //Creates minions
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (!(x == 1 && y == 1))
                    {
                        switch (RandomType)
                        {
                            case 0:
                                Minions.Add(new NormalCore(new Vector2(Pos.X + (x - 1) * 14, Pos.Y + (y - 1) * 14), this, y * 3 + x, SceneMan));
                                if (!OnlyOneType)
                                {
                                    RandomType = SceneMan.rand.Next(0, 4);
                                }
                                break;
                            case 1:
                                Minions.Add(new HomingCore(new Vector2(Pos.X + (x - 1) * 14, Pos.Y + (y - 1) * 14), this, y * 3 + x, SceneMan));
                                if (!OnlyOneType)
                                {
                                    RandomType = SceneMan.rand.Next(0, 4);
                                }
                                break;
                            case 2:
                                Minions.Add(new FlameCore(new Vector2(Pos.X + (x - 1) * 14, Pos.Y + (y - 1) * 14), this, y * 3 + x, SceneMan));
                                if (!OnlyOneType)
                                {
                                    RandomType = SceneMan.rand.Next(0, 4);
                                }
                                break;
                            case 3:
                                Minions.Add(new BounceCore(new Vector2(Pos.X + (x - 1) * 14, Pos.Y + (y - 1) * 14), this, y * 3 + x, SceneMan));
                                if (!OnlyOneType)
                                {
                                    RandomType = SceneMan.rand.Next(0, 4);
                                }
                                break;
            
                        }
                    }
                }
            }
            //Minions.Add(new BounceCore(new Vector2(Pos.X-14,Pos.Y-14), this, 0, SceneMan));;
            //Minions.Add(new BounceCore(new Vector2(Pos.X,Pos.Y-14), this, 1, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X+14,Pos.Y-14), this, 2, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X-14,Pos.Y), this, 3, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X+14,Pos.Y), this, 5, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X-14,Pos.Y+14), this, 6, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X,Pos.Y+14), this, 7, SceneMan));
            //Minions.Add(new BounceCore(new Vector2(Pos.X+14,Pos.Y+14), this, 8, SceneMan));
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;

            //Relic Mod Enemy Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyUpdate)
                {
                    rel.ModEneUpdate(this, GT);
                }
            }


            if (Helper.GetDistance(Pos, GotoPos) < 13)
            {
                LockedInPlace = true;
                if (CheckIfMinionsAreLocked())
                {
                    GotoPos = new Vector2(SceneMan.rand.Next(0, 288 - 42), SceneMan.rand.Next(10, 101));
                }
            }
            else
            {
                GotoAngle = Helper.GetRadiansOfTwoPoints(Pos, GotoPos);
                LockedInPlace = false;
                Delta.X = ((float)Math.Cos(GotoAngle) * ((2f / Minions.Count) + 0.25f));
                Delta.Y = ((float)Math.Sin(GotoAngle) * ((2f / Minions.Count)+0.25f));
            }

            //updates all minions
            Health = 0;
            foreach (Enemy min in Minions)
            {
                min.Update(GT);
                Health += min.Health;
            }
            Minions.RemoveAll(I => I.Health <= 0);

            //add a wee bit of slide
            if (LockedInPlace)
            {
                if (Minions.Count > 4)
                {
                    Delta /= 1.05f;
                }
                else
                {
                    Delta /= 1.13f;
                }
            }

            //collision with bullets
            CheckCollision(WidthHeight);
        }
        public override void Draw(SpriteBatch sb)
        {
            //Relic Mod Enemy Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyDraw)
                {
                    rel.ModEneDraw(this, sb);
                }
            }
            //Draws all minions
            foreach (Enemy min in Minions)
            {
                min.Draw(sb);
            }
            sb.Draw(SceneMan.Textures["Overseer"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X - 20, (int)Pos.Y - 20, ((int)Health / 4) + 1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        private bool CheckIfMinionsAreLocked()
        {
            foreach (dynamic dyn in Minions)
            {
                if (dyn != null)
                {
                    if (!dyn.LockedInPlace)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
