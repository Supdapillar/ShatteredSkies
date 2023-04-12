using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Sparklers : Wings
    {
        public Sparklers(SceneManager sceneman) : base(sceneman)
        {
            MaxDelay = 10f;
            SceneMan = sceneman;
            Width = 6;
            Height = 13;
            WingTexture = SceneMan.Textures["Sparklers"];
        }
        public override void Activated(Player play, GameTime GT)
        {
            if (play.LastState.Buttons.Y != ButtonState.Pressed)
            {
                AbilityActivated = !AbilityActivated;
            }
        }
        public override void Update(Player play, GameTime GT)
        {
            if (AbilityActivated)
            {
                if (play.AbilityDelay < 10)
                {
                    play.AbilityDelay += GT.ElapsedGameTime.TotalSeconds * 4.33;
                }
                else
                {
                    AbilityActivated = false;
                }
            }
        }

        public override void DrawUI(SpriteBatch sb)
        {
        }
    }
}
