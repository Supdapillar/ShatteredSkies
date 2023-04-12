using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Cloud : EnemyBullet
    {

        private Vector2 Gotopos;


        public Cloud(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(SceneMan.Textures["Cloud"].Width, SceneMan.Textures["Cloud"].Height);
            ShotBy = shotBy;
            Gotopos = new Vector2(SceneMan.rand.Next(16, (int)(288 - WidthHeight.X - 16)), SceneMan.rand.Next(81 - 30, 81 + 10));
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;

            double Angle = Helper.GetRadiansOfTwoPoints(Pos,Gotopos);

            Delta.X += (float)(Math.Cos(Angle) * GT.ElapsedGameTime.TotalSeconds);
            Delta.Y += (float)(Math.Sin(Angle) * GT.ElapsedGameTime.TotalSeconds);

            if (Helper.GetDistance(Pos,Gotopos) < 5)
            {
                Gotopos = new Vector2(SceneMan.rand.Next(16, (int)(288 - WidthHeight.X-16)), SceneMan.rand.Next(81 - 30, 81 + 10));
            }

            if (SceneMan.rand.Next(0,10)==0)
            {
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(SceneMan.rand.Next((int)Pos.X,(int)(Pos.X+WidthHeight.X)),Pos.Y+WidthHeight.Y+2),new Vector2(0,1),ShotBy,SceneMan));
            }

            Delta /= 1.01f;
        }
        public override void Draw(SpriteBatch sb)
        {
            //Enemy relic Mod Enemy Bullet Draw
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["Cloud"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(255, 255, 255), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
        }
    }
}
