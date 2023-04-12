using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShatteredSkies.Classes
{
    class ButtonHelper
    {
        public static Action<SceneManager, Button>[] DefaultStartMenu = new Action<SceneManager, Button>[4]
        {
            //Start Button
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.ResetDicList();
                SceneMan.Particles.Clear();
                SceneMan.game.Scene = 1;
                SceneMan.menupos = 0;
                SceneMan.AltScene = 0;
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.Buttons.Add(new Button(new Vector2(0, 64), new Rectangle(0, 0, SceneMan.Textures["EndlessWorld"].Width, SceneMan.Textures["EndlessWorld"].Height), DefaultMapMenu[0],SceneMan.Textures["EndlessWorld"], SceneMan));

                SceneMan.Buttons.Add(new Button(new Vector2(5,144),new Rectangle(0, 0, 0, 0),DefaultMapMenu[10],"DICTIONARY",SceneMan));

                SceneMan.Buttons.Add(new Button(new Vector2(37, 35), new Rectangle(0, 0, SceneMan.Textures["VeryEasy"].Width, SceneMan.Textures["VeryEasy"].Height), DefaultMapMenu[1],SceneMan.Textures["VeryEasy"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(40, 85), new Rectangle(0, 0, SceneMan.Textures["Easy"].Width, SceneMan.Textures["Easy"].Height), DefaultMapMenu[2],SceneMan.Textures["Easy"], SceneMan));

                SceneMan.Buttons.Add(new Button(new Vector2(80, 35), new Rectangle(0, 0, SceneMan.Textures["Standard"].Width, SceneMan.Textures["Standard"].Height), DefaultMapMenu[3],SceneMan.Textures["Standard"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(82, 85), new Rectangle(0, 0, SceneMan.Textures["Medium"].Width, SceneMan.Textures["Medium"].Height), DefaultMapMenu[4],SceneMan.Textures["Medium"], SceneMan));
                
                SceneMan.Buttons.Add(new Button(new Vector2(119, 35), new Rectangle(0, 0, SceneMan.Textures["Challenging"].Width, SceneMan.Textures["Challenging"].Height), DefaultMapMenu[4],SceneMan.Textures["Challenging"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(123, 85), new Rectangle(0, 0, SceneMan.Textures["VeryHard"].Width, SceneMan.Textures["VeryHard"].Height), DefaultMapMenu[4],SceneMan.Textures["VeryHard"], SceneMan));
                
                SceneMan.Buttons.Add(new Button(new Vector2(170, 35), new Rectangle(0, 0, SceneMan.Textures["Reckless"].Width, SceneMan.Textures["Reckless"].Height), DefaultMapMenu[4],SceneMan.Textures["Reckless"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(170, 85), new Rectangle(0, 0, SceneMan.Textures["Extreme"].Width, SceneMan.Textures["Extreme"].Height), DefaultMapMenu[4],SceneMan.Textures["Extreme"], SceneMan));
                
                SceneMan.Buttons.Add(new Button(new Vector2(215, 48), new Rectangle(0, 0, SceneMan.Textures["Reaper"].Width, SceneMan.Textures["Reaper"].Height), DefaultMapMenu[5],SceneMan.Textures["Reaper"], SceneMan));


            },
            //Co-op Button
            (SceneManager SceneMan, Button butt) => 
            { 
                SceneMan.Players.Add(new Player(new Vector2(0,0),SceneMan,null)
                {
                    Pos = new Vector2(20, 20),
                });
            },
            //Config Button
            (SceneManager SceneMan, Button butt) => { },
            //Exit Button
            (SceneManager SceneMan, Button butt) => {SceneMan.game.Exit(); },
        };

        public static Action<SceneManager, Button>[] DefaultMapMenu = new Action<SceneManager, Button>[12]
        {
            //Endless world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 0);
            }, 
            //Very Ez World
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 3);
            }, 
            //Ez World
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 4);
            }, 
            //Stand World
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 5);
            },
            //Medium owo world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 6);
            },
            //Challeng world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 7);
            },
            //Very hard owo world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 8);
            },
            //Reckless world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 9);
            },
            //Extreme world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 10);
            },
            //Da star world
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 11);
            },
            //Da Dictionary
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.game.Scene = 1;
                SceneMan.AltScene = 10;
                SceneMan.menupos = 0;
                SceneMan.Buttons.Add(new Button(new Vector2(1,44),new Rectangle(0, 0, 0, 0),ShipDictionary[0],"MECHANICAL",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,51),new Rectangle(0, 0, 0, 0),ShipDictionary[1],"CRIMSON",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,58),new Rectangle(0, 0, 0, 0),ShipDictionary[2],"MAGICAL",SceneMan){TextHoverColor = new Color(128,128,128) });

                SceneMan.Buttons.Add(new Button(new Vector2(1,79),new Rectangle(0, 0, 0, 0),ShipDictionary[3],"SMALL",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,86),new Rectangle(0, 0, 0, 0),ShipDictionary[4],"MEDIUM",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,93),new Rectangle(0, 0, 0, 0),ShipDictionary[5],"LARGE",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,100),new Rectangle(0, 0, 0, 0),ShipDictionary[6],"PARAGON",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,107),new Rectangle(0, 0, 0, 0),ShipDictionary[7],"BOSS",SceneMan){TextHoverColor = new Color(128,128,128) });
                
                SceneMan.Buttons.Add(new Button(new Vector2(1,128),new Rectangle(0, 0, 0, 0),ShipDictionary[8],"ENEMY",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,135),new Rectangle(0, 0, 0, 0),ShipDictionary[9],"RELIC",SceneMan){TextHoverColor = new Color(128,128,128) });
                SceneMan.Buttons.Add(new Button(new Vector2(1,142),new Rectangle(0, 0, 0, 0),ShipDictionary[10],"LAST FOUGHT",SceneMan){TextHoverColor = new Color(128,128,128) });


                SceneMan.Buttons.Add(new Button(new Vector2(59, 134), new Rectangle(59, 134, 31, 28), DefaultStartMenu[0], SceneMan.Textures["ShipDictionary"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(96, 133), new Rectangle(96, 133, 15, 28), ShipDictionary[11], SceneMan.Textures["ShipDictionary"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(128, 133), new Rectangle(128, 133, 15, 28), ShipDictionary[12], SceneMan.Textures["ShipDictionary"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(148, 133), new Rectangle(148, 133, 19, 12), ShipDictionary[13], SceneMan.Textures["ShipDictionary"], SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(148, 149), new Rectangle(148, 149, 19, 12), ShipDictionary[14], SceneMan.Textures["ShipDictionary"], SceneMan));

                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)),new Color(0,255,0),SceneMan));
                        SceneMan.DicList[y+(x*4)].Pos = new Vector2(60 + (y * 27), 19+(x*28));
                        SceneMan.Buttons.Add(SceneMan.DicList[y+(x*4)]);
                    }
                }
            },

            (SceneManager SceneMan, Button butt) => { SceneMan.game.Scene = 1; SceneMan.menupos = 0; SceneMan.AltScene = 0;
                SceneMan.LastFought.Clear();
            },

        };

        public static Action<SceneManager, Button>[] DefualtMapStore = new Action<SceneManager, Button>[9]
        {
            //Ship button
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }

                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts[0];
                SceneMan.AltScene = 2;
                for (int i = 0; i < 5; i++)
                {
                    SceneMan.Buttons.Add(new Button(new Vector2(2, 13+(i*24)), new Rectangle(2, 13, 22, 23), SecondaryMapStore[0], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopShip1"], SceneMan.Textures["ShopShip2"], SceneMan.Textures["ShopShip3"], i, 22, 0, SceneMan));
                }
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Wing button
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts[1];
                SceneMan.AltScene = 3;
                for (int i = 0; i < 5; i++)
                {
                    SceneMan.Buttons.Add(new Button(new Vector2(2, 13+(i*24)), new Rectangle(2, 13, 22, 23), SecondaryMapStore[1], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopWings1"], SceneMan.Textures["ShopWings2"], SceneMan.Textures["ShopWings3"], i, 22, 1, SceneMan));
                }
                    SceneMan.Buttons.Add(new Button(new Vector2(26, 13), new Rectangle(2, 13, 22, 23), SecondaryMapStore[1], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopWings1"], SceneMan.Textures["ShopWings2"], SceneMan.Textures["ShopWings3"], 5, 22, 1, SceneMan));
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Bullet 1
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[0];
                SceneMan.AltScene = 4;
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 12), 13+(y*12)), new Rectangle(2, 85, 10, 11), SecondaryMapStore[2], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], y+(x*3), 10, 2, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 85, 10, 11), SecondaryMapStore[2], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 12, 10, 2, SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(50, 25), new Rectangle(2, 85, 10, 11), SecondaryMapStore[2], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 13, 10, 2, SceneMan));

                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Bullet 2          
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[1];
                SceneMan.AltScene = 4;
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 12), 13+(y*12)), new Rectangle(2, 85, 10, 11), SecondaryMapStore[3], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], y+(x*3), 10, 2, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 85, 10, 11), SecondaryMapStore[3], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 12, 10, 2, SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(50, 25), new Rectangle(2, 85, 10, 11), SecondaryMapStore[3], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 13, 10, 2, SceneMan));

                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Bullet 3
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[2];
                SceneMan.AltScene = 4;
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 12), 13+(y*12)), new Rectangle(2, 85, 10, 11), SecondaryMapStore[4], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], y+(x*3), 10, 2, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 85, 10, 11), SecondaryMapStore[4], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 12, 10, 2, SceneMan));
                SceneMan.Buttons.Add(new Button(new Vector2(50, 25), new Rectangle(2, 85, 10, 11), SecondaryMapStore[4], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], 13, 10, 2, SceneMan));

                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Relic 1
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[0];
                SceneMan.AltScene = 5;
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 24), 13+(y*24)), new Rectangle(2, 13, 22, 23), SecondaryMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], y+(x*5), 22, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 13, 22, 23), SecondaryMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 10, 22, SceneMan));//CHAOS
                SceneMan.Buttons.Add(new Button(new Vector2(50, 37), new Rectangle(2, 13, 22, 23), SecondaryMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 11, 22, SceneMan));//ART
                SceneMan.Buttons.Add(new Button(new Vector2(50, 61), new Rectangle(2, 13, 22, 23), SecondaryMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 12, 22, SceneMan));//PASMA
                SceneMan.Buttons.Add(new Button(new Vector2(50, 85), new Rectangle(2, 13, 22, 23), SecondaryMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 13, 22, SceneMan));//TECH
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Relic 2
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[1];
                SceneMan.AltScene = 5;
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 24), 13+(y*24)), new Rectangle(2, 13, 22, 23), SecondaryMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], y+(x*5), 22, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 13, 22, 23), SecondaryMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 10, 22, SceneMan));//CHAOS
                SceneMan.Buttons.Add(new Button(new Vector2(50, 37), new Rectangle(2, 13, 22, 23), SecondaryMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 11, 22, SceneMan));//ART
                SceneMan.Buttons.Add(new Button(new Vector2(50, 61), new Rectangle(2, 13, 22, 23), SecondaryMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 12, 22, SceneMan));//PASMA
                SceneMan.Buttons.Add(new Button(new Vector2(50, 85), new Rectangle(2, 13, 22, 23), SecondaryMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 13, 22, SceneMan));//TECH
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            //Relic 3
            (SceneManager SceneMan, Button butt) =>
            {

                foreach (Button element in SceneMan.Buttons)
                {
                    element.NeedsDeletionQ = true;
                }
                SceneMan.menupos = SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[2];
                SceneMan.AltScene = 5;
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        SceneMan.Buttons.Add(new Button(new Vector2(2 + (x * 24), 13+(y*24)), new Rectangle(2, 13, 22, 23), SecondaryMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], y+(x*5), 22, SceneMan));
                    }
                }
                SceneMan.Buttons.Add(new Button(new Vector2(50, 13), new Rectangle(2, 13, 22, 23), SecondaryMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 10, 22, SceneMan));//CHAOS
                SceneMan.Buttons.Add(new Button(new Vector2(50, 37), new Rectangle(2, 13, 22, 23), SecondaryMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 11, 22, SceneMan));//ART
                SceneMan.Buttons.Add(new Button(new Vector2(50, 61), new Rectangle(2, 13, 22, 23), SecondaryMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 12, 22, SceneMan));//PASMA
                SceneMan.Buttons.Add(new Button(new Vector2(50, 85), new Rectangle(2, 13, 22, 23), SecondaryMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], 13, 22, SceneMan));//TECH
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },

            //Start Button
            (SceneManager SceneMan, Button butt) =>
            {
                if (SceneMan.CurrentPlayerShop+1 == SceneMan.Players.Count)//truely starting the game
                {
                    SceneMan.game.Scene = 2;
                    SceneMan.AltScene = 0;
                    SceneMan.TotalTime = 0;
                    foreach(Player play in SceneMan.Players)
                    {
                        play.Pos = new Vector2(144,150);
                    }
                }
                SceneMan.menupos = 0;
                //relics
                int index = 0;
                bool[] HasAdded = new bool[]{false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false };
                foreach(int x in SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics)
                {
                    index += 1;
                    switch (x)
                    {
                        case 0:
                            break;
                        case 1:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Crimson(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics,1),SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 2:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Magic(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics,2),SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 3://corr
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Corruption(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 3), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 4:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Living(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 4), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 5:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Chronology(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 5), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 6:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Soul(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 6), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 7:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Energy(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 7), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 8:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Future(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 8), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 9:
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Essence(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 9), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                        case 10:
                            if (!HasAdded[x])
                            {
                                SceneMan.ActiveRelics.Add(new Chaos(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics,10),SceneMan, new bool[4]{index==1,index==2,index==3,false}, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                                HasAdded[x] = true;
                            }
                            else
                            {
                                foreach (dynamic dyn in SceneMan.ActiveRelics)
                                {
                                    if (dyn is Chaos)
                                    {
                                        if(dyn.ConnectedPlayer == SceneMan.Players[SceneMan.CurrentPlayerShop])
                                        {
                                            dyn.Slots[index-1] = true;
                                        }
                                    }
                                }
                            }
                            break;
                            case 11: // Infusion
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Infusion(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 11), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                            case 12: // Strike
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Strike(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 12), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                            case 13: // Technology
                            if (!HasAdded[x])
                            { SceneMan.ActiveRelics.Add(new Technology(Helper.CheckForDuplicates(SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 13), SceneMan, SceneMan.Players[SceneMan.CurrentPlayerShop]));
                            HasAdded[x] = true; }
                            break;
                    }
                }
                SceneMan.Players[SceneMan.CurrentPlayerShop].Health = (float)SceneMan.Players[SceneMan.CurrentPlayerShop].AllCores[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts[0]].Stats.MaxHealth;
                if (!(SceneMan.CurrentPlayerShop+1 == SceneMan.Players.Count))
                {
                    SceneMan.CurrentPlayerShop += 1;
                    CreateShopButtions(SceneMan, -1);
                    HasAdded = new bool[]{false,false,false,false,false,false,false,false,false,false,false };
                }
                else 
                {
                    foreach (Button element in SceneMan.Buttons)
                    {
                        element.NeedsDeletionQ = true;
                    }
                }
            },
        };
        public static Action<SceneManager, Button>[] SecondaryMapStore = new Action<SceneManager, Button>[]
        {
            // Cores
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts[0] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 0;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Wings
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts[1] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 1;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Bullets 1
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[0] = butt.StaticSlider;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 2;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
             // Bullets 2
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[1] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 3;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Bullets 3
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets[2] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 4;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Relics 1
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[0] = butt.StaticSlider;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 6;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Relics 2
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[1] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 7;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
            // Relics 3
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[2] = SceneMan.menupos;
                SceneMan.AltScene = 1;
                SceneMan.menupos = 8;
                CreateShopButtions(SceneMan, -1);
                foreach(Button buttt in SceneMan.Buttons)
                {
                    buttt.BlockInput = true;
                }
            },
        };

        public static Action<SceneManager, Button>[] ShipDictionary = new Action<SceneManager, Button>[]
        {
            // Mechanical
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Mechanical");

            },
            // Crimson
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Crimson");

            },
            // Magical
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Magical");
            },
            // Small
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Small");
            },
            // Medium
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Medium");
            },
            // Large
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Large");
            },
            // Paragon
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Paragon");
            },
            // Boss
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Boss");
            },
            // Enemy
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Enemy");
            },
            // Relic
            (SceneManager SceneMan, Button butt) =>
            {
                SearchDictionary(SceneMan,butt,"Relic");
            },
            // Last Fought
            (SceneManager SceneMan, Button butt) =>
            {
                foreach (Button buttt in SceneMan.DicList)
                {
                    bool HasFought = false;
                    foreach (string enemy in SceneMan.LastFought)
                    {
                        if (buttt.Tags.Contains(enemy))
                        {
                            HasFought = true;
                        }
                    }
                    if (HasFought)
                    {
                        if (!buttt.Tags.Contains("Last Fought"))
                        {
                            buttt.Tags.Add("Last Fought");
                        }
                    }
                    else
                    {
                        if (buttt.Tags.Contains("Last Fought"))
                        {
                            buttt.Tags.RemoveAll(I => I == "Last Fought");
                        }
                    }
                }

                SearchDictionary(SceneMan,butt,"Last Fought");
            },
            //Back uses start so | Page Down
            (SceneManager SceneMan, Button butt) =>
            {
                for(int i = 0; i < 4; i++)
                {
                    SceneMan.DicList.Add(SceneMan.DicList[0]);
                    SceneMan.DicList.RemoveAt(0);
                }
                SceneMan.Buttons.RemoveAll(I => I.Tags.Contains("Page"));
                List<Button> CorrectList = new List<Button>();
                List<Button> OkList = new List<Button>();
                List<Button> Badlist = new List<Button>();
                foreach (Button buttt in SceneMan.DicList)
                {
                    bool HasAll = true;
                    bool Has1 = false;
                    foreach (string str in SceneMan.DicTags)
                    {
                        if (buttt.Tags.Contains(str))
                        {
                            Has1 = true;
                        }
                        else
                        {
                            HasAll = false;
                        }
                    }

                    if (HasAll) // Perfect
                    {
                        CorrectList.Add(buttt);
                    }
                    else if((!OkList.Contains(buttt)) && Has1 && (!HasAll)) // okay
                    {
                        OkList.Add(buttt);
                    }
                    else //bad
                    {
                        Badlist.Add(buttt);
                    }
                }

                SceneMan.Particles.Clear();
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (CorrectList.Contains(SceneMan.DicList[y + (x * 4)]))
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)),new Color(0,255,0),SceneMan));
                        }
                        else if (OkList.Contains(SceneMan.DicList[y + (x * 4)]))
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 128, 0), SceneMan));
                        }
                        else
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 0, 0), SceneMan));
                        }

                        SceneMan.DicList[y + (x * 4)].Pos = new Vector2(60 + (y * 27), 19 + (x * 28));
                        SceneMan.Buttons.Add(SceneMan.DicList[y + (x * 4)]);
                    }
                }
            },
            //Page Up
            (SceneManager SceneMan, Button butt) =>
            {
                for(int i = 0; i < 4; i++)
                {
                    SceneMan.DicList.Insert(0,SceneMan.DicList[^1]);
                    SceneMan.DicList.RemoveAt(SceneMan.DicList.Count-1);
                }
                SceneMan.Buttons.RemoveAll(I => I.Tags.Contains("Page"));
                List<Button> CorrectList = new List<Button>();
                List<Button> OkList = new List<Button>();
                List<Button> Badlist = new List<Button>();
                foreach (Button buttt in SceneMan.DicList)
                {
                    bool HasAll = true;
                    bool Has1 = false;
                    foreach (string str in SceneMan.DicTags)
                    {
                        if (buttt.Tags.Contains(str))
                        {
                            Has1 = true;
                        }
                        else
                        {
                            HasAll = false;
                        }
                    }

                    if (HasAll) // Perfect
                    {
                        CorrectList.Add(buttt);
                    }
                    else if((!OkList.Contains(buttt)) && Has1 && (!HasAll)) // okay
                    {
                        OkList.Add(buttt);
                    }
                    else //bad
                    {
                        Badlist.Add(buttt);
                    }
                }

                SceneMan.Particles.Clear();
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (CorrectList.Contains(SceneMan.DicList[y + (x * 4)]))
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)),new Color(0,255,0),SceneMan));
                        }
                        else if (OkList.Contains(SceneMan.DicList[y + (x * 4)]))
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 128, 0), SceneMan));
                        }
                        else
                        {
                            SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 0, 0), SceneMan));
                        }

                        SceneMan.DicList[y + (x * 4)].Pos = new Vector2(60 + (y * 27), 19 + (x * 28));
                        SceneMan.Buttons.Add(SceneMan.DicList[y + (x * 4)]);
                    }
                }
            },
            //Quick fight
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.CurrentLevel = 1;
                
                SceneMan.game.Scene = 2;
                SceneMan.AltScene = 0;
                SceneMan.TotalTime = 0;
                if (SceneMan.Buttons[SceneMan.SelectedEnemy].Tags.Contains("Page"))
                {
                    if (SceneMan.Buttons[SceneMan.SelectedEnemy].Tags[^1] != "Last Fought")
                    {
                        string id = SceneMan.Buttons[SceneMan.SelectedEnemy].Tags.First(r => r.Contains("id"));
                        id = id.Remove(0,2);
                        if (Int32.TryParse(id, out int numValue))
                        {
                            SceneMan.Levels[1].Layers[0].data[0] = numValue;
                        }
                        else
                        {
                            SceneMan.Levels[1].Layers[0].data[0] = 1;
                        }
                    }
                    else
                    {

                    }
                }
                SceneMan.Particles.Clear();
                SceneMan.GameTime = 1;
                foreach (Button buttt in SceneMan.Buttons)
                {
                    buttt.NeedsDeletionQ = true;
                }
                foreach (Player play in SceneMan.Players)
                {
                    play.CurrentShipParts[0]=0;
                    play.CurrentShipParts[1]=0;
                    play.Pos = new Vector2(144,150);
                }
            },
            //Long fight
            (SceneManager SceneMan, Button butt) =>
            {
                CreateShopButtions(SceneMan, 2);
                if (SceneMan.Buttons[SceneMan.SelectedEnemy].Tags.Contains("Page"))
                {
                    if (SceneMan.Buttons[SceneMan.SelectedEnemy].Tags[^1] != "Last Fought")
                    {
                        string id = SceneMan.Buttons[SceneMan.SelectedEnemy].Tags.First(r => r.Contains("id"));
                        id = id.Remove(0,2);
                        if (Int32.TryParse(id, out int numValue))
                        {
                            for(int i = 0; i < SceneMan.Levels[2].Layers[0].data.Length; i++)
                            {
                                if (SceneMan.Levels[2].Layers[0].data[i]==1) SceneMan.Levels[2].Layers[0].data[i] = numValue;
                            }
                        }
                        else
                        {
                            for(int i = 0; i < SceneMan.Levels[2].Layers[0].data.Length; i++)
                            {
                                if (SceneMan.Levels[2].Layers[0].data[i]==1) SceneMan.Levels[2].Layers[0].data[i] = 1;
                            }
                        }
                    }
                    else
                    {

                    }
                }
                SceneMan.Particles.Clear();
            },
            //Page buttons
            (SceneManager SceneMan, Button butt) =>
            {
                SceneMan.SelectedEnemy = SceneMan.menupos;
            }

        };

        private static void CreateShopButtions(SceneManager SceneMan, int Level)
        {
            SceneMan.AltScene = 1;
            if (Level > -1)
            {
                SceneMan.menupos = 0;
                SceneMan.CurrentLevel = Level;
                SceneMan.GameTime = SceneMan.Levels[Level].Layers[0].height;
                if (SceneMan.Levels[Level].Layers[0].speed != null)
                {
                    SceneMan.Speed = (double)SceneMan.Levels[Level].Layers[0].speed;
                }
            }
            foreach (Button element in SceneMan.Buttons)
            {
                element.NeedsDeletionQ = true;
            }
            SceneMan.Buttons.Add(new Button(new Vector2(2, 13), new Rectangle(2, 13, 22, 23), DefualtMapStore[0], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopShip1"], SceneMan.Textures["ShopShip2"], SceneMan.Textures["ShopShip3"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts, 0, 22, 0, SceneMan));
            SceneMan.Buttons.Add(new Button(new Vector2(2, 51), new Rectangle(2, 51, 22, 23), DefualtMapStore[1], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopWings1"], SceneMan.Textures["ShopWings2"], SceneMan.Textures["ShopWings3"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentShipParts, 1, 22, 1, SceneMan));

            SceneMan.Buttons.Add(new Button(new Vector2(2, 85), new Rectangle(2, 85, 10, 11), DefualtMapStore[2], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets, 0, 10, 2, SceneMan));
            SceneMan.Buttons.Add(new Button(new Vector2(2, 97), new Rectangle(2, 97, 10, 11), DefualtMapStore[3], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets, 1, 10, 2, SceneMan));
            SceneMan.Buttons.Add(new Button(new Vector2(2, 109), new Rectangle(2, 109, 10, 11), DefualtMapStore[4], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopBullets1"], SceneMan.Textures["ShopBullets2"], SceneMan.Textures["ShopBullets3"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentBullets, 2, 10, 2, SceneMan));

            SceneMan.Buttons.Add(new Button(new Vector2(0, 123), new Rectangle(0, 123, 46, 39), DefualtMapStore[8], SceneMan.Textures["UpgradeShop"], SceneMan));

            SceneMan.Buttons.Add(new Button(new Vector2(87, 13), new Rectangle(87, 13, 22, 23), DefualtMapStore[5], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 0, 22, SceneMan));
            SceneMan.Buttons.Add(new Button(new Vector2(87, 51), new Rectangle(87, 51, 22, 23), DefualtMapStore[6], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 1, 22, SceneMan));
            SceneMan.Buttons.Add(new Button(new Vector2(87, 90), new Rectangle(87, 90, 22, 23), DefualtMapStore[7], SceneMan.Textures["UpgradeShop"], SceneMan.Textures["ShopRelicSlider"], SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics, 2, 22, SceneMan));
        }

        private static void SearchDictionary(SceneManager SceneMan,Button butt,string tag)
        {
            if (SceneMan.DicTags.Contains(tag))
            {
                SceneMan.DicTags.RemoveAll(I => I == tag);
                butt.TextColor = new Color(255, 255, 255);
                butt.TextHoverColor = new Color(128, 128, 128);
            }
            else
            {
                SceneMan.DicTags.Add(tag);
                butt.TextColor = new Color(128, 0, 255);
                butt.TextHoverColor = new Color(64, 0, 64);
            }

            SceneMan.Particles.Clear();
            SceneMan.Buttons.RemoveAll(I => I.Tags.Contains("Page"));

            List<Button> CorrectList = new List<Button>();
            List<Button> OkList = new List<Button>();
            List<Button> Badlist = new List<Button>();
            foreach (Button buttt in SceneMan.DicList)
            {
                bool HasAll = true;
                bool Has1 = false;
                foreach (string str in SceneMan.DicTags)
                {
                    if (buttt.Tags.Contains(str))
                    {
                        Has1 = true;
                    }
                    else
                    {
                        HasAll = false;
                    }
                }

                if (HasAll) // Perfect
                {
                    CorrectList.Add(buttt);
                }
                else if((!OkList.Contains(buttt)) && Has1 && (!HasAll)) // okay
                {
                    OkList.Add(buttt);
                }
                else //bad
                {
                    Badlist.Add(buttt);
                }
            }

            SceneMan.DicList.Clear();
            SceneMan.DicList.AddRange(CorrectList);
            SceneMan.DicList.AddRange(OkList);
            SceneMan.DicList.AddRange(Badlist);
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (CorrectList.Contains(SceneMan.DicList[y + (x * 4)]))
                    {
                        SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)),new Color(0,255,0),SceneMan));
                    }
                    else if (OkList.Contains(SceneMan.DicList[y + (x * 4)]))
                    {
                        SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 128, 0), SceneMan));
                    }
                    else
                    {
                        SceneMan.Particles.Add(new DictionaryParticle(new Vector2(60 + (y * 27), 19 + (x * 28)), new Color(255, 0, 0), SceneMan));
                    }

                    SceneMan.DicList[y + (x * 4)].Pos = new Vector2(60 + (y * 27), 19 + (x * 28));
                    SceneMan.Buttons.Add(SceneMan.DicList[y + (x * 4)]);
                }
            }
        }
    }
}
