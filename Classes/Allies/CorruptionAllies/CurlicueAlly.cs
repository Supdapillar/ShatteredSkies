using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class CurlicueAlly : Ally
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0; //which way the next bullet needs to travel

        public CurlicueAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            SceneMan = Scenemana;
            GotoPos = new Vector2(SceneMan.rand.Next(32, 256), -1000);
            WidthHeight = new Vector2(15, 15);
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
            if (Math.Sqrt(Math.Pow(Pos.X - GotoPos.X, 2) + Math.Pow(GotoPos.Y - GotoPos.Y, 2)) < 45)// random
            {
                if (Pos.Y !> 162) GotoPos = new Vector2(SceneMan.rand.Next(64, 224), -1000);
            }
            if (Pos.X < GotoPos.X & Delta.X < (1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.X > GotoPos.X & Delta.X > (-1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.125f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the down lmao
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(8 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.125f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed))
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(8 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }

            if (Pos.Y > 162)
            {
                GotoPos = new Vector2(SceneMan.rand.Next(64, 224), -1000);
            }
            if (Pos.Y < 0)
            {
                GotoPos = new Vector2(SceneMan.rand.Next(64, 224), 178);
            }


            if (ShotDelay <= 0)
            {
                ShotDirection += 1;
                switch (ShotDirection)
                {
                    case 0:// up
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 6f, Pos.Y - 3), new Vector2(0, -0.5f), SceneMan, this)); //Bullets
                        break;
                    case 1://up right
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 12f, Pos.Y ), new Vector2(0.5f, -0.5f), SceneMan, this)); //Bullets
                        break;
                    case 2:// right
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 15f, Pos.Y + 6), new Vector2(0.5f, 0), SceneMan, this)); //Bullets
                        break;
                    case 3://right down
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 12f, Pos.Y + 12), new Vector2(0.5f, 0.5f), SceneMan, this)); //Bullets
                        break;
                    case 4://down
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 6f, Pos.Y + 15), new Vector2(0, 0.5f), SceneMan, this)); //Bullets
                        break;
                    case 5://down left
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X, Pos.Y + 12), new Vector2(-0.5f, 0.5f), SceneMan, this)); //Bullets
                        break;
                    case 6://left
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + -3, Pos.Y + 6), new Vector2(-0.5f, 0), SceneMan, this)); //Bullets
                        break;
                    case 7://left up
                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X, Pos.Y), new Vector2(-0.5f, -0.5f), SceneMan, this)); //Bullets
                        ShotDirection = -1;
                        break;
                }

                ShotDelay = Health / 10 - 0.1;
            }

            //add a wee bit of slide
            Delta.X /= 1.00f;
            Delta.Y /= 1.00f;

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

            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Delta.X /= 1.03f;
            }
            else if (Pos.X > 288 - 15) // Right Wall
            {
                Delta.X /= 1.03f;
            }
            if (Pos.Y < 0) //Top wall
            {
                Delta.Y /= 1.03f;
            }
            else if (Pos.Y > 162) // Bottom Wall
            {
                Delta.Y /= 1.03f;
            }
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
            sb.Draw(SceneMan.Textures["CurlicueOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["CurlicueInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y-3, ((int)Health*2)+1, 1), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
