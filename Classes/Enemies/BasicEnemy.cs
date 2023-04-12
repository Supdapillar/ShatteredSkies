using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class BasicEnemy : Enemy
    {

        public Vector2 StartingPos;
        public Vector2 GotoPos;

        public double LeavingTimer = 0;
        private double LeavesAfter = 1000;
        public BasicEnemy(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(8,9);
            Name = "BasicEnemy";
            SprOutline = SceneMan.Textures["BasicEnemyOutline"];
            SprInside = SceneMan.Textures["BasicEnemyInside"];
            Enemy_init();
            StartingPos = PS;
            if (!(StartingPos.X+64<288 && StartingPos.X - 64 > 0))
            {
                if (StartingPos.X + 64 > 288)
                {
                    StartingPos.X = 240;
                }
                else
                {
                    StartingPos.X = 48;
                }
            }
            GotoPos = new Vector2(SceneMan.rand.Next((int)StartingPos.X-64, (int)StartingPos.X+64), StartingPos.Y+4);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            LeavingTimer += GT.ElapsedGameTime.TotalSeconds;

            //AI Stuff
            if (LeavingTimer < LeavesAfter)
            {
                double Angle = Helper.GetRadiansOfTwoPoints(Pos, GotoPos);
                Delta.X += (float)(Math.Cos(Angle) * GT.ElapsedGameTime.TotalSeconds / 1f);
                Delta.Y += (float)(Math.Sin(Angle) * GT.ElapsedGameTime.TotalSeconds / 1f);
                if (Helper.GetDistance(Pos, GotoPos) < 3)
                {
                    GotoPos = new Vector2(SceneMan.rand.Next((int)StartingPos.X - 48, (int)StartingPos.X + 48), SceneMan.rand.Next((int)GotoPos.Y + 8, (int)GotoPos.Y + 10));
                }
            }
            else // Leaving
            {
                if (Pos.X < 288/2)
                {
                    GotoPos = new Vector2(-8,162+9);
                }
                else
                {
                    GotoPos = new Vector2(288+8, 162 + 9);
                }
                double Angle = Helper.GetRadiansOfTwoPoints(Pos, GotoPos);
                Delta.X += (float)(Math.Cos(Angle) * GT.ElapsedGameTime.TotalSeconds / 1.5f);
                Delta.Y += (float)(Math.Sin(Angle) * GT.ElapsedGameTime.TotalSeconds / 2f);
                if ((Pos.X <= -8 || Pos.X >= 288+8) && Pos.Y >= 162+9)
                {
                    Health = 0;
                    int random = SceneMan.rand.Next(200, 250);
                    for(int i = 0; i < random; i++)
                    {
                        SceneMan.Particles.Add(new ColoredParticle(Pos, new Vector2((-Delta.X*4)*(float)SceneMan.rand.NextDouble(), (-Delta.Y * 4) * (float)SceneMan.rand.NextDouble()), SceneMan, Color.Red, true, 1f) { DeleteWhenOutside = false }) ;
                    }
                }
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
                Erel.ModEneUpdate(this,GT);
            }




            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            if (ShotDelay <= 0)
            {
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (float)SceneMan.rand.NextDouble() + 1.5f, Pos.Y + 9), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1),this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
            }

            //add a wee bit of slide
            Delta /= 1.01f;

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

            if (LeavingTimer < LeavesAfter)
            {
                sb.Draw(SprOutline, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SprOutline, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(1f, (float)LeavingTimer % 1, (float)LeavingTimer % 1), 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            if (EnemyRelics.Count > 0)
            { sb.Draw(SprInside, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            { sb.Draw(SprInside, new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
