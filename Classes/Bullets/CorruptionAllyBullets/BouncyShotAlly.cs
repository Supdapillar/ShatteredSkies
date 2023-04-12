using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class BouncyShotAlly : Bullet
    {
        private int Bounces;

        public BouncyShotAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Pos = pos;
            Delta = D;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(4, 5);
            Bounces = 3;
            ShotBy = shotby;
            ProcChance = 0.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            Damage = 1.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }

        public BouncyShotAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Pos = pos;
            Delta = D;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(4, 5);
            Bounces = 3;
            ShotBy = shotby;
            ProcChance = 0.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            Damage = 1.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Delta.X = -Delta.X;
                Bounces -= 1;
            }
            else if (Pos.X > 284) // Right Wall
            {
                Delta.X = -Delta.X;
                Bounces -= 1;
            }
            if (Pos.Y < 0) //Top wall
            {
                Delta.Y = -Delta.Y;
                Bounces -= 1;
            }
            else if (Pos.Y > 162 - 4) // Bottom Wall
            {
                Delta.Y = -Delta.Y;
                Bounces -= 1;
            }

            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }

            if (Bounces <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (ShotBy is Player)
            {
                sb.Draw(SceneMan.Textures["Bouncy"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else if (ShotBy is Ally)
            {
                sb.Draw(SceneMan.Textures["Bouncy"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["Bouncy"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            //Relic Mod Nullet Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletDraw)
                {
                    rel.ModBulDraw(this, sb);
                }
            }
        }
    }
}
