using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EnemyMeteor : EnemyBullet
    {
        private readonly int RandomRock;
        public EnemyMeteor(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            RandomRock = SceneMan.rand.Next(0, 2);
            switch (RandomRock)
            {
                case 0:
                    WidthHeight = new Vector2(10, 9);
                    break;
                case 1:
                    WidthHeight = new Vector2(10, 10);
                    break;
            }
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
            SceneMan.Particles.Add(new FlameParticle(new Vector2(SceneMan.rand.Next((int)Pos.X+2, (int)(Pos.X + WidthHeight.X)-2), SceneMan.rand.Next((int)Pos.Y+1, (int)(Pos.Y + WidthHeight.Y))-1),SceneMan));
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
            //Collision with the floor
            if (Pos.Y > (162-WidthHeight.Y))
            {
                Health = 0;
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(-0.5f, -0.5f), ShotBy,SceneMan));// up left
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(-0.25f, -0.75f), ShotBy, SceneMan));// up leftish
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0, -1), ShotBy, SceneMan));// UP
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0.25f, -0.75f), ShotBy, SceneMan));// up rightish
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (WidthHeight.X / 2), Pos.Y + (WidthHeight.Y / 2)), new Vector2(0.5f, -0.5f), ShotBy, SceneMan));// up right
            }
            //collision with player bullets
            foreach (Bullet bull in SceneMan.Bullets)
            {
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.Y, (int)WidthHeight.Y, (int)bull.Pos.X, (int)bull.Pos.Y, (int)bull.WidthHeight.X, (int)bull.WidthHeight.Y))
                {
                    Health = 0;
                    bull.Health -= 1;
                }
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
            switch (RandomRock)
            {
                case 0:
                    sb.Draw(SceneMan.Textures["MeteorologistRocks"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(255, 128, 128), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 1:
                    sb.Draw(SceneMan.Textures["MeteorologistRocks"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(10, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(255, 128, 128), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
            }
            if (Pos.Y + WidthHeight.Y < 0)
            {
                sb.DrawString(SceneMan.Pico8, "!", new Vector2((float)Math.Ceiling(Pos.X + (WidthHeight.X / 2)), 3), Color.DarkRed,0f,new Vector2(0,0),2F,SpriteEffects.None,0.1f);
                sb.DrawString(SceneMan.Pico8, "!", new Vector2((float)Math.Ceiling(Pos.X + (WidthHeight.X / 2)), 2), Color.Red, 0f, new Vector2(0, 0), 2F, SpriteEffects.None, 0.05f);
            }
        }
    }
}
