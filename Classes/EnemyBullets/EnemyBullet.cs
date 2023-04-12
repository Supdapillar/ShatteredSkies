using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class EnemyBullet
    {
        public Vector2 Pos;
        public Vector2 Delta;

        public Vector2 WidthHeight;

        public int Type;
        public float TimeSinceCreation = 0;
        public float Health = 1;
        public float Damage = 1;

        public SceneManager SceneMan;

        //Local storage Relics
        public List<dynamic> LocalEnemyRelics = new List<dynamic>();

        public Color ParticleColor = new Color(255, 255, 255);

        public Texture2D EnemyBulletPic;

        public Enemy ShotBy;

        public virtual void Update(GameTime GT)
        {
        }
        public virtual void Draw(SpriteBatch sb)
        {
        }
    }
}
