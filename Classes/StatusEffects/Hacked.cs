using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Hacked : StatusEffect
    {
        //Draw Stuff
        private double GlitchDelay;
        private List<Vector2> Glitches = new List<Vector2>();

        public Hacked(Enemy enem) : base(enem)
        {
            Host = enem;
        }
        public override void Update(GameTime GT)
        {
            GlitchDelay -= GT.ElapsedGameTime.TotalSeconds;
            if (EffectAmount > 0)
            {
                EffectAmount -= (float)GT.ElapsedGameTime.TotalSeconds;
                foreach (EnemyBullet Ebull in Host.SceneMan.EnemyBullets)
                {
                    if (Ebull is EnemyBasicShot)
                    {
                        if (Ebull.ShotBy == Host)
                        {
                            Ebull.Health = 0;
                            if (Host.LastHitBy != null)
                            {
                                Host.SceneMan.Bullets.Add(new BasicShotWeak(0, Ebull.Pos, Host.SceneMan, Host.LastHitBy.CreatedBy) { Delta = Ebull.Delta });
                            }
                            else
                            {
                                Host.SceneMan.Bullets.Add(new BasicShotWeak(0, Ebull.Pos, Host.SceneMan, Host.SceneMan.Players[0]) { Delta = Ebull.Delta });
                            }
                        }
                    }
                }
            }
            else
            {
                EffectAmount = 0;
            }
        }
        public override void Draw(SpriteBatch sb) //+ Host.WidthHeight.X/2 - Host.SceneMan.Textures["BleedEffect"].Width/2
        {
            if (EffectAmount > 0)
            {
                if (GlitchDelay <= 0)
                {
                    GlitchDelay = Host.SceneMan.rand.NextDouble() / 3;
                    Glitches.Clear();
                    int rand = Host.SceneMan.rand.Next(3, 6);
                    for (int i = 0; i < rand+Host.Size; i++)
                    {
                        Glitches.Add(new Vector2(Host.SceneMan.rand.Next(-3, 4), Host.SceneMan.rand.Next(0, Host.SprOutline.Height)));
                    }
                }

                sb.Draw(Host.SprOutline, new Rectangle((int)Host.Pos.X, (int)Host.Pos.Y, (int)Host.WidthHeight.X, (int)Host.WidthHeight.Y), new Rectangle(0, 0, (int)Host.WidthHeight.X, (int)Host.WidthHeight.Y), Color.Lime, 0f, new Vector2(), SpriteEffects.None, 0f);
                sb.Draw(Host.SprInside, new Rectangle((int)Host.Pos.X, (int)Host.Pos.Y, (int)Host.WidthHeight.X, (int)Host.WidthHeight.Y), new Rectangle(0, 0, (int)Host.WidthHeight.X, (int)Host.WidthHeight.Y), Color.Blue, 0f, new Vector2(), SpriteEffects.None, 0f);

                foreach (Vector2 vec in Glitches)
                {
                    sb.Draw(Host.SceneMan.Textures["WhitePixel"], new Rectangle((int)(Host.Pos.X + vec.X), (int)(Host.Pos.Y + vec.Y), (int)Host.WidthHeight.X, 1), new Rectangle(0, 0, 1, 1), Color.Black, 0f, new Vector2(), SpriteEffects.None, 0f);
                    sb.Draw(Host.SprOutline, new Rectangle((int)(Host.Pos.X + vec.X), (int)(Host.Pos.Y + vec.Y), (int)Host.WidthHeight.X, 1), new Rectangle(0, (int)vec.Y, (int)Host.WidthHeight.X, 1), Color.Lime, 0f, new Vector2(), SpriteEffects.None, 0f);
                    
                    sb.Draw(Host.SprInside, new Rectangle((int)(Host.Pos.X + vec.X), (int)(Host.Pos.Y + vec.Y), (int)Host.WidthHeight.X, 1), new Rectangle(0, (int)vec.Y, (int)Host.WidthHeight.X, 1), Color.Blue, 0f, new Vector2(), SpriteEffects.None, 0f);
                }
            }
        }
    }
}
