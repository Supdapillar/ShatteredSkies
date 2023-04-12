using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class KamikazeFall : EnemyBullet
    {
        
        public KamikazeFall(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            Damage = 4f;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(15, 14);
            ShotBy = shotBy;
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            if (Delta.Y < 3)
            {
                Delta.Y = 3;
            }
            Delta.Y += 6.5f * (float)GT.ElapsedGameTime.TotalSeconds;
            //explode on ground
            if (Pos.Y + 14 > 162)
            {
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(-0.5f, -0.5f), ShotBy, SceneMan));// up left
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(-0.25f, -0.75f), ShotBy, SceneMan));// up leftish
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0, -1), ShotBy, SceneMan));// UP
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0.25f, -0.75f), ShotBy, SceneMan));// up rightish
                SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + (15 / 2), Pos.Y + (14 / 2)), new Vector2(0.5f, -0.5f), ShotBy, SceneMan));// up right
                Health = 0;
            }
            //Relic Mod Enemy Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyBulletUpdate)
                {
                    rel.ModEneBulUpdate(this, GT);
                }
            }
            //Enemy relic Mod Enemy Bullet Update
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulUpdate(this, GT);
            }
            if (TimeSinceCreation >= 9999)
            {
                Health = 0;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            //Enemy relic Mod Enemy Bullet Draw
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["KamikazeOutline"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            sb.Draw(SceneMan.Textures["KamikazeInside"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(128,0,0,255), 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
        }
    }
}
