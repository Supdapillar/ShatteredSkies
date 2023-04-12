using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Fragmentisers : Wings
    {
        public Fragmentisers(SceneManager sceneman) : base(sceneman)
        {
            MaxDelay = 10f;
            SceneMan = sceneman;
            Width = 8;
            Height = 15;
            WingTexture = SceneMan.Textures["Fragmentisers"];
        }
        public override void Activated(Player play, GameTime GT)
        {
            if (play.AbilityDelay <= 2)
            {
                AbilityActivated = true;
            }
        }

        public override void Update(Player play, GameTime GT)
        {
            if (AbilityActivated && play.AbilityDelay < 10)
            {
                play.AbilityDelay += GT.ElapsedGameTime.TotalSeconds * 6;
                SceneMan.Particles.Add(new FragmentisersParticle(new Vector2(Helper.CenterPlayer(play).X - (SceneMan.Textures["Snap"].Height / 8), Helper.CenterPlayer(play).Y - SceneMan.Textures["Snap"].Height / 8), SceneMan)) ;
                ConvertBullets(play, GT); // 
            }
            else
            {
                AbilityActivated = false;
            }
        }
        public override void DrawUI(SpriteBatch sb)
        {

        }

        private void ConvertBullets(Player play, GameTime GT)
        {
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Math.Sqrt(Math.Pow((play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Width / 2) - Ebull.Pos.X + 1, 2) + Math.Pow((play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2) - Ebull.Pos.Y + 1, 2)) < 25)
                {
                    if (Ebull.Health > 0)
                    {
                        Ebull.Health = 0;
                        switch (Ebull.GetType().ToString())
                        {
                            case "ShatteredSkies.Classes.EnemyBasicShot":
                                SceneMan.Bullets.Add(new BasicShotWeak(0, Ebull.Pos, new Vector2(Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                SceneMan.Bullets.Add(new BasicShotWeak(0, Ebull.Pos, new Vector2(-Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                break;
                            case "ShatteredSkies.Classes.BouncyShot":
                                SceneMan.Bullets.Add(new BouncyShotAlly(0, Ebull.Pos, new Vector2(Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                SceneMan.Bullets.Add(new BouncyShotAlly(0, Ebull.Pos, new Vector2(-Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                break;
                            case "ShatteredSkies.Classes.EnemyMeteor":
                                SceneMan.Bullets.Add(new EnemyMeteorAlly(0, Ebull.Pos, new Vector2(Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                SceneMan.Bullets.Add(new EnemyMeteorAlly(0, Ebull.Pos, new Vector2(-Ebull.Delta.X, -Ebull.Delta.Y), SceneMan, play));
                                Ebull.Pos.Y = 600;
                                break;
                            case "ShatteredSkies.Classes.KamikazeFall":
                                SceneMan.Bullets.Add(new KamikazeFallAlly(0, Ebull.Pos, new Vector2(Ebull.Delta.X * 3, -Ebull.Delta.Y), SceneMan, play));
                                SceneMan.Bullets.Add(new KamikazeFallAlly(0, Ebull.Pos, new Vector2(-Ebull.Delta.X * 3, -Ebull.Delta.Y), SceneMan, play));
                                break;
                        }
                    }
                }
            }
        }
    }
}
