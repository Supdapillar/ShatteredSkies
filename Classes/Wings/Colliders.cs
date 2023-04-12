using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Colliders : Wings
    {
        public Colliders(SceneManager sceneman) : base(sceneman)
        {
            MaxDelay = 5f;
            SceneMan = sceneman;
            Width = 5;
            Height = 10;
            WingTexture = SceneMan.Textures["Colliders"];
        }
        public override void Activated(Player play, GameTime GT)
        {
            if (play.AbilityDelay <= 0)
            {
                SceneMan.Particles.Add(new CollidersParticle(new Vector2(play.Pos.X - SceneMan.Textures["Snap"].Width / 2, play.Pos.Y - SceneMan.Textures["Snap"].Height / 2), SceneMan));// star particles
                SceneMan.EnemyBullets.RemoveAll(i => Math.Sqrt(Math.Pow(play.Pos.X - i.Pos.X, 2) + Math.Pow(play.Pos.Y - i.Pos.Y, 2)) < 100);

                foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
                {
                    if (Helper.GetDistance(new Vector2(play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Width/2, play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Height/2),Ebull.Pos) < 100)
                    {
                        Ebull.Health = 0;
                    }
                }
                play.AbilityDelay = 5;
            }
        }
        public override void DrawUI(SpriteBatch sb)
        {

        }
    }
}
