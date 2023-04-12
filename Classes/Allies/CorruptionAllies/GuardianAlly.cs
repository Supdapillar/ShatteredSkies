using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class GuardianAlly : Ally
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0;
        private double ChargeTime = 0;

        private bool FullyCharged = false;




        public GuardianAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 130);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(25, 18);
            Health = 40;
            MaxHealth = 40;
            CreatedBy = createdby;
            //Relic Mod Ally Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyConstructor)
                {
                    rel.ModAllyCons(this);
                }
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            if (ChargeTime < 10 && !FullyCharged)
            {
                ChargeTime += GT.ElapsedGameTime.TotalSeconds * 0.70;
            }
            else
            {
                ChargeTime -= GT.ElapsedGameTime.TotalSeconds * 0.70;
                FullyCharged = true;
                if (ChargeTime <= 0)
                {
                    FullyCharged = false;
                }
            }
            if (GoLeft & Pos.X <= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                if (GotoPos.X > 288)
                {
                    GotoPos.X = 278;
                }
                GotoPos.Y = SceneMan.rand.Next(130, 140);
            }
            else if (!GoLeft & Pos.X >= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                if (GotoPos.X < 0)
                {
                    GotoPos.X = 10;
                }
                GotoPos.Y = SceneMan.rand.Next(130, 140);
            }

            if (Pos.X < GotoPos.X & Delta.X < (1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.X > GotoPos.X & Delta.X > (-1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }

            if (ShotDelay <= 0)
            {
                //without charge
                if (!FullyCharged)
                {
                    if (ShotDirection == 0)
                    {
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 11, Pos.Y -4), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                        ShotDirection = 1;
                        ShotDelay = 0.5;
                    }
                    else if (ShotDirection == 1)
                    {
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 2, Pos.Y + 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 20, Pos.Y + 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                        ShotDirection = 2;
                        ShotDelay = 0.25;
                    }
                    else
                    {
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 2, Pos.Y + 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 20, Pos.Y + 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
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
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X - 5 - (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 27 + (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 11, Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            ShotDirection = 1;
                            ShotDelay = 0.5;
                            break;
                        case 1:
                            for (int i = 0; i < (int)Pos.X / 18 + 18; i++)
                            {
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X - 11 - (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 33 + (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 2, Pos.Y + 1), new Vector2(0, -1), SceneMan, this)); //Bullets
                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 20, Pos.Y + 1), new Vector2(0, -1), SceneMan, this)); //Bullets
                            ShotDirection = 2;
                            ShotDelay = 0.5;
                            break;
                        case 2:
                            for (int i = 0; i < (int)Pos.X / 18 + 18; i++)
                            {
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X - 17 - (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                            for (int i = 0; i < (int)(288 - Pos.X) / 18 + 18; i++)
                            {
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 39 + (i * 18), Pos.Y - 4), new Vector2(0, -1), SceneMan, this)); //Bullets
                            }
                                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 2, Pos.Y + 1), new Vector2(0, -1), SceneMan, this)); //Bullets
                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 20, Pos.Y + 1), new Vector2(0, -1), SceneMan, this)); //Bullets
                            ShotDirection = 0;
                            ShotDelay = 0.5;
                            break;
                    }
                }
            }

            //add a wee bit of slide
            Delta /= 1;
            Delta /= 1;

            //Relic Mod Ally Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyUpdate)
                {
                    rel.ModAllyUpdate(this, GT);
                }
            }

            //collision with bullets
            CheckCollision(WidthHeight);
        }
        public override void Draw(SpriteBatch sb)
        {
            //Relic Mod Ally Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyDraw)
                {
                    rel.ModAllyDraw(this, sb);
                }
            }
            sb.Draw(SceneMan.Textures["GuardianOutline"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["GuardianInside"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y-3, ((int)Health/2)+1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //Charge bar
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 14, (int)((ChargeTime - 0.5) / 1.25), 1), new Rectangle(0, 0, 1, 1), Color.Cyan, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 15, (int)((ChargeTime - 0.5) / 1.25), 1), new Rectangle(0, 0, 1, 1), Color.DarkCyan, 0f, new Vector2(0, 0), SpriteEffects.None, 0.25f);
            if (FullyCharged)
            {
                for (int i = 0; i < (int)Pos.X / 12; i++)// Left //
                {
                    if (i == 0)//makes the left endpiece show
                    {
                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X - 17, (int)Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                    }
                    //The rest of the wall
                    sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X - 29 - (i * 12), (int)Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                }
                for (int i = 0; i < (int)(288 - Pos.X) / 12; i++)// Right //
                {
                    if (i == 0)//makes the Right endpiece show
                    {
                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X + 26, (int)Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                    }
                    //The rest of the wall
                    sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Pos.X + 42 + (i * 12), (int)Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                }
            }
        }
    }
}
