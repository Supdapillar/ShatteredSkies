using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ExplosiveMine : Bullet
    {
        private Animation ArmAnimation;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(5, 3);
            ShotBy = shotby;
            Damage = 1;
            Delta.X = 0;
            Delta.Y = -3;
            ProcChance = 1f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            ArmAnimation = new Animation(SceneMan.Textures["ExplosiveMine"], 7, 5, false);
            Health = 999;
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
        public ExplosiveMine(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        //Constructor for relics that need to put them selves in at start
        public ExplosiveMine(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            double ClosestDistance = 100;
            foreach (Enemy ene in SceneMan.Enemies)
            {
                if (ene.Health > 0)
                {
                    double distance = Helper.GetDistance(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2));
                    if (distance < ClosestDistance)
                    {
                        ClosestDistance = distance;
                    }
                }
            }
            ArmAnimation.Update(GT);
            Pos += Delta;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            //Explode
            if (LifeSpan < 23.4f)
            {
                if (ClosestDistance < 25)
                {
                    Health = 0;
                    SceneMan.Bullets.Add(new Explosion(0, new Vector2(Pos.X - 23.5f, Pos.Y - 23.5f), SceneMan, 3f, 25, ShotBy));
                }
            }


            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }

            Delta.Y /= 1.1f;
        }
        public override void Draw(SpriteBatch sb)
        {
            ArmAnimation.Draw(sb,Pos,0.5f,
            SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], SpriteEffects.None,SceneMan);
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
