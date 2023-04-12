using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShatteredSkies.Classes
{
    public class RetaliatorAlly : Ally
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0; //which way the next bullet needs to travel


        public RetaliatorAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 130);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(13, 14);
            Health = 7;
            MaxHealth = 7;
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
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            // ai shet dont work 2 good rn, fix later
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
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }

            if (ShotDelay <= 0)
            {
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 5, Pos.Y - 4), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;

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
            sb.Draw(SceneMan.Textures["RetaliatorOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["RetaliatorInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, ((int)Health * 2) + 1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        public override void CheckCollision(Vector2 WH)
        {
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WH.X, (int)WH.Y, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                {
                    Health -= Ebull.Damage;
                    Ebull.Health -= 1;
                    if (ShotDirection == 0)
                    {
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 0, Pos.Y - 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.5f, -1), SceneMan, this)); //Bullets
                        ShotDirection = 1;
                    }
                    else
                    {
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 10, Pos.Y - 1), new Vector2(((float)SceneMan.rand.NextDouble() / 2), -1), SceneMan, this)); //Bullets
                        ShotDirection = 0;
                    }
                    //Relic Mod Ally OnHit
                    foreach (Relic rel in SceneMan.ActiveRelics)
                    {
                        if (rel.ModifiesAllyOnHit)
                        {
                            rel.ModAllyOnHit(this, Ebull);
                        }
                    }
                }
            }
        }
    }
}
