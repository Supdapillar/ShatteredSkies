using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class OmegaDrone : Ally
    {
        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private Vector2 StartingPos;
        public OmegaDrone(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            StartingPos = PS;
            GotoPos = new Vector2(SceneMan.rand.Next((int)(StartingPos.X - 25), (int)(StartingPos.X + 25)), SceneMan.rand.Next((int)(StartingPos.X - 25), (int)(StartingPos.X + 25)));
            SceneMan = Scenemana;
            WidthHeight = new Vector2(7, 10);
            Health = 4;
            MaxHealth = 6;
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
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyFireRate;
            // ai shet dont work 2 good rn, fix later
            GotoPos = CreatedBy.Pos;


            if (Pos.X < GotoPos.X & Delta.X < (1 * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds * (float)(CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.X > GotoPos.X & Delta.X > (-1 * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds * (float)(CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < (0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > (-0.5f * CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed)) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / (float)(2 / CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllySpeed);
            }

            if (ShotDelay <= 0)
            {
                SceneMan.Bullets.Add(new OmegaDroneShot(0, new Vector2(Pos.X+3, Pos.Y - 2), SceneMan, this)); //Bullets
                ShotDelay = (SceneMan.rand.NextDouble() / 2)+0.25f;
            }

            //add a wee bit of slide
            Delta /= 1;

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
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, ((int)Health * 2) + 1, 1), new Rectangle(0, 0, 1, 1), Color.Green, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            sb.Draw(SceneMan.Textures["OmegaDrone"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["OmegaDrone"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle((int)WidthHeight.X, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["OmegaDrone"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle((int)WidthHeight.X*2, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
        }
    }
}
