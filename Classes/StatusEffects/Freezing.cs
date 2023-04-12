using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Freezing : StatusEffect
    {
        public Freezing(Enemy enem) : base(enem)
        {
            Host = enem;
        }
        public override void Update(GameTime GT)
        {
            if (EffectAmount < 5) 
            {
                Host.Pos.X -= Host.Delta.X * (EffectAmount / 5);
                Host.Pos.Y -= Host.Delta.Y * (EffectAmount/5);
                Host.ShotDelay += GT.ElapsedGameTime.TotalSeconds * (EffectAmount / 5);
            }
            else
            {
                Host.Delta.X = 0;
                Host.Delta.Y = 0;
                Host.ShotDelay += GT.ElapsedGameTime.TotalSeconds;
            }
            EffectAmount -= (float)GT.ElapsedGameTime.TotalSeconds * 2;
            if (EffectAmount < 0)
            {
                EffectAmount = 0;
            }
        }
        public override void Draw(SpriteBatch sb) //+ Host.WidthHeight.X/2 - Host.SceneMan.Textures["BleedEffect"].Width/2
        {
            sb.Draw(Host.SceneMan.Textures["EffectIndicators"], new Rectangle((int)Math.Ceiling(Host.Pos.X), (int)Math.Ceiling(Host.Pos.Y-9),5,5), new Rectangle(10, 0, 10, 10), new Color(EffectAmount / 5f, EffectAmount / 5f, EffectAmount / 5f, EffectAmount / 5f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}
