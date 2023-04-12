using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ShatteredSkies.Classes
{
    public class Player : Ally
    {

        public double AbilityDelay;

        public Animation ThrusterAnimation;

        public float HitAniFade;

        //Death
        public bool IsDead = false;
        public Vector2 GotoPos;
        public bool GoLeft;

        //Store Stuff
        public int[] CurrentShipParts = new int[2] { 0, 0 }; // first is core | second is wings
        public int[] CurrentBullets = new int[3] { 0, 0, 0 };        //this will be the three bullets you can choose
        public int[] CurrentRelics = new int[3] { 0, 0, 0 }; //13tech
        public int CurrentSelectedBullet = 0;

        public double[] BulletData = new double[] {0,0,0};

        //Invincibility
        public int IFrames = 0;

        public List<EnemyBullet> AllCollidingBullets = new List<EnemyBullet>();


        // Core List
        public Core[] AllCores;
        // Wing List
        public Wings[] AllWings;

        //Input stuff
        public GamePadState State;
        public GamePadState LastState;

        public Player(Vector2 PS, SceneManager Scenemana, Player createdby) : base(PS, Scenemana, createdby)
        {
            SceneMan = Scenemana;
            ThrusterAnimation = new Animation(SceneMan.Textures["Dart_Thrusters"], 7);
            CreatedBy = this;
            Health = 10;

            AllCores = new Core[]
            {
                new Dart(SceneMan),
                new Para(SceneMan),
                new Omega(SceneMan),
                new Assassin(SceneMan),
                new Destroyer(SceneMan)
            };
            AllWings = new Wings[]
            {
                new Colliders(SceneMan),
                new Shimmers(SceneMan),
                new Sparklers(SceneMan),
                new Flashers(SceneMan),
                new Fragmentisers(SceneMan),
                new Paralyzers(SceneMan),
            };
        }
        public override void Update(GameTime GT)
        {
            ThrusterAnimation.Update((int)(-Delta.Y + 3));         // state.ThumbSticks.Left.X
            AllCores[CurrentShipParts[0]].Update(this, GT);
            //Position Updating and all that junk
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            ShotDelay -= GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.FireRate;
            if (AbilityDelay > 0)
            {
                AbilityDelay -= GT.ElapsedGameTime.TotalSeconds;
            } 
            else
            {
                AbilityDelay = 0;
            }
            IFrames -= 1;

            //Setting Stats
            TopSpeed = new Vector2((float)(50 * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.TopSpeed), (float)(50 * AllCores[CurrentShipParts[0]].Stats.TopSpeed));

            if (HitAniFade > 0)
            {
                HitAniFade -= (float)GT.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                HitAniFade = 0;
            }
            if (!IsDead) // If you're alive
            {
                if (State == null) { return; }
                else // Math.Abs(1 - (input.LeftTrigger * 0.75f))
                {
                    //dpad movment
                    if (State.DPad.Left == ButtonState.Pressed & Pos.X > 0 && Delta.X > -TopSpeed.X)
                    {
                        Delta.X += -12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }
                    else if (State.DPad.Right == ButtonState.Pressed && Pos.X < 288 && Delta.X < TopSpeed.X)
                    {
                        Delta.X += 12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }

                    if ((State.ThumbSticks.Left.X > 0.05 & Pos.X < 288 - AllCores[CurrentShipParts[0]].Width || (State.ThumbSticks.Left.X < -0.05 & Pos.X > 0)) && Math.Abs(Delta.X) < Math.Abs(TopSpeed.X))
                    {
                        Delta.X += State.ThumbSticks.Left.X * 12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }

                    if (State.DPad.Up == ButtonState.Pressed & Pos.Y > 0 && Delta.Y > -TopSpeed.Y)
                    {
                        Delta.Y += -12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }
                    else if (State.DPad.Down == ButtonState.Pressed & Pos.Y < 162 && Delta.Y < TopSpeed.Y)
                    {
                        Delta.Y += 12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }

                    if (((State.ThumbSticks.Left.Y > 0.05 & Pos.Y > 0) || (State.ThumbSticks.Left.Y < -0.05 & Pos.Y < 216 + 7)) && Math.Abs(Delta.Y) < Math.Abs(TopSpeed.Y))
                    {
                        Delta.Y += -State.ThumbSticks.Left.Y * 12f * (float)(GT.ElapsedGameTime.TotalSeconds * AllCores[CurrentShipParts[0]].Stats.Speed * AllCores[CurrentShipParts[0]].Stats.Accerleration);
                    }


                    //Shooting stuff
                    if (State.Buttons.X == ButtonState.Pressed)
                    {
                        if (ShotDelay <= 0) // For non-charge bullets
                        {
                            foreach (Vector2 vect in AllCores[CurrentShipParts[0]].GunPositions)
                            {
                                switch (CurrentBullets[CurrentSelectedBullet])
                                {
                                    case 0:
                                        SceneMan.Bullets.Add(new BasicShot(0, new Vector2(Pos.X+vect.X - 1f, Pos.Y + vect.Y - 2), SceneMan, this));
                                        ShotDelay = 0.20f;
                                        break;
                                    case 1:
                                        SceneMan.Bullets.Add(new ShrapnelShot(0, new Vector2(Pos.X + vect.X - 2f, Pos.Y + vect.Y - 4), SceneMan, this));
                                        ShotDelay = 0.30f;
                                        break;
                                    case 2:
                                        SceneMan.Bullets.Add(new PiercingShot(0, new Vector2(Pos.X + vect.X - 1f, Pos.Y + vect.Y - 5), SceneMan, this));
                                        ShotDelay = 0.25f;
                                        break;
                                    case 3:
                                        SceneMan.Bullets.Add(new SummoningShot(0, new Vector2(Pos.X + vect.X - 3f, Pos.Y + vect.Y - 5), SceneMan, this));
                                        ShotDelay = 0.60f;
                                        break;
                                    case 4:
                                        SceneMan.Bullets.Add(new GatlingShot(0, new Vector2(Pos.X + vect.X - 1f, Pos.Y + vect.Y), SceneMan, this));
                                        ShotDelay = 0.05f;
                                        break;
                                    case 5:
                                        SceneMan.Bullets.Add(new Flamethrower(0, new Vector2(Pos.X + vect.X - 2f, Pos.Y + vect.Y - 5), SceneMan, this));
                                        ShotDelay = 0.0f;
                                        break;
                                    case 6://charge
                                        ShotDelay = AllCores[CurrentShipParts[0]].Stats.ChargeStartAt + (float)GT.ElapsedGameTime.TotalSeconds * (2 * AllCores[CurrentShipParts[0]].Stats.FireRate);
                                        break;
                                    case 7:
                                        SceneMan.Allies.Add(new MicroDrone(new Vector2(Pos.X + vect.X - 3f, Pos.Y + vect.Y - 7), SceneMan, this));
                                        ShotDelay = 1f;
                                        break;
                                    case 8:
                                        SceneMan.Bullets.Add(new CryoDetonator(0, new Vector2(Pos.X + vect.X - 2f, Pos.Y + vect.Y - 10), SceneMan, this));
                                        ShotDelay = 0.5f;
                                        break;
                                    case 9: //Shattering
                                        ShotDelay = AllCores[CurrentShipParts[0]].Stats.ChargeStartAt + (float)GT.ElapsedGameTime.TotalSeconds * (2 * AllCores[CurrentShipParts[0]].Stats.FireRate);
                                        break;
                                    case 10:
                                        SceneMan.Bullets.Add(new ExplosiveShot(0, new Vector2(Pos.X + vect.X - 2f, Pos.Y + vect.Y - 2), SceneMan, this));
                                        ShotDelay = 0.25f;
                                        break;
                                    case 11:
                                        SceneMan.Bullets.Add(new LockOnShot(0, new Vector2(Pos.X + vect.X - 1f, Pos.Y + vect.Y - 4), SceneMan, this));
                                        ShotDelay = 0.25f;
                                        break;
                                    case 12:
                                        SceneMan.Bullets.Add(new ExplosiveMine(0, new Vector2(Pos.X + vect.X - 2f, Pos.Y + vect.Y - 1), SceneMan, this));
                                        ShotDelay = 0.75f;
                                        break;
                                    case 13: // Star cannon
                                        if (State.Triggers.Right > 0.25f)
                                        {
                                            if (BulletData[CurrentSelectedBullet] > 0) // 6.666
                                            {
                                                SceneMan.Bullets.Add(new StarCannon(0, new Vector2(Pos.X + vect.X - 5f, Pos.Y + vect.Y - 8), SceneMan, this));
                                                BulletData[CurrentSelectedBullet] -= 1;
                                                ShotDelay = 0.15f;
                                            }
                                        }
                                        else
                                        {
                                            ShotDelay = AllCores[CurrentShipParts[0]].Stats.ChargeStartAt + (float)GT.ElapsedGameTime.TotalSeconds * (2 * AllCores[CurrentShipParts[0]].Stats.FireRate);
                                        }
                                        break;
                                }
                            }
                        }
                        else//For any bullets that can shoot without a delay
                        {
                            switch (CurrentBullets[CurrentSelectedBullet])
                            {
                                case 6://RailGun
                                    ShotDelay += GT.ElapsedGameTime.TotalSeconds * (2* (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate));
                                    if (ShotDelay >= 2.25f)
                                    {
                                        if ((ShotDelay - GT.ElapsedGameTime.TotalSeconds * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate)) < 2.25f)
                                        {
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                        }
                                    }
                                    else if (ShotDelay >= 1.5f)
                                    {
                                        if ((ShotDelay - GT.ElapsedGameTime.TotalSeconds * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate)) < 1.5f)
                                        {
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                        }
                                    }
                                    else if (ShotDelay >= 0.75f)
                                    {
                                        if ((ShotDelay - GT.ElapsedGameTime.TotalSeconds * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate)) < 0.75f)
                                        {
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                        }
                                    }
                                    else if (ShotDelay >= 0.25f)
                                    {
                                        if ((ShotDelay - GT.ElapsedGameTime.TotalSeconds * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate)) < 0.25f)
                                        {
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                        }
                                    }
                                    break;
                                case 9:
                                    ShotDelay += GT.ElapsedGameTime.TotalSeconds * (2 * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate));
                                    if (ShotDelay % 0.20f < 0.02f && (ShotDelay - GT.ElapsedGameTime.TotalSeconds) % 0.20f > 0.15f)
                                    {
                                        SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                        SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                    }
                                    break;
                                case 13:
                                    if (State.Triggers.Right < 0.25f)
                                    {
                                        ShotDelay += GT.ElapsedGameTime.TotalSeconds * (2 * (AllCores[CurrentShipParts[0]].Stats.FireRate * AllCores[CurrentShipParts[0]].Stats.ChargeRate));
                                        if (ShotDelay >= 1f)
                                        {
                                            ShotDelay = 0;
                                            BulletData[CurrentSelectedBullet] += 1;
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(1, 0), 0.5f, SceneMan));
                                            SceneMan.Particles.Add(new ChargeEffectParticle(new Vector2(Pos.X + (AllCores[CurrentShipParts[0]].Width / 2), Pos.Y - 4), new Vector2(-1, 0), 0.5f, SceneMan));
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    else if (LastState.Buttons.X == ButtonState.Pressed)// For charge bullets
                    {
                        foreach (Vector2 vect in AllCores[CurrentShipParts[0]].GunPositions)
                        {
                            switch (CurrentBullets[CurrentSelectedBullet])
                            {
                                case 6: // rail cannon
                                    if (ShotDelay >= 2.25f)
                                    {
                                        SceneMan.Bullets.Add(new RailCannon(3, new Vector2(Pos.X + vect.X - 23f, Pos.Y + vect.Y - 104), SceneMan, this));
                                    }
                                    else if (ShotDelay >= 1.5f)
                                    {
                                        SceneMan.Bullets.Add(new RailCannon(2, new Vector2(Pos.X + vect.X - 11f, Pos.Y + vect.Y - 72), SceneMan, this));
                                    }
                                    else if (ShotDelay >= 0.75f)
                                    {
                                        SceneMan.Bullets.Add(new RailCannon(1, new Vector2(Pos.X + vect.X - 6f, Pos.Y + vect.Y - 33), SceneMan, this));
                                    }
                                    else if (ShotDelay >= 0.25f)
                                    {
                                        SceneMan.Bullets.Add(new RailCannon(0, new Vector2(Pos.X + vect.X - 3f, Pos.Y + vect.Y - 18), SceneMan, this));
                                    }
                                    if (vect == AllCores[CurrentShipParts[0]].GunPositions[^1])
                                    {
                                        ShotDelay = 0;
                                    }
                                    break;
                                case 9:
                                    while (ShotDelay > 0.20f)
                                    {
                                        foreach (Vector2 vecna in AllCores[CurrentShipParts[0]].GunPositions)
                                        {
                                            SceneMan.Bullets.Add(new ShatteringShot(0, new Vector2(Pos.X + vecna.X, Pos.Y + vecna.Y - 1), SceneMan, this));
                                        }
                                        ShotDelay -= 0.20;
                                    }
                                    if (vect == AllCores[CurrentShipParts[0]].GunPositions[^1])
                                    {
                                        ShotDelay = 0;
                                    }
                                    break;
                            }
                        }
                    }

                    //switching bullets
                    if (State.Buttons.LeftShoulder == ButtonState.Pressed & !(LastState.Buttons.LeftShoulder == ButtonState.Pressed) & CurrentSelectedBullet > 0)
                    {
                        if (CurrentBullets[CurrentSelectedBullet] == 6)
                        {
                            ShotDelay = 0;
                        }
                        CurrentSelectedBullet -= 1;
                    }
                    else if (State.Buttons.RightShoulder == ButtonState.Pressed & !(LastState.Buttons.RightShoulder == ButtonState.Pressed) & CurrentSelectedBullet < 2)
                    {
                        if (CurrentBullets[CurrentSelectedBullet] == 6)
                        {
                            ShotDelay = 0;
                        }
                        CurrentSelectedBullet += 1;
                    }

                    //Activating ability
                    if (State.Buttons.Y == ButtonState.Pressed)
                    {
                        AllWings[CurrentShipParts[1]].Activated(this,GT);
                    }
                    //Some abilities need to be constantly updated
                    AllWings[CurrentShipParts[1]].Update(this, GT);
                    //adds a bit of a slide
                    //left right
                    if (State.DPad.Left == 0 && State.DPad.Right == 0 && State.ThumbSticks.Left.X == 0)
                    {
                        Delta.X /= 1f + (float)(0.2f * AllCores[CurrentShipParts[0]].Stats.Deaccerleration);
                    }
                    else
                    {
                        Delta.X /= 1.05f;
                    }
                    //up down
                    if (State.DPad.Up == 0 && State.DPad.Down == 0 && State.ThumbSticks.Left.Y == 0)
                    {
                        Delta.Y /= 1f + (float)(0.2f * AllCores[CurrentShipParts[0]].Stats.Deaccerleration);
                    }
                    else
                    {
                        Delta.Y /= 1.05f;
                        Delta.Y /= 1.05f;
                    }
                }
                //Relic Player Mod Update
                for (int i = 0; i < SceneMan.ActiveRelics.Count; i++)
                {
                    if (SceneMan.ActiveRelics[i].ModifiesPlayerUpdate)
                    {
                        SceneMan.ActiveRelics[i].ModPlayUpdate(this, GT);
                    }
                }
            }
            else // Death
            {
                //Ghost orb AI
                if (GoLeft & Pos.X < GotoPos.X)
                {
                    GoLeft = !GoLeft;
                    GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(32, 130));
                }
                else if (!GoLeft & Pos.X > GotoPos.X)
                {
                    GoLeft = !GoLeft;
                    GotoPos = new Vector2(SceneMan.rand.Next(32, 256), SceneMan.rand.Next(32, 130));
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

                foreach (Player play in SceneMan.Players)
                {
                    if (play == this) continue;
                    else
                    {
                        if (Helper.BoxCollision((int)play.Pos.X + AllCores[CurrentShipParts[0]].HitBox.X, (int)play.Pos.Y + AllCores[CurrentShipParts[0]].HitBox.Y, play.AllCores[CurrentShipParts[0]].HitBox.Width, play.AllCores[CurrentShipParts[0]].HitBox.Height, (int)Pos.X, (int)Pos.Y, 10, 11))
                        {
                            if (play.Health / 2 > 0.5)
                            {
                                IsDead = false;
                                if (play.Health / 2 <= AllCores[CurrentShipParts[0]].Stats.MaxHealth)
                                {
                                    Health = (float)Math.Floor(play.Health / 2);
                                    play.Health = (float)Math.Ceiling(play.Health / 2);
                                }
                                else
                                {
                                    Health = (float)AllCores[CurrentShipParts[0]].Stats.MaxHealth;
                                    play.Health -= (float)AllCores[CurrentShipParts[0]].Stats.MaxHealth;
                                }
                            }
                        }
                    }
                }
            }

            //Wall Collision
            if (Pos.X < 0) //Left wall
            {
                Pos.X = 0;
                Delta.X = 0;
            }
            else if (Pos.X > 288 - AllCores[CurrentShipParts[0]].Width) // Right Wall
            {
                Pos.X = 288 - AllCores[CurrentShipParts[0]].Width;
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

            //enemy bullet collision
            if (!IsDead)
            {
                if (Health > AllCores[CurrentShipParts[0]].Stats.MaxHealth)
                {
                    Health = (float)AllCores[CurrentShipParts[0]].Stats.MaxHealth;
                }
                CheckCollision();
                //Checks if player can collider with the bullet agian
                for (int i = 0; i < AllCollidingBullets.Count; i++) 
                {
                    
                    if (!Helper.BoxCollision((int)Pos.X + AllCores[CurrentShipParts[0]].HitBox.X, (int)Pos.Y + AllCores[CurrentShipParts[0]].HitBox.Y, AllCores[CurrentShipParts[0]].HitBox.Width, AllCores[CurrentShipParts[0]].HitBox.Height, (int)AllCollidingBullets[i].Pos.X, (int)AllCollidingBullets[i].Pos.Y, (int)AllCollidingBullets[i].WidthHeight.X, (int)AllCollidingBullets[i].WidthHeight.Y))
                    {
                        AllCollidingBullets.Remove(AllCollidingBullets[i]);
                    }
                }
            }
            //if death
            if (Health <= 0)
            {
                if (Helper.SumAllPlayerHealth(SceneMan) > 0)//for co op
                {
                    IsDead = true;
                }
                else
                {
                    if (SceneMan.CurrentLevel > 0)
                    {
                        SceneMan.ResetAllNumbers();
                    }
                    else
                    {
                        SceneMan.ResetAllNumbers();
                    }
                }
            }
        }

        private void CheckCollision()
        {
            if (IFrames < 1)
            {
                foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
                {
                    if (Helper.BoxCollision((int)Pos.X + AllCores[CurrentShipParts[0]].HitBox.X, (int)Pos.Y + AllCores[CurrentShipParts[0]].HitBox.Y, AllCores[CurrentShipParts[0]].HitBox.Width, AllCores[CurrentShipParts[0]].HitBox.Height, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                    {
                        if (!AllCollidingBullets.Contains(Ebull))
                        {
                            Health -= Ebull.Damage * (float)AllCores[CurrentShipParts[0]].Stats.IncomingDamageMultiplier;
                            Ebull.Health -= 1;
                            AllCollidingBullets.Add(Ebull);
                            if (Health < 0)
                            {
                                Health = 0;
                            }
                            HitAniFade = 0.25f;
                            for (int i = 0; i < 15; i++)
                            {
                                SceneMan.Particles.Add(new ColoredParticle
                                (
                                new Vector2(Pos.X + AllCores[CurrentShipParts[0]].Width / 2, Pos.Y + (AllCores[CurrentShipParts[0]].Height - 8) / 2),
                                new Vector2((float)(SceneMan.rand.NextDouble() - 0.5f) - Ebull.Delta.X, (float)(SceneMan.rand.NextDouble() - 0.5f) - Ebull.Delta.Y),
                                SceneMan,
                                new Color(1f, 1f, 1f),
                                true,
                                0.75f

                                ));
                            }
                            //Relic Player Mod Update
                            foreach (Relic rel in SceneMan.ActiveRelics)
                            {
                                if (rel.ModifiesPlayerOnHit)
                                {
                                    rel.ModPlayOnHit(this, Ebull);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            //Relic Player Mod Draw
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesPlayerDraw)
                {
                    rel.ModPlayDraw(this, sb);
                }
            }
            if (!IsDead)
            {
                //cores
                AllCores[CurrentShipParts[0]].Draw(this, sb);
                //DrawCoreHitBox
                sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Math.Ceiling(Pos.X + AllCores[CurrentShipParts[0]].HitBox.X), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].HitBox.Y), AllCores[CurrentShipParts[0]].HitBox.Width, AllCores[CurrentShipParts[0]].HitBox.Height), new Rectangle(0, 0, AllCores[CurrentShipParts[0]].HitBox.Width, AllCores[CurrentShipParts[0]].HitBox.Height), new Color(State.Triggers.Right, 0f, 0f, State.Triggers.Right), 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);

                //wings
                //LeftSide\
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - AllWings[CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(0, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Color(1f, 1f - (HitAniFade * 4), 1f - (HitAniFade * 4), 1f), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - AllWings[CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(AllWings[CurrentShipParts[1]].Width, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), SceneMan.RelicsColors1[CurrentRelics[1]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - AllWings[CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(AllWings[CurrentShipParts[1]].Width * 2, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), SceneMan.RelicsColors2[CurrentRelics[1]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);

                //Rightside
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + AllCores[CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(0, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Color(1f, 1f - (HitAniFade * 4), 1f - (HitAniFade * 4), 1f), 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + AllCores[CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(AllWings[CurrentShipParts[1]].Width, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), SceneMan.RelicsColors1[CurrentRelics[1]], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);
                sb.Draw(AllWings[CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + AllCores[CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + AllCores[CurrentShipParts[0]].WingOffset[CurrentShipParts[1]].Y), AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), new Rectangle(AllWings[CurrentShipParts[1]].Width * 2, 0, AllWings[CurrentShipParts[1]].Width, AllWings[CurrentShipParts[1]].Height), SceneMan.RelicsColors2[CurrentRelics[1]], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);

                //if the bullet uses bullet data to draw a number
                if (BulletData[CurrentSelectedBullet] != 0)
                {
                    sb.DrawString(SceneMan.Pico8, BulletData[CurrentSelectedBullet].ToString(), new Vector2((int)Pos.X, (int)Pos.Y-5), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                }

            }
            else // Ghost
            {
                sb.Draw(SceneMan.Textures["ShipGhost"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), 10, 11), new Rectangle(0, 0, 10, 11), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            }
        }
    }
}
