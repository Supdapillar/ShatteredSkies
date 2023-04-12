using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Jinx : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private int ShotDirection = 0;

        public Jinx(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(14,16);
            Name = "Jinx";
            SprOutline = SceneMan.Textures["JinxOutline"];
            SprInside = SceneMan.Textures["JinxInside"];
            Size = 1;
            Health = 8;
            MaxHealth = 8;
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



            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            if (ShotDelay <= 0)
            {
                if (ShotDirection == 0)
                {
                    SceneMan.EnemyBullets.Add(new JinxBullet(new Vector2(Pos.X , Pos.Y + 16), new Vector2(0, 2), this, SceneMan)); //Bullets
                    ShotDelay = SceneMan.rand.NextDouble() + 1;
                    ShotDirection = 1;
                }
                else
                {
                    SceneMan.EnemyBullets.Add(new JinxBullet(new Vector2(Pos.X + 9, Pos.Y + 16), new Vector2(0,2), this, SceneMan)); //Bullets
                    ShotDelay = SceneMan.rand.NextDouble() + 1;
                    ShotDirection = 0;
                }
            }

            //add a wee bit of slide
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
            sb.Draw(SceneMan.Textures["JinxOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);

            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["JinxInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["JinxInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
