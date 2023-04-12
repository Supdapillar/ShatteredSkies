using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class LockOnShot : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ShotBy = shotby;

            if (SubType == 0) // big bull
            {
                Damage = 2.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = 0;
                Delta.Y = -2;
                WidthHeight = new Vector2(3, 5);

                LifeSpan = 0.65f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;
                ProcChance = 1f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            }
            else if (SubType == 1)// smol bull
            {
                WidthHeight = new Vector2(3, 3);
                double angle;
                if (SceneMan.Enemies.Count > 0)
                {
                    Enemy ene = SceneMan.Enemies[SceneMan.rand.Next(0, SceneMan.Enemies.Count)];
                    angle = Helper.GetRadiansOfTwoPoints(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2),new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2));
                }
                else
                {
                    angle = Math.PI + Math.PI/2;
                }

                Damage = 1.25f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = (float)(Math.Cos(angle) * 6);
                Delta.Y = (float)(Math.Sin(angle) * 6);

                ProcChance = 0.75f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;

                //Kool particle
                int rando = SceneMan.rand.Next(5, 10);

                for (int i = 0; i < rando; i++)
                {
                    Color col;
                    if (SceneMan.rand.Next(0, 3) == 0) // 2 color
                    {
                        col = SceneMan.RelicsColors2[ShotBy.CreatedBy.CurrentRelics[2]];
                    }
                    else // first
                    {
                        col = SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]];
                    }
                    SceneMan.Particles.Add(new ColoredParticle
                    (
                        new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2),
                        new Vector2((float)((SceneMan.rand.NextDouble() - 0.5f)-(Delta.X/4)), (float)((SceneMan.rand.NextDouble() - 0.5f) / 4)- (Delta.Y/4)),
                        SceneMan,
                        col,
                        true,
                        0.5f
                    ));
                }
            }

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        //Normal Constructor
        public LockOnShot(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        //Constructor for relics that need to put them selves in at start
        public LockOnShot(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            //Change into small bullet



            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }

            if (LifeSpan <= 0)
            {
                Health = 0;
                SceneMan.Bullets.Add(new LockOnShot(1,Pos,SceneMan, ShotBy));
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (SubType == 0) 
            {
                sb.Draw(SceneMan.Textures["LockOnShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
                sb.Draw(SceneMan.Textures["LockOnShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle((int)WidthHeight.X, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            }
            else if (SubType == 1) // small bullet
            {
                sb.Draw(SceneMan.Textures["LockOnShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(6, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
                sb.Draw(SceneMan.Textures["LockOnShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(9, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            }

            //Relic Mod Bullet Draw
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
