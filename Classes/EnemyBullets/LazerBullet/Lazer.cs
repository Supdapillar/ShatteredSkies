using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Lazer : EnemyBullet
    {
        private int Orientation = 0;
        private LazerBullet LBullet;

        public Lazer(Vector2 PS, Vector2 D, LazerBullet Ebull, int orientation, Enemy shotBy, SceneManager Sceneman)
        {
            SceneMan = Sceneman;
            Orientation = orientation;
            LBullet = Ebull;
            Health = 9999;
            Damage = 1f;

            switch (Orientation)
            {
                case 0:
                    WidthHeight = new Vector2(2,400);
                    break;
                case 1:
                    WidthHeight = new Vector2(2, 400);
                    break;
                case 2:
                    WidthHeight = new Vector2(400, 2);
                    break;
                case 3:
                    WidthHeight = new Vector2(400, 2);
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
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            switch (Orientation)
            {
                case 0:
                    WidthHeight = new Vector2(2, 400);
                    Pos = new Vector2(LBullet.Pos.X+1, LBullet.Pos.Y-402);
                    break;
                case 1:
                    WidthHeight = new Vector2(2, 400);
                    Pos = new Vector2(LBullet.Pos.X+1, LBullet.Pos.Y+6);
                    break;
                case 2:
                    WidthHeight = new Vector2(400, 2);
                    Pos = new Vector2(LBullet.Pos.X-402, LBullet.Pos.Y+1);
                    break;
                case 3:
                    WidthHeight = new Vector2(400, 2);
                    Pos = new Vector2(LBullet.Pos.X+5, LBullet.Pos.Y+1);
                    break;
            }
            if (TimeSinceCreation > 3 || !SceneMan.EnemyBullets.Contains(LBullet))
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(SceneMan.Pico8, LBullet.Health.ToString(), new Vector2((int)33, (int)56), Color.White, 0f, new Vector2(0, 0), 1F, SpriteEffects.None, 0.1f);
            switch (Orientation)
            {
                case 0://ip
                    sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y, 2, 400), new Rectangle(0, 0, 1, 1), new Color(1f, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 1://down
                    sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y, 2, 400), new Rectangle(0, 0, 1, 1), new Color(1f, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 2://left
                    sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y, 400, 2), new Rectangle(0, 0, 1, 1), new Color(1f, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
                case 3://right
                    sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y, 400, 2), new Rectangle(0, 0, 1, 1), new Color(1f, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                    break;
            }
        }
    }
}
