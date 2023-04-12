using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Curse : EnemyRelic
    {
        private double Cooldown;
        private double ClosestDistance;
        public Curse(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(128,0,255,255);
            HealthIncrease = 1.6f;
            EndlessCostIncrease = 2;
            Cooldown = 0;
        }


        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            bool CanCurse = false;
            double Distance;
            ClosestDistance = 500;
            foreach (Ally Al in SceneMan.Allies)
            {
                Distance = Helper.GetDistance(Helper.CenterActor(ene.Pos, ene.WidthHeight), Helper.CenterActor(Al.Pos, Al.WidthHeight));
                if (Distance < 50 + 12.5f * ene.Size)
                {
                    ClosestDistance = 50;
                    CanCurse = true;
                    break;
                }
                else if (Distance < ClosestDistance)
                {
                    ClosestDistance = Distance;
                }
            }
            foreach (Player play in SceneMan.Players)
            {
                Distance = Helper.GetDistance(Helper.CenterActor(ene.Pos, ene.WidthHeight), Helper.CenterPlayer(play));
                if (Distance < 50 + 12.5f * ene.Size)
                {
                    ClosestDistance = 50;
                    CanCurse = true;
                    break;
                }
                else if (Distance < ClosestDistance)
                {
                    ClosestDistance = Distance;
                }
            }


            //charging a curse
            if (CanCurse)
            {
                Cooldown += GT.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Cooldown = 0;
            }

            //cursing
            if (Cooldown >= 1)
            {
                Cooldown = 0;
                foreach (Ally Al in SceneMan.Allies)
                {
                    if (Helper.GetDistance(Helper.CenterActor(ene.Pos, ene.WidthHeight), Helper.CenterActor(Al.Pos, Al.WidthHeight)) < 50+ 12.5f * ene.Size)
                    {
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(Helper.CenterActor(Al.Pos, Al.WidthHeight),new Vector2(0,0),ene,SceneMan));
                    }
                }
                foreach (Player play in SceneMan.Players)
                {
                    if (Helper.GetDistance(Helper.CenterActor(ene.Pos, ene.WidthHeight), Helper.CenterPlayer(play)) < 50 + 12.5f * ene.Size)
                    {
                        SceneMan.EnemyBullets.Add(new EnemyBasicShot(Helper.CenterPlayer(play), new Vector2(0, 0), ene, SceneMan));
                    }
                }
            }
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
        }

        public override void ModEneDraw(Enemy ene, SpriteBatch sb)
        {
            if (ClosestDistance <= 50 + 12.5 * ene.Size)
            {
                MonoGame.Extended.ShapeExtensions.DrawCircle(sb, new Vector2((int)(ene.Pos.X + ene.WidthHeight.X / 2), (int)(ene.Pos.Y + ene.WidthHeight.Y / 2)), 50 + 12.5f * ene.Size, 100, Color, 0.5f, 0.3f);
            }
            else if (ClosestDistance <= 100 + 25 * ene.Size)
            {
                MonoGame.Extended.ShapeExtensions.DrawCircle(sb, new Vector2((int)(ene.Pos.X + ene.WidthHeight.X / 2), (int)(ene.Pos.Y + ene.WidthHeight.Y / 2)), 50 + 12.5f * ene.Size, 100, Color * (float)(((50 + 12.5f * ene.Size) - (ClosestDistance-(50 + 12.5f * ene.Size)))/50), 0.5f, 0.3f);
            }
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)ene.Pos.X, (int)ene.Pos.Y - 4, (int)((Cooldown) * ene.WidthHeight.X), 1), new Rectangle(0, 0, 1, 1), Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }





    }
}
