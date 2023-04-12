using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Retaliator : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0; //which way the next bullet needs to travel


        public Retaliator(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(13, 14);
            Health = 7;
            MaxHealth = 7;
            Name = "Retaliator";
            SprOutline = SceneMan.Textures["RetaliatorOutline"];
            SprInside = SceneMan.Textures["RetaliatorInside"];
            Size = 1;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X < GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                GotoPos.Y += SceneMan.rand.Next(1, 3);
            }
            else if (!GoLeft & Pos.X > GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                GotoPos.Y += SceneMan.rand.Next(1, 3);
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

            if (Pos.X < GotoPos.X & Delta.X < 1.5) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            else if (Pos.X > GotoPos.X & Delta.X > -1.5) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < 0.5) // move to the left
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
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 5, Pos.Y + 14), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1),this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
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
            sb.Draw(SceneMan.Textures["RetaliatorOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["RetaliatorInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["RetaliatorInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            RenderHealth(sb);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }

        public override void CheckCollision(Vector2 WH)
        {
            foreach (Bullet bull in SceneMan.Bullets)
            {
                if (bull.Health > 0)
                {
                    if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WH.X, (int)WH.Y, (int)bull.Pos.X, (int)bull.Pos.Y, (int)bull.WidthHeight.X, (int)bull.WidthHeight.Y))
                    {
                        if (AllCollidingBullets.Contains(bull) == false)
                        {
                            Health -= bull.Damage;
                            bull.Health -= 1;
                            AllCollidingBullets.Add(bull);
                            LastHitBy = bull.ShotBy;

                            if (ShotDirection == 0)
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 0, Pos.Y + 11), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.5f, 1), this, SceneMan)); //Bullets
                                ShotDirection = 1;
                            }
                            else
                            {
                                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 10, Pos.Y + 11), new Vector2(((float)SceneMan.rand.NextDouble() / 2), 1), this, SceneMan)); //Bullets
                                ShotDirection = 0;
                            }

                            foreach (KeyValuePair<string, float> Strint in bull.OnHitEffects)
                            {
                                switch (Strint.Key)
                                {
                                    case "Burning":
                                        StatusEffects[0].EffectAmount += Strint.Value;
                                        break;
                                    case "Bleeding":
                                        StatusEffects[1].EffectAmount += Strint.Value;
                                        break;
                                }
                            }
                            //Relic Mod Enemy OnHit
                            foreach (Relic rel in SceneMan.ActiveRelics)
                            {
                                if (rel.ModifiesEnemyOnHit)
                                {
                                    rel.ModEneOnHit(this, bull);
                                }
                            }
                        }
                        if (bull is ShrapnelShot && bull.SubType == 0 && Health + bull.Damage > 0)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Contains.StoredBullets.Add(new ShrapnelShot(1, new Vector2(WidthHeight.X / 2, WidthHeight.Y / 2), SceneMan, bull.ShotBy));
                            }
                        }
                    }
                }
            }
            //Checks if enemy can collider with the bullet agian
            for (int i = 0; i < AllCollidingBullets.Count; i++)
            {
                if (!Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)AllCollidingBullets[i].Pos.X, (int)AllCollidingBullets[i].Pos.Y, (int)AllCollidingBullets[i].WidthHeight.X, (int)AllCollidingBullets[i].WidthHeight.Y))
                {
                    AllCollidingBullets.Remove(AllCollidingBullets[i]);
                }
            }
        }
    }
}
