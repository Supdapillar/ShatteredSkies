using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Magic : Relic
    {
        //Local Storage Variables
        public List<Bullet> MagicBullets = new List<Bullet>();// caps at 3 | 5
        public float MagicBulletTimer = 0f;
        //Global Variables
        public Magic(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            //make sure this doesnt overlap with any other relics that draw in the future

            ModifiesBulletOnDeath = true;
            ModifiesPlayerUpdate = true;
            ModifiesPlayerDraw = true;
            //makes sure all premade players get the relic
            ConnectedPlayer.LocalRelics.Add(new Magic(PowerLevel, SceneMan, ConnectedPlayer, true));
            for (int i = 0; i < ConnectedPlayer.LocalRelics.Count; i++)
            {
                if (ConnectedPlayer.LocalRelics[i] is Magic)
                {
                    foreach (Bullet bul in SceneMan.Bullets)
                    {
                        if (bul is MagicBullet && bul.ShotBy == ConnectedPlayer)
                        {
                            ConnectedPlayer.LocalRelics[i].MagicBullets.Add(bul);
                        }
                    }
                }
            }
        }
        //for the local constructor
        public Magic(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power; 
            SceneMan = sceneman;
            ConnectedPlayer = play;
        }

        public override void ModPlayUpdate(Player Play, GameTime GT)
        {
            for (int i = 0; i < Play.LocalRelics.Count; i++)
            {
                if (Play.LocalRelics[i] is Magic)
                {
                    //Increase timer speed with powerlevel
                    if (PowerLevel > 2) // MAX LEVEL
                    {
                        if (Play.LocalRelics[i].MagicBulletTimer <= 7)
                        {
                            Play.LocalRelics[i].MagicBulletTimer += GT.ElapsedGameTime.TotalSeconds * 12;
                        }
                    }
                    else if (PowerLevel > 1)// LEVEL 2
                    {
                        if (Play.LocalRelics[i].MagicBulletTimer <= 7)
                        {
                            Play.LocalRelics[i].MagicBulletTimer += GT.ElapsedGameTime.TotalSeconds * 6;
                        }
                    }
                    else // LEVEL 1
                    {
                        if (Play.LocalRelics[i].MagicBulletTimer <= 7)
                        {
                            Play.LocalRelics[i].MagicBulletTimer += GT.ElapsedGameTime.TotalSeconds * 3;
                        }
                    }
                    //spawn the bullet if time lit
                    if (Play.LocalRelics[i].MagicBulletTimer >= 7)
                    {
                        if (PowerLevel > 2) // max level
                        {
                            if (Play.LocalRelics[i].MagicBullets.Count <= 11)
                            {
                                Play.LocalRelics[i].MagicBulletTimer = 0;
                                SceneMan.Bullets.Add(new MagicBullet(0, Play.Pos, SceneMan, Play, Play));
                            }
                        }
                        else if (PowerLevel > 1) // level 2
                        {
                            if (Play.LocalRelics[i].MagicBullets.Count <= 5)
                            {
                                Play.LocalRelics[i].MagicBulletTimer = 0;
                                SceneMan.Bullets.Add(new MagicBullet(0, Play.Pos, SceneMan, Play, Play));
                            }
                        }
                        else // level 1
                        {
                            if (Play.LocalRelics[i].MagicBullets.Count <= 2)
                            {
                                Play.LocalRelics[i].MagicBulletTimer = 0;
                                SceneMan.Bullets.Add(new MagicBullet(0, Play.Pos, SceneMan, Play, Play));
                            }
                        }
                    }
                    ConnectedPlayer.LocalRelics[i].MagicBullets.Clear();
                }
                foreach (Bullet bul in SceneMan.Bullets)
                {
                    if (bul is MagicBullet && bul.ShotBy == ConnectedPlayer)
                    {
                        ConnectedPlayer.LocalRelics[i].MagicBullets.Add(bul);
                    }
                }

                double angle = 0;
                if (Play.LocalRelics[i].MagicBullets.Count > 0)
                {
                    angle = Play.LocalRelics[i].MagicBullets[0].Angle;
                }
                foreach (MagicBullet Mbul in Play.LocalRelics[i].MagicBullets)
                {
                    angle += (Math.PI*2)/ Play.LocalRelics[i].MagicBullets.Count;

                    Mbul.Angle = angle;
                }
            }
            
        }
        public override void ModPlayDraw(Player Play, SpriteBatch sb)
        {
            for (int i = 0; i < Play.LocalRelics.Count; i++)
            {
                if (Play.LocalRelics[i] is Magic)
                {
                    if (SceneMan.game.Scene == 2)
                    {
                        sb.DrawString(SceneMan.Pico8, Play.LocalRelics[i].MagicBullets.Count.ToString(),new Vector2(40,60),Color.Aqua);
                        sb.Draw(SceneMan.Textures["MagicUI"], new Rectangle(1, 26, 16, 9), new Rectangle(0, 0, 16, 9), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                        sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(9, 29, (int)Play.LocalRelics[i].MagicBulletTimer, 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors1[2], 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                        sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(9, 30, (int)Play.LocalRelics[i].MagicBulletTimer, 1), new Rectangle(0, 0, 1, 1), new Color(128, 0, 128, 255), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override void ModBulOnDeath(Bullet bul)
        {
            if (bul is MagicBullet)
            {
                if (bul.ShotBy == ConnectedPlayer)
                {
                    for (int i = 0; i < ConnectedPlayer.LocalRelics.Count; i++)
                    {
                        if (ConnectedPlayer.LocalRelics[i] is Magic)
                        {
                            ConnectedPlayer.LocalRelics[i].MagicBullets.Remove(bul);
                        }
                    }
                }
            }
        }
    }
}
