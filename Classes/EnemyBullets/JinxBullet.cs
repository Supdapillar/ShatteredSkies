using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class JinxBullet : EnemyBullet
    {
        private readonly Player TargetedPlayer;
        private double Angle;
        public JinxBullet(Vector2 PS, Vector2 D, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            Delta = D;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(5, 5);
            TargetedPlayer = SceneMan.Players[SceneMan.rand.Next(0,SceneMan.Players.Count)];
            ShotBy = shotBy;
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta/3;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            Angle = Helper.GetRadiansOfTwoPoints(new Vector2(Pos.X + WidthHeight.X / 2, Pos.Y + WidthHeight.Y / 2), new Vector2(TargetedPlayer.Pos.X + TargetedPlayer.AllCores[TargetedPlayer.CurrentShipParts[0]].Width/2, TargetedPlayer.Pos.Y + TargetedPlayer.AllCores[TargetedPlayer.CurrentShipParts[0]].Height / 2)); ;
            Delta += new Vector2((float)Math.Cos(Angle)/15, (float)Math.Sin(Angle) / 15);
            SceneMan.Particles.Add(new JinxBulletParticle(new Vector2((int)(Pos.X + 2.5), (int)(Pos.Y + 2.5)), SceneMan, (float)(Math.PI * TimeSinceCreation)));
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
            if (TimeSinceCreation > 4) Health = 0;
            Delta /= 1.01f;
        }
        public override void Draw(SpriteBatch sb)
        {
            //Enemy relic Mod Enemy Bullet Draw
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulDraw(this, sb);
            }
            //sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 5, 5), new Rectangle(0, 0, 5, 5), new Color(1f, 0, 1f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
            sb.Draw(SceneMan.Textures["JinxBullet"], new Rectangle((int)(Pos.X+2.5), (int)(Pos.Y+2.5), 5, 5), new Rectangle(0, 0, 5, 5), new Color(1f, 0, 0,1f), (float)(Math.PI*TimeSinceCreation), new Vector2(2.5f, 2.5f), SpriteEffects.None, 0.3f);
        }
    }
}
