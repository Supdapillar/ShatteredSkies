using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Contaminator : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        public int ShotDirection = 0;

        public Contaminator(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(39,41);
            Name = "Contaminator";
            SprOutline = SceneMan.Textures["ContaminatorOutline"];
            SprInside = SceneMan.Textures["ContaminatorInside"];
            Size = 3;
            Health = 250f;
            MaxHealth = 250f;
            Enemy_init();
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
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

            if (ShotDelay <= 0)
            {//top left
                switch (ShotDirection)
                {
                    case 0:
                    SceneMan.EnemyBullets.Add(new ContaminatorBomb(new Vector2(Pos.X + 14, Pos.Y + 28), ((Math.PI / 2) * SceneMan.rand.NextDouble()), this, SceneMan)); //Bullets
                        ShotDirection++;
                        break;
                    case 1:
                    SceneMan.EnemyBullets.Add(new ContaminatorBomb(new Vector2(Pos.X + 17, Pos.Y + 28), ((Math.PI / 2) * SceneMan.rand.NextDouble()) + (Math.PI / 2), this, SceneMan)); //Bullets
                        ShotDirection++;
                        break;
                    case 2:
                    SceneMan.EnemyBullets.Add(new ContaminatorBomb(new Vector2(Pos.X + 11, Pos.Y + 40), ((Math.PI / 2) * SceneMan.rand.NextDouble()), this, SceneMan)); //Bullets
                        ShotDirection++;
                        break;
                    case 3:
                    SceneMan.EnemyBullets.Add(new ContaminatorBomb(new Vector2(Pos.X + 20, Pos.Y + 40), ((Math.PI / 2) * SceneMan.rand.NextDouble()) + (Math.PI / 2), this, SceneMan)); //Bullets
                        ShotDirection = 0;
                        break;
                }
                ShotDelay =  1 + SceneMan.rand.NextDouble();
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
            sb.Draw(SceneMan.Textures["ContaminatorOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            sb.Draw(SceneMan.Textures["ContaminatorInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Pink, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y-3, ((int)Health/6)+1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
