using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class StatusEffect
    {
        public float EffectAmount = 0;
        public Enemy Host;

        public StatusEffect(dynamic obj)
        {
        }
        public virtual void Update(GameTime GT)
        {
        }
        public virtual void Draw(SpriteBatch sb)
        {

        }
    }
}
