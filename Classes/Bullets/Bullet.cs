using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Bullet
    {
        public Vector2 Pos;
        public Vector2 WidthHeight;
        public Vector2 Delta;

        public int RandomNumber;
        public int Type;
        public int SubType;
        public float LifeSpan = 25;
        public int Health = 1;
        public float Damage = 1;

        public float ProcChance = 0;

        public dynamic ShotBy; //This is for telling what shot a bullet

        public Dictionary<string, float> OnHitEffects = new Dictionary<string, float>();

        public SceneManager SceneMan;

        //Local storage Relics
        public List<dynamic> LocalRelics = new List<dynamic>();

        public Bullet(int subtype, Vector2 pos, SceneManager sceneman)
        {

        }


        public virtual void Update(GameTime GT)
        {
        }
        public virtual void Draw(SpriteBatch sb)
        {
        }
        public virtual void Constructor(int subtype, Vector2 pos, SceneManager sceneman, dynamic shotby)
        {

        }
    }
}
