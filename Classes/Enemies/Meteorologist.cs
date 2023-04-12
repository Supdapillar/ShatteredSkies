using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Meteorologist : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private double MeteorDelay = 0;
        private double SuperStormDelay = 0;
        public Meteorologist(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 30);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(18, 20);
            Health = 50;
            MaxHealth = 50;
            Name = "Meteorologist";
            SprOutline = SceneMan.Textures["MeteorologistOutline"];
            SprInside = SceneMan.Textures["MeteorologistInside"];
            Size = 2;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            SuperStormDelay += GT.ElapsedGameTime.TotalSeconds / 3.5;
            if (SuperStormDelay >= 5)
            {
                MeteorDelay += GT.ElapsedGameTime.TotalSeconds * 13;
                Pos += Delta + Delta + Delta;
            }
            else
            {
                MeteorDelay += GT.ElapsedGameTime.TotalSeconds * 3;
                Pos += Delta;
            }
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

            //bullet shot
            if (ShotDelay <= 0)
            {
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X+7, Pos.Y + 20), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1), this, SceneMan)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
            }

            //meteor fall down
            if (MeteorDelay >= 5)
            {
                SceneMan.EnemyBullets.Add(new EnemyMeteor(new Vector2(SceneMan.rand.Next(32,248),-200), new Vector2((float)(SceneMan.rand.NextDouble()-0.5), (float)SceneMan.rand.NextDouble()+1.5f), this, SceneMan)); //Bullets
                MeteorDelay = 0;
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
            sb.Draw(SceneMan.Textures["MeteorologistOutline"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["MeteorologistInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["MeteorologistInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            RenderHealth(sb);
            //////Charge stuff////
            //Small charges
            if (MeteorDelay < 5)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 2, 3, (int)MeteorDelay), new Rectangle(0, 0, 1, 1), Color.DarkMagenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 1, 3, (int)MeteorDelay), new Rectangle(0, 0, 1, 1), Color.Magenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 2, 3, 5), new Rectangle(0, 0, 1, 1), Color.DarkMagenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 1, 3, 5), new Rectangle(0, 0, 1, 1), Color.Magenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            //Girthy Charge
            if (SuperStormDelay < 5)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 9 - (int)SuperStormDelay, 3, (int)SuperStormDelay), new Rectangle(0, 0, 1, 1), Color.DarkRed, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 10 - (int)SuperStormDelay, 3, (int)SuperStormDelay), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 5, 3, (int)(0 + 5)), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
