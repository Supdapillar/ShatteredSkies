using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class NormalCore : Enemy
    {

        private readonly Overseer HostOverseer;
        private readonly double Speed = 1;

        public float RotationAngle;
        private double GotoAngle;
        private Vector2 RestingOffset;
        public bool LockedInPlace;

        public NormalCore(Vector2 PS,Overseer hostOverseer,int restingposition, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
            WidthHeight = new Vector2(14,14);
            Health = 25f;
            Name = "NormalCore";
            Enemy_init();
            RotationAngle = 0;
            HostOverseer = hostOverseer;
            Speed = (SceneMan.rand.NextDouble()*2)+1.25;
            LockedInPlace = false;
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
                if (Helper.GetDistance(Pos, new Vector2(HostOverseer.Pos.X + RestingOffset.X, HostOverseer.Pos.Y + RestingOffset.Y)) > 3)
                {
                    LockedInPlace = false;
                    Delta.X = (float)(Math.Cos(GotoAngle) * Speed * (8 / (float)HostOverseer.Minions.Count)) / 6;
                    Delta.Y = (float)(Math.Sin(GotoAngle) * Speed * (8 / (float)HostOverseer.Minions.Count)) / 6;


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
                RotationAngle = 0;
                Delta *= 0;
                LockedInPlace = false;
            }




            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            //Shooting
            while (RotationAngle >= 135 || RotationAngle <= -135)
            {
                if (RotationAngle >= 135)
                {
                    RotationAngle -= 90;
                    SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 6, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() * 2) - 0.5f, 1), this, SceneMan)); //Bullets
                }
                else if (RotationAngle <= -135)
                {
                    RotationAngle += 90;
                    SceneMan.EnemyBullets.Add(new EnemyBasicShot(new Vector2(Pos.X + 6, Pos.Y + 13), new Vector2(((float)SceneMan.rand.NextDouble() * 2) - 1.5f, 1), this, SceneMan)); //Bullets
                }
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
            sb.Draw(SceneMan.Textures["NormalCore"], new Vector2((int)Pos.X + 7, (int)Pos.Y+7), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, (float)Helper.ConvertDegreesToRadians((double)RotationAngle), new Vector2(7, 7), 1, SpriteEffects.None, 0.33f);
            //sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y-3, ((int)Health/2)+1, 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
