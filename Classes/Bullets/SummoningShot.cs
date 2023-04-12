using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class SummoningShot : Bullet
    {
        private double Delay = 0;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ShotBy = shotby;

            if (SubType == 0)
            {
                Damage = 2f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = 0;
                Delta.Y = -0.75f;
                WidthHeight = new Vector2(6, 7);
                ProcChance = 1f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
            }
            else//summonig child
            {
                Damage = 0.5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) * 2;
                Delta.Y = ((float)SceneMan.rand.NextDouble() - 0.5f) * 2;
                LifeSpan = 1f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;
                WidthHeight = new Vector2(2, 3);
                ProcChance = 0.25f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
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
        public SummoningShot(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }
        public SummoningShot(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;
            Delay -= (float)GT.ElapsedGameTime.TotalSeconds;

            //Hardcoded way to not make soul broken
            for (int i = 0; i < LocalRelics.Count; i++)
            {
                if (LocalRelics[i] is Soul)
                {
                    Delay += (float)GT.ElapsedGameTime.TotalSeconds / 2;
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

            if (Delay <= 0 & SubType == 0)
            {
                SceneMan.Bullets.Add(new SummoningShot(1, new Vector2(Pos.X + 2, Pos.Y + 2), SceneMan, ShotBy));//Bullets
                Delay = 0.10;
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
                sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(11, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else//summonig child
            {
                sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(11, 8, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
