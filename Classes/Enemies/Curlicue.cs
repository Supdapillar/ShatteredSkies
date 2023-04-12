using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Curlicue : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0; //which way the next bullet needs to travel

        public Curlicue(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            GotoPos = new Vector2(SceneMan.rand.Next(32, 256), 1000);
            WidthHeight = new Vector2(15, 15);
            Health = 7;
            MaxHealth = 7;
            Name = "Curlicue";
            SprOutline = SceneMan.Textures["CurlicueOutline"];
            SprInside = SceneMan.Textures["CurlicueInside"];
            Size = 1;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
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
            //Enemy relic Update
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneUpdate(this, GT);
            }
            //Enemy relic Update
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneUpdate(this, GT);
            }

            // ai shet dont work 2 good rn, fix later
            if (Math.Sqrt(Math.Pow(Pos.X - GotoPos.X, 2) + Math.Pow(GotoPos.Y - GotoPos.Y, 2)) < 45)// random
            {
                if (Pos.Y !> 162) GotoPos = new Vector2(SceneMan.rand.Next(64, 224), 1000);
            }
            if (Pos.X < GotoPos.X & Delta.X < 1.5) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            else if (Pos.X > GotoPos.X & Delta.X > -1.5) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < 0.125) // move to the down lmao
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / 8;
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > -0.125)
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }

            if (Pos.Y > 162)
            {
                GotoPos = new Vector2(SceneMan.rand.Next(64, 224), -10);
            }
            if (Pos.Y < 0)
            {
                GotoPos = new Vector2(SceneMan.rand.Next(64, 224), 1000);
            }
            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            if (ShotDelay <= 0)
            {
                ShotDirection += 1;
                switch (ShotDirection)
                {
                    case 0:// up
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 6f, Pos.Y - 3), new Vector2(0, -0.5f),this, SceneMan)); //Bullets
                        break;
                    case 1://up right
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 12f, Pos.Y ), new Vector2(0.5f, -0.5f), this, SceneMan)); //Bullets
                        break;
                    case 2:// right
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 15f, Pos.Y + 6), new Vector2(0.5f, 0), this, SceneMan)); //Bullets
                        break;
                    case 3://right down
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 12f, Pos.Y + 12), new Vector2(0.5f, 0.5f), this, SceneMan)); //Bullets
                        break;
                    case 4://down
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 6f, Pos.Y + 15), new Vector2(0, 0.5f), this, SceneMan)); //Bullets
                        break;
                    case 5://down left
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X, Pos.Y + 12), new Vector2(-0.5f, 0.5f), this, SceneMan)); //Bullets
                        break;
                    case 6://left
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + -3, Pos.Y + 6), new Vector2(-0.5f, 0), this, SceneMan)); //Bullets
                        break;
                    case 7://left up
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X, Pos.Y), new Vector2(-0.5f, -0.5f), this, SceneMan)); //Bullets
                        ShotDirection = -1;
                        break;
                }

                ShotDelay = Health / 10 - 0.1;
            }

            //add a wee bit of slide
            Delta.X /= 1.00f;
            Delta.Y /= 1.00f;

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
            sb.Draw(SceneMan.Textures["CurlicueOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["CurlicueInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["CurlicueInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            RenderHealth(sb);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
