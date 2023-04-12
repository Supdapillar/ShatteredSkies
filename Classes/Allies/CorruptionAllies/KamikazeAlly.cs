using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class KamikazeAlly : Ally
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        public KamikazeAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 130);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(15, 14);
            Health = 7;
            MaxHealth = 7;
            CreatedBy = createdby;
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X <= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                if (GotoPos.X > 288)
                {
                    GotoPos.X = 278;
                }
                GotoPos.Y = SceneMan.rand.Next(120, 130);
            }
            else if (!GoLeft & Pos.X >= GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                if (GotoPos.X < 0)
                {
                    GotoPos.X = 10;
                }
                GotoPos.Y = SceneMan.rand.Next(120, 130);
            }

            if (Pos.X < GotoPos.X & Delta.X < (1f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / (float)(1.75f / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.X > GotoPos.X & Delta.X > (-1f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(1.75f / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(3.5f / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(3.5f / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (ShotDelay <= 0)
            {
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + 6f, Pos.Y - 4), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
            }

            //Relic Mod Ally Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyUpdate)
                {
                    rel.ModAllyUpdate(this, GT);
                }
            }

            //collision with bullets
            CheckCollision(WidthHeight);
            //turn into a kamikaze bullet
            if (Health <= 0)
            {
                SceneMan.Bullets.Add(new KamikazeFallAlly(0, Pos, Delta, SceneMan, this));
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            //Relic Mod Ally Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyDraw)
                {
                    rel.ModAllyDraw(this, sb);
                }
            }
            sb.Draw(SceneMan.Textures["KamikazeOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["KamikazeInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, ((int)Health * 2) + 1, 1), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
