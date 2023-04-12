using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Scanner : Enemy
    {

        public bool GoLeft = true; // fasle is left // true is right
        private float Speed = 1;


        public Scanner(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(17, 15);
            Health = 15;
            MaxHealth = 15;
            Name = "Scanner";
            SprOutline = SceneMan.Textures["ScannerOutline"];
            SprInside = SceneMan.Textures["ScannerInside"];
            Size = 1;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * Speed;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft)
            {
                Delta.X = -Speed;
            }
            else
            {
                Delta.X = Speed;
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

            if (Pos.X <= 0 && GoLeft)
            {
                GoLeft = !GoLeft;
                Pos.Y += 6;
                Speed += 0.5f;
            }
            else if (Pos.X + 17 >= 288 && !GoLeft)
            {
                GoLeft = !GoLeft;
                Pos.Y += 6;
                Speed += 0.5f;
            }

            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            //explodes if under the map
            if (Pos.Y + 15 > 162)
            {
                if (GoLeft && Pos.X < SceneMan.Players[0].Pos.X)
                {
                    for (int i = 0; i < 64; i++)
                    {
                    }
                    Health = 0;
                }
                else if (!GoLeft && Pos.X > SceneMan.Players[0].Pos.X)
                {
                    for (int i = 0; i < 64; i++)
                    {
                    }
                    Health = 0;
                }
            }
            if (ShotDelay <= 0)
            {
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 7, Pos.Y + 15), new Vector2(((float)SceneMan.rand.NextDouble() / 4) - 0.25f, 1), this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
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
            sb.Draw(SceneMan.Textures["ScannerOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["ScannerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["ScannerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
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
