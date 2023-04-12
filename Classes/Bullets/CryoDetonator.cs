using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class CryoDetonator : Bullet
    {
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(6, 10);
            ShotBy = shotby;

            Health = 3;
            Damage = 2f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            Delta.X = ((float)SceneMan.rand.NextDouble() - 0.5f) / (float)(4 * ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Accuracy);
            Delta.Y = -2;

            ProcChance = 1f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        public CryoDetonator(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Constructor(subtype, pos, sceneman, shotby);
        }

        public CryoDetonator(int subtype, Vector2 pos, SceneManager sceneman, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
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
            //kool hip particle
            if (SceneMan.rand.Next(0, 3) == 0)
            {
                SceneMan.Particles.Add(new ColoredParticle
                (
                    new Vector2(SceneMan.rand.Next((int)Pos.X, (int)(Pos.X + WidthHeight.X)), Pos.Y + WidthHeight.Y),
                    new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) / 2, (float)(SceneMan.rand.NextDouble() / 2)),
                    SceneMan,
                    SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]],
                    true,
                    2f

                ));
            }

            //on death thing
            if (Health <= 1)
            {
                if (LocalRelics.OfType<Soul>().Any())
                {
                    SceneMan.Particles.Add(new CryoParticle(new Vector2(Pos.X - 25, Pos.Y - 25), SceneMan));
                    foreach (Enemy ene in SceneMan.Enemies)
                    {
                        if (Helper.GetDistance(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2)) < 25)
                        {
                            ene.StatusEffects[2].EffectAmount += (float)(Helper.GetDistance(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2)) / 5f);
                        }
                    }
                    Health = 0;
                }
                else
                {
                    SceneMan.Particles.Add(new CryoParticle(new Vector2(Pos.X - 25, Pos.Y - 25), SceneMan));
                    foreach (Enemy ene in SceneMan.Enemies)
                    {
                        if (Helper.GetDistance(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2)) < 25)
                        {
                            ene.StatusEffects[2].EffectAmount += (float)(Helper.GetDistance(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2)) / 2.5f);
                        }
                    }
                    Health = 0;
                }
            }

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["CryoDetonator"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            sb.Draw(SceneMan.Textures["CryoDetonator"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X,(int)WidthHeight.Y), new Rectangle((int)WidthHeight.X, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors2[ShotBy.CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
