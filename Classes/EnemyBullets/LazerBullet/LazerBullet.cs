using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShatteredSkies.Classes
{
    public class LazerBullet : EnemyBullet
    {
        private int ShotDirection;
        private double ShotDelay = 2;
        public LazerBullet(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(4, 5);
            ShotDirection = Sceneman.rand.Next(0, 4);
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
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
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
            if (ShotDelay <= 0)
            {
                switch (ShotDirection)//Creates a lazer
                {
                    case 0://UP
                        SceneMan.EnemyBullets.Add(new Lazer(Pos, Pos, this,0, ShotBy,SceneMan));
                        break;
                    case 1://DOWN
                        SceneMan.EnemyBullets.Add(new Lazer(Pos, Pos, this, 1, ShotBy, SceneMan));
                        break;
                    case 2://LEFT
                        SceneMan.EnemyBullets.Add(new Lazer(Pos, Pos, this, 2, ShotBy, SceneMan));
                        break;
                    case 3://RIGHT
                        SceneMan.EnemyBullets.Add(new Lazer(Pos, Pos, this, 3, ShotBy, SceneMan));
                        break;
                }
                ShotDelay = 5;
                ShotDirection = SceneMan.rand.Next(0,4);
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
            sb.Draw(SceneMan.Textures["LazerBullet"], new Rectangle((int)Pos.X, (int)Pos.Y, 4, 5), new Rectangle(0, 0, 4, 5), new Color(255, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            //sb.DrawString(SceneMan.Pico8, ShotDelay.ToString(), new Vector2((int)33, (int)56), new Color(1f, (float)(ShotDelay % (ShotDelay / 3)), (float)(ShotDelay % (ShotDelay / 3)), 1f), 0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.1f);
            if (ShotDelay < 2)
            {
                switch (ShotDirection)
                {
                    case 0://UP
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)Pos.X - 1, (int)Pos.Y - 12),
                        new Color(1f, (float)(ShotDelay % (0.5f) * 2), (float)(ShotDelay % (0.5f) * 2), 1f), 0f, new Vector2(0, 0), 2F, SpriteEffects.None, 0.1f);
                        break;
                    case 1://DOWN
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)Pos.X + 5, (int)Pos.Y + 17),
                        new Color(1f, (float)(ShotDelay % (0.5f) * 2), (float)(ShotDelay % (0.5f) * 2), 1f), (float)Math.PI, new Vector2(0, 0), 2F, SpriteEffects.None, 0.1f);
                        break;
                    case 2://LEFT
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)Pos.X - 12, (int)Pos.Y + 5),
                        new Color(1f, (float)(ShotDelay % (0.5f) * 2), (float)(ShotDelay % (0.5f) * 2), 1f), (float)-Math.PI / 2, new Vector2(0, 0), 2F, SpriteEffects.None, 0.1f);
                        break;
                    case 3://RIGHT
                        sb.DrawString(SceneMan.Pico8, "!", new Vector2((int)Pos.X + 16, (int)Pos.Y - 1),
                        new Color(1f, (float)(ShotDelay % (0.5f) * 2), (float)(ShotDelay % (0.5f) * 2), 1f), (float)Math.PI / 2, new Vector2(0, 0), 2F, SpriteEffects.None, 0.1f);
                        break;
                }
            }
        }
    }
}
