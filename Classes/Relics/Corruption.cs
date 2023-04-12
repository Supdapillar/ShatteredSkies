using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Corruption : Relic
    {
        public int SmallCap = 0; // caps at 6 | 12
        public int MediumCap = 0; // caps at 2 | 6
        public int LargeCap = 0; // caps at 0 | 2
        public Corruption(int power,SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnDeath = true;
            ModifiesAllyOnDeath = true;

            //for dynamic adding
            //small
            SmallCap += SceneMan.Allies.Where(item => item.GetType() == typeof(BasicEnemyAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            SmallCap += SceneMan.Allies.Where(item => item.GetType() == typeof(SizzlerAlly)&&(item.CreatedBy == ConnectedPlayer)).Count();
            //medium
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(BouncerAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(CurlicueAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(ExploderAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(KamikazeAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(RetaliatorAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            MediumCap += SceneMan.Allies.Where(item => item.GetType() == typeof(ScannerAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            //large
            LargeCap += SceneMan.Allies.Where(item => item.GetType() == typeof(GuardianAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
            LargeCap += SceneMan.Allies.Where(item => item.GetType() == typeof(MeteorologistAlly) && (item.CreatedBy == ConnectedPlayer)).Count();
        }
        public override void ModEneOnDeath(Enemy ene)
        {
            if (PowerLevel == 1)
            {
                switch (ene.Name)
                {
                    case "BasicEnemy":
                        if (SmallCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new BasicEnemyAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                SmallCap += 1;
                            }
                        break;
                    case "Sizzler":
                        if (SmallCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new SizzlerAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                SmallCap += 1;
                            }
                        break;
                    case "Exploder":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new ExploderAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Retaliator":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new RetaliatorAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Curlicue":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new CurlicueAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Kamikaze":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new KamikazeAlly(new Vector2(ene.Pos.X, ene.Pos.Y - 15), SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Bouncer":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new BouncerAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Scanner":
                        if (MediumCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new ScannerAlly(new Vector2(ene.Pos.X, 147), SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                }
            }
            else if (PowerLevel > 1)
            {
                switch (ene.GetType().Name)
                {
                    case "BasicEnemy":
                        if (SmallCap < 12)
                            if (SceneMan.rand.Next(0, 100) <= 60)
                            {
                                SceneMan.Allies.Add(new BasicEnemyAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                SmallCap += 1;
                            }
                        break;
                    case "Sizzler":
                        if (SmallCap < 12)
                            if (SceneMan.rand.Next(0, 100) <= 60)
                            {
                                SceneMan.Allies.Add(new SizzlerAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                SmallCap += 1;
                            }
                        break;
                    case "Exploder":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new ExploderAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Retaliator":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new RetaliatorAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Curlicue":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new CurlicueAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Kamikaze":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new KamikazeAlly(new Vector2(ene.Pos.X, ene.Pos.Y - 15), SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Bouncer":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new BouncerAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Scanner":
                        if (MediumCap < 6)
                            if (SceneMan.rand.Next(0, 100) <= 40)
                            {
                                SceneMan.Allies.Add(new ScannerAlly(new Vector2(ene.Pos.X, 147), SceneMan, ConnectedPlayer));
                                MediumCap += 1;
                            }
                        break;
                    case "Guardian":
                        if (LargeCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new GuardianAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                LargeCap += 1;
                            }
                        break;
                    case "Meteorologist":
                        if (LargeCap < 2)
                            if (SceneMan.rand.Next(0, 100) <= 20)
                            {
                                SceneMan.Allies.Add(new MeteorologistAlly(ene.Pos, SceneMan, ConnectedPlayer));
                                LargeCap += 1;
                            }
                        break;

                }
            }
        }
        public override void ModAllyOnDeath(Ally Al)
        {
            switch (Al.GetType().Name)
            {
                case "BasicEnemyAlly":
                    SmallCap -= 1;
                    break;
                case "BouncerAlly":
                    MediumCap -= 1;
                    break;
                case "CurlicueAlly":
                    MediumCap -= 1;
                    break;
                case "ExploderAlly":
                    MediumCap -= 1;
                    break;
                case "RetaliatorAlly":
                    MediumCap -= 1;
                    break;
                case "KamikazeAlly":
                    MediumCap -= 1;
                    break;
                case "ScannerAlly":
                    MediumCap -= 1;
                    break;
                case "GuardianAlly":
                    LargeCap -= 1;
                    break;
                case "MeterologistAlly":
                    LargeCap -= 1;
                    break;
            }
        }
    }
}
