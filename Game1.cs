using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShatteredSkies.Classes;
using System;
using System.Collections.Generic;

namespace ShatteredSkies
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont pico;
        public SpriteFont debug;

        public int Scene = 0; // 0,1,2 Menu,Map,Game
        public SceneManager SceneManager1;

        readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;   // set this value to the desired height of your window;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Window.Position = new Point(0, 0);
            Window.IsBorderless = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //making all the pictures the game will need
            pico = Content.Load<SpriteFont>("Pico");
            debug = Content.Load<SpriteFont>("Debug");

            //Dev Pictures
            Textures.Add("AnimationTest", Content.Load<Texture2D>("AnimationTest")); //A test of my animation system
            Textures.Add("BoundingBox", Content.Load<Texture2D>("BoundingBox")); // a simple dev box to align things nicely
            Textures.Add("WhitePixel", Content.Load<Texture2D>("MultiSceneStuff/WhitePixel")); // >:( monogame is a silly


            //Menu Textures
            Textures.Add("Title", Content.Load<Texture2D>("MenuStuff/Title")); //Title Logo
            Textures.Add("MenuArrow", Content.Load<Texture2D>("MenuStuff/MenuArrow")); // Menu arrow
            Textures.Add("BackwardsMenuArrow", Content.Load<Texture2D>("MenuStuff/BackwardsMenuArrow")); // the same menu arrow but backwards
            Textures.Add("BackgroundArt", Content.Load<Texture2D>("MenuStuff/BackgroundArt")); // a simple dev box to align things nicely
            ////////////////////////////////////
            ////////// Map Textures ////////////
            ////////////////////////////////////
            Textures.Add("EndlessWorld", Content.Load<Texture2D>("MapStuff/Endless Planet")); //Demo World

            Textures.Add("VeryEasy", Content.Load<Texture2D>("MapStuff/Planets/VERYEASY"));
            Textures.Add("Easy", Content.Load<Texture2D>("MapStuff/Planets/EASY"));
            Textures.Add("Standard", Content.Load<Texture2D>("MapStuff/Planets/STANDARD"));
            Textures.Add("Medium", Content.Load<Texture2D>("MapStuff/Planets/MEDIUM"));
            Textures.Add("Challenging", Content.Load<Texture2D>("MapStuff/Planets/CHALLENGING"));
            Textures.Add("VeryHard", Content.Load<Texture2D>("MapStuff/Planets/VERYHARD"));
            Textures.Add("Reckless", Content.Load<Texture2D>("MapStuff/Planets/RECKLESS"));
            Textures.Add("Extreme", Content.Load<Texture2D>("MapStuff/Planets/EXTREME"));
            Textures.Add("Reaper", Content.Load<Texture2D>("MapStuff/Planets/REAPER"));

            //Map shop textures
            Textures.Add("UpgradeShop", Content.Load<Texture2D>("MapStuff/Store/UpgradeStore")); //Shop Template
            Textures.Add("SelectionOutlineBig", Content.Load<Texture2D>("MapStuff/Store/SelectionOutlineBig")); //Selector Big
            Textures.Add("ShopShip1", Content.Load<Texture2D>("MapStuff/Store/ShopShip1")); //Ship Slider Borders
            Textures.Add("ShopShip2", Content.Load<Texture2D>("MapStuff/Store/ShopShip2")); //Ship Slider Insides
            Textures.Add("ShopShip3", Content.Load<Texture2D>("MapStuff/Store/ShopShip3")); //Ship Slider Insides 2
            Textures.Add("ShopWings1", Content.Load<Texture2D>("MapStuff/Store/ShopWings1")); //Ship Slider Borders
            Textures.Add("ShopWings2", Content.Load<Texture2D>("MapStuff/Store/ShopWings2")); //Ship Slider Insides
            Textures.Add("ShopWings3", Content.Load<Texture2D>("MapStuff/Store/ShopWings3")); //Ship Slider Insides 2
            Textures.Add("ShopWingSliderBorder", Content.Load<Texture2D>("MapStuff/Store/ShopWingSliderBorder")); //Wing Slider Borders
            Textures.Add("ShopWingSliderInside", Content.Load<Texture2D>("MapStuff/Store/ShopWingSliderInside")); //Wing Slider Insides
            Textures.Add("ShopBullets1", Content.Load<Texture2D>("MapStuff/Store/ShopBullets1")); //Bullet Slider Borders
            Textures.Add("ShopBullets2", Content.Load<Texture2D>("MapStuff/Store/ShopBullets2")); //Bullet Slider Borders
            Textures.Add("ShopBullets3", Content.Load<Texture2D>("MapStuff/Store/ShopBullets3")); //Bullet Slider Borders
            Textures.Add("ShopRelicSlider", Content.Load<Texture2D>("MapStuff/Store/ShopRelicSlider")); //Wing Slider Insides

            //Dictionary
            Textures.Add("ShipDictionary", Content.Load<Texture2D>("MapStuff/ShipDictionary/Dictionary")); 
            Textures.Add("EnemyList", Content.Load<Texture2D>("MapStuff/ShipDictionary/EnemyList"));
            Textures.Add("EnemyRelicList", Content.Load<Texture2D>("MapStuff/ShipDictionary/EnemyRelicList"));
            Textures.Add("DictionaryParticle", Content.Load<Texture2D>("MapStuff/ShipDictionary/DictionaryParticle"));

            ////////////////////////////////////
            /////////// Game Textures //////////
            ////////////////////////////////////
            //particles
            Textures.Add("Snap", Content.Load<Texture2D>("GameStuff/Snap")); //Snap
            Textures.Add("PlasmaIndicator", Content.Load<Texture2D>("GameStuff/PlasmaIndicator")); //PlasmaIndicator
            Textures.Add("PlasmaShock", Content.Load<Texture2D>("GameStuff/PlasmaShock")); //PlasmaShock
            Textures.Add("FlameParticle", Content.Load<Texture2D>("GameStuff/FlameParticle")); //FlameParticle
            Textures.Add("StarCannonParticle", Content.Load<Texture2D>("GameStuff/StarCannonParticle")); //StarCannonParticle
            Textures.Add("WarpParticle", Content.Load<Texture2D>("GameStuff/WarpParticle")); //WarpParticle
            Textures.Add("TargetParticle", Content.Load<Texture2D>("GameStuff/TargetParticle")); //TargetParticle
            //ui stuff
            Textures.Add("HealthBar", Content.Load<Texture2D>("GameStuff/HealthBar")); //HealthBar lamo
            Textures.Add("SpecialBarBorder", Content.Load<Texture2D>("GameStuff/UI/SpecialBarBorder")); //SpecialBar lamo
            Textures.Add("SpecialBarInside", Content.Load<Texture2D>("GameStuff/UI/SpecialBarInside")); //SpecialBar Inside lamo
            Textures.Add("MagicUI", Content.Load<Texture2D>("GameStuff/MagicUI")); //SpecialBar Inside lamo
            Textures.Add("EssenceUI", Content.Load<Texture2D>("GameStuff/EssenceUI")); //SpecialBar Inside lamo
            Textures.Add("FutureUI", Content.Load<Texture2D>("GameStuff/FutureUI")); //SpecialBar Inside lamo
            Textures.Add("BulletUI", Content.Load<Texture2D>("GameStuff/BulletUI")); //SpecialBar lamo
            //Bullets
            Textures.Add("BulletSheet", Content.Load<Texture2D>("GameStuff/Bullets/BulletSheet")); //Bullet Sheet will prob et rid of
            Textures.Add("BasicShot", Content.Load<Texture2D>("GameStuff/Bullets/BasicShot")); //BasicShot
            Textures.Add("LockOnShot", Content.Load<Texture2D>("GameStuff/Bullets/LockOnShot")); //BasicShot
            Textures.Add("ExplosiveMine", Content.Load<Texture2D>("GameStuff/Bullets/ExplosiveMines")); //BasicShot
            Textures.Add("StarCannon", Content.Load<Texture2D>("GameStuff/Bullets/StarCannon")); //BasicShot
            Textures.Add("ExplosiveShot", Content.Load<Texture2D>("GameStuff/Bullets/ExplosiveShot")); //ExplosiveShot
            Textures.Add("Bouncy", Content.Load<Texture2D>("GameStuff/Bullets/Bouncy")); //bouncy bullet
            Textures.Add("Blood", Content.Load<Texture2D>("GameStuff/Bullets/Blood")); //Blood bullet
            Textures.Add("BloodClot", Content.Load<Texture2D>("GameStuff/Bullets/BloodClot")); //BloodClot bullet
            Textures.Add("TopHat", Content.Load<Texture2D>("GameStuff/Bullets/TopHat")); //TopHat bullet
            Textures.Add("LivingThorn", Content.Load<Texture2D>("GameStuff/Bullets/LivingThorn")); //TopHat bullet
            Textures.Add("LivingFlower", Content.Load<Texture2D>("GameStuff/Bullets/LivingFlower")); //TopHat bullet
            Textures.Add("CryoDetonator", Content.Load<Texture2D>("GameStuff/Bullets/CryoDetonator")); //TopHat bullet
            Textures.Add("ShatteringShot", Content.Load<Texture2D>("GameStuff/Bullets/ShatteringShot")); //TopHat bullet
            Textures.Add("EssenceOrb", Content.Load<Texture2D>("GameStuff/Bullets/EssenceOrb"));
            Textures.Add("EssenceTimer", Content.Load<Texture2D>("GameStuff/Bullets/EssenceTimer"));
            //////// Enemy Bullets ////////
            Textures.Add("LazerBullet", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/LazerBullet"));
            Textures.Add("JinxBullet", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/JinxBullet"));
            Textures.Add("EnemyFlame", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/EnemyFlame"));
            Textures.Add("Spike", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/Spike"));
            Textures.Add("Cloud", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/Nowcaster/Cloud")); // Now caster
            Textures.Add("Hurricane", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/Nowcaster/Hurricane"));
            Textures.Add("ContaminatorBomb", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/ContaminatorBomb")); // Contaiminator
            Textures.Add("ContaminatorAOE", Content.Load<Texture2D>("GameStuff/Bullets/EnemyBullets/ContaminatorAOE"));

            //Random Game Objects
            Textures.Add("Shield", Content.Load<Texture2D>("GameStuff/GameObjects/Shield"));

            //Effects
            Textures.Add("BleedEffect", Content.Load<Texture2D>("GameStuff/Effects/BleedEffect")); //BleedEffect
            Textures.Add("BleedEffect2Wide", Content.Load<Texture2D>("GameStuff/Effects/BloodEffect2Wide")); //BleedEffect 2 wid
            Textures.Add("EffectIndicators", Content.Load<Texture2D>("GameStuff/EffectIndicators")); //BleedEffect 2 wid
            Textures.Add("CryoCircle", Content.Load<Texture2D>("GameStuff/CryoCircle")); //circle lamo
            Textures.Add("PulsarWarning", Content.Load<Texture2D>("GameStuff/PulsarWarning")); //circle lamo
            //Relic Effects
            Textures.Add("ChronologyCircle", Content.Load<Texture2D>("GameStuff/ChronologyCircle")); //circle lamo
            //Cores
            Textures.Add("Dart", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Dart/Dart")); //A sheet of all core insides
            Textures.Add("Para", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Para/Para")); //A sheet of all core insides
            Textures.Add("Omega", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Omega/Omega")); //A sheet of all core insides
            Textures.Add("Assassin", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Assassin/Assassin")); //A sheet of all core insides
            Textures.Add("Destroyer", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Destroyer/Destroyer")); //A sheet of all core insides
            Textures.Add("Dart_Thrusters", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Cores/Dart/DartThrusters")); //A sheet of all core borders

            Textures.Add("ShipGhost", Content.Load<Texture2D>("MultiSceneStuff/ShipGhost")); //Death Icon
            //Core thrusters
            //Wings
            Textures.Add("Colliders", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Colliders/Colliders"));
            Textures.Add("Shimmers", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Shimmers/Shimmers"));
            Textures.Add("Sparklers", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Sparklers/Sparklers"));
            Textures.Add("Flashers", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Flashers/Flashers"));
            Textures.Add("Fragmentisers", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Fragmentisers/Fragmentisers"));
            Textures.Add("Paralyzers", Content.Load<Texture2D>("MultiSceneStuff/AllShips/Wings/Paralyzers/Paralyzers"));
            //Allies
            Textures.Add("MicroDrone", Content.Load<Texture2D>("GameStuff/Allies/MicroDrone")); //MicroDrone
            Textures.Add("OmegaDrone", Content.Load<Texture2D>("GameStuff/Allies/OmegaDrone")); //Omega Drone
            /////// Enemies ///////
            //Small Enemies
            Textures.Add("BasicEnemyOutline", Content.Load<Texture2D>("GameStuff/Enemies/BasicEnemy/BasicEnemyOutline"));
            Textures.Add("BasicEnemyInside", Content.Load<Texture2D>("GameStuff/Enemies/BasicEnemy/BasicEnemyInside"));
            Textures.Add("SizzlerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Sizzler/SizzlerOutline"));
            Textures.Add("SizzlerInside", Content.Load<Texture2D>("GameStuff/Enemies/Sizzler/SizzlerInside"));
            //Medium Enemies
            Textures.Add("ExploderOutline", Content.Load<Texture2D>("GameStuff/Enemies/Exploder/ExploderOutline"));// Exploder
            Textures.Add("ExploderInside", Content.Load<Texture2D>("GameStuff/Enemies/Exploder/ExploderInside"));
            Textures.Add("RetaliatorOutline", Content.Load<Texture2D>("GameStuff/Enemies/Retaliator/RetaliatorOutline"));// Retaliator
            Textures.Add("RetaliatorInside", Content.Load<Texture2D>("GameStuff/Enemies/Retaliator/RetaliatorInside"));
            Textures.Add("CurlicueOutline", Content.Load<Texture2D>("GameStuff/Enemies/Curlicue/CurlicueOutline"));// Curlique
            Textures.Add("CurlicueInside", Content.Load<Texture2D>("GameStuff/Enemies/Curlicue/CurlicueInside"));
            Textures.Add("KamikazeOutline", Content.Load<Texture2D>("GameStuff/Enemies/Kamikaze/KamikazeOutline"));// Kamikaze
            Textures.Add("KamikazeInside", Content.Load<Texture2D>("GameStuff/Enemies/Kamikaze/KamikazeInside"));
            Textures.Add("BouncerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Bouncer/BouncerOutline"));// Bouncer
            Textures.Add("BouncerInside", Content.Load<Texture2D>("GameStuff/Enemies/Bouncer/BouncerInside"));
            Textures.Add("ScannerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Scanner/ScannerOutline"));// Scanner
            Textures.Add("ScannerInside", Content.Load<Texture2D>("GameStuff/Enemies/Scanner/ScannerInside"));
            Textures.Add("JinxOutline", Content.Load<Texture2D>("GameStuff/Enemies/Jinx/JinxOutline"));// Jinx
            Textures.Add("JinxInside", Content.Load<Texture2D>("GameStuff/Enemies/Jinx/JinxInside"));
            Textures.Add("SniperOutline", Content.Load<Texture2D>("GameStuff/Enemies/Sniper/SniperOutline")); // Sniper
            Textures.Add("SniperInside", Content.Load<Texture2D>("GameStuff/Enemies/Sniper/SniperInside"));
            Textures.Add("SniperGunOutline", Content.Load<Texture2D>("GameStuff/Enemies/Sniper/SniperGunOutline"));
            Textures.Add("SniperGunInside", Content.Load<Texture2D>("GameStuff/Enemies/Sniper/SniperGunInside"));
            Textures.Add("FlamethrowerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Flamethrower/FlamethrowerOutline")); // Flamethrower
            Textures.Add("FlamethrowerInside", Content.Load<Texture2D>("GameStuff/Enemies/Flamethrower/FlamethrowerInside"));
            //Large Enemeis
            Textures.Add("GuardianOutline", Content.Load<Texture2D>("GameStuff/Enemies/Guardian/GuardianOutline"));// Guardian
            Textures.Add("GuardianInside", Content.Load<Texture2D>("GameStuff/Enemies/Guardian/GuardianInside"));// Guardian
            Textures.Add("GuardianWall", Content.Load<Texture2D>("GameStuff/Enemies/Guardian/Wall"));
            Textures.Add("MeteorologistOutline", Content.Load<Texture2D>("GameStuff/Enemies/Meteorologist/MeteorologistOutline"));// Meteorologist
            Textures.Add("MeteorologistInside", Content.Load<Texture2D>("GameStuff/Enemies/Meteorologist/MeteorologistInside"));// Meteorologist
            Textures.Add("MeteorologistRocks", Content.Load<Texture2D>("GameStuff/Enemies/Meteorologist/Rocks"));
            Textures.Add("LazermancerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Lazermancer/LazermancerOutline"));// Lazermancer
            Textures.Add("LazermancerInside", Content.Load<Texture2D>("GameStuff/Enemies/Lazermancer/LazermancerInside"));
            Textures.Add("PulsarOutline", Content.Load<Texture2D>("GameStuff/Enemies/Pulsar/PulsarOutline")); // Pulsar
            Textures.Add("PulsarInside", Content.Load<Texture2D>("GameStuff/Enemies/Pulsar/PulsarInside"));
            Textures.Add("PulsarRing", Content.Load<Texture2D>("GameStuff/Enemies/Pulsar/PulsarRing"));
            Textures.Add("SpikerOutline", Content.Load<Texture2D>("GameStuff/Enemies/Spiker/SpikerOutline")); // Spiker
            Textures.Add("SpikerInside", Content.Load<Texture2D>("GameStuff/Enemies/Spiker/SpikerInside"));
            Textures.Add("MirageInside", Content.Load<Texture2D>("GameStuff/Enemies/Mirage/MirageInside")); // Mirage
            //Paragons
            Textures.Add("Overseer", Content.Load<Texture2D>("GameStuff/Enemies/Overseer/Overseer")); // Overseer
            Textures.Add("NormalCore", Content.Load<Texture2D>("GameStuff/Enemies/Overseer/NormalCore"));
            Textures.Add("HomingCore", Content.Load<Texture2D>("GameStuff/Enemies/Overseer/HomingCore"));
            Textures.Add("FlameCore", Content.Load<Texture2D>("GameStuff/Enemies/Overseer/FlameCore"));
            Textures.Add("BounceCore", Content.Load<Texture2D>("GameStuff/Enemies/Overseer/BounceCore"));
            Textures.Add("Nowcaster", Content.Load<Texture2D>("GameStuff/Enemies/Nowcaster/Nowcaster")); // Nowcaster
            Textures.Add("ContaminatorOutline", Content.Load<Texture2D>("GameStuff/Enemies/Contaminator/ContaminatorOutline")); // Contaminator
            Textures.Add("ContaminatorInside", Content.Load<Texture2D>("GameStuff/Enemies/Contaminator/ContaminatorInside"));
            //Bosses

            /////// Resultscreen ////////
            Textures.Add("ResultScreen", Content.Load<Texture2D>("GameStuff/ResultScreen/ResultScreen")); // Nowcaster

            SceneManager1 = new SceneManager(Textures, pico) { game = this };
        }

        protected override void Update(GameTime gameTime)
        {
            //Update Function
            SceneManager1.state = GamePad.GetState(0);//gets the current state

            for (int i = 0; i < SceneManager1.Players.Count; i++)
            {
                SceneManager1.Players[i].State = GamePad.GetState(i);
            }
            SceneManager1.UpdateAll(gameTime);

            SceneManager1.TimeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds;//how long the game has been running

            if (Scene == 0)
            {
                SceneManager1.UpdateMenu(gameTime);
            }
            else if (Scene == 1)
            {
                SceneManager1.UpdateMap(gameTime);
            }
            else
            {
                SceneManager1.UpdateGame(gameTime);
            }

            for (int i = 0; i < SceneManager1.Players.Count; i++)
            {
                SceneManager1.Players[i].LastState = SceneManager1.Players[i].State;
            }

            SceneManager1.LastState = SceneManager1.state; //gets the current state to use for the next frame

            base.Update(gameTime);
        }
        //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/192
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //Draw Function
            switch (Scene)
            {
                case 0://menu
                    _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 192f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 108f, 1f));
                    break;
                case 1://map
                    _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 288f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 162f, 1f));
                    break;
                case 2://game
                    if (SceneManager1.AltScene == 0)
                    {
                        _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 288f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 162f, 1f));

                    }
                    else
                    {
                        _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 192f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 108f, 1f));

                    }
                    //_spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 384f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 216f, 1f));
                    break;
            }
            Console.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);

            SceneManager1.DrawAll(_spriteBatch);

            if (Scene == 0)
            {
                SceneManager1.DrawMenu(_spriteBatch);
            }
            else if (Scene == 1)
            {
                SceneManager1.DrawMap(_spriteBatch);
            }
            else
            {
                SceneManager1.DrawGame(_spriteBatch);
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
