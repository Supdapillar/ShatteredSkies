using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Flamethrower : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(5, 7);
            ShotBy = shotby;

            OnHitEffects.Add("Burning", 1);

            Damage = 0.15f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) / (float)(ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy);
            Delta.Y = -2;
            LifeSpan = (0.4f + (float)SceneMan.rand.NextDouble() / 10) * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;

            ProcChance = 0.15f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        public Flamethrower(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public Flamethrower(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
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

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle(19, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
