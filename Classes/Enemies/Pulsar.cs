using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Pulsar : Enemy
    {

        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        private List<EnemyBullet> EBullets = new List<EnemyBullet>();
        private double Angle;
        private Player TargetedPlayer;
        private double AttackDelay = 0f;

        public Pulsar(Vector2 PS, SceneManager Scenemana) : base(PS, Scenemana)
        {
            Pos = PS;
            GotoPos = new Vector2(144, 10);
            SceneMan = Scenemana;
            WidthHeight = new Vector2(21,26);
            Name = "Pulsar";
            SprOutline = SceneMan.Textures["PulsarOutline"];
            SprInside = SceneMan.Textures["PulsarInside"];
            Size = 2;
            Health = 40f;
            MaxHealth = 40f;
            Enemy_init();
            TimeSinceCreation += (float)(SceneMan.rand.NextDouble() * Math.PI);
            ShotDelay = 0f - SceneMan.rand.NextDouble()*2;
            TargetedPlayer = SceneMan.Players[SceneMan.rand.Next(0,SceneMan.Players.Count)];
            Angle = 0;
        }

        public override void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
            AttackDelay += GT.ElapsedGameTime.TotalSeconds;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;

            if (AttackDelay > 2)
            {
                AttackDelay = 2;
            }
            // ai shet dont work 2 good rn, fix later
            if (GoLeft & Pos.X < GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(5, 10);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
            }
            else if (!GoLeft & Pos.X > GotoPos.X)
            {
                GoLeft = !GoLeft;
                GotoPos.X += SceneMan.rand.Next(-10, -5);
                GotoPos.Y += SceneMan.rand.Next(3, 9);
            }

            //Relic Mod Enemy Update
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyUpdate)
                {
                    rel.ModEneUpdate(this, GT);
                }
            }
            //Enemy relic Update
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneUpdate(this, GT);
            }

            if (Pos.X < GotoPos.X & Delta.X < 1.5) // move to the left
            {
                Delta.X += (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            else if (Pos.X > GotoPos.X & Delta.X > -1.5) // move to the right
            {
                Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds / 2;
            }
            if (Pos.Y < GotoPos.Y & Delta.Y < 0.5) // move up
            {
                Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds / 4;
            }
            else if (Pos.Y > GotoPos.Y & Delta.Y > -0.5) // moves down6
            {
                Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds / 4;
            }



            //status effect updating
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Update(GT);
            }

            //Updates bullets
            if (ShotDelay <= 0)
            {
                if (EBullets.Count > 0)
                {
                    double Anga = Helper.GetRadiansOfTwoPoints(EBullets[^1].Pos, TargetedPlayer.Pos);
                    EBullets[^1].Delta = new Vector2((float)Math.Cos(Anga), (float)Math.Sin(Anga));
                    EBullets.Remove(EBullets[^1]);
                    ShotDelay = 0.125f;
                }
                else if (ShotDelay <= -5)
                {
                    AttackDelay = 0f;
                    ShotDelay = 4f;
                    Angle = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        Angle -= Math.PI / 6;
                        EBullets.Add(new EnemyBasicShot
                            (
                            new Vector2(Helper.CenterPlayer(TargetedPlayer).X + (float)Math.Cos(Angle) * 15, Helper.CenterPlayer(TargetedPlayer).Y + (float)Math.Sin(Angle) * 15),
                            new Vector2(0, 0),
                            this,
                            SceneMan
                            ));
                    }
                    SceneMan.EnemyBullets.AddRange(EBullets);
                }
            }
            Angle = TimeSinceCreation * 1;
            foreach (EnemyBullet Ebull in EBullets)
            {
                Vector2 bullpos = new Vector2(Helper.CenterPlayer(TargetedPlayer).X + (float)Math.Cos(Angle) * (float)(15 * (AttackDelay + 1)-2), Helper.CenterPlayer(TargetedPlayer).Y + (float)Math.Sin(Angle) * (float)(15 * (AttackDelay + 1) - 2));
                Angle -= Math.PI / 6;
                if (Helper.BoxCollision(-2,-2,292,166,(int)bullpos.X, (int)bullpos.Y, 3, 4))
                {
                    Ebull.Pos = bullpos;
                    //kool hip particle
                    SceneMan.Particles.Add(new ColoredParticle
                    (
                        new Vector2(Ebull.Pos.X + Ebull.WidthHeight.X / 2, Ebull.Pos.Y + Ebull.WidthHeight.Y / 2),
                        new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) / 4, (float)(SceneMan.rand.NextDouble() - 0.5f) / 4),
                        SceneMan,
                        new Color(128,0,0),
                        true,
                        0.25f

                    ));
                }
                else
                {
                    Ebull.Pos = new Vector2(-3, -4);
                }
            }
            for(int i = 0; i < EBullets.Count; i++)
            {
                if (!SceneMan.EnemyBullets.Contains(EBullets[i]))
                {
                    EBullets.RemoveAt(i);
                }
            }

            //add a wee bit of slide
            Delta /= 1;

            //collision with bullets
            CheckCollision(WidthHeight);
            //delete bullets on death
            if(Health <= 0)
            {
                foreach (EnemyBullet Ebull in EBullets)
                {
                    Ebull.Health = 0;
                }
            } 
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
            //Enemy Relic Mod Enemy Draw
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneDraw(this, sb);
            }
            sb.Draw(SceneMan.Textures["PulsarOutline"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
            
            if (EnemyRelics.Count > 0)
            {
                sb.Draw(SceneMan.Textures["PulsarInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), EnemyRelics[0].Color, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
                sb.Draw(SceneMan.Textures["PulsarRing"], new Rectangle((int)(Pos.X + 10.5f), (int)(Pos.Y + 28f), (int)13, (int)13), new Rectangle(0, 0, 13, 13), EnemyRelics[0].Color, (float)(Math.PI * TimeSinceCreation * 2), new Vector2(6.5f, 6.5f), SpriteEffects.None, 0.34f);
            }
            else
            {
                sb.Draw(SceneMan.Textures["PulsarInside"], new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)WidthHeight.X, (int)WidthHeight.Y), new Rectangle(0, 0, (int)WidthHeight.X, (int)WidthHeight.Y), Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.33f);
                sb.Draw(SceneMan.Textures["PulsarRing"], new Rectangle((int)(Pos.X + 10.5f), (int)(Pos.Y + 28f), (int)13, (int)13), new Rectangle(0, 0, 13, 13), Color.Gray, (float)(Math.PI * TimeSinceCreation * 2), new Vector2(6.5f, 6.5f), SpriteEffects.None, 0.34f);
            }

            RenderHealth(sb);
            if (EBullets.Count == 0)
            {
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 4, (int)(20+(ShotDelay*4)), 1), new Rectangle(0, 0, 1, 1), Color.Blue, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            }

            if (ShotDelay <= 2)
            {
                if (EBullets.Count > 0)
                {
                    sb.Draw(SceneMan.Textures["PulsarWarning"], new Rectangle((int)EBullets[^1].Pos.X - 1, (int)EBullets[^1].Pos.Y - 7, (int)7, (int)14), new Rectangle(0, 0, 7, 14),
                    new Color(1f, (float)(ShotDelay % (0.5f) * 2), (float)(ShotDelay % (0.5f) * 2), 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                }
            }

            //status effect drawing
            foreach (StatusEffect stat in StatusEffects)
            {
                stat.Draw(sb);
            }
        }
    }
}
