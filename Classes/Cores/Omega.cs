using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Omega : Core
    {
        private int DroneCount = 0;
        private double DroneDelay = 0;

        private double AllyBuff = 0f;
        public Omega(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            //Stuff about the diamentions of the ship
            Width = 13;
            Height = 14;
            HitBox = new Rectangle(5, 6, 3, 3);
            CoreTexture = SceneMan.Textures["Omega"];
            WingOffset = new Vector2[]
            {
            new Vector2(0,6),
            new Vector2(0,5),
            new Vector2(0,3),
            new Vector2(0,-1),
            new Vector2(0,-1),
            new Vector2(0,2),
            };
            GunPositions.Add(new Vector2(6, 1));
            /*
            "TITLE: OMEGA @@" +
            "TITLE: OMEGA @@" +
            "POSITIVES: @" +
            "CONSTRUCTS FRIENDLY DRONES " +
            "+50% ALLY SPEED, FIRERATE, DAMAGE AND ARMOR @" +
            "NEGATIVES: @" +
            "ONLY GAINS POSITIVES WHEN AROUND ALLIES @" +
            "GAINS NEGITIVES WHEN FAR FROM ALLIES @" +
            "OMEGA DRONES DONT COUNT " +
            "-100% ACCELERATION @" +
            "-50% FIRERATE @" +
            "BULLETS HIT TWICE AS HARD @",
            */
            //Whole buncha stats
            //Stats.AllyDamage
        }
        public override void Update(Player play, GameTime GT)
        {
            double Distance = 0f;
            Stats.Damage = 1f / SceneMan.Players.Count;
            DroneDelay -= GT.ElapsedGameTime.TotalSeconds;

            DroneCount = 0;
            //Drone Stuff
            foreach (Ally Al in SceneMan.Allies)
            {
                if (Al is OmegaDrone)
                {
                    if (Al.CreatedBy == play)
                    {
                        DroneCount += 1;
                    }
                }
            }
            if (DroneDelay <= 0)
            {
                if (DroneCount <= 2)
                {
                    SceneMan.Allies.Add(new OmegaDrone(play.Pos, SceneMan, play));
                    DroneCount += 1 ;
                    DroneDelay = 10f;
                }
            }

            AllyBuff = 0;
            //Stat stuff
            foreach (Ally Al in SceneMan.Allies)
            {
                if (!(Al is OmegaDrone))
                {
                    Distance = Helper.GetDistance(new Vector2(Al.Pos.X + Al.WidthHeight.X / 2, Al.Pos.Y + Al.WidthHeight.Y / 2), new Vector2(play.Pos.X + 6.5f, play.Pos.Y + 7));
                    if (Distance < 75)
                    {
                        if ((75 - Distance) / 75 > AllyBuff)
                        {
                            AllyBuff = (75 - Distance) / 75; // a range from -0.5 to 0.5
                        }
                    }
                }
            }
            AllyBuff -= 0.5f;

            //Ending Stats
            if (AllyBuff < 0) // NEGITIVES
            {
                Stats.IncomingDamageMultiplier = 2f;
                Stats.FireRate = 0.5f;
                Stats.Accerleration = 0.30f;

                Stats.AllyDamage = 1f;
                Stats.AllyArmor = 0;
                Stats.AllyFireRate = 1f;
                Stats.AllySpeed = 1f;
            }
            else // POSSITIVES
            {
                Stats.IncomingDamageMultiplier = 1f;
                Stats.FireRate = 1f;
                Stats.Accerleration = 1f;

                Stats.AllyDamage = 1 + AllyBuff*1.4f;
                Stats.AllyArmor = AllyBuff * 1.4f;
                Stats.AllyFireRate = 1 + AllyBuff * 1.4f;
                Stats.AllySpeed = 1 + AllyBuff * 1.4f;
            }
        }
        public override void Draw(Player play, SpriteBatch sb)
        {
            //sb.DrawString(SceneMan.Pico8, "Damage: " + AllyBuff.ToString(), new Vector2(233, 15), Color.Pink, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
            if (play.IFrames > 0)
            {
                sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(0, 0, Width, Height), new Color(1f, 1f, 0f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else
            {
                sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(0, 0, Width, Height), new Color(1f, 1f - (play.HitAniFade * 4), 1f - (play.HitAniFade * 4), 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width, 0, Width, Height), SceneMan.RelicsColors1[play.CurrentRelics[0]] * (float)AllyBuff * 2, 0f, new Vector2(0, 0), SpriteEffects.None, 0.29f);
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width * 2, 0, Width, Height), SceneMan.RelicsColors2[play.CurrentRelics[0]] * (float)AllyBuff*2, 0f, new Vector2(0, 0), SpriteEffects.None, 0.28f);
        }
    }
}
