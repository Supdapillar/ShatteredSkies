using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Burning : StatusEffect
    {
        public Burning(Enemy enem) : base(enem)
        {
            Host = enem;
        }
        public override void Update(GameTime GT)
        {
            if (EffectAmount > 0)
            {
                Host.Health -= EffectAmount / 8 * (float)GT.ElapsedGameTime.TotalSeconds;
                EffectAmount -= 0.25f * (float)GT.ElapsedGameTime.TotalSeconds;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Host.SceneMan.Textures["EffectIndicators"], new Rectangle((int)Math.Ceiling(Host.Pos.X-5), (int)Math.Ceiling(Host.Pos.Y - 9), 5, 5), new Rectangle(0, 0, 10, 10), new Color(EffectAmount / 5f, EffectAmount / 5f, EffectAmount / 5f, EffectAmount / 5f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}
