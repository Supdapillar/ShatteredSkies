using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ContaminatorAOE : EnemyBullet
    {
        public ContaminatorAOE(Vector2 PS, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            SceneMan = Sceneman;
            WidthHeight = new Vector2(50, 50);
            ShotBy = shotBy;
            Health = 9999;
            //Enemy relic Mod Enemy Bullet Contruc
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulCons(this);
            }
        }

        public override void Update(GameTime GT)
        {
            Pos += Delta;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;

            //Particles
            if (SceneMan.rand.Next(0,5)==0)
            {
                double Angle = SceneMan.rand.NextDouble() * 2 * Math.PI;
                SceneMan.Particles.Add(new ContaminatorParticle(
                    new Vector2
                    (
                        Pos.X + 25 + ((float)(Math.Cos(Angle) * SceneMan.rand.NextDouble() * 25)),
                        Pos.Y + 25 + (float)(Math.Sin(Angle) * SceneMan.rand.NextDouble() * 25)
                        )
                    ,this, SceneMan)); ;
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
            if (TimeSinceCreation >= 5)
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
            sb.Draw(SceneMan.Textures["ContaminatorAOE"], new Rectangle((int)(Pos.X), (int)(Pos.Y), 50, 50), new Rectangle(0, 0, 50, 50), new Color(255, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
        }
    }
}
