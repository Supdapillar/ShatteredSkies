using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Destroyer : Core
    {
        public Destroyer(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            //Stuff about the diamentions of the ship
            Width = 16;
            Height = 15;
            HitBox = new Rectangle(5, 6, 6, 6);
            CoreTexture = SceneMan.Textures["Destroyer"];
            WingOffset = new Vector2[]
            {
            new Vector2(0,7),
            new Vector2(0,7),
            new Vector2(0,5),
            new Vector2(0,0),
            new Vector2(0,2),
            new Vector2(0,4),
            };
            GunPositions.Add(new Vector2(3, 0));
            GunPositions.Add(new Vector2(12, 0));
            ///{"WingYOffset_0_Ship_4", 7},
            ///{"WingYOffset_1_Ship_4", 7},
            ///{"WingYOffset_2_Ship_4", 5},
            ///{"WingYOffset_3_Ship_4", 0},
            ///{"WingYOffset_4_Ship_4", 2},
            //Whole buncha stats
            Stats.TopSpeed = 0.40f;
            Stats.Accerleration = 0.40f;
            Stats.Deaccerleration = 0.40f;
            Stats.Accuracy = 2f;
            Stats.Damage = 0.65f;
            Stats.MaxHealth = 15f;
            Stats.BulletLifeSpan = 1.5f;
            Stats.ProcPercent = 0.75f;

        }
        public override void Update(Player play, GameTime GT)
        {
            Stats.Damage = 0.75f / SceneMan.Players.Count;
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
