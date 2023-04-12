using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ShrapnelShot : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ShotBy = shotby;

            if (SubType == 0)
            {
                Damage = 1.25f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) / (float)(2 * ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy);
                Delta.Y = -2;
                WidthHeight = new Vector2(5, 6);
                ProcChance = 0.75f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
            }
            else //shrap child
            {
                Damage = 0.75f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) * 2;
                Delta.Y = ((float)SceneMan.rand.NextDouble() - 0.5f) * 2;
                LifeSpan = (1.5f + (float)SceneMan.rand.NextDouble() - 0.5f) * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletLifeSpan;
                WidthHeight = new Vector2(1, 2);
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
        public ShrapnelShot(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public ShrapnelShot(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
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
            switch (SubType)
            {
                case 0:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(3, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 1:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(3, 6, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
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
