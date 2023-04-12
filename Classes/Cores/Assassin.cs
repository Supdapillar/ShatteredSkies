using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Assassin : Core
    {
        public double Danger = 1f;
        public Assassin(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            //Stuff about the diamentions of the ship
            Width = 9;
            Height = 13;
            HitBox = new Rectangle(3, 5, 3, 3);
            CoreTexture = SceneMan.Textures["Assassin"];
            WingOffset = new Vector2[]
            {
            new Vector2(0,5),
            new Vector2(0,4),
            new Vector2(0,2),
            new Vector2(0,-3),
            new Vector2(0,-3),
            new Vector2(0,-1),
            };
            GunPositions.Add(new Vector2(4, 0));
            //Whole buncha stats
            Stats.Damage = 2f;
            Stats.Speed = 1.5f;
            Stats.MaxHealth = 5f;
        }
        public override void Update(Player play, GameTime GT)
        {
            double HighestDamage = 0;
            double Distance = 0;
            Danger = 1f;
            if (play.Pos.Y < 122)
            {
                Danger += (162-(play.Pos.Y)) / 324;
            }
            foreach(EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                Distance = Helper.GetDistance(new Vector2(Ebull.Pos.X + Ebull.WidthHeight.X / 2, Ebull.Pos.Y + Ebull.WidthHeight.Y / 2), new Vector2(play.Pos.X + 4.5f, play.Pos.Y + 6.5f));
                if (Distance < 75)
                {
                    if ((75 - Distance) / 150 > HighestDamage)
                    {
                        HighestDamage = (75 - Distance) / 150;
                    }
                }

                //ending
                if (Ebull == SceneMan.EnemyBullets[^1])
                {
                    Danger += HighestDamage;
                }
            }

            HighestDamage = 0;
            Distance = 0;
            foreach (Enemy Ene in SceneMan.Enemies)
            {
                Distance = Helper.GetDistance(new Vector2(Ene.Pos.X + Ene.WidthHeight.X / 2, Ene.Pos.Y + Ene.WidthHeight.Y / 2), new Vector2(play.Pos.X + 4.5f, play.Pos.Y + 6.5f));
                if (Distance < 75)
                {
                    if ((75 - Distance) / 150 > HighestDamage)
                    {
                        HighestDamage = (75 - Distance) / 150;
                    }
                }

                //ending
                if (Ene == SceneMan.Enemies[^1])
                {
                    Danger += HighestDamage;
                }
            }

            Stats.Damage = Danger / SceneMan.Players.Count;
            Stats.FireRate = 1f + (5 - play.Health)/7.5f;
        }

        public override void Draw(Player play, SpriteBatch sb)
        {
            //sb.DrawString(SceneMan.Pico8, "Damage: " + Danger.ToString(), new Vector2(233, 15), Color.Pink, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(0, 0, Width, Height), new Color(1f, 1f - (play.HitAniFade * 4), 1f - (play.HitAniFade * 4), 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width, 0, Width, Height), SceneMan.RelicsColors1[play.CurrentRelics[0]] * ((float)Danger - 0.75f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.29f);
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width * 2, 0, Width, Height), SceneMan.RelicsColors2[play.CurrentRelics[0]] * ((float)Danger - 0.75f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.28f);
        }
    }
}

