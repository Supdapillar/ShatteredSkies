using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class FlamethrowerEnemy : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private bool Shoot = false;
        private Player TargetedPlayer;

        public FlamethrowerEnemy(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(11,10);
            Name = "Flamethrower";
            SprOutline = SceneMan.Textures["FlamethrowerOutline"];
            SprInside = SceneMan.Textures["FlamethrowerInside"];
            Size = 1;
            Health = 8;
            MaxHealth = 8;
            Enemy_init();
            TargetedPlayer = SceneMan.Players[SceneMan.rand.Next(0,SceneMan.Players.Count)];
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            Shoot = false;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X < GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
            }
            else if (!GoLeft & Pos.X > GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
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

            if (Health > 4)
            {
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
            }


            //Low health AI
            if (Health < 5)
            {
                if (Pos.X < GotoPos.X & Delta.X < 1.5) // move to the left
                {
                    Delta.X += (float)GT.ElapsedGameTime.TotalSeconds;
                }
                else if (Pos.X > GotoPos.X & Delta.X > -1.5) // move to the right
                {
                    Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds;
                }
                GotoPos.X = TargetedPlayer.Pos.X;
                if (Delta.Y < 1)
                {
                    Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds;
                }
                if (Pos.Y >= 162 - 64)
                {
                    Pos.Y = 162 - 64;
                    Delta.Y = 0;
                }
            }
            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            foreach(Player play in SceneMan.Players)
            {
                if (Helper.GetDistance(new Vector2(Pos.X + WidthHeight.Y / 2, Pos.Y + WidthHeight.Y / 2),new Vector2(play.Pos.X+play.AllCores[play.CurrentShipParts[0]].Width/2, play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2))<60)
                {
                    if (play.Pos.Y > Pos.Y)
                    {
                        Shoot = true;
                    }
                }
            }
            foreach (Ally al in SceneMan.Players)
            {
                if (Helper.GetDistance(new Vector2(Pos.X + WidthHeight.Y / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(al.Pos.X + al.WidthHeight.X/2, al.Pos.Y + al.WidthHeight.Y/2)) < 60)
                {
                    if (al.Pos.Y > Pos.Y)
                    {
                        Shoot = true;
                    }
                }
            }

            if (Shoot)
            {
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 1, Pos.Y + 10), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 8, Pos.Y + 10), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
            }

            //add a wee bit of slide
            if (Health < 5)
            {
                Delta /= 1.01f;
            }
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
            sb.Draw(SceneMan.Textures["FlamethrowerOutline"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["FlamethrowerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["FlamethrowerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            //sb.Draw(SceneMan.Textures["Enemy1"], new Vector2(Convert.ToSingle(Math.Ceiling(Pos.X)), Convert.ToSingle(Math.Ceiling(Pos.Y))), Color.White);
            RenderHealth(sb);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
