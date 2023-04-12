using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class PlasmaShockParticle : Particle
    {
        private Enemy LockedOnEnemy;
        public PlasmaShockParticle(Vector2 pos, Enemy ene, SceneManager sceneman) : base(pos, sceneman)
        {
            SceneMan = sceneman;
            LockedOnEnemy = ene;
        }

        public override void Update(GameTime GT)
        {
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation > 0.5f)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["PlasmaShock"], new Rectangle((int)(LockedOnEnemy.Pos.X) + (int)LockedOnEnemy.WidthHeight.X / 2 - SceneMan.Textures["PlasmaShock"].Width / 2, (int)(LockedOnEnemy.Pos.Y) + (int)LockedOnEnemy.WidthHeight.Y / 2 - SceneMan.Textures["PlasmaShock"].Height / 2,
                SceneMan.Textures["PlasmaShock"].Width, SceneMan.Textures["PlasmaShock"].Height), new Rectangle(0, 0 ,SceneMan.Textures["PlasmaShock"].Width, SceneMan.Textures["PlasmaShock"].Height),
                Color.White * ((0.5f - TimeSinceCreation) * 4), 0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }

    }
}
