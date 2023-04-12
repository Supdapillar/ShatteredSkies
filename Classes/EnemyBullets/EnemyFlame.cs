using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EnemyFlame : EnemyBullet
    {

        private readonly Animation FlameAnimation;
        private int RandomFlip;
        private double RandomTime;
        public EnemyFlame(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(2, 2);
            FlameAnimation = new Animation(SceneMan.Textures["EnemyFlame"],4+SceneMan.rand.NextDouble(), 4, false);
            Damage = 0.2f;
            RandomFlip = SceneMan.rand.Next(0, 2);
            RandomTime = Sceneman.rand.NextDouble();
            ShotBy = shotBy;
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            FlameAnimation.Update(GT);
            //Relic Mod Enemy Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyBulletUpdate)
                {
                    rel.ModEneBulUpdate(this, GT);
                }
            }
            //Enemy relic Mod Enemy Bullet Update
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulUpdate(this, GT);
            }
            if (TimeSinceCreation >= 0.60f+ (RandomTime/4))
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            //Enemy relic Mod Enemy Bullet Draw
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulDraw(this, sb);
            }
            if (RandomFlip == 0)
            {
                FlameAnimation.Draw(sb, new Vector2(Pos.X-2,Pos.Y-2), 0.4f, new Color(1f, 0.5f, 0, 0), SpriteEffects.FlipHorizontally, SceneMan);
            }
            else
            {
                FlameAnimation.Draw(sb, new Vector2(Pos.X - 2, Pos.Y - 2), 0.4f, new Color(1f, 0.5f, 0, 0), SpriteEffects.None, SceneMan);
            }
            //sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 2, 2), new Rectangle(0, 0, 5, 5), new Color(1f, 0, 1f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
            //sb.Draw(SceneMan.Textures["BulletSheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 3, 4), new Rectangle(0, 0, 3, 4), new Color(255, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
        }
    }
}
