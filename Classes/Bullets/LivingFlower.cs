using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class LivingFlower : Bullet
    {
        //Normal Constructor
        public Enemy GrabbedEnemy;
        private double Angle;
        private int RandomSelection = 0;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(12, 12);
            Damage = 0;
            Health = 9999;
            Delta.X = 0;
            Delta.Y = 0;
            ProcChance = 0f;
            ShotBy = shotby;
            if (shotby is Player)
            {
                LifeSpan = (5f) * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;
            }
            else
            {
                LifeSpan = (5f) * (float)ShotBy.Createdby.AllCores[ShotBy.Createdby.CurrentShipParts[0]].Stats.BulletLifeSpan;
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
        public LivingFlower(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public LivingFlower(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            // Relic Mod Bullet Update
            //foreach (Relic rel in SceneMan.ActiveRelics)
            //{
            //    if (rel.ModifiesBulletUpdate)
            //    {
            //        rel.ModBulUpdate(this, GT);
            //    }
            //}

            //Grab an enemy
            if (GrabbedEnemy == null)
            {
                if (SceneMan.Enemies.Count > 0)
                {
                    RandomSelection = SceneMan.rand.Next(0, SceneMan.Enemies.Count);
                    if (!Helper.CheckFlowersForEnemy(SceneMan.Enemies[RandomSelection], SceneMan))
                    {
                        GrabbedEnemy = SceneMan.Enemies[RandomSelection];
                    }
                }
            }
            else if (GrabbedEnemy.Health < 0)
            {
                if (SceneMan.Enemies.Count > 0)
                {
                    RandomSelection = SceneMan.rand.Next(0, SceneMan.Enemies.Count);
                    if (!Helper.CheckFlowersForEnemy(SceneMan.Enemies[RandomSelection], SceneMan))
                    {
                        GrabbedEnemy = SceneMan.Enemies[RandomSelection];
                    }
                }
            }

            if (GrabbedEnemy != null)
            {
                Angle = Math.Atan2(Pos.Y - GrabbedEnemy.Pos.Y, Pos.X - GrabbedEnemy.Pos.X);
                GrabbedEnemy.Pos.X += (float)Math.Cos(Angle);
                GrabbedEnemy.Pos.Y += (float)Math.Sin(Angle);
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)GrabbedEnemy.Pos.X, (int)GrabbedEnemy.Pos.Y, (int)GrabbedEnemy.WidthHeight.X, (int)GrabbedEnemy.WidthHeight.Y))
                {
                    GrabbedEnemy.Health -= (float)GT.ElapsedGameTime.TotalSeconds * 2;
                    GrabbedEnemy.Delta.X = 0;
                    GrabbedEnemy.Delta.Y = 0;
                    GrabbedEnemy.Pos = Pos;
                }
            }

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["LivingFlower"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(1f * (LifeSpan / 5), 1f * (LifeSpan / 5), 1f * (LifeSpan / 5), 1f * (LifeSpan / 5)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.39f);
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
