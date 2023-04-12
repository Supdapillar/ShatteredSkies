using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Lazermancer : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private double Angle = 0;

        public Lazermancer(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 20);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(18,21);
            Health = 45;
            MaxHealth = 45;
            Name = "Lazermancer";
            SprOutline = SceneMan.Textures["LazermancerOutline"];
            SprInside = SceneMan.Textures["LazermancerInside"];
            Size = 2;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X <= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X = 288 - 114;
                GotoPos.Y = SceneMan.rand.Next(10,30);
            }
            else if (!GoLeft & Pos.X > GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X = 288 - 174;
                GotoPos.Y = SceneMan.rand.Next(10, 30);
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

            if (Pos.X < GotoPos.X & Delta.X < 1) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds;
            }
            else if (Pos.X > GotoPos.X & Delta.X > -1) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds;
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < 0.5f) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > -0.5f) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }



            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            if (ShotDelay <= 0)
            {
                Angle = ((SceneMan.rand.NextDouble() * Math.PI)/2)+(Math.PI/4);
                SceneMan.EnemyBullets.Add(new LazerBullet(new Vector2(Pos.X + 7, Pos.Y + 19), new Vector2((float)Math.Cos(Angle)/6, (float)Math.Sin(Angle)/6), this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 5;
            }

            //add a wee bit of slide
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
            //Enemy Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["LazermancerOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["LazermancerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["LazermancerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
