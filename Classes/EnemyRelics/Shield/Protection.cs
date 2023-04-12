using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Protection : EnemyRelic
    {

        private List<Shield> Shields = new List<Shield>();
        public Protection(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            Color = new Color(64,64,255,255);
            HealthIncrease = 2f;
            EndlessCostIncrease = 2.5f;
        }


        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            if (HealthIncrease != 1)
            {
                ene.Health *= HealthIncrease;
                ene.MaxHealth *= HealthIncrease;
                HealthIncrease = 1;
                Shields.Add(new Shield(ene, new Vector2(ene.WidthHeight.X + 1, ene.WidthHeight.Y + 5), SceneMan));
                Shields.Add(new Shield(ene,new Vector2(-14,ene.WidthHeight.Y + 5), SceneMan));
            }
            foreach (Shield Shi in Shields)
            {
                Shi.Update(GT);
            }
        }

        public override void ModEneDraw(Enemy ene, SpriteBatch sb)
        {
            foreach (Shield Shi in Shields)
            {
                MonoGame.Extended.ShapeExtensions.DrawLine(sb, new Vector2(ene.Pos.X + ene.WidthHeight.X / 2, ene.Pos.Y + ene.WidthHeight.Y / 2), new Vector2(Shi.Pos.X + 14 / 2, Shi.Pos.Y + 5 / 2), Color, 1, 0.6f);
                Shi.Draw(sb);
            }
        }
    }
}
