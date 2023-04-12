using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class StarCannon : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            SubType = subtype;
            Pos = pos;
            SceneMan = sceneman;
            ShotBy = shotby;
                WidthHeight = new Vector2(11, 12);
                Health = 3;
                Damage = 4 * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.BulletDamage;
                Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) / (float)(2 * ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.Accuracy);
                Delta.Y = -2.5f;
                ProcChance = 1.5f * (float)ShotBy.CreatedBy.AllCores[ShotBy.CreatedBy.CurrentShipParts[0]].Stats.ProcPercent;
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
        public StarCannon(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        //Constructor for relics that need to put them selves in at start
        public StarCannon(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
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

            if (SceneMan.rand.Next(0, 10) == 0)
            {
                Color PartCol;
                if (SceneMan.rand.Next(0,3)==0)
                {
                    PartCol = SceneMan.RelicsColors2[ShotBy.CreatedBy.CurrentRelics[2]];
                }
                else 
                {
                    PartCol = SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]];
                }
                SceneMan.Particles.Add(new StarCannonParticle(new Vector2(SceneMan.rand.Next((int)Pos.X,(int)(Pos.X+WidthHeight.X-5)),Pos.Y+10), PartCol, SceneMan));
            }

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["StarCannon"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
            sb.Draw(SceneMan.Textures["StarCannon"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle((int)WidthHeight.X, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[ShotBy.CreatedBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
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
