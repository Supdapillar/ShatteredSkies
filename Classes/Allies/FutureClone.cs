using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShatteredSkies.Classes
{
    public class FutureClone : Ally
    {
        public Vector2 GotoPos;
        public bool GoLeft = true; // 0 is left // 1 is right
        public bool IsPast = false;
        public List<Tuple<Vector2, float>> PastPositions = new List<Tuple<Vector2, float>>();// for past clone
        private float SecondCounter = 0f;
        private bool MakeParticles = false;
        private Player ConnectedPlayer;

        public FutureClone(Vector2 PS, SceneManager Scenemana, bool isPast, Player createdby) : base(PS, Scenemana, createdby)
        {
            IsPast = isPast;
            Pos = PS;
            GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(115, 140));
            SceneMan = Scenemana;
            WidthHeight = new Vector2(5, 10);
            MakeParticles = true;
            ConnectedPlayer = createdby;
        }
        public FutureClone(Vector2 PS, SceneManager Scenemana, Vector2 widthHeight, Player createdby) : base(PS, Scenemana, createdby)
        {
            Pos = PS;
            GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(115, 140));
            SceneMan = Scenemana;
            WidthHeight = widthHeight;
            ConnectedPlayer = createdby;
        }
        public override void Update(GameTime GT)
        {
            Pos += Delta;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds;

            if (!IsPast)
            {
                // ai shet dont work 2 good rn, fix later
                if (GoLeft & Pos.X <= GotoPos.X)
                {
                    GoLeft = !GoLeft;
                    GotoPos.X = SceneMan.rand.Next(64, 222);
                    GotoPos.Y = SceneMan.rand.Next(115, 140);
                }
                else if (!GoLeft & Pos.X >= GotoPos.X)
                {
                    GoLeft = !GoLeft;
                    GotoPos.X = SceneMan.rand.Next(64, 222);
                    GotoPos.Y = SceneMan.rand.Next(115, 140);
                }
                if (Pos.X < GotoPos.X & Delta.X < 2.5) // move to the left
                {
                    Delta.X += (float)GT.ElapsedGameTime.TotalSeconds * 2;
                }
                else if (Pos.X > GotoPos.X & Delta.X > -2.5) // move to the right
                {
                    Delta.X -= (float)GT.ElapsedGameTime.TotalSeconds * 2;
                }
                if (Pos.Y < GotoPos.Y & Delta.Y < 1.25) // move up
                {
                    Delta.Y += (float)GT.ElapsedGameTime.TotalSeconds;
                }
                else if (Pos.Y > GotoPos.Y & Delta.Y > -1.25) // moves down6
                {
                    Delta.Y -= (float)GT.ElapsedGameTime.TotalSeconds;
                }
            }
            else//past clone
            {
                for (int i = 0; i < PastPositions.Count; i++)
                {
                    SecondCounter += PastPositions[i].Item2;
                    if (SecondCounter >= 0.5f)
                    {
                        Pos = PastPositions[i].Item1;
                        PastPositions.RemoveAt(i);
                        break;
                    }
                }
                SecondCounter = 0;
            }

            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Pos.X = 0;
                Delta.X = 0;
            }
            else if (Pos.X > 288 - WidthHeight.X) // Right Wall
            {
                Pos.X = 288 - WidthHeight.X;
                Delta.X = 0;
            }
            if (Pos.Y < 0) //Top wall
            {
                Pos.Y = 0;
                Delta.Y = 0;
            }
            else if (Pos.Y > 162 - 7) // Bottom Wall
            {
                Pos.Y = 162 - 7;
                Delta.Y = 0;
            }

            //add a wee bit of slide
            Delta /= 1;
            if (MakeParticles)
            {
                if (!IsPast) // future
                {
                    SceneMan.Particles.Add(new FutureCloneParticle(Pos, SceneMan, new Color(0, 255, 255), ConnectedPlayer));
                }
                else
                {
                    SceneMan.Particles.Add(new FutureCloneParticle(Pos, SceneMan, new Color(255, 0, 255),ConnectedPlayer));
                }
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            if (!IsPast) // future
            {
                sb.Draw(ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].CoreTexture, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Rectangle(0, 0, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Color(0,1f,1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(0,1f,1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(0,1f,1f), 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);
            }
            else // past
            {
                sb.Draw(ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].CoreTexture, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Rectangle(0, 0, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Color(1f, 0f, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(1f, 0, 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(1f, 0, 1f), 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);
            }
        }
    }
}
