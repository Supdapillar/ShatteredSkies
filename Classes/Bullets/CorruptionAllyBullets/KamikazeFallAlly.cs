using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class KamikazeFallAlly : Bullet
    {
        
        public KamikazeFallAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Pos = pos;
            Delta = D;
            ShotBy = shotby;
            Damage = 4f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;
            SceneMan = sceneman;
            WidthHeight = new Vector2(15, 14);
            ProcChance = 1 * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;

            //Relic Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }

        public KamikazeFallAlly(int subtype, Vector2 pos, Vector2 D, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Pos = pos;
            Delta = D;
            SceneMan = sceneman;
            WidthHeight = new Vector2(15, 14);
            ShotBy = shotby;
            ProcChance = 1 * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
            Damage = 4f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.AllyDamage;

            //Relic Mod Bullet Contructor
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
            if (Delta.Y > -3)
            {
                Delta.Y = -3;
            }
            Delta.Y -= 6.5f * (float)GT.ElapsedGameTime.TotalSeconds;
            //explode on ground
            if (Pos.Y < 0)
            {
                SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(-0.5f, 0.5f), SceneMan, ShotBy));// up left
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(-0.25f, 0.75f), SceneMan, ShotBy));// up leftish
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0, 1), SceneMan, ShotBy));// UP
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0.25f, 0.75f), SceneMan, ShotBy));// up rightish
                SceneMan.Bullets.Add(new BasicShotWeak(0,new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0.5f, 0.5f), SceneMan, ShotBy));// up right
                Health = 0;
            }
            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["KamikazeOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
            sb.Draw(SceneMan.Textures["KamikazeInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(128,0,0,255), 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.33f);
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
