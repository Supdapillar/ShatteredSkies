using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Dart : Core
    {
        public Dart(SceneManager sceneman) : base(sceneman)
        {
            SceneMan = sceneman;
            //Stuff about the diamentions of the ship
            Width = 5;
            Height = 10;
            HitBox = new Rectangle(1, 4, 3, 3);
            CoreTexture = SceneMan.Textures["Dart"];
            WingOffset = new Vector2[]
            {
            new Vector2(0,3),
            new Vector2(0,3),
            new Vector2(0,0),
            new Vector2(0,-3),
            new Vector2(0,-3),
            new Vector2(0,-1),
            };
            GunPositions.Add(new Vector2(2,0));
            //wing offsets
            ///{"WingYOffset_0_Ship_0", 3},
            ///{"WingYOffset_1_Ship_0", 3},
            ///{"WingYOffset_2_Ship_0", 0},
            ///{"WingYOffset_3_Ship_0", -3},
            ///{"WingYOffset_4_Ship_0", -3},
            //Whole buncha stats
        }
        public override void Update(Player play, GameTime GT)
        {
            Stats.Damage = 1f / SceneMan.Players.Count;
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
