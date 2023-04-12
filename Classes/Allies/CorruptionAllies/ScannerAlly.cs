using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ScannerAlly : Ally
    {

        public bool GoLeft = true; // fasle is left // true is right
        private float Speed = 1;


        public ScannerAlly(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(17, 15);
            Health = 15;
            MaxHealth = 15;
            CreatedBy = createdby;
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= (GT.ElapsedGameTime.TotalSeconds * Speed) * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            // ai shet dont work 2 good rn, fix later
            if (GoLeft)
            {
                Delta.X = -Speed;
            }
            else
            {
                Delta.X = Speed;
            }

            if (Pos.X <= 0 && GoLeft)
            {
                GoLeft = !GoLeft;
                Pos.Y -= 6;
                Speed += 0.5f;
            }
            else if (Pos.X + 17 >= 288 && !GoLeft)
            {
                GoLeft = !GoLeft;
                Pos.Y -= 6;
                Speed += 0.5f;
            }

            //explodes if under the map
            if (Pos.Y < 0 && (Pos.X > 100 && Pos.X < 200))
            {
                for (int i = 0; i < 64; i++)
                {
                    SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (float)SceneMan.rand.NextDouble() + 1.5f, Pos.Y + 9), new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f), (float)(SceneMan.rand.NextDouble() - 0.5f)), SceneMan, this)); //Bullets
                }
                Health = 0;
            }
            if (ShotDelay <= 0)
            {
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + 7, Pos.Y - 4), new Vector2(((float)SceneMan.rand.NextDouble() / 4) - 0.25f, -1), SceneMan, this)); //Bullets
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
            sb.Draw(SceneMan.Textures["ScannerOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["ScannerInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[3], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, (int)Health + 1, 1), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
