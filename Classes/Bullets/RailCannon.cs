using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class RailCannon : Bullet
    {

        public Vector2 Origin;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            ShotBy = shotby;
            Origin = Pos - shotby.Pos;

            switch (SubType)
            {
                case 0:
                    Damage = 3f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                    Delta.X = 0;
                    Delta.Y = 0;
                    LifeSpan = 0.5f;
                    Health = 2;
                    WidthHeight = new Vector2(7, 18);
                    ProcChance = 0.5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
                    break;
                case 1:
                    Damage = 5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                    Delta.X = 0;
                    Delta.Y = 0;
                    LifeSpan = 0.5f;
                    Health = 4;
                    WidthHeight = new Vector2(13, 33);
                    ProcChance = 0.75f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
                    break;
                case 2:
                    Damage = 7f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                    Delta.X = 0;
                    Delta.Y = 0;
                    LifeSpan = 0.5f;
                    Health = 6;
                    WidthHeight = new Vector2(23, 72);
                    ProcChance = 1f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
                    break;
                case 3:
                    Damage = 10f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
                    Delta.X = 0;
                    Delta.Y = 0;
                    LifeSpan = 0.5f;
                    Health = 8;
                    WidthHeight = new Vector2(47, 104);
                    ProcChance = 2f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;
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
        public RailCannon(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public RailCannon(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            LifeSpan -= (float)GT.ElapsedGameTime.TotalSeconds;

            switch (SubType)
            {
                case 0:
                    Pos.X = ShotBy.Pos.X + Origin.X;
                    Pos.Y = ShotBy.Pos.Y + Origin.Y;
                    break;
                case 1:
                    Pos.X = ShotBy.Pos.X + Origin.X;
                    Pos.Y = ShotBy.Pos.Y + Origin.Y;
                    break;
                case 2:
                    Pos.X = ShotBy.Pos.X + Origin.X;
                    Pos.Y = ShotBy.Pos.Y + Origin.Y;
                    break;
                case 3:
                    Pos.X = ShotBy.Pos.X + Origin.X;
                    Pos.Y = ShotBy.Pos.Y + Origin.Y;
                    break;
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
            switch (SubType)
            {
                case 0:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(33, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 1:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(40, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 2:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(53, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 3:
                    sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(76, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
