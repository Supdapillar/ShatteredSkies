using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace ShatteredSkies.Classes
{
    public class Essence : Relic
    {
        //Local Variables
        public string CurrentPower = "none";
        public float MaxTime = 0;
        public float Time = 0; //these are used for the amount of time you have with a power
        //stuff 4 curlicue
        public float ShotDelay = 0;
        public int ShotDirection = 0;
        //stuff 4 guardian
        public double ChargeTime = 0;
        //exploder
        public double Angle = -Math.PI * 2;

        public Essence(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnDeath = true;
            ModifiesPlayerUpdate = true;
            ModifiesPlayerDraw = true;

            //all mods for enemy stuff
            ModifiesBulletUpdate = true;
            ModifiesPlayerOnHit = true;


            //makes sure all premade players get the relic
            foreach (Player Play in SceneMan.Players)
            {
                Play.LocalRelics.Add(new Essence(PowerLevel, SceneMan, Play, true));
            }
            if (PowerLevel > 1)//Powerlevel 2 Stuff
            {
                ModifiesAllyConstructor = true;
                ModifiesAllyDraw = true;
                ModifiesAllyUpdate = true;
                ModifiesAllyOnHit = true;
                ModifiesAllyOnDeath = true;
                foreach (Ally Al in SceneMan.Allies)
                {
                    Al.LocalRelics.Add(new Essence(PowerLevel, SceneMan, play, true));
                }
            }
        }

        public Essence(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

        }

        public override void ModEneOnDeath(Enemy ene)
        {
            if (!(ene is BasicEnemy || ene is Sizzler))
            {
                if (SceneMan.rand.Next(0,2)==0)
                {
                    SceneMan.Bullets.Add(new EssenceOrb(0, ene.Pos, SceneMan, ConnectedPlayer, ene.Name));
                }
            }
            else
            {
                SceneMan.Bullets.Add(new EssenceOrb(0, ene.Pos, SceneMan, ConnectedPlayer, "Time+"));
            }
        }

        //////////// Player ////////////
        public override void ModPlayUpdate(Player play, GameTime GT)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Essence)
                {
                    if (PowerLevel == 1)
                    {
                        play.LocalRelics[i].Time -= (float)GT.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        play.LocalRelics[i].Time -= (float)GT.ElapsedGameTime.TotalSeconds * 0.75f;
                    }
                    if (play.LocalRelics[i].Time <= 0)
                    {
                        play.LocalRelics[i].MaxTime = 0;
                        play.LocalRelics[i].Time = 0;
                        play.LocalRelics[i].CurrentPower = "none";
                    }
                    switch (play.LocalRelics[i].CurrentPower.ToUpper())
                    {
                        case "CURLICUE":
                            play.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
                            if (play.LocalRelics[i].ShotDelay <= 0)
                            {
                                play.LocalRelics[i].ShotDirection += 1;
                                switch (play.LocalRelics[i].ShotDirection)
                                {
                                    case 0:// up
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) - 1, play.Pos.Y - 3), new Vector2(0, -0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 1://up right
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) + 5, play.Pos.Y), new Vector2(0.5f, -0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 2:// right
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) + 8, play.Pos.Y + 6), new Vector2(0.5f, 0), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 3://right down
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) + 5, play.Pos.Y + 12), new Vector2(0.5f, 0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 4://down
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) - 1, play.Pos.Y + 15), new Vector2(0, 0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 5://down left
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) - 7, play.Pos.Y + 12), new Vector2(-0.5f, 0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 6://left
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) - 10, play.Pos.Y + 6), new Vector2(-0.5f, 0), SceneMan, ConnectedPlayer)); //Bullets
                                        break;
                                    case 7://left up
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2) - 7, play.Pos.Y), new Vector2(-0.5f, -0.5f), SceneMan, ConnectedPlayer)); //Bullets
                                        play.LocalRelics[i].ShotDirection = -1;
                                        break;
                                }

                                play.LocalRelics[i].ShotDelay = play.Health / 10 - 0.1f;
                            }
                            break;
                        case "EXPLODER":
                            foreach (Enemy ene in SceneMan.Enemies)
                            {
                                if (Math.Sqrt(Math.Pow(play.Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2 - ene.Pos.X, 2) + Math.Pow(play.Pos.Y + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height - 4) / 2 - ene.Pos.Y, 2)) < 45)
                                {
                                    play.LocalRelics[i].CurrentPower = "none";
                                    for (int x = 0; x < 32; x++)
                                    {
                                        play.LocalRelics[i].Angle += Math.PI / 16;
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + play.WidthHeight.X / 2, play.Pos.Y + play.WidthHeight.Y / 2), new Vector2((float)Math.Cos(play.LocalRelics[i].Angle) / 1.5f, (float)Math.Sin(play.LocalRelics[i].Angle) / 1.5f), SceneMan, play)); //Bullets
                                    }
                                }
                            }
                            break;
                        case "GUARDIAN":
                            //1
                            play.LocalRelics[i].ChargeTime += GT.ElapsedGameTime.TotalSeconds * 0.70;
                            play.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
                            if (play.LocalRelics[i].ShotDelay <= 0 && (play.State.Buttons.X == ButtonState.Pressed) && play.LocalRelics[i].ChargeTime > 10)
                            {
                                switch (play.LocalRelics[i].ShotDirection)
                                {
                                    case 0:
                                        for (int x = 0; x < (int)play.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X - 13 - (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - play.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + 11 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        play.LocalRelics[i].ShotDirection = 1;
                                        play.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                    case 1:
                                        for (int x = 0; x < (int)play.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X - 19 - (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - play.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + 17 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        play.LocalRelics[i].ShotDirection = 2;
                                        play.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                    case 2:
                                        for (int x = 0; x < (int)play.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X - 25 - (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - play.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(play.Pos.X + 23 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), play.Pos.Y - 4), new Vector2(0, -1), SceneMan, ConnectedPlayer)); //Bullets
                                        }
                                        play.LocalRelics[i].ShotDirection = 0;
                                        play.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                }
                            }
                            break;
                        case "METEOROLOGIST":
                            ConnectedPlayer.LocalRelics[i].ChargeTime += GT.ElapsedGameTime.TotalSeconds;
                            if (ConnectedPlayer.LocalRelics[i].ChargeTime > 20)
                            {
                                ConnectedPlayer.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds * 13;
                            }
                            else
                            {
                                ConnectedPlayer.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds * 2;
                            }

                            if (ConnectedPlayer.LocalRelics[i].ShotDelay <= 0)
                            {
                                SceneMan.Bullets.Add(new EnemyMeteorAlly(0, new Vector2(SceneMan.rand.Next(32, 248), 338), new Vector2((float)(SceneMan.rand.NextDouble() - 0.5), (float)SceneMan.rand.NextDouble() - 2.5f), SceneMan, ConnectedPlayer)); //Bullets
                                ConnectedPlayer.LocalRelics[i].ShotDelay = 5f;
                            }
                            break;
                        case "SCANNER":
                            play.Pos.X += play.Delta.X * (play.Pos.Y - 82) / 82;
                            if (!(ConnectedPlayer.CurrentBullets[ConnectedPlayer.CurrentSelectedBullet] == 6) && !(ConnectedPlayer.CurrentBullets[ConnectedPlayer.CurrentSelectedBullet] == 9))
                            {
                                play.ShotDelay -= (GT.ElapsedGameTime.TotalSeconds * (play.Pos.Y - 82) / 162);
                            }
                            else
                            {
                                play.ShotDelay += (GT.ElapsedGameTime.TotalSeconds * (play.Pos.Y - 82) / 162);
                            }
                            break;
                    }
                    break;
                }
            }
        }
        public override void ModPlayOnHit(Player play, EnemyBullet Ebull)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Essence)
                {
                    if (play.LocalRelics[i].CurrentPower.ToUpper() == "RETALIATOR")
                    {
                        SceneMan.Bullets.Add(new BasicShot(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2 - 1f), play.Pos.Y - 2), SceneMan, play));
                        SceneMan.Bullets.Add(new BasicShot(0, new Vector2(play.Pos.X + (ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width / 2 - 1f), play.Pos.Y - 2), SceneMan, play));
                    }
                    break;
                }
            }
        }
        public override void ModPlayDraw(Player play, SpriteBatch sb)
        {
            for (int i = 0; i < play.LocalRelics.Count; i++)
            {
                if (play.LocalRelics[i] is Essence)
                {
                    if (SceneMan.game.Scene == 2)
                    {
                        if (!(play.LocalRelics[i].CurrentPower == "none"))
                        {
                            sb.Draw(SceneMan.Textures["EssenceUI"], new Rectangle(1, 35, 23, 9), new Rectangle(0, 0, 23, 9), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(10, 38, (int)Math.Ceiling(play.LocalRelics[i].Time * (13 / play.LocalRelics[i].MaxTime)), 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle(10, 39, (int)Math.Ceiling(play.LocalRelics[i].Time * (13 / play.LocalRelics[i].MaxTime)), 1), new Rectangle(0, 0, 1, 1), SceneMan.RelicsColors2[9], 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                        }
                        switch (play.LocalRelics[i].CurrentPower.ToUpper())
                        {
                            case "BOUNCER":
                                sb.Draw(SceneMan.Textures["Bouncer"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 14, 13), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                            case "CURLICUE":
                                sb.Draw(SceneMan.Textures["Curlicue"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 15, 15), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                            case "EXPLODER":
                                sb.Draw(SceneMan.Textures["Exploder"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 16, 13), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                            case "GUARDIAN":
                                sb.Draw(SceneMan.Textures["Guardian"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 25, 18), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                if (play.LocalRelics[i].ChargeTime > 10)
                                {
                                    for (int x = 0; x < (int)play.Pos.X / 12; x++)// Left //
                                    {
                                        if (x == 0)//makes the left endpiece show
                                        {
                                            sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)play.Pos.X - 25, (int)play.Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                                        }
                                        //The rest of the wall
                                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)play.Pos.X - 37 - (x * 12), (int)play.Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                                    }
                                    for (int x = 0; x < (int)(288 - play.Pos.X) / 12; x++)// Right //
                                    {
                                        if (x == 0)//makes the Right endpiece show
                                        {
                                            sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)play.Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + 10, (int)play.Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                                        }
                                        //The rest of the wall
                                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)play.Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + 26 + (x * 12), (int)play.Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                                    }
                                }
                                break;
                            case "METEOROLOGIST":
                                sb.Draw(SceneMan.Textures["Meteorologist"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 18, 20), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                            case "RETALIATOR":
                                sb.Draw(SceneMan.Textures["Retaliator"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 13, 14), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                            case "SCANNER":
                                sb.Draw(SceneMan.Textures["Scanner"], new Rectangle(2, 36, 6, 6), new Rectangle(0, 0, 17, 15), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.00f);
                                break;
                        }
                    }
                }
            }
        }
        public override void ModBulUpdate(Bullet bul, GameTime GT)
        {
            for (int i = 0; i < SceneMan.Players[0].LocalRelics.Count; i++)
            {
                if (SceneMan.Players[0].LocalRelics[i] is Essence)
                {
                    if (SceneMan.Players[0].LocalRelics[i].CurrentPower.ToUpper() == "BOUNCER")
                    {
                        if (bul.Pos.Y < 0) // hits the top screen
                        {
                            bul.Delta.Y = -bul.Delta.Y;
                        }
                        else if ((bul.Pos.Y + bul.WidthHeight.Y) > 162) // hits the bottom screen
                        {
                            bul.Delta.Y = -bul.Delta.Y;
                        }
                        if (bul.Pos.X < 0) // hits the Left screen
                        {
                            bul.Delta.X = -bul.Delta.X;
                        }
                        else if ((bul.Pos.Y + bul.WidthHeight.Y) > 288) // hits the Right screen
                        {
                            bul.Delta.X = -bul.Delta.X;
                        }
                    }
                    break;
                }
            }
        }
        //////////// Allies ////////////
        public override void ModAllyCons(Ally Al)
        {
            if (!Al.LocalRelics.OfType<Essence>().Any())
            {
                Al.LocalRelics.Add(new Essence(0, SceneMan, ConnectedPlayer, true));
            }
        }
        public override void ModAllyUpdate(Ally Al, GameTime GT)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Essence)
                {
                    Al.LocalRelics[i].Time -= (float)GT.ElapsedGameTime.TotalSeconds;
                    if (Al.LocalRelics[i].Time <= 0)
                    {
                        Al.LocalRelics[i].MaxTime = 0;
                        Al.LocalRelics[i].Time = 0;
                        Al.LocalRelics[i].CurrentPower = "none";
                    }
                    switch (Al.LocalRelics[i].CurrentPower.ToUpper())
                    {
                        case "CURLICUE":
                            Al.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
                            if (Al.LocalRelics[i].ShotDelay <= 0)
                            {
                                Al.LocalRelics[i].ShotDirection += 1;
                                switch (Al.LocalRelics[i].ShotDirection)
                                {
                                    case 0:// up
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) - 1, Al.Pos.Y - 3), new Vector2(0, -0.5f), SceneMan, Al)); //Bullets
                                        break;
                                    case 1://up right
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) + 5, Al.Pos.Y), new Vector2(0.5f, -0.5f), SceneMan, Al)); //Bullets
                                        break;
                                    case 2:// right
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) + 8, Al.Pos.Y + 6), new Vector2(0.5f, 0), SceneMan, Al)); //Bullets
                                        break;
                                    case 3://right down
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) + 5, Al.Pos.Y + 12), new Vector2(0.5f, 0.5f), SceneMan, Al)); //Bullets
                                        break;
                                    case 4://down
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) - 1, Al.Pos.Y + 15), new Vector2(0, 0.5f), SceneMan, Al)); //Bullets
                                        break;
                                    case 5://down left
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) - 7, Al.Pos.Y + 12), new Vector2(-0.5f, 0.5f), SceneMan, Al)); //Bullets
                                        break;
                                    case 6://left
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) - 10, Al.Pos.Y + 6), new Vector2(-0.5f, 0), SceneMan, Al)); //Bullets
                                        break;
                                    case 7://left up
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + (Al.WidthHeight.X / 2) - 7, Al.Pos.Y), new Vector2(-0.5f, -0.5f), SceneMan, Al)); //Bullets
                                        Al.LocalRelics[i].ShotDirection = -1;
                                        break;
                                }

                                Al.LocalRelics[i].ShotDelay = Al.Health / 10 - 0.1f;
                            }
                            break;
                        case "EXPLODER":
                            foreach (Enemy ene in SceneMan.Enemies)
                            {
                                if (Math.Sqrt(Math.Pow(Al.Pos.X + Al.WidthHeight.X / 2 - ene.Pos.X, 2) + Math.Pow(Al.Pos.Y + Al.WidthHeight.Y / 2 - ene.Pos.Y, 2)) < 45)
                                {
                                    Al.LocalRelics[i].CurrentPower = "none";
                                    for (int x = 0; x < 32; x++)
                                    {
                                        Al.LocalRelics[i].Angle += Math.PI / 16;
                                        SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + Al.WidthHeight.X / 2, Al.Pos.Y + Al.WidthHeight.Y / 2), new Vector2((float)Math.Cos(Al.LocalRelics[i].Angle) / 1.5f, (float)Math.Sin(Al.LocalRelics[i].Angle) / 1.5f), SceneMan, Al)); //Bullets
                                    }
                                }
                            }
                            break;
                        case "GUARDIAN":
                            //1
                            Al.LocalRelics[i].ChargeTime += GT.ElapsedGameTime.TotalSeconds * 0.70;
                            Al.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds;
                            if (Al.LocalRelics[i].ShotDelay <= 0 && (SceneMan.state.Buttons.X == ButtonState.Pressed) && Al.LocalRelics[i].ChargeTime > 10)
                            {
                                switch (Al.LocalRelics[i].ShotDirection)
                                {
                                    case 0:
                                        for (int x = 0; x < (int)Al.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X - 13 - (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - Al.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + 11 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        Al.LocalRelics[i].ShotDirection = 1;
                                        Al.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                    case 1:
                                        for (int x = 0; x < (int)Al.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X - 19 - (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - Al.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + 17 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        Al.LocalRelics[i].ShotDirection = 2;
                                        Al.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                    case 2:
                                        for (int x = 0; x < (int)Al.Pos.X / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X - 25 - (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        for (int x = 0; x < (int)(288 - Al.Pos.X) / 18 + 18; x++)
                                        {
                                            SceneMan.Bullets.Add(new BasicShotWeak(0, new Vector2(Al.Pos.X + 23 + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width + (x * 18), Al.Pos.Y - 4), new Vector2(0, -1), SceneMan, Al)); //Bullets
                                        }
                                        Al.LocalRelics[i].ShotDirection = 0;
                                        Al.LocalRelics[i].ShotDelay = 0.5f;
                                        break;
                                }
                            }
                            break;
                        case "METEOROLOGIST":
                            if (Al.LocalRelics[i].ChargeTime > 15)
                            {
                                Al.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds * 13;
                            }
                            else
                            {
                                Al.LocalRelics[i].ShotDelay -= GT.ElapsedGameTime.TotalSeconds * 2;
                            }

                            if (Al.LocalRelics[i].ShotDelay <= 0)
                            {
                                SceneMan.Bullets.Add(new EnemyMeteorAlly(0, new Vector2(SceneMan.rand.Next(32, 248), 338), new Vector2((float)(SceneMan.rand.NextDouble() - 0.5), (float)SceneMan.rand.NextDouble() - 2.5f), SceneMan, ConnectedPlayer)); //Bullets
                                Al.LocalRelics[i].ShotDelay = 2.5f;
                            }
                            break;
                        case "SCANNER":
                            Al.Pos.X += Al.Delta.X * (Al.Pos.Y - 82) / 82;
                            if (!(ConnectedPlayer.CurrentBullets[ConnectedPlayer.CurrentSelectedBullet] == 6) && !(ConnectedPlayer.CurrentBullets[ConnectedPlayer.CurrentSelectedBullet] == 9))
                            {
                                Al.ShotDelay -= (GT.ElapsedGameTime.TotalSeconds * (Al.Pos.Y - 82) / 162);
                            }
                            else
                            {
                                Al.ShotDelay += (GT.ElapsedGameTime.TotalSeconds * (Al.Pos.Y - 82) / 162);
                            }
                            break;
                    }
                    break;
                }
            }
        }
        public override void ModAllyOnHit(Ally Al, EnemyBullet Ebull)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Essence)
                {
                    if (Al.LocalRelics[i].CurrentPower.ToUpper() == "RETALIATOR")
                    {
                        SceneMan.Bullets.Add(new BasicShot(0, new Vector2(Al.Pos.X + Al.WidthHeight.X / 2 - 1f, Al.Pos.Y - 2), SceneMan, Al));
                        SceneMan.Bullets.Add(new BasicShot(0, new Vector2(Al.Pos.X + Al.WidthHeight.X / 2 - 1f, Al.Pos.Y - 2), SceneMan, Al));
                    }
                    break;
                }
            }
        }

        public override void ModAllyOnDeath(Ally Al)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Essence)
                {
                    if (Al.LocalRelics[i].CurrentPower.ToUpper() == "KAMIKAZE")
                    {
                        SceneMan.Bullets.Add(new KamikazeFallAlly(0, new Vector2(Al.Pos.X + Al.WidthHeight.X/2, Al.Pos.Y), Al.Delta, SceneMan, Al));
                    }
                    break;
                }
            }
        }

        public override void ModAllyDraw(Ally Al, SpriteBatch sb)
        {
            for (int i = 0; i < Al.LocalRelics.Count; i++)
            {
                if (Al.LocalRelics[i] is Essence)
                {
                    if (SceneMan.game.Scene == 2)
                    {
                        switch (Al.LocalRelics[i].CurrentPower.ToUpper())
                        {
                            case "GUARDIAN":
                                if (Al.LocalRelics[i].ChargeTime > 10)
                                {
                                    for (int x = 0; x < (int)Al.Pos.X / 12; x++)// Left //
                                    {
                                        if (x == 0)//makes the left endpiece show
                                        {
                                            sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Al.Pos.X - 25, (int)Al.Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                                        }
                                        //The rest of the wall
                                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)Al.Pos.X - 37 - (x * 12), (int)Al.Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipVertically, 0.25f);
                                    }
                                    for (int x = 0; x < (int)(288 - Al.Pos.X) / 12; x++)// Right //
                                    {
                                        if (x == 0)//makes the Right endpiece show
                                        {
                                            sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)(Al.Pos.X + Al.WidthHeight.X) + 10, (int)Al.Pos.Y, 16, 13), new Rectangle(0, 0, 16, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                                        }
                                        //The rest of the wall
                                        sb.Draw(SceneMan.Textures["GuardianWall"], new Rectangle((int)(Al.Pos.X + Al.WidthHeight.X) + 26 + (x * 12), (int)Al.Pos.Y, 12, 13), new Rectangle(0, 0, 12, 13), SceneMan.RelicsColors1[9], 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0.25f);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
