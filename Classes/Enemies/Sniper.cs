using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Sniper : Enemy
    {

        public Vector2 GotoPos;
        private double RelocationTimer;
        private double Angle;
        private double ShootingAngle;
        private readonly Player TargetedPlayer;
        private int NumOfShots;

        public Sniper(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            RelocationTimer = 5;
            GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(10, 71));
            WidthHeight = new Vector2(9,15);
            Health = 5;
            MaxHealth = 5;
            Name = "Sniper";
            SprOutline = SceneMan.Textures["SniperOutline"];
            SprInside = SceneMan.Textures["SniperInside"];
            Size = 1;
            Enemy_init();
            TargetedPlayer = SceneMan.Players[SceneMan.rand.Next(0, SceneMan.Players.Count)];
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
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
            //Movement
            Angle = Helper.GetRadiansOfTwoPoints(Pos, GotoPos);
            if (Helper.GetDistance(Pos,GotoPos)>15)
            {
                if (Delta.X < 1 && Delta.X > -1)
                {
                    Delta.X += (float)(Math.Cos(Angle) * (GT.ElapsedGameTime.TotalSeconds/2));
                }

                if (Delta.Y < 0.5 && Delta.Y > -0.5)
                {
                    Delta.Y += (float)(Math.Sin(Angle) * (GT.ElapsedGameTime.TotalSeconds/4));
                }
            }
            else
            {
                RelocationTimer -= (float)GT.ElapsedGameTime.TotalSeconds;
            }
            //random location
            if (RelocationTimer <= 0)
            {
                RelocationTimer = 3+(SceneMan.rand.NextDouble());
                while(Math.Abs(Pos.Y - GotoPos.Y) < 10)
                {
                    GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(10, 71));
                }
            }
            //End of movement

            //Shooting
            if (Helper.GetDistance(Pos, GotoPos) < 15)
            {
                ShootingAngle = Helper.GetRadiansOfTwoPoints(Pos, TargetedPlayer.Pos);
                if (Math.Abs(Delta.X) < 0.2f)
                {
                    ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
                    if (ShotDelay <= 0)
                    {
                        if (NumOfShots <= 3)
                        {
                            SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 3, Pos.Y + 15), new Vector2((float)Math.Cos(ShootingAngle) * 2f, (float)Math.Sin(ShootingAngle) * 2f), this, SceneMan)); //Bullets
                            NumOfShots += 1;
                            ShotDelay = 0.2f;
                        }
                        else
                        {
                            NumOfShots = 0;
                            ShotDelay = 2f;
                        }
                    }
                }
            }



            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Pos.X = 0;
                Delta.X = 0;
                Delta.Y = 0;
            }
            else if (Pos.X > 288 - WidthHeight.X) // Right Wall
            {
                Pos.X = 288 - WidthHeight.X;
                Delta.X = 0;
                Delta.Y = 0;
            }

            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            //add a wee bit of slide
            if (Helper.GetDistance(Pos, GotoPos) < 15)
            {
                Delta /= 1.04f;
            }
            Delta /= 1.00f;

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
            //Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            //Enemy Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["SniperOutline"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            sb.Draw(SceneMan.Textures["SniperGunOutline"], new Vector2((int)Pos.X + 4f, (int)Pos.Y + 8f), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(4f, 8f), 1f, SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {sb.Draw(SceneMan.Textures["SniperInside"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(SceneMan.Textures["SniperGunInside"], new Vector2((int)Pos.X + 4f, (int)Pos.Y + 8f), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(4f, 8f), 0f, SpriteEffects.None, 0.33f);
            }
            else
            {sb.Draw(SceneMan.Textures["SniperInside"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(SceneMan.Textures["SniperGunInside"], new Vector2((int)Pos.X + 4f, (int)Pos.Y + 8f), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(4f, 8f), 0f, SpriteEffects.None, 0.33f);
            }
            //sb.Draw(SceneMan.Textures["Enemy1"], new Vector2(Convert.ToSingle((Pos.X)), Convert.ToSingle((Pos.Y))), Color.White);
            RenderHealth(sb);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
