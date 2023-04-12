using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ShatteringShot : Bullet
    {
        private int RandomSprite;
        private double Angle;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(2, 3);
            ShotBy = shotby;

            Damage = 2f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            Angle = (Math.PI / 180) * SceneMan.rand.Next(-180 + (int)(15 * ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy), 0 - (int)(15 * ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy));

            Delta.X = (float)Math.Cos(Angle) * 1.5f;
            Delta.Y = (float)Math.Sin(Angle) * 1.5f;
            ProcChance = 0.8f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent; ;
            RandomSprite = SceneMan.rand.Next(0, 4);

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        //Normal Constructor
        public ShatteringShot(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        //Constructor for relics that need to put them selves in at start
        public ShatteringShot(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
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
            sb.Draw(SceneMan.Textures["ShatteringShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle(RandomSprite*2, 3, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["ShatteringShot"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(RandomSprite * 2, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
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
