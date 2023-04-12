using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class MeteorologistAlly : Ally
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right

        private double MeteorDelay = 0;
        private double SuperStormDelay = 0;
        public MeteorologistAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 40);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(18, 20);
            Health = 50;
            MaxHealth = 50;
            CreatedBy = createdby;
            //Relic Mod Ally Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesAllyConstructor)
                {
                    rel.ModAllyCons(this);
                }
            }
        }

        public override void Update(GameTime GT)
        {
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            SuperStormDelay += GT.ElapsedGameTime.TotalSeconds / 3;
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

            if (Pos.X < GotoPos.X & Delta.X < (1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.X > GotoPos.X & Delta.X > (-1.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(4 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }

            //bullet shot
            if (ShotDelay <= 0)
            {
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + 7, Pos.Y - 4), new Vector2(((float)SceneMan.rand.NextDouble() / 2) - 0.25f, -1), SceneMan, this)); //Bullets
                ShotDelay = SceneMan.rand.NextDouble() + 0.5;
            }

            //meteor fall down
            if (MeteorDelay >= 5)
            {
                SceneMan.Bullets.Add(new EnemyMeteorAlly(0,new Vector2(SceneMan.rand.Next(32,248),338), new Vector2((float)(SceneMan.rand.NextDouble()-0.5), (float)SceneMan.rand.NextDouble() - 2.5f), SceneMan, CreatedBy)); //Bullets
                MeteorDelay = 0;
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
            sb.Draw(SceneMan.Textures["MeteorologistOutline"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["MeteorologistInside"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, ((int)Health / 2) + 1, 1), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //////Charge stuff////
            //Small charges
            if (MeteorDelay < 5)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)(Pos.Y + 18 - (int)MeteorDelay), 3, (int)MeteorDelay), new Rectangle(0, 0, 1, 1), Color.DarkMagenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)(Pos.Y + 19 - (int)MeteorDelay), 3, (int)MeteorDelay), new Rectangle(0, 0, 1, 1), Color.Magenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 13, 3, 5), new Rectangle(0, 0, 1, 1), Color.DarkMagenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 5, (int)Pos.Y + 14, 3, 5), new Rectangle(0, 0, 1, 1), Color.Magenta, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            //Girthy Charge
            if (SuperStormDelay < 5)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 11, 3, (int)SuperStormDelay), new Rectangle(0, 0, 1, 1), Color.DarkRed, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 10, 3, (int)SuperStormDelay), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X + 9, (int)Pos.Y + 10, 3, (int)(0 + 5)), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
        }
    }
}
