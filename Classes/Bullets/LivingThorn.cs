using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class LivingThorn : Bullet
    {
        //Normal Constructor
        private int RandomSprite;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(2, 4);
            RandomSprite = SceneMan.rand.Next(0, 5);
            ShotBy = shotby;
            if (shotby is Player)
            {
                LifeSpan = (5f) * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;
            }
            else
            {
                LifeSpan = (5f) * (float)ShotBy.Createdby.AllCores[ShotBy.Createdby.CurrentShipParts[0]].Stats.BulletLifeSpan;
            }
            Damage = 0.25f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f);
            Delta.Y = 0;
            ProcChance = 0f;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        public LivingThorn(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public LivingThorn(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            // Relic Mod Bullet Update
            //foreach (Relic rel in SceneMan.ActiveRelics)
            //{
            //    if (rel.ModifiesBulletUpdate)
            //    {
            //        rel.ModBulUpdate(this, GT);
            //    }
            //}

            Delta.X /= 1.15f;
            Delta.Y /= 1.15f;

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["LivingThorn"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(RandomSprite * 2, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(0f,1f*(LifeSpan / 5),0, 1f * (LifeSpan / 5)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.4f);
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
