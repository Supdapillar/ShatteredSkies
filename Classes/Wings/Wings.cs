using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Wings
    {
        public SceneManager SceneMan;
        //Dimentions
        public int Width;
        public int Height;
        public Texture2D WingTexture;

        public double MaxDelay;
        public bool AbilityActivated;
        public Wings(SceneManager sceneman)
        {
        }

        public virtual void Update(Player play, GameTime GT)
        {

        }
        public virtual void Activated(Player play, GameTime GT)
        {

        }
        public virtual void DrawUI(SpriteBatch sb)
        {

        }
    }
}
