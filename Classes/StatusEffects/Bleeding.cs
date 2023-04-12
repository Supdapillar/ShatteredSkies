using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Bleeding : StatusEffect
    {
        private double BleedTimer = 0;
        public Bleeding(Enemy enem) : base(enem)
        {
            Host = enem;
        }
        public override void Update(GameTime GT)
        {
            BleedTimer += GT.ElapsedGameTime.TotalSeconds * EffectAmount;
            if (BleedTimer >= 1)
            {
                Host.SceneMan.Bullets.Add(new BloodBullet(1, new Vector2(Host.Pos.X+(Host.WidthHeight.X/2), Host.Pos.Y+Host.WidthHeight.Y+3), Host.SceneMan, Host.SceneMan.Players[0]));
                Host.Health -= 0.5f;
                BleedTimer -= 1;
                EffectAmount -= 0.25f;
            }
        }
        public override void Draw(SpriteBatch sb) //+ Host.WidthHeight.X/2 - Host.SceneMan.Textures["BleedEffect"].Width/2
        {
            if (Host.WidthHeight.X % 2 == 0)
            {
                sb.Draw(Host.SceneMan.Textures["BleedEffect2Wide"], new Rectangle((int)Math.Ceiling(Host.Pos.X) + (int)Host.WidthHeight.X / 2 - Host.SceneMan.Textures["BleedEffect2Wide"].Width / 2, (int)Math.Ceiling(Host.Pos.Y) + (int)Host.WidthHeight.Y / 2 - Host.SceneMan.Textures["BleedEffect2Wide"].Height / 2, Host.SceneMan.Textures["BleedEffect2Wide"].Width, Host.SceneMan.Textures["BleedEffect2Wide"].Height), new Rectangle(0, 0, Host.SceneMan.Textures["BleedEffect2Wide"].Width, Host.SceneMan.Textures["BleedEffect2Wide"].Height), new Color(EffectAmount / 2f, 0f, 0f, EffectAmount / 2f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
            else
            {
                sb.Draw(Host.SceneMan.Textures["BleedEffect"], new Rectangle((int)Math.Ceiling(Host.Pos.X) + (int)Host.WidthHeight.X / 2 - Host.SceneMan.Textures["BleedEffect"].Width / 2, (int)Math.Ceiling(Host.Pos.Y) + (int)Host.WidthHeight.Y / 2 - Host.SceneMan.Textures["BleedEffect"].Height / 2, Host.SceneMan.Textures["BleedEffect"].Width, Host.SceneMan.Textures["BleedEffect"].Height), new Rectangle(0, 0, Host.SceneMan.Textures["BleedEffect"].Width, Host.SceneMan.Textures["BleedEffect"].Height), new Color(EffectAmount / 2f, 0f, 0f, EffectAmount / 2f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);

            }
        }
    }
}
