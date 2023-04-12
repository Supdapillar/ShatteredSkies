using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Guardian : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0;
        private double ChargeTime = 0;

        private bool FullyCharged = false;



        public Guardian(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(25, 18);
            Health = 40;
            MaxHealth = 40;
            Name = "Guardian";
            SprOutline = SceneMan.Textures["GuardianOutline"];
            SprInside = SceneMan.Textures["GuardianInside"];
            Size = 2;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (ChargeTime < 10)
            {
                ChargeTime += GT.ElapsedGameTime.TotalSeconds * 0.70;
            }
            else
            {
                ChargeTime = 10;
                FullyCharged = true;
            }
            //Relic Mod Enemy Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyUpdate)
                {
                    rel.ModEneUpdate(this, GT);
                }
            }
            //Enemy relic Update
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneUpdate(this, GT);
            }
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X <= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
            }
            else if (!GoLeft & Pos.X >= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
            }

            if (Pos.X < GotoPos.X & Delta.X < 1.5) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            else if (Pos.X > GotoPos.X & Delta.X > -1.5) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < 0.5) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / 4;
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > -0.5) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / 4;
            }
            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            if (ShotDelay <= 0)
            {
                //without charge
                if (!FullyCharged)
                {
                    if (ShotDirection == 0)
                    {
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 11, Pos.Y + 18), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        ShotDirection = 1;
                        ShotDelay = 0.5;
                    }
                    else if (ShotDirection == 1)
                    {
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 2, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 20, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        ShotDirection = 2;
                        ShotDelay = 0.25;
                    }
                    else
                    {
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 2, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 20, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        ShotDirection = 0;
                        ShotDelay = 0.5;
                    }
                }
                //with charge
                if (FullyCharged)
                {
                    //1
                    switch (ShotDirection)
                    {
                        case 0:
                            for (int i = 0; i < (int)Pos.X / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X - 5 - (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 27 + (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                            SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 11, Pos.Y + 18), new Vector2(0, 1), this, SceneMan)); //Bullets
                            ShotDirection = 1;
                            ShotDelay = 0.5;
                            break;
                        case 1:
                            for (int i = 0; i < (int)Pos.X / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X - 11 - (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 33 + (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                            SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 2, Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 20, Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            ShotDirection = 2;
                            ShotDelay = 0.5;
                            break;
                        case 2:
                            for (int i = 0; i < (int)Pos.X / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X - 17 - (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 39 + (i * 18), Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            }
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 2, Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 20, Pos.Y + 13), new Vector2(0, 1), this, SceneMan)); //Bullets
                            ShotDirection = 0;
                            ShotDelay = 0.5;
                            break;
                    }
                }
            }

            //add a wee bit of slide
            Delta /= 1;
            Delta /= 1;

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
            //Enemy Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            sb.Draw(SprOutline, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SprInside, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SprInside, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            RenderHealth(sb);
            //Charge bar
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X + 9), (int)(Pos.Y + 2), (int)((ChargeTime - 0.5) / 1.25), 1), new Rectangle(0, 0, 1, 1), Color.Cyan, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X + 9), (int)(Pos.Y + 3), (int)((ChargeTime - 0.5) / 1.25), 1), new Rectangle(0, 0, 1, 1), Color.DarkCyan, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
            Color WallColor;
            if (EnemyRelics.Count > 0)
            {
                WallColor = EnemyRelics[0].Color;
            }
            else
            {
                WallColor = Color.Gray;
            }
            if (FullyCharged)
            {
                for (int i = 0; i < (int)Pos.X / 12; i++)// Left //
                {
                    if (i == 0)//makes the left endpiece show
                    {
                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X - 17, (int)Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), WallColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
                    }
                    //The rest of the wall
                    sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X - 29 - (i * 12), (int)Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), WallColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
                }
                for (int i = 0; i < (int)(288 - Pos.X) / 12; i++)// Right //
                {
                    if (i == 0)//makes the Right endpiece show
                    {
                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X + 26, (int)Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), WallColor, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.25f);
                    }
                    //The rest of the wall
                    sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X + 42 + (i * 12), (int)Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), WallColor, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.25f);
                }
            }
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
