using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class ContaminatorBomb : EnemyBullet
    {
        public double BaseAngle;
        public double Angle;
        public Vector2 RandomPos;
        public double lifeSpan;
        double SinSpeed;
        double TrailDelay = 0;

        public ContaminatorBomb(Vector2 PS, double angle, Enemy shotBy, SceneManager Sceneman)
        {
            Pos = PS;
            BaseAngle = angle;
            RandomPos = Pos;
            SceneMan = Sceneman;
            lifeSpan = 1 + SceneMan.rand.NextDouble()*2;
            WidthHeight = new Vector2(8, 8);
            SinSpeed = SceneMan.rand.NextDouble() + 0.5;
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
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
            Angle = BaseAngle + (float)((Math.PI / 4) * Math.Sin(TimeSinceCreation*4* SinSpeed));
            TrailDelay -= GT.ElapsedGameTime.TotalSeconds;
            Delta.X = (float)(Math.Cos(Angle));
            Delta.Y = (float)(Math.Sin(Angle));
            //Relic Mod Enemy Bullet Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyBulletUpdate)
                {
                    rel.ModEneBulUpdate(this, GT);
                }
            }
            //Particles
            if (TrailDelay <= 0)
            {
                Vector2 Center = Helper.CenterActor(Pos, WidthHeight);
                SceneMan.Particles.Add(new ColoredParticle(Center,new Vector2(0,0),SceneMan,Color.Red,true,1f));
                TrailDelay = 0.125f;
            }

            //Enemy relic Mod Enemy Bullet Update
            foreach (EnemyRelic Erel in ShotBy.EnemyRelics)
            {
                Erel.ModEneBulUpdate(this, GT);
            }
            if (TimeSinceCreation >= lifeSpan)
            {
                SceneMan.EnemyBullets.Add(new ContaminatorAOE(new Vector2(Pos.X-25,Pos.Y-25),ShotBy,SceneMan));
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
            sb.Draw(SceneMan.Textures["ContaminatorBomb"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), new Color(255, 0, 0), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
        }
    }
}