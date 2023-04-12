using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Shield
    {
        public Vector2 Pos;
        private Vector2 Delta;
        private Enemy ConnectedEnemy;
        private SceneManager SceneMan;

        public Vector2 Offset;

        private float Health = 10;

        private double RechargeTime;
        public Shield(Enemy ene,Vector2 offset, SceneManager Scenemana)
        {
            ConnectedEnemy = ene;
            SceneMan = Scenemana;
            Pos = new Vector2(SceneMan.rand.Next((int)(ConnectedEnemy.Pos.X-10),(int)(ConnectedEnemy.Pos.X + ConnectedEnemy.WidthHeight.X + 10)), SceneMan.rand.Next((int)(ConnectedEnemy.Pos.Y - 10), (int)(ConnectedEnemy.Pos.Y + ConnectedEnemy.WidthHeight.Y + 10)));
            Offset = offset;
        }

        public void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;



            double Angle = Helper.GetRadiansOfTwoPoints(Pos,ConnectedEnemy.Pos + Offset);
            Delta.X += (float)(Math.Cos(Angle)/30);
            Delta.Y += (float)(Math.Sin(Angle)/30);


            //Recharges
            if (RechargeTime >= 7.5f)
            {
                Health = 10f;
                RechargeTime = 0;
            }

            //collision with bullets
            if (Health > 0)
            {
                for (int g = 0; g < SceneMan.Bullets.Count; g++)
                {
                    if (SceneMan.Bullets[g].Health > 0 && SceneMan.Bullets[g].Health < 2)
                    {
                        if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, 14, 5, (int)SceneMan.Bullets[g].Pos.X, (int)SceneMan.Bullets[g].Pos.Y, (int)SceneMan.Bullets[g].WidthHeight.X, (int)SceneMan.Bullets[g].WidthHeight.Y))
                        {
                            Health -= SceneMan.Bullets[g].Damage;
                            SceneMan.Bullets[g].Health -= 1;
                        }
                    }
                }
            }
            else
            {
                RechargeTime += GT.ElapsedGameTime.TotalSeconds;
            }


            //sliiiiiide
            Delta /= 1.01f;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["Shield"], new Rectangle((int)Pos.X, (int)Pos.Y, (int)14, 5), new Rectangle(0, 0, 14, 5), new Color(0.25f, 0.25f, (Health / 7.5f) + 0.25f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
