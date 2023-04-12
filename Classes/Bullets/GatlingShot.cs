using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class GatlingShot : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(2, 3);
            ShotBy = shotby;

            Damage = 0.5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) / (float)(4 * ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy);
            Delta.Y = -4;

            ProcChance = 0.25f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        public GatlingShot(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public GatlingShot(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }
        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }
            //DELETE BULLETS
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Helper.BoxCollision((int)Pos.X-1, (int)Pos.Y, (int)WidthHeight.X+2, (int)WidthHeight.Y, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                {
                    Health -= 1;
                    Ebull.Health -= 0.25f;
                    Ebull.Pos.Y -= 3;
                    if (Ebull.Delta.Y > .5f)
                    {
                        Ebull.Delta.Y -= 0.2f;
                    }
                }
            }

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle(17, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
