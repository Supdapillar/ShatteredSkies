using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Spike : EnemyBullet
    {
        private Animation SpikeAnimation;
        private int Rotation;
        public Spike(Vector2 PS, Vector2 D,int rotation, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            Health = 9999;
            Rotation = rotation;
            SpikeAnimation = new Animation(SceneMan.Textures["Spike"], 15, 17, false);
            Damage = 2f;
            ShotBy = shotBy;
            switch (Rotation)
            {
                case 0:
                    WidthHeight = new Vector2(13, 81);
                    break;
                case 1:
                    WidthHeight = new Vector2(81, 13);
                    break;
                case 2:
                    WidthHeight = new Vector2(13, 81);
                    break;
                case 3:
                    WidthHeight = new Vector2(81, 13);
                    break;
            }
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            SpikeAnimation.Update(GT);
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
            if (TimeSinceCreation >= 1-0.1250)
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
            //sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, 5, 5), new Color(1f, 0, 1f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
            switch (Rotation)
            {
                case 0:
                    SpikeAnimation.Draw(sb, new Vector2(Pos.X - 2, Pos.Y), 0.5f, new Color(255, 0, 0, 255), SpriteEffects.None, 0, SceneMan);
                    break;
                case 1:
                    SpikeAnimation.Draw(sb, new Vector2(Pos.X - 2 + 81, Pos.Y-2), 0.5f, new Color(255, 0, 0, 255), SpriteEffects.None, (float)Math.PI/2, SceneMan);
                    break;
                case 2:
                    SpikeAnimation.Draw(sb, new Vector2(Pos.X - 2+17, Pos.Y+81), 0.5f, new Color(255, 0, 0, 255), SpriteEffects.None, (float)Math.PI, SceneMan);
                    break;
                case 3:
                    SpikeAnimation.Draw(sb, new Vector2(Pos.X, Pos.Y+15), 0.5f, new Color(255, 0, 0, 255), SpriteEffects.None, (float)(Math.PI+(Math.PI/2)), SceneMan);
                    break;
            }
        }
    }
}
