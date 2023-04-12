using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShatteredSkies.Classes
{
    public class Chronology : Relic
    {
        private float Distance;
        private float EffectRange = 0;
        public Chronology(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            // PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyUpdate = true;
            ModifiesEnemyBulletUpdate = true;
            ModifiesBulletDraw = true;
        }

        public override void ModEneUpdate(Enemy ene, GameTime GT)
        {
            foreach (Bullet bull in SceneMan.Bullets)
            {
                if (bull.ShotBy is Player)
                {
                    if (bull.ShotBy == ConnectedPlayer)
                    {
                        if (bull.ProcChance > 0.05f)
                        {
                            Distance = (float)Math.Sqrt(Math.Pow(ene.Pos.X + ene.WidthHeight.X / 2 - bull.Pos.X + bull.WidthHeight.X / 2, 2) + Math.Pow(ene.Pos.Y + ene.WidthHeight.Y / 2 - bull.Pos.Y + bull.WidthHeight.Y / 2, 2));
                            if ((((50f * bull.ProcChance) - Distance) / (50f * bull.ProcChance)) > EffectRange)
                            {
                                EffectRange = (((50f * bull.ProcChance) - Distance) / (50f * bull.ProcChance));
                            }
                        }
                    }
                }
            }
            ene.Pos.X -= ene.Delta.X * EffectRange;
            ene.Pos.Y -= ene.Delta.Y * EffectRange;
            ene.ShotDelay += GT.ElapsedGameTime.TotalSeconds * EffectRange;
            EffectRange = 0;
            Distance = 500;
        }
        public override void ModEneBulUpdate(EnemyBullet ebull, GameTime GT)
        {
            foreach (Bullet bull in SceneMan.Bullets)
            {
                if (bull.ShotBy is Player)
                {
                    if (bull.ShotBy == ConnectedPlayer)
                    {
                        if (bull.ProcChance > 0.05f && ebull.Pos.Y < 288)
                        {
                            Distance = (float)Math.Sqrt(Math.Pow(ebull.Pos.X + ebull.WidthHeight.X / 2 - bull.Pos.X + bull.WidthHeight.X / 2, 2) + Math.Pow(ebull.Pos.Y + ebull.WidthHeight.Y / 2 - bull.Pos.Y + bull.WidthHeight.Y / 2, 2));
                            if ((((50f * bull.ProcChance) - Distance) / (50f * bull.ProcChance)) > EffectRange)
                            {
                                EffectRange = (((50f * bull.ProcChance) - Distance) / (50f * bull.ProcChance));
                            }
                        }
                    }
                }
            }
            ebull.Pos.X -= ebull.Delta.X * EffectRange;
            ebull.Pos.Y -= ebull.Delta.Y * EffectRange;
            EffectRange = 0;
            Distance = 500;
        }
        public override void ModBulDraw(Bullet bul, SpriteBatch sb)
        {

            if (bul.ShotBy is Player)
            {
                if (bul.ShotBy == ConnectedPlayer) 
                {
                    MonoGame.Extended.ShapeExtensions.DrawCircle(sb,new Vector2(bul.Pos.X + bul.WidthHeight.X / 2, bul.Pos.Y + bul.WidthHeight.Y / 2), 50 * bul.ProcChance,100, new Color(0, 64, 64, 64),0.5f,0.3f);
                    //sb.Draw(SceneMan.Textures["ChronologyCircle"], new Rectangle((int)(bul.Pos.X + bul.WidthHeight.X / 2) - (int)(50 * bul.ProcChance), (int)(bul.Pos.Y + bul.WidthHeight.Y / 2) - (int)(50 * bul.ProcChance), (int)(100 * bul.ProcChance), (int)(100 * bul.ProcChance)), null, new Color(0, 64, 64, 64), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                }
            }
        }
    }
}
