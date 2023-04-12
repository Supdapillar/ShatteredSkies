using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class FlameCore : Enemy
    {

        private readonly Overseer HostOverseer;
        private readonly double Speed = 1;

        public  float RotationAngle;
        private double GotoAngle;
        private Vector2 RestingOffset;
        public bool LockedInPlace;

        private double Angle;
        private double TankCharge;

        public FlameCore(Vector2 PS,Overseer hostOverseer,int restingposition, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(14,14);
            Health = 25f;
            Name = "FlameCore";
            Enemy_init();
            RotationAngle = 0;
            HostOverseer = hostOverseer;
            Speed = (SceneMan.rand.NextDouble()*2)+1.25;
            LockedInPlace = false;
            TankCharge = 0.5f;
            switch (restingposition)
            {
                case 0:
                    RestingOffset = new Vector2(-14,-14); // top left
                    break;
                case 1:
                    RestingOffset = new Vector2(0, -14);// top middle
                    break;
                case 2:
                    RestingOffset = new Vector2(14, -14);// top right
                    break;
                case 3:
                    RestingOffset = new Vector2(-14, 0);// middle left
                    break;
                case 5:
                    RestingOffset = new Vector2(14, 0);// middle right
                    break;
                case 6:
                    RestingOffset = new Vector2(-14, 14);// bottom left
                    break;
                case 7:
                    RestingOffset = new Vector2(0, 14);// bottom middle
                    break;
                case 8:
                    RestingOffset = new Vector2(14, 14);// bottom right
                    break;
            }
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;

            //Relic Mod Enemy Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyUpdate)
                {
                    rel.ModEneUpdate(this, GT);
                }
            }

            if (HostOverseer.LockedInPlace && HostOverseer.Delta.X < 0.01f)
            {
                GotoAngle = Helper.GetRadiansOfTwoPoints(Pos,new Vector2(HostOverseer.Pos.X + RestingOffset.X, HostOverseer.Pos.Y + RestingOffset.Y));
                if (Helper.GetDistance(Pos, new Vector2(HostOverseer.Pos.X + RestingOffset.X, HostOverseer.Pos.Y + RestingOffset.Y)) > 2)
                {
                    LockedInPlace = false;
                    Delta.X = (float)(Math.Cos(GotoAngle) * Speed * (8 / (float)HostOverseer.Minions.Count)) / 6;
                    Delta.Y = (float)(Math.Sin(GotoAngle) * Speed*(8 / (float)HostOverseer.Minions.Count)) / 6;


                    RotationAngle += ((float)GT.ElapsedGameTime.TotalSeconds * (880 * (Delta.X * (Math.Abs(Delta.Y) + 1))));

                }
                else
                {
                    LockedInPlace = true;
                    Pos.X = HostOverseer.Pos.X + RestingOffset.X;
                    Pos.Y = HostOverseer.Pos.Y + RestingOffset.Y;
                    RotationAngle = 0;
                }
            }
            else
            {
                Delta *= 0;
                RotationAngle = 0;
                LockedInPlace = false;
                TankCharge = SceneMan.rand.NextDouble()*2 + 0.25f;
            }




            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            //Shooting
            if (TankCharge >= 0)
            {
                Angle = Helper.ConvertDegreesToRadians(RotationAngle+45);
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 6 + (((float)Math.Cos(Angle)) * 14), Pos.Y + 6 + (((float)Math.Sin(Angle)) * 14)), new Vector2((float)Math.Cos(Angle) * (8 / ((float)HostOverseer.Minions.Count) / 2), (float)Math.Sin(Angle) * (8 / ((float)HostOverseer.Minions.Count) / 2)), this, SceneMan)); //Bullets
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 6 + (((float)Math.Cos(Angle + Math.PI/2)) * 14), Pos.Y + 6 + (((float)Math.Sin(Angle + Math.PI / 2)) * 14)), new Vector2((float)Math.Cos(Angle + Math.PI/2) * (8 / ((float)HostOverseer.Minions.Count) / 2), (float)Math.Sin(Angle + Math.PI / 2) * (8 / ((float)HostOverseer.Minions.Count) / 2)), this, SceneMan)); //Bullets
               
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 6 + (((float)Math.Cos(Angle+Math.PI)) * 14), Pos.Y + 6 + (((float)Math.Sin(Angle + Math.PI)) * 14)), new Vector2((float)Math.Cos(Angle + Math.PI)* (8 / ((float)HostOverseer.Minions.Count)/2), (float)Math.Sin(Angle + Math.PI) * (8 / ((float)HostOverseer.Minions.Count) / 2)),this, SceneMan)); //Bullets
                SceneMan.EnemyBullets.Add(new EnemyFlame(new Vector2(Pos.X + 6 + (((float)Math.Cos(Angle+Math.PI*1.5)) * 14), Pos.Y + 6 + (((float)Math.Sin(Angle + Math.PI*1.5)) * 14)), new Vector2((float)Math.Cos(Angle + Math.PI * 1.5) * (8 / ((float)HostOverseer.Minions.Count) / 2), (float)Math.Sin(Angle + Math.PI * 1.5) * (8 / ((float)HostOverseer.Minions.Count) / 2)), this, SceneMan)); //Bullets
                TankCharge -= GT.ElapsedGameTime.TotalSeconds;
            }

            //collision with bullets
            CheckCollision(WidthHeight);
        }
        public override void Draw(SpriteBatch sb)
        {
            //Relic Mod Enemy Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyDraw)
                {
                    rel.ModEneDraw(this, sb);
                }
            }
            sb.Draw(SceneMan.Textures["FlameCore"], new Vector2((int)Pos.X+7, (int)Pos.Y+7), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, (float)Helper.ConvertDegreesToRadians((double)RotationAngle), new Vector2(7, 7), 1, SpriteEffects.None, 0.33f);
            //sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y-3, ((int)Health/2)+1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
