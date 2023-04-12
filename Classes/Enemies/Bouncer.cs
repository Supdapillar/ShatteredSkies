using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Bouncer : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private int ShotDirection = 0;

        public Bouncer(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(14, 13);
            Health = 5;
            MaxHealth = 5;
            Name = "Bouncer";
            SprOutline = SceneMan.Textures["BouncerOutline"];
            SprInside = SceneMan.Textures["BouncerInside"];
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
                switch (ShotDirection)
                {
                    case 0:
                        SceneMan.EnemyBullets.Add(new BouncyShot(new Vector2(Pos.X + 1f, Pos.Y + 11), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.75f, 1), this, SceneMan)); //Bullets
                        ShotDelay = 0.35f;
                        ShotDirection = 1;
                        break;
                    case 1:
                        ShotDirection = 2;
                        SceneMan.EnemyBullets.Add(new BouncyShot(new Vector2(Pos.X + 5f, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                        ShotDelay = 0.35f;
                        break;
                    case 2:
                        ShotDirection = 0;
                        SceneMan.EnemyBullets.Add(new BouncyShot(new Vector2(Pos.X + 9f, Pos.Y + 11), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - -0.25f, 1), this, SceneMan)); //Bullets
                        ShotDelay = SceneMan.rand.NextDouble() + 0.75;
                        break;
                }
            }

            //add a wee bit of slide
            Delta /= 1;
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
            sb.Draw(SceneMan.Textures["BouncerOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["BouncerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["BouncerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
