using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Para : Core
    {
        private bool Charging = false;
        public Para(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            //Stuff about the diamentions of the ship
            Width = 9;
            Height = 9;
            HitBox = new Rectangle(3, 4, 3, 3);
            CoreTexture = SceneMan.Textures["Para"];
            WingOffset = new Vector2[]
            {
            new Vector2(0,1),
            new Vector2(0,1),
            new Vector2(0,-1),
            new Vector2(0,-5),
            new Vector2(0,-5),
            new Vector2(0,-2),
            };
            GunPositions.Add(new Vector2(4, 0));
            ///{"WingYOffset_0_Ship_1", 1},
            ///{"WingYOffset_1_Ship_1", 1},
            ///{"WingYOffset_2_Ship_1", -1},
            ///{"WingYOffset_3_Ship_1", -5},
            ///{"WingYOffset_4_Ship_1", -5},
            //Whole buncha stats
        }
        public override void Update(Player play, GameTime GT)
        {
            Stats.Damage = 1f / SceneMan.Players.Count;
            if ((play.CurrentBullets[play.CurrentSelectedBullet] == 6 || play.CurrentBullets[play.CurrentSelectedBullet] == 9)&& play.State.Buttons.X == ButtonState.Pressed)
            {
                Charging = true;
            }
            else
            {
                Charging = false;
            }

            //Stats
            Stats.ChargeRate = 1.5f;
            Stats.ChargeStartAt = 1f;
            Stats.MaxHealth = 8f;
            if (Charging)
            {
                Stats.IncomingDamageMultiplier = 1f;
                Stats.Speed = 1f;
                Stats.FireRate = 1f;
            }
            else
            {
                Stats.IncomingDamageMultiplier = 1.5f;
                Stats.Speed = 0.5f;
                Stats.FireRate = 0.5f;
            }
        }
        public override void Draw(Player play, SpriteBatch sb)
        {
            if (play.IFrames > 0)
            {
                sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(0, 0, Width, Height), new Color(1f, 1f, 0f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
            else
            {
                sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(0, 0, Width, Height), new Color(1f, 1f - (play.HitAniFade * 4), 1f - (play.HitAniFade * 4), 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f); 
            }
            
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width, 0, Width, Height), SceneMan.RelicsColors1[play.CurrentRelics[0]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.29f);
            sb.Draw(CoreTexture, new Rectangle((int)Math.Ceiling(play.Pos.X), (int)Math.Ceiling(play.Pos.Y), Width, Height), new Rectangle(Width * 2, 0, Width, Height), SceneMan.RelicsColors2[play.CurrentRelics[0]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.28f);
        }
    }
}