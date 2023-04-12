using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EnemyMeteorAlly : Bullet
    {
        private readonly int RandomRock;
        public EnemyMeteorAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Pos = pos;
            Delta = D;
            SceneMan = sceneman;
            RandomRock = SceneMan.rand.Next(0, 2);
            ShotBy = shotby;
            ProcChance = 0.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            Damage = 1 * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;
            Health = 1;
            switch (RandomRock)
            {
                case 0:
                    WidthHeight = new Vector2(10, 9);
                    break;
                case 1:
                    WidthHeight = new Vector2(10, 10);
                    break;
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

        public EnemyMeteorAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Pos = pos;
            Delta = D;
            SceneMan = sceneman;
            RandomRock = SceneMan.rand.Next(0, 2);
            ShotBy = shotby;
            ProcChance = 0.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            Damage = 1 * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;
            Health = 1;
            switch (RandomRock)
            {
                case 0:
                    WidthHeight = new Vector2(10, 9);
                    break;
                case 1:
                    WidthHeight = new Vector2(10, 10);
                    break;
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

        public override void Update(GameTime GT)
        {
            Pos += Delta;

            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }
            //Collision with the floor
            if (Pos.Y+WidthHeight.Y < 0)
            {
                Health = 0;
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(-0.5f, 0.5f),SceneMan, ShotBy));// up left
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(-0.25f, 0.75f), SceneMan, ShotBy));// up leftish
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0, 1), SceneMan, ShotBy));// UP
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0.25f, 0.75f), SceneMan, ShotBy));// up rightish
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0.5f, 0.5f), SceneMan, ShotBy));// up right
            }
            //collision with player bullets
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.Y, (int)WidthHeight.Y, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                {
                    Health = 0;
                    Ebull.Health -= 1;
                }
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            //Relic Mod Nullet Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletDraw)
                {
                    rel.ModBulDraw(this, sb);
                }
            }
            switch (RandomRock)
            {
                case 0:
                    sb.Draw(SceneMan.Textures["MeteorologistRocks"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 1:
                    sb.Draw(SceneMan.Textures["MeteorologistRocks"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(10, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
            }
        }
    }
}
