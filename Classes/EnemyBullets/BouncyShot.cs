using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class BouncyShot : EnemyBullet
    {
        private int Bounces;
        public BouncyShot(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(4, 5);
            Bounces = 3;
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
                Erel.ModEneBulUpdate(this,GT);
            }

            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Delta.X = -Delta.X;
                Bounces -= 1;
            }
            else if (Pos.X > 284) // Right Wall
            {
                Delta.X = -Delta.X;
                Bounces -= 1;
            }
            if (Pos.Y < 0) //Top wall
            {
                Delta.Y = -Delta.Y;
                Bounces -= 1;
            }
            else if (Pos.Y > 162 - 4) // Bottom Wall
            {
                Delta.Y = -Delta.Y;
                Bounces -= 1;
            }
            if (Bounces <= 0)
            {
                Health = 0;
            }
            if (TimeSinceCreation >= 9999)
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
            sb.Draw(SceneMan.Textures["Bouncy"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(255, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
        }
    }
}
