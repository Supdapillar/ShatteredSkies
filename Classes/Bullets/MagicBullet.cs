using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class MagicBullet : Bullet
    {
        private Player Play;
        private double Speed;
        public double Angle = 0;
        public override void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {
            Pos = pos;
            SceneMan = sceneman;
            SubType = subtype;
            WidthHeight = new Vector2(5, 6);
            ShotBy = shotby;

            Health = 3;
            Damage = 1.5f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.Damage * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.BulletDamage;
            ProcChance = 1f * (float)ShotBy.AllCores[ShotBy.CurrentShipParts[0]].Stats.ProcPercent;

            Speed = 4.5f;

            //Enemy Mod Bullet Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletConstructor)
                {
                    rel.ModBulCons(this);
                }
            }
        }
        public MagicBullet(int subtype, Vector2 pos, SceneManager sceneman, Player play, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Play = play;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public MagicBullet(int subtype, Vector2 pos, SceneManager sceneman, Player play, List<dynamic> localRelics, dynamic shotby) : base(subtype, pos, sceneman)
        {
            Play = play;
            LocalRelics = localRelics;
            Constructor(subtype, pos, sceneman, shotby);
        }

        public override void Update(GameTime GT)
        {
            Angle += (float)(GT.ElapsedGameTime.TotalSeconds * Speed);
            Pos.X = (float)(Play.Pos.X + (Math.Cos(Angle) * 25));
            Pos.Y = (float)(Play.Pos.Y + (Math.Sin(Angle) * 25));
            // Relic Mod Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesBulletUpdate)
                {
                    rel.ModBulUpdate(this, GT);
                }
            }
            //kool hip particle
            SceneMan.Particles.Add(new ColoredParticle
            (
                new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2),
                new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) / 2, (float)(SceneMan.rand.NextDouble() - 0.5f) / 2),
                SceneMan,
                SceneMan.RelicsColors1[2],
                true,
                1f

            ));
            //Collide With Enemy Bullets
            //enemy bullet collision
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Ebull.Health > 0)
                {
                    if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                    {
                        Health -= 1;
                        Ebull.Health -= 999;
                    }
                }

            }

            if (LifeSpan <= 0)
            {
                Health = 0;
            }
            if (Play == null)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(3, 0, (int)WidthHeight.X, (int)WidthHeight.Y), SceneMan.RelicsColors1[2], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
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
