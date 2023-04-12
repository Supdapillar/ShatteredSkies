using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Core
    {
        public SceneManager SceneMan;
        //Stuff about the dimentions of the ship
        public int Width;
        public int Height;
        public Rectangle HitBox;
        public Texture2D CoreTexture;
        public Vector2[] WingOffset;
        //Whole buncha stats
        public List<Vector2> GunPositions = new List<Vector2>();
        public Stat Stats = new Stat();
        public Core(SceneManager sceneman)
        {

        }

        public virtual void Update(Player play, GameTime GT)
        {

        }

        public virtual void Draw(Player play, SpriteBatch sb)
        {

        }
    }
}
