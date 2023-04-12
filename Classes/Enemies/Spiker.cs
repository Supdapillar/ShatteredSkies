using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Spiker : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private int ShotDirection = 0;

        private double TenticleDelay;
        private Vector2 TenticleLocation;
        private int TenticleRotation;

        public Spiker(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(21,25);
            Name = "Spiker";
            SprOutline = SceneMan.Textures["SpikerOutline"];
            SprInside = SceneMan.Textures["SpikerInside"];
            Health = 50;
            MaxHealth = 50;
            TenticleDelay = 2.5f + SceneMan.rand.NextDouble();
            TenticleRotation = SceneMan.rand.Next(0, 4);
            switch (TenticleRotation)
            {
                case 0:
                    TenticleLocation = new Vector2(SceneMan.rand.Next(0, 271), 162 - 81 + SceneMan.rand.Next(0, 8));
                    break;
                case 1:
                    TenticleLocation = new Vector2(0, SceneMan.rand.Next(0, 162 - 17));
                    break;
                case 2:
                    TenticleLocation = new Vector2(SceneMan.rand.Next(0, 271), 0);
                    break;
                case 3:
                    TenticleLocation = new Vector2(288 - 81, SceneMan.rand.Next(0, 162 - 17));
                    break;
            }
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TenticleDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
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

            //Regular
            if (ShotDelay <= 0)
            {
                if (ShotDirection == 0)
                {
                    SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X, Pos.Y + 19), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1.5f), this, SceneMan)); //Bullets
                    ShotDirection = 1;
                    ShotDelay = 0.5 + (SceneMan.rand.NextDouble() / 4);
                }
                else
                {
                    SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 18, Pos.Y + 19), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, 1.5f), this, SceneMan)); //Bullets
                    ShotDirection = 0;
                    ShotDelay = 0.5 + (SceneMan.rand.NextDouble()/4);
                }
            }

            //Tenticle
            if (TenticleDelay <= 0)
            {
                SceneMan.EnemyBullets.Add(new Spike(TenticleLocation, new Vector2(0,0),TenticleRotation, this, SceneMan)); //Bullets
                TenticleRotation = SceneMan.rand.Next(0, 4);
                switch (TenticleRotation)
                {
                    case 0:
                        TenticleLocation = new Vector2(SceneMan.rand.Next(0, 271), 162 - 81 + SceneMan.rand.Next(0, 8));
                        break;
                    case 1:
                        TenticleLocation = new Vector2(0, SceneMan.rand.Next(0, 162 - 17));
                        break;
                    case 2:
                        TenticleLocation = new Vector2(SceneMan.rand.Next(0, 271), 0);
                        break;
                    case 3:
                        TenticleLocation = new Vector2(288-81, SceneMan.rand.Next(0, 162 - 17));
                        break;
                }
                TenticleDelay = 2.5f + SceneMan.rand.NextDouble();
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
            sb.Draw(SceneMan.Textures["SpikerOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["SpikerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["SpikerInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            }

            RenderHealth(sb);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 4, (int)((TenticleDelay * 5)), 1), new Rectangle(0, 0, 1, 1), Color.Blue, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

            
            if (TenticleDelay < 1)
            {
                switch (TenticleRotation)
                {
                    case 0:
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)TenticleLocation.X + 4, (int)TenticleLocation.Y + 24),
                        new Color(1f, (float)(TenticleDelay % (0.5f) * 2), (float)(TenticleDelay % (0.5f) * 2), 1f), 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0.1f);
                        break;
                    case 1:
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)TenticleLocation.X + 57, (int)TenticleLocation.Y + 3),
                        new Color(1f, (float)(TenticleDelay % (0.5f) * 2), (float)(TenticleDelay % (0.5f) * 2), 1f), (float)Math.PI/2, new Vector2(0, 0), 2f, SpriteEffects.None, 0.1f);
                        break;
                    case 2:
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)TenticleLocation.X + 9, (int)TenticleLocation.Y + 57),
                        new Color(1f, (float)(TenticleDelay % (0.5f) * 2), (float)(TenticleDelay % (0.5f) * 2), 1f), (float)Math.PI, new Vector2(0, 0), 2f, SpriteEffects.None, 0.1f);
                        break;
                    case 3: 
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)TenticleLocation.X + 24, (int)TenticleLocation.Y + 7),
                        new Color(1f, (float)(TenticleDelay % (0.5f) * 2), (float)(TenticleDelay % (0.5f) * 2), 1f), (float)(Math.PI + (Math.PI / 2)), new Vector2(1, 0), 2f, SpriteEffects.None, 0.1f);
                        break;
                }
            }
            
            
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
