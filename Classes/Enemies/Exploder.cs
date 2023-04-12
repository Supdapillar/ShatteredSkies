using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Exploder : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private double Angle = -Math.PI*2;
        private Player TargetedPlayer;

        public Exploder(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(16, 13);
            Health = 7;
            MaxHealth = 7;
            Name = "Exploder";
            SprOutline = SceneMan.Textures["ExploderOutline"];
            SprInside = SceneMan.Textures["ExploderInside"];
            Size = 1;
            Enemy_init();
            TargetedPlayer = SceneMan.Players[SceneMan.rand.Next(0,SceneMan.Players.Count)];
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            // ai shet dont work 2 good rn, fix later

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

            GotoPos = TargetedPlayer.Pos;
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
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (float)SceneMan.rand.NextDouble() + 1.5f, Pos.Y + 9), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
            }

            //add a wee bit of slide
            Delta.X /= 1;
            Delta.Y /= 1;

            //If in radius to explode
            foreach (Player play in SceneMan.Players)
            {
                if (Math.Sqrt(Math.Pow(Pos.X + SceneMan.Textures["ExploderOutline"].Width / 2 - play.Pos.X, 2) + Math.Pow(Pos.Y + SceneMan.Textures["ExploderOutline"].Height / 2 - play.Pos.Y, 2)) < 45)
                {
                    Health = 0;
                    for (int i = 0; i < 32; i++)
                    {
                        Angle += Math.PI / 16;
                        Contains.StoredEnemyBullets.Add(new EnemyBasicShot(new Vector2(0,0), new Vector2((float)Math.Cos(Angle) / 1.5f, (float)Math.Sin(Angle) / 1.5f), this, SceneMan));
                    }
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
            //Enemy Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["ExploderOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["ExploderInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["ExploderInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
