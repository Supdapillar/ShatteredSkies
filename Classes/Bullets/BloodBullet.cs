using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class BloodBullet : Bullet
    {
        private Animation BloodAnimation;

        private double ClotTimer;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ShotBy = shotby;
            if (SubType == 0)
            {
                LifeSpan = (float)SceneMan.rand.NextDouble() * 2 + 1;
                Damage = 0f;
                Delta.X = 0;
                Delta.Y = 0;
                Health = 9999;
                WidthHeight = new Vector2(12, 9);
                ProcChance = 0;
            }
            else
            {
                Damage = 0.5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = 0;
                Delta.Y = 1;
                WidthHeight = new Vector2(5, 11);
                BloodAnimation = new Animation(SceneMan.Textures["Blood"], 7, 5, true);
                ProcChance = 0.75f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
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
        public BloodBullet(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public BloodBullet(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }
        public BloodBullet(int subtype, Vector2 pos, Vector2 delta, SceneManager sceneman) : base(subtype, pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            Delta = delta;
            Damage = 0.5f;
            OnHitEffects.Add("Bleeding", 1);
            BloodAnimation = new Animation(SceneMan.Textures["Blood"], 7, 5, true);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            if (SubType == 0)
            {
                ClotTimer += GT.ElapsedGameTime.TotalSeconds;
                if (ClotTimer >= 0.75)
                {
                    SceneMan.Bullets.Add(new BloodBullet(1, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)),new Vector2(0, -1), SceneMan));//up
                    SceneMan.Bullets.Add(new BloodBullet(1, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)),new Vector2(1,0), SceneMan));//right
                    SceneMan.Bullets.Add(new BloodBullet(1, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)),new Vector2(0,1), SceneMan));//down
                    SceneMan.Bullets.Add(new BloodBullet(1, new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)),new Vector2(-1,0), SceneMan));//left
                    ClotTimer -= 0.75f;
                }
            }
            else
            {
                foreach (Ally Al in SceneMan.Allies)
                {
                    if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)Al.Pos.X, (int)Al.Pos.Y, (int)Al.WidthHeight.X, (int)Al.WidthHeight.Y))
                    {
                        if (Al.Health + 0.5f < Al.MaxHealth)
                        Al.Health += 0.5f;
                        Health = 0;
                    }
                }
                BloodAnimation.Update(GT);
            }
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
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (SubType == 0)
            {
                sb.Draw(SceneMan.Textures["BloodClot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else
            {
                BloodAnimation.Draw(sb, Pos, 0.3f, SceneMan);
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
