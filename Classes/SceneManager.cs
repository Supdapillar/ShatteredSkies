using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MonoGame.Extended.Graphics.Geometry;
using System.Linq;

namespace ShatteredSkies.Classes
{
    public class SceneManager
    {
        public float TimeSinceStart;

        public Dictionary<string, Texture2D> Textures;

        public List<Particle> Particles = new List<Particle>();
        public List<Ally> Allies = new List<Ally>();
        public List<Player> Players = new List<Player>();
        public List<Bullet> Bullets = new List<Bullet>();
        public List<Button> Buttons = new List<Button>();
        public List<Enemy> Enemies = new List<Enemy>();
        public List<EnemyBullet> EnemyBullets = new List<EnemyBullet>();
        public double TotalTime;

        public SpriteFont Pico8;

        public Game1 game;

        public Color RealDarkCyan = new Color(0, 128, 128);
        public Color Purple = new Color(128, 0, 255);
        public Color DarkPurple = new Color(64, 0, 128);

        public Random rand = new Random();

        public int AltScene = 0; //0 is St,Con,Exit //1 is slot 1-3// 2 is deletion of slot // 3 is resolution,gamepad setup,keyboard setup, volume // 4 gamepad setup // 5 keyboard setup // 6 volume
        public int COUNTER = 0;



        public GamePadState state;
        public GamePadState LastState;

        public int menupos;

        ///////////////// DICTIONARY STUFF ///////////////////////
        public List<Button> DicList = new List<Button>();
        public List<String> DicTags = new List<string>();
        public List<String> LastFought = new List<string>();
        public int SelectedEnemy = 0;
        public Dictionary<string, string> EnemyDescriptions = new Dictionary<string, string>() 
        {
            { "BasicEnemy",
                "AS BASIC AS IT GET, DOESN'T DO MUCH"
            },
            { "Bouncer",
                "LAUNCHES A BARRAGE OF 3 BULLETS THAT BOUNCE THREE TIMES OFF THE WALLS"
            },
            { "Curlicue",
                "FOR EVERY DAMAGE IT TAKES IT GETS 10% FASTER TIL IT REACHES INFINITE FIRERATE"
            },
            { "Exploder",
                "CHASES THE PLAYER AND IF THE PLAYER GETS CLOSE IT EXPLODES INTO A RING OF BULLETS "
            },
            { "FlamethrowerEnemy",
                "IF AN ALLY GETS CLOSE IT ACTIVATES A FLAMETHROWER THAT DEALS LOTS OF DAMAGE"
            },
            { "Guardian",
                "CREATES A MACHINE GUN WALL AFTER 12 SECONDS"
            },
            { "Jinx",
                "CREATES A MAGIC CUBE THE HOMES IN ON THE PLAYER"
            },
            { "Kamikaze",
                "WHEN IT DIES IN FALLS EXTREMLY FAST AND DEALS 4 DAMAGE"
            },
            { "Lazermancer",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Meteorologist",
                "RAINS METEORS FROM THE SKY AND AFTER 10 SECONDS MAKES METEORS MUCH FASTER"
            },
            { "Nowcaster",
                "lol"
            },
            { "Overseer",
                "SPAWNS WITH 4 RANDOM CUBES THAT WILL FOLLOW IT AND CREATE BULLETS"
            },
            { "Pulsar",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Retaliator",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Scanner",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Sizzler",
                "HAS VERY LOW HEALTH AND FIRES A BULLET AT THE PLAYER WHEN IT DIES"
            },
            { "Sniper",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Spiker",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Watchdog",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "CXZCZX",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            //Relics
            { "Warp",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Target",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Health",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Curse",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Shield",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
        };

        public Dictionary<string, string> EnemyStats = new Dictionary<string, string>() 
        {
            { "BasicEnemy",
                "NAME: BASIC ENEMY @"+
                "FACTION: MECHANICAL @"+
                "CLASS: SMALL @"+
                "HP: 3 @"+
                "FIRERATE: 0.5-1.5 @"+
                "SPEED: SLOW @" +
                "TIME: 15 @"
            },
            { "Bouncer",
                "NAME: BOUNCER @"+
                "FACTION: MECHANICAL @"+
                "CLASS: MEDIUM @"+
                "HP: 5 @"+
                "FIRERATE: 0.75-1.75 @"+
                "SPEED: MEDIUM @" +
                "TIME: 15 @"
            },
            { "Curlicue",
                "NAME: CURLICUE @"+
                "FACTION: MECHANICAL @"+
                "CLASS: MEDIUM @"+
                "HP: 7 @"+
                "FIRERATE: HP/10 @"+
                "SPEED: SLOW @" +
                "TIME: 40 @"
            },
            { "Exploder",
                "NAME: BASIC ENEMY @"+
                "FACTION: CRIMSON @"+
                "CLASS: MEDIUM @"+
                "HP: 7 @"+
                "FIRERATE: 0.5-1.5 @"+
                "SPEED: SLOW @" +
                "TIME: 15 @"
            },
            { "FlamethrowerEnemy",
                "NAME: FLAMETHROWER @"+
                "FACTION: MECHANICAL @"+
                "CLASS: SMALL @"+
                "HP: 8 @"+
                "RANGE: 50PX @"+
                "FIRERATE: 0 @"+
                "SPEED: MEDIUM @" +
                "TIME: 15 @"
            },
            { "Guardian",
                "NAME: GUARDIAN @"+
                "FACTION: MECHANICAL @"+
                "CLASS: LARGE @"+
                "HP: 40 @"+
                "FIRERATE: 0.25-0.5 @"+
                "SPEED: SLOW @" +
                "TIME: LOL @"
            },
            { "Jinx",
                "NAME: JINX @"+
                "FACTION: MAGICAL @"+
                "CLASS: MEDIUM @"+
                "HP: 8 @"+
                "FIRERATE: 1-2 @"+
                "SPEED: MEDIUM @" +
                "TIME: 15 @"
            },
            { "Kamikaze",
                "NAME: KAMIKAZE @"+
                "FACTION: MECHANICAL @"+
                "CLASS: MEDIUM @"+
                "HP: 7 @"+
                "FIRERATE: 1-1.5 @"+
                "SPEED: FAST @" +
                "TIME: 15 @"
            },
            { "Lazermancer",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Meteorologist",
                "RAINS METEORS FROM THE SKY AND AFTER 10 SECONDS MAKES METEORS MUCH FASTER"
            },
            { "Nowcaster",
                "lol"
            },
            { "Overseer",
                "SPAWNS WITH 4 RANDOM CUBES THAT WILL FOLLOW IT AND CREATE BULLETS"
            },
            { "Pulsar",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Retaliator",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Scanner",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Sizzler",
                "HAS VERY LOW HEALTH AND FIRES A BULLET AT THE PLAYER WHEN IT DIES"
            },
            { "Sniper",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Spiker",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Watchdog",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "CXZCZX",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            //Relics
            { "Warp",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Target",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Health",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Curse",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
            { "Shield",
                "CREATES AND ORB THAT SHOOTS LAZERS IN RANDOM DIRECTIONS"
            },
        };


        ////////////////  STORE STUFF  //////////////////////////

        public int CurrentPlayerShop = 0;

        public string[] CoreNames = new string[]
{
            "DART",
            "PARA",
            "OMEGA",
            "ASSASSIN",
            "DESTROYER",
};
        public string[] WingNames = new string[]
        {
            "COLLIDERS",
            "SHIMMERS",
            "SPARKLERS",
            "FLASHERS",
            "FRAGMENTISERS",
            "PARALYZERS",
        };
        public string[] BulletNames = new string[]
        {
            "BASIC SHOT",
            "SHRAPNEL SHOT",
            "PIERCING SHOT",
            "SUMMONING SHOT",
            "GATLING SHOT",
            "FLAMETHROWER",
            "RAILCANNON",
            "MICRO DRONE",
            "CRYO DETONATOR",
            "SHATTERING SHOT",
            "EXPLOSIVE SHOT",
            "LOCK ON SHOT",
            "EXPLOSIVE MINES",
            "STAR CANNON"
        };
        public string[] RelicNames = new string[]
        {
            "NO RELIC",
            "CRIMSON",
            "MAGIC",
            "CORRUPTION",
            "LIVING",
            "CHRONOLOGY",
            "SOUL",
            "ENERGY",
            "FUTURE",
            "ESSENCE",
            "CHAOS",
            "INFUSION",
            "STRIKE",
            "TECHNOLOGY"
        };

        public string[] CoreDescriptions = new string[]
        {
            "HAS NO POSITIVE OR NEGITIVE STAT CHANGES",
            //PARA
            "TITLE: PARA @@" +
            "POSITIVES: @" +
            "CHARGE SHOTS START AT 1 SECOND @" +
            "+50% CHARGE RATE @" +
            "NEGATIVES: @" +
            "-50% SPEED @" +
            "+50% DAMAGE TAKEN @" +
            "-50% FIRERATE @" +
            "8 MAX HEALTH @" +
            "GAINS NEGITIVES WHEN NOT CHARGING. NOT INCLUDING HEALTH ",
            //OMEGA
            "TITLE: OMEGA @@" +
            "POSITIVES: @" +
            "CONSTRUCTS FRIENDLY DRONES " +
            "+40% ALLY SPEED, FIRERATE, DAMAGE AND ARMOR @" +
            "NEGATIVES: @" +
            "ONLY GAINS POSITIVES WHEN AROUND ALLIES @" +
            "GAINS NEGITIVES WHEN FAR FROM ALLIES @" +
            "OMEGA DRONES DONT COUNT  " +
            "-70% ACCELERATION @" +
            "-50% FIRERATE @" +
            "BULLETS HIT TWICE AS HARD @",
            //ASSASSIN
            "TITLE: ASSASSIN @@" +
            "POSITIVES: @" +
            "+15% FIRERATE FOR EVERY DAMAGE TAKEN @" +
            "250% DAMAGE WHEN IN DANGER @" +
            "+50% ACCELERATION @" +
            "+50% TOP SPEED @" +
            "NEGATIVES: @" +
            "5 MAX HEALTH @" ,
            //DESTROYERS
            "TITLE: DESTROYERS @" +
            "POSITIVES: @" +
            "SHOOTS 2 STREAMS OF BULLETS AT 65% DAMAGE @" +
            "+100% ACCURACY @" +
            "+50% PROJECTILE LIFESPAN @" +
            "15 MAX HEALTH @" +
            "NEGATIVES: @" +
            "-60% ACCELERATION @" +
            "-60% TOP SPEED @" +
            "-60% DEACCELERATION @" +
            "6x6 HITBOX @" +
            "75% BULLET PROC",
        };
        public string[] WingDescriptions = new string[]
        {
            //Colliders
            "TITLE: COLLIDERS @" +
            "COOLDOWN: 5 SECONDS @" +
            "ABILITY: SNAP @" +
            "DESC: REMOVES ALL BULLETS IN A MASSIVE AREA AROUND YOU. PRESS (Y) TO USE",
            //Shimmers
            "TITLE: SHIMMERS @" +
            "CHARGE TIME: 10 SECONDS @" +
            "ABILITY TIME: 2 SECONDS" +
            "ABILITY: CLOAK @" +
            "DESC: MAKES THE PLAYER INVINCIBLE FOR AS LONG AS (Y) IS HELD",
            //Sparklers
            "TITLE: SPARKLERS @" +
            "CHARGE TIME: 10 SECONDS @" +
            "ABILITY TIME: 3 SECONDS" +
            "ABILITY: SHATTER @" +
            "DESC: ALL ENEMIES KILLED WITH THIS ABILITY ACTIVE EXPLODE INTO A RING OF 16 BULLETS. PRESS (Y) TO TOGGLE ON AND OFF ",
            //Flashers
            "TITLE: FLASHERS @" +
            "COOLDOWN: 4 SECONDS @" +
            "ABILITY: REMOTE DETONATION @" +
            "DESC: CAUSES EVERY ENEMY WITH A STATUS EFFECT TO EXPLODE WITH THE POWER OF SAID STATUS EFFECT. PRESS (Y) TO USE",
            //Fragmentisers
            "TITLE: FRAGMENTISERS @" +
            "CHARGE TIME: 10 SECONDS @" +
            "ABILITY TIME: 2 SECONDS @" +
            "ABILITY: FRAGMENTIZE @" +
            "DESC: CONVERTS ENEMY BULLETS IN A MEDIUM RADIUS INTO 2 FRIENDLY BULLETS. PRESS (Y) TO ACTIVATE 3 SECONDS OF ABILITY ",
            //Paralyzers
            "TITLE: PARALYZERS @" +
            "CHARGE TIME:  SECONDS @" +
            "ABILITY TIME: 3 SECONDS @" +
            "ABILITY: TIME STOP @" +
            "DESC: ALL ENEMIES AND ENEMY BULLETS STOP IN PLACE. PRESS (Y) TO TOGGLE ON AND OFF ",

        };
        public string[] BulletDescriptions = new string[]
        {
            //BASIC SHOT
            "TITLE: BASIC SHOT @" +
            "FIRERATE: 5 @" +
            "DAMAGE: 1.75 @" +
            "PROC: 1 @" +
            "DESC: A VERY AVERAGE SHOT",
            //SHRAPNEL SHOT
            "TITLE: SHRAPNEL SHOT " +
            "FIRERATE: 3.33 @" +
            "DAMAGE: 1.25 @" +
            "PROC: 0.75 @" +
            "SHRAPNEL INFUSION AMOUNT: 12 @" +
            "SHRAPNEL DAMAGE: 0.75 @" +
            "SHRAPNEL PROC: 0.25 @" +
            "DESC: WHEN AN ENEMY IS HIT WITH THIS BULLET IT BECOMES INFUSED WITH SHRAPNEL, THAT IT WILL RELEASE ON DEATH",
            //PIERCING SHOT
            "TITLE: PIERCING SHOT " +
            "FIRERATE: 4 @" +
            "DAMAGE: 1.5 @" +
            "PROC: 0.80 @" +
            "DESC: PIERCES THROUGH UP TO FIVE ENEMIES",
            //SUMMONING SHOT
            "TITLE: SUMMONING SHOT " +
            "FIRERATE: 1.66 @" +
            "DAMAGE: 2 @" +
            "PROC: 0.8 @" +
            "ORB FIRERATE: 1 @" +
            "SMALL BULLET DAMAGE: 0.5 @" +
            "SMALL BULLET PROC: 0.25 @" +
            "DESC: A SLOW MOVING ORB THAT CREATES LOTS OF OTHER BULLETS IN ITS PLACE",
            //GATLING SHOT
            "TITLE: GATLING SHOT " +
            "FIRERATE: 20 @" +
            "DAMAGE: 0.5 @" +
            "PROC: 0.25 @" +
            "DESC: A FAST MOVING SMALL PROJECTILE THAT CAN PUSH BACK AND DESTROY ENEMY BULLETS",
            //FLAMETHROWER
            "TITLE: FLAMETHROWER " +
            "FIRERATE: 60 @" +
            "DAMAGE: 0.15 @" +
            "PROC: 0.15 @" +
            "DESC: A SHORT RANGED FLAME THROWER THAT INFLICS A BURNING DEBUFF TO ALL ENEMIES HIT",
            //RAILCANNON
            "TITLE: RAILCANNON @" +
            "CHARGE TIME: 0.25/0.75/1.5/2.25 SECONDS " +
            "DAMAGE: 3/5/7/10 @" +
            "PROC: 0.5/0.75/1/2 @" +
            "DESC: A CHARGE BASED LAZER THAT GAINS DAMAGE THE LONGER YOU CHARGE",
            //MICRO DRONE
            "TITLE: MICRO DRONE @" +
            "FIRERATE: 1 @" +
            "DAMAGE: 0 @" +
            "DRONE FIRERATE: 0.5-1.5 @" +
            "DRONE DAMAGE: 0.5 @" +
            "PROC: 0.25 @" +
            "DRONE PEIRCE: 2 @" +
            "DRONE HEALTH: 3 @" +
            "DESC: SHOT OUT A FRIENDLY ALLY THAT PERIODICALLY SHOTS LOW DAMAGE BULLETS",
            //CRYO DETONATOR
            "TITLE: CRYO DETONATOR @" +
            "FIRERATE: 2 @" +
            "DAMAGE: 2 @" +
            "PROC: 1 @" +
            "DESC: SHOOT AN ICE SPEAR AND IF IT PIERCES THROUGH 2 ENEMIES IT EXPLODES, FREEZING AND SLOWING EVERYTHING IN A RADIUS",
            //SHATTERING SHOT
            "TITLE: SHATTERING SHOT @" +
            "FIRERATE: 5 @" +
            "DAMAGE: 2 @" +
            "PROC: 0.8 @" +
            "DESC: THE LONGER YOU CHARGE THE MORE BULLETS YOU SHOOT OUT. DO BE WARNED IT IS HIGHLY INACCURATE",
            //EXPLOSIVE SHOT
            "TITLE: EXPLOSIVE SHOT @" +
            "FIRERATE: 4 @" +
            "DAMAGE: 1 @" +
            "PROC: 0.5 @" +
            "AOE DAMANGE: 0.75 @" +
            "DESC: A FAST SHOOTING BULLET THAT CAN DAMAGE ENEMIES IN A RADIUS",
            //LOCK ON SHOT
            "TITLE: LOCK ON SHOT @" +
            "FIRERATE: 4 @" +
            "DAMAGE: 2.5 @" +
            "PROC: 1 @" +
            "SNIPE DAMAGE: 1.25 @" +
            "SNIPE PROC: 0.75 @" +
            "DESC: A FAST BULLET THE WILL LOCK ON TO AN ENEMY AND SNIPE IT DEALING LESS DAMAGE",
            //EXPLOSIVE MINES
            "EXPLOSIVE MINES",
            //STAR CANNON
            "STAR CANNON"
        };
        public string[] RelicDescriptions = new string[]
{
            //NO RELIC
            "RELIC SLOT IS EMPTY ;-;",
            //CRIMSON
            "TITLE: CRIMSON @" +
            "1 RELIC: ALL BULLETS CAUSE ENEMIES TO BLEED HEALTH AS BULLETS. ALLIES CAN ALSO COLLECT THE BLOOD FOR EXTRA HEALTH @" +
            "2 RELICS: THE BLEED EFFECT CAN ALSO CAUSE BLEED AND WHEN ENEMIES DIE WITH ALOT OF BLEED THEY LEAVE BEHIND A BLOOD CLOT THAT SHOOTS BLOOD IN ALL DIRECTIONS",
            //MAGIC
            "TITLE: MAGIC @" +
            "1 RELIC: GAIN AN ORBITING BULLET (MAX OF 3) THAT CAN DESTROY BULLETS AND DAMAGE ENEMIES EVERY 2.5 SECONDS @" +
            "2 RELICS: DOUBLE THE SPEED OF CREATING BULLETS AND DOUBLE THE CAP OF BULLETS @" +
            "3 RELICS: QUADRUPLE THE SPEED OF CREATING BULLETS AND QUADRUPLE THE CAP OF BULLETS @",
            //CORRUPTION
            "TITLE: CORRUPTION @" +
            "1 RELIC: PLAYER BULLETS CONVERT ENEMIES ON DEATH @ 40% FOR A SMALL ENEMY @ 20% FOR A MEDIUM ENEMY @" +
            "2 RELICS: 60% FOR A SMALL ENEMY @ 40% FOR A MEDIUM ENEMY @ 20% FOR A LARGE ENEMY @",
            //LIVING
            "TITLE: LIVING @" +
            "1 RELIC: ALL BULLETS WILL LEAVE BEHIND A TRAIL OF THORNS THAT LAST 5 SECONDS @" +
            "2 RELICS: ALL BULLETS WILL SOMETIMES CREATE A FLOWER THAT SUCKS IN AND DESTROYS AN ENEMY ",
            //CHRONOLOGY
            "TITLE: CHRONOLOGY @" +
            "1 RELIC: PLAYER BULLETS NOW HAVE AN AURA THAT SLOWS DOWN TIME%",
            //SOUL
            "TITLE: SOUL @" +
            "1 RELIC: PLAYER BULLETS HAVE A MIRRORED GHOST ON THE OTHER SIDE AND IF EITHER ARE HIT THEY WILL EXPIRE @",
            //ENERGY
            "TITLE: ENERGY @" +
            "1 RELIC: ENEMIES WILL RANDOMLY BE CONNECTED AND IF EITHER ARE HIT THEY WILL BOTH TAKE DAMAGE @" +
            "2 RELICS: ALL BULLETS CAN NOW CONNECT ENEMIES",
            //FUTURE
            "TITLE: FUTURE @" +
            "1 RELIC: YOU CAN SEE AN ALTERNATE VERSION OF YOURSELF AND IF YOU ARE HIT YOU SWAP PLACES AND TAKE NO DAMAGE @" +
            "2 RELICS: YOU CAN SEE A PAST VERSION OF YOUR SELF AND ALL ALLIES GAIN THE FIRST EFFECT",
            //ESSENCE
            "TITLE: ESSENCE @" +
            "1 RELIC: WHEN YOU KILL AN ENEMY THAT ENEMY WILL DROP ITS ABILLITY AS AN ORB YOU CAN COLLECT AND USE FOR A SHORT PERIOD OF TIME @" +
            "2 RELICS: ALLIES CAN NOW PICK UP THESE ORBS AND YOUR TIME WILL DRAIN 25% SLOWER",
            //CHAOS
            "TITLE: CHAOS @" +
            "1 RELIC: CHANGES THE RELIC IN WHAT EVER SLOT CHAOS IS IN EVERY 5-15 SECONDS @" +
            "3 RELICS: ON TOP OF THE THREE RANDOM RELICS YOU GET A FOUTH RANDOM RELIC AND ALL YOUR SHOP ITEMS GET RANDOMIZED",
            //INFUSION
            "TITLE: INFUSION @" +
            "1 RELIC: EVERYTIME AN ENEMY IS HIT WITH A BULLET IT HAS A 50% TO INFUSE THE ENEMY WITH THE BULLET. ENEMIES WILL DROP ALL INFUSED BULLETS WHEN THEY DIE @" +
            "2 RELICS: BULLETS WITHOUT A PROC CHANCE NOW HAVE A 25% CHANCE TO INFUSE AND THE REST OF THE BULLETS HAVE A 100% TO INFUSE",
            //STRIKE
            "TITLE: STRIKE @" +
            "1 RELIC: BULLETS HAVE A 50% CHANCE TO THROW ENEMIES AT HIGH SPEED. WHEN AN ENEMY HITS SOMTHING WHILE BEING IN THIS HIGH SPEED STATE IT TAKES DAMAGE BASED OFF THE SPEED IT WAS @",
            //TECHNOLOGY
            "TITLE: TECHNOLOGY @" +
            "1 RELIC: WHEN AN ENEMY IS HIT WITH A BULLET IT HAS A 75%*PROC TO HACK THE ENEMY CAUSING ALL OF ITS PROJECTILES TO BECOME FRIENDLY @" +
            "2 RELICS: EVERYTIME YOU KILL AN ENEMY ALL ENEMIES OF THE SAME CLASS (SMALL, MEDIUM, ETC) GET HACKED",
};

        public Color[] RelicsColors1 = new Color[]
        {
        new Color(128,128,128),//none
        new Color(181,0,0),//Crimson
        new Color(255,0,255),//Magic
        new Color(128,0,255),//Corruption
        new Color(0,255,0),//Living
        new Color(0,255,255),//Chronology
        new Color(255,255,255),//Soul
        new Color(255,255,0),//Energy
        new Color(0,255,255),//Future
        new Color(255,0,128),//Essence
        new Color(255,255,255),//Chaos
        new Color(0,0,255),//Infusion
        new Color(255,64,64),//Strike
        new Color(0,255,0),//Technology
        };

        public Color[] RelicsColors2 = new Color[]
        {
        new Color(64,64,64),//none
        new Color(102,0,0),//Crimson
        new Color(128,0,128),//Magic
        new Color(64,0,128),//Corruption
        new Color(0,128,0),//Living
        new Color(0,128,128),//Chronology
        new Color(128,128,128),//Soul
        new Color(128,128,0),//Energy
        new Color(255,0,255),//Future
        new Color(128,0,64),//Essence
        new Color(128,128,128),//Chaos
        new Color(0,0,128),//Infusion
        new Color(128,32,32),//Strike
        new Color(0,0,255),//Technology
        };

        public List<Relic> RelicUI = new List<Relic>(); // This is for UI elements that relics can draw

        //Relic List
        public List<Relic> ActiveRelics = new List<Relic>();

        ////////////////////  LEVEL STUFF  //////////////////////////

        public double GameTime = 0; //this is how far into a level you are
        public double Speed = 1f; //this is how fast the levels progresses
        public int CurrentLevel;
        public List<Level> Levels = new List<Level>();

        //Level stats
        public int EnemiesKilled = 0;
        public double TimeTaken = 0;
        public double DamageTaken = 0;
        public double SideObjective = 0;

        public LevelSpawner LevelSpawner;

        /////////////////// ENDLESS STUFF ///////////////////////////
        
        public EndlessSpawner EndlessSpawner;


        ///////////// DEBUG STUFF /////////////
        public bool DebugOn = false;
        public bool Cheated = false;


        public SceneManager(Dictionary<string, Texture2D> Tex, SpriteFont menufont)
        {
            Textures = Tex;
            Refreshlevels();
            //Menu Stuff
            Pico8 = menufont;
            menupos = 0;
            LastState = GamePad.GetState(PlayerIndex.One);
            //Map Stuff
            //Game Stuffas
            Players.Add(new Player(new Vector2(0, 0), this, null)
            {
                Pos = new Vector2(144, 20),
            });

            //Add all the buttons to dictionary
            ResetDicList();

            Enemies.Add(new Meteorologist(new Vector2(144, 40), this) { });;
            //Enemies[^1].EnemyRelics.Add(new Warp(this));

            //Bullets.Add(new LazerBulletAlly(new Vector2(144, 40), new Vector2(0, 0),Players[0],this));

            //Level and endless stuff
            for(int i = 0; i < 1; i++)
            {
                Enemies.Add(new Guardian(new Vector2(rand.Next(0,288), rand.Next(0, 70)), this));
                //Enemies[^1].EnemyRelics.Add(new Target(this));
            
            }
            //Enemies.Add(new Guardian(new Vector2(rand.Next(0, 288), 0), this));

            //Enemies.Add(new Contaminator(new Vector2(144, 30), this));

            LevelSpawner = new LevelSpawner(this);
            EndlessSpawner = new EndlessSpawner(this);

            //Ui
            Buttons.Add(new Button(new Vector2(88, 35), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[0], "SOLO", this));
            Buttons.Add(new Button(new Vector2(86, 45), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[1], "CO-OP", this));
            Buttons.Add(new Button(new Vector2(84, 55), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[2], "CONFIG", this));
            Buttons.Add(new Button(new Vector2(88, 65), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[3], "EXIT", this));
        }



        public void UpdateMenu(GameTime GT)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                GamePad.SetVibration(i, 0, 0, 0, 0);
            }
        }
        public void DrawMenu(SpriteBatch sb)
        {
            //debug stuff
            //sb.DrawString(Pico8, Levels[0].Data[0].ToString(), new Vector2(0, 0), Color.HotPink); //menupos
            //Background art
            //sb.Draw(Textures["BackgroundArt"], new Vector2(0, (float)(-500 + (TimeSinceStart * 20))), Color.White);
            //sb.Draw(Textures["BackgroundArt"], new Vector2(0, (float)(-500 + Math.Ceiling(TimeSinceStart * 20))), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
            sb.Draw(Textures["BackgroundArt"], new Rectangle(0, (int)(-500 + Math.Ceiling(TimeSinceStart * 20)), 192, Textures["BackgroundArt"].Height), new Rectangle(0, 0, 192, Textures["BackgroundArt"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);

            //title
            sb.Draw(Textures["Title"], new Vector2(62, 0), Color.White);
            sb.DrawString(Pico8, "DEMO", new Vector2(175, 1), Color.DarkViolet, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
            sb.DrawString(Pico8, "DEMO", new Vector2(175, 0), Color.Violet, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
            //sb.DrawString(Textures["HealthBar"], new Rectangle(1, 1, Textures["HealthBar"].Width, Textures["HealthBar"].Height), new Rectangle(0, 0, Textures["HealthBar"].Width, Textures["HealthBar"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);

            //Draws the button that is selected a different colour and gives it arrows
            switch (menupos)
            {
                case 0:
                    sb.Draw(Textures["MenuArrow"], new Vector2(108, 35), Purple);
                    sb.Draw(Textures["BackwardsMenuArrow"], new Vector2(80, 35), Purple);
                    break;
                case 1:
                    sb.Draw(Textures["MenuArrow"], new Vector2(110, 45), Purple);
                    sb.Draw(Textures["BackwardsMenuArrow"], new Vector2(78, 45), Purple);
                    break;
                case 2:
                    sb.Draw(Textures["MenuArrow"], new Vector2(112, 55), Purple);
                    sb.Draw(Textures["BackwardsMenuArrow"], new Vector2(76, 55), Purple);
                    break;
                case 3:
                    sb.Draw(Textures["MenuArrow"], new Vector2(108, 65), Purple);
                    sb.Draw(Textures["BackwardsMenuArrow"], new Vector2(80, 65), Purple);
                    break;
            }
            //  sb.Draw(BoundingBox, new Vector2(0, 0), Color.White);
        }

        public void UpdateMap(GameTime GT)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                GamePad.SetVibration(i, 0, 0, 0, 0);
            }
            if (AltScene == 0)
            {
            }
            else if (AltScene == 10)
            {
                Particles.RemoveAll(I => !(I is DictionaryParticle));
                
            }
            else if (AltScene > 0)
            {
                Particles.Clear();
            }
        }

        public void DrawMap(SpriteBatch sb)
        {
            switch (AltScene) // 0 - Planets // 1 - shop // 2 - 5 Sub shop // 10 - Dictionary
            {
                case 1: ////// Main Shop scene //////
                        //   Cores   //
                    sb.DrawString(Pico8, CoreNames[Players[CurrentPlayerShop].CurrentShipParts[0]], new Vector2(26, 22), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, CoreNames[Players[CurrentPlayerShop].CurrentShipParts[0]], new Vector2(26, 21), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                    //  Wings   //
                    sb.DrawString(Pico8, WingNames[Players[CurrentPlayerShop].CurrentShipParts[1]], new Vector2(26, 60), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, WingNames[Players[CurrentPlayerShop].CurrentShipParts[1]], new Vector2(26, 59), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                    //   Bullets   //
                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[0]], new Vector2(13, 89), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[0]], new Vector2(13, 88), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[1]], new Vector2(13, 101), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[1]], new Vector2(13, 100), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[2]], new Vector2(13, 113), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, BulletNames[Players[CurrentPlayerShop].CurrentBullets[2]], new Vector2(13, 112), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

                    //  Relics  //
                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[0]], new Vector2(111, 22), RelicsColors2[Players[CurrentPlayerShop].CurrentRelics[0]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[0]], new Vector2(111, 21), RelicsColors1[Players[CurrentPlayerShop].CurrentRelics[0]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[1]], new Vector2(111, 60), RelicsColors2[Players[CurrentPlayerShop].CurrentRelics[1]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[1]], new Vector2(111, 59), RelicsColors1[Players[CurrentPlayerShop].CurrentRelics[1]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[2]], new Vector2(111, 98), RelicsColors2[Players[CurrentPlayerShop].CurrentRelics[2]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.12f);
                    sb.DrawString(Pico8, RelicNames[Players[CurrentPlayerShop].CurrentRelics[2]], new Vector2(111, 97), RelicsColors1[Players[CurrentPlayerShop].CurrentRelics[2]], 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);


                    sb.Draw(Textures["UpgradeShop"], new Rectangle(0, 0, Textures["UpgradeShop"].Width, Textures["UpgradeShop"].Height), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);

                    //DESCRIPTION TEXT
                    string Text = "";
                    switch (menupos)
                    {
                        case 0: // 173
                            Text = Helper.WrapText(Pico8, CoreDescriptions[Players[CurrentPlayerShop].CurrentShipParts[0]], 115);
                            break;
                        case 1:
                            Text = Helper.WrapText(Pico8, WingDescriptions[Players[CurrentPlayerShop].CurrentShipParts[1]], 115);
                            break;
                        case 2:
                            Text = Helper.WrapText(Pico8, BulletDescriptions[Players[CurrentPlayerShop].CurrentBullets[0]], 115);
                            break;
                        case 3:
                            Text = Helper.WrapText(Pico8, BulletDescriptions[Players[CurrentPlayerShop].CurrentBullets[1]], 115);
                            break;
                        case 4:
                            Text = Helper.WrapText(Pico8, BulletDescriptions[Players[CurrentPlayerShop].CurrentBullets[2]], 115);
                            break;
                        case 6:
                            Text = Helper.WrapText(Pico8, RelicDescriptions[Players[CurrentPlayerShop].CurrentRelics[0]], 115);
                            break;
                        case 7:
                            Text = Helper.WrapText(Pico8, RelicDescriptions[Players[CurrentPlayerShop].CurrentRelics[1]], 115);
                            break;
                        case 8:
                            Text = Helper.WrapText(Pico8, RelicDescriptions[Players[CurrentPlayerShop].CurrentRelics[2]], 115);
                            break;

                    }
                    sb.DrawString(Pico8, Text, new Vector2(173, 48), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                    //render all the players in the scene
                    Players[CurrentPlayerShop].Draw(sb);
                    Players[CurrentPlayerShop].Pos.X = 229;
                    Players[CurrentPlayerShop].Pos.Y = 24;
                    break;
                case 2: ////////// CORES //////////
                    Helper.WrapString(CoreDescriptions[menupos], new Rectangle(173, 48, 115, 114), Color.White, sb, this);
                    sb.Draw(Textures["UpgradeShop"], new Rectangle(172, 46, 116, 116), new Rectangle(172, 46, 116, 116), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);
                    break;
                case 3:  ////////// WINGS //////////
                    Helper.WrapString(WingDescriptions[menupos], new Rectangle(173, 48, 115, 114), Color.White, sb, this);
                    sb.Draw(Textures["UpgradeShop"], new Rectangle(172, 46, 116, 116), new Rectangle(172, 46, 116, 116), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);
                    break;
                case 4:  ////////// BULLETS //////////
                    Helper.WrapString(BulletDescriptions[menupos], new Rectangle(173, 48, 115, 114), Color.White, sb, this);
                    sb.Draw(Textures["UpgradeShop"], new Rectangle(172, 46, 116, 116), new Rectangle(172, 46, 116, 116), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);
                    break;
                case 5:  ////////// RELICS //////////
                    Helper.WrapString(RelicDescriptions[menupos], new Rectangle(173, 48, 115, 114), Color.White, sb, this);
                    sb.Draw(Textures["UpgradeShop"], new Rectangle(172, 46, 116, 116), new Rectangle(172, 46, 116, 116), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.13f);
                    break;
                case 10:  ////////// Dictionary //////////
                    sb.Draw(Textures["ShipDictionary"], new Rectangle(0, 0, 288, 162), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.83f);

                    if (Buttons[SelectedEnemy].Tags.Contains("Page"))
                    {
                        Helper.WrapString(EnemyDescriptions[Buttons[SelectedEnemy].Tags[^1]], new Rectangle(169, 78, 119, 85), Color.White, sb, this);
                        Helper.WrapString(EnemyStats[Buttons[SelectedEnemy].Tags[^1]], new Rectangle(169, 19, 119, 58), Color.White, sb, this);
                    }
                    break;
            }
        }

        public void UpdateGame(GameTime GT)
        {
            if (AltScene == 0) // Game scene
            {
                //////////////// Levels ////////////////
                if (CurrentLevel > 0)
                {   //Check if level is completed
                    if (GameTime < 1 && Enemies.Count < 1)
                    {
                        ResetAllNumbers();
                        return;
                    }
                    //makes the level progress
                    if ((GameTime - GT.ElapsedGameTime.TotalSeconds / Speed) > 0)
                    {
                        GameTime -= GT.ElapsedGameTime.TotalSeconds / Speed;
                    }
                    LevelSpawner.Update(GT);
                }
                else //////////// Endless ///////////////
                {
                    EndlessSpawner.Update(GT);
                }

                //Player Updating
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].HitAniFade > 0)
                    {
                        GamePad.SetVibration(i, Players[i].HitAniFade * 4, Players[i].HitAniFade * 4, Players[i].HitAniFade * 4, Players[i].HitAniFade * 4);
                    }
                    else
                    {
                        GamePad.SetVibration(i, 0, 0, 0, 0);
                    }
                    Players[i].Update(GT);
                }

                //Update all Allies
                foreach (Ally Al in Allies)
                {
                    Al.Update(GT);
                    if (Al.Health <= 0)
                    {
                        //Ally On Death Relic
                        foreach (Relic rel in ActiveRelics)
                        {
                            if (rel.ModifiesAllyOnDeath)
                            {
                                rel.ModAllyOnDeath(Al);
                            }
                        }
                    }
                }
                Allies.RemoveAll(I => I.Health <= 0);

                //Update all Enemies
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].Update(GT);
                    if (Enemies[i].Health <= 0)
                    {
                        //Enemy On Death Relic
                        foreach (Relic rel in ActiveRelics)
                        {
                            if (rel.ModifiesEnemyOnDeath)
                            {
                                rel.ModEneOnDeath(Enemies[i]);
                            }
                        }
                        //Hard coded sparklers
                        double Angle = -Math.PI * 2;
                        foreach (Player play in Players)
                        {
                            if (play.AllWings[play.CurrentShipParts[1]].AbilityActivated == true && play.CurrentShipParts[1] == 2)
                            {
                                for (int x = 0; x < 12; x++)
                                {
                                    Angle += Math.PI / 6;
                                    Bullets.Add(new BasicShotWeak(0, new Vector2(Enemies[i].Pos.X + Enemies[i].WidthHeight.X / 2, Enemies[i].Pos.Y + Enemies[i].WidthHeight.Y / 2), new Vector2((float)Math.Cos(Angle) * 1.5f, (float)Math.Sin(Angle) * 1.5f), this, play)); //Bullets
                                }
                            }
                        }
                        //Empty container
                        for (int x = 0; x < Enemies[i].Contains.StoredEnemies.Count; x++) // Enemies
                        {
                            Enemies[i].Contains.StoredEnemies[x].Pos = Enemies[i].Pos + Enemies[i].Contains.StoredEnemies[x].Pos;
                            Enemies.Add(Enemies[i].Contains.StoredEnemies[x]);
                        }

                        for (int x = 0; x < Enemies[i].Contains.StoredEnemyBullets.Count; x++) // Enemy bullets
                        {
                            Enemies[i].Contains.StoredEnemyBullets[x].Pos = Enemies[i].Pos + Enemies[i].Contains.StoredEnemyBullets[x].Pos;
                            EnemyBullets.Add(Enemies[i].Contains.StoredEnemyBullets[x]);
                        }

                        for (int x = 0; x < Enemies[i].Contains.StoredBullets.Count; x++) // Bullets
                        {
                            Enemies[i].Contains.StoredBullets[x].Pos = Enemies[i].Pos + Enemies[i].Contains.StoredBullets[x].Pos;
                            Bullets.Add(Enemies[i].Contains.StoredBullets[x]);
                        }
                    }
                }
                Enemies.RemoveAll(I => I.Health <= 0);

                //Update all bullets
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Update(GT);
                    if (Bullets[i].Health <= 0)
                    {
                        //Bullet On Death Relic
                        foreach (Relic rel in ActiveRelics)
                        {
                            if (rel.ModifiesBulletOnDeath)
                            {
                                rel.ModBulOnDeath(Bullets[i]);
                            }
                        }
                    }
                }
                Bullets.RemoveAll(I => I.Health <= 0);

                Bullets.RemoveAll(I => I.Pos.Y < -105);
                Bullets.RemoveAll(I => I.Pos.X < -50);
                Bullets.RemoveAll(I => I.Pos.X > 338);
                Bullets.RemoveAll(I => !(I is EnemyMeteorAlly) && I.Pos.Y > 172);

                //Update all Enemy bullets
                for (int i = 0; i < EnemyBullets.Count; i++)
                {
                    EnemyBullets[i].Update(GT);
                }
                EnemyBullets.RemoveAll(I => I.Health <= 0);
                EnemyBullets.RemoveAll(I => !((I is EnemyMeteor) || I is Lazer) && (I.Pos.Y < -10));
                EnemyBullets.RemoveAll(I => !(I is Lazer) && I.Pos.X < -7);
                EnemyBullets.RemoveAll(I => I.Pos.X > 288 + 7);
                EnemyBullets.RemoveAll(I => I.Pos.Y > 167);
            }
            else
            {

            }
        }

        public void DrawGame(SpriteBatch sb)
        {
            if (AltScene == 0) // Game Scene
            {
                if (CurrentLevel > 0)
                {
                    sb.DrawString(Pico8, Math.Floor((Levels[CurrentLevel].Layers[0].height - GameTime) / (Levels[CurrentLevel].Layers[0].height) * 100).ToString() + "%", new Vector2(240, 1), Color.DarkBlue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.03f);
                    sb.DrawString(Pico8, Math.Floor((Levels[CurrentLevel].Layers[0].height - GameTime) / (Levels[CurrentLevel].Layers[0].height) * 100).ToString() + "%", new Vector2(240, 0), Color.Blue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                }
                else
                {
                    sb.DrawString(Pico8, "TIME: " + Math.Floor(GameTime).ToString(), new Vector2(233, 1), Color.DarkBlue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                    sb.DrawString(Pico8, "TIME: " + Math.Floor(GameTime).ToString(), new Vector2(233, 0), Color.Blue, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.00f);
                }
                foreach (Player play in Players)
                {
                    if (Players.Count > 0 && Players.Count < 3)
                    {
                        if (play == Players[0])
                        {
                            sb.Draw(Textures["HealthBar"], new Rectangle(1, 1, Textures["HealthBar"].Width, Textures["HealthBar"].Height), new Rectangle(0, 0, Textures["HealthBar"].Width, Textures["HealthBar"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            sb.Draw(Textures["WhitePixel"], new Rectangle(12, 3, (int)((play.Health / play.AllCores[play.CurrentShipParts[0]].Stats.MaxHealth) * 20), 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(Textures["WhitePixel"], new Rectangle(12, 5, (int)((play.Health / play.AllCores[play.CurrentShipParts[0]].Stats.MaxHealth) * 20), 1), new Rectangle(0, 0, 1, 1), Color.DarkRed, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(Textures["SpecialBarBorder"], new Rectangle(2, 12, Textures["SpecialBarBorder"].Width, Textures["SpecialBarBorder"].Height), new Rectangle(0, 0, Textures["SpecialBarBorder"].Width, Textures["SpecialBarBorder"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            if (play.AbilityDelay < 0)
                            {
                                sb.Draw(Textures["SpecialBarInside"], new Rectangle(3, 13, Textures["SpecialBarInside"].Width, Textures["SpecialBarInside"].Height), new Rectangle(0, 0, Textures["SpecialBarInside"].Width, Textures["SpecialBarInside"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.02f);
                            }
                            else
                            {
                                sb.Draw(Textures["SpecialBarInside"], new Rectangle(3, 13, Textures["SpecialBarInside"].Width, (int)(-(play.AbilityDelay / play.AllWings[play.CurrentShipParts[1]].MaxDelay) * 10 + 10)), new Rectangle(0, 0, Textures["SpecialBarInside"].Width, (int)(-(play.AbilityDelay / play.AllWings[play.CurrentShipParts[1]].MaxDelay) * 10 + 10)), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.02f);
                            }
                            sb.Draw(Textures["BulletUI"], new Rectangle(12, 10, 16, 11), new Rectangle(16 * Players[0].CurrentSelectedBullet, 0, 16, 11), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(Textures["ShopBullets1"], new Rectangle(12 + Players[0].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[0].CurrentBullets[Players[0].CurrentSelectedBullet] * 9, 0, 10, 11), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                            sb.Draw(Textures["ShopBullets2"], new Rectangle(12 + Players[0].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[0].CurrentBullets[Players[0].CurrentSelectedBullet] * 9, 0, 10, 11), RelicsColors1[Players[0].CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.29f);
                            sb.Draw(Textures["ShopBullets3"], new Rectangle(12 + Players[0].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[0].CurrentBullets[Players[0].CurrentSelectedBullet] * 9, 0, 10, 11), RelicsColors2[Players[0].CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.28f);
                        }
                    }
                    if (Players.Count == 2)
                    {
                        if (play == Players[1])
                        {
                            sb.Draw(Textures["HealthBar"], new Rectangle(255, 1, Textures["HealthBar"].Width, Textures["HealthBar"].Height), new Rectangle(0, 0, Textures["HealthBar"].Width, Textures["HealthBar"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.02f);
                            sb.Draw(Textures["WhitePixel"], new Rectangle(256, 3, (int)((play.Health / play.AllCores[play.CurrentShipParts[0]].Stats.MaxHealth) * 20), 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            sb.Draw(Textures["WhitePixel"], new Rectangle(256, 5, (int)((play.Health / play.AllCores[play.CurrentShipParts[0]].Stats.MaxHealth) * 20), 1), new Rectangle(0, 0, 1, 1), Color.DarkRed, 0f, new Vector2(0, 0), SpriteEffects.None, 0.01f);
                            sb.Draw(Textures["SpecialBarBorder"], new Rectangle(279, 12, Textures["SpecialBarBorder"].Width, Textures["SpecialBarBorder"].Height), new Rectangle(0, 0, Textures["SpecialBarBorder"].Width, Textures["SpecialBarBorder"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.01f);
                            if (play.AbilityDelay < 0)
                            {
                                sb.Draw(Textures["SpecialBarInside"], new Rectangle(280, 13, Textures["SpecialBarInside"].Width, Textures["SpecialBarInside"].Height), new Rectangle(0, 0, Textures["SpecialBarInside"].Width, Textures["SpecialBarInside"].Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.02f);
                            }
                            else
                            {
                                sb.Draw(Textures["SpecialBarInside"], new Rectangle(280, 13, Textures["SpecialBarInside"].Width, (int)(-(play.AbilityDelay / play.AllWings[play.CurrentShipParts[1]].MaxDelay) * 10 + 10)), new Rectangle(0, 0, Textures["SpecialBarInside"].Width, (int)(-(play.AbilityDelay / play.AllWings[play.CurrentShipParts[1]].MaxDelay) * 10 + 10)), Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.02f);
                            }
                            sb.Draw(Textures["BulletUI"], new Rectangle(260, 10, 16, 11), new Rectangle(16 * Players[1].CurrentSelectedBullet, 0, 16, 11), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                            sb.Draw(Textures["ShopBullets1"], new Rectangle(260 + Players[1].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[1].CurrentBullets[Players[1].CurrentSelectedBullet] * 9, 0, 10, 11), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
                            sb.Draw(Textures["ShopBullets2"], new Rectangle(260 + Players[1].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[1].CurrentBullets[Players[1].CurrentSelectedBullet] * 9, 0, 10, 11), RelicsColors1[Players[1].CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.29f);
                            sb.Draw(Textures["ShopBullets3"], new Rectangle(260 + Players[1].CurrentSelectedBullet * 3, 10, 10, Textures["ShopBullets1"].Height), new Rectangle(Players[1].CurrentBullets[Players[1].CurrentSelectedBullet] * 9, 0, 10, 11), RelicsColors2[Players[1].CurrentRelics[2]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.28f);
                        }
                    }
                    play.Draw(sb);
                }

                //Render all Allies
                foreach (Ally Al in Allies)
                {
                    Al.Draw(sb);
                }

                //Render all Enemy bullets
                foreach (EnemyBullet Ebull in EnemyBullets)
                {
                    Ebull.Draw(sb);
                }

                for (int i = 0; i < EnemyBullets.Count; i++)
                {
                    EnemyBullets[i].Draw(sb);
                }

                //Render all Enemies
                foreach (Enemy Ene in Enemies)
                {
                    Ene.Draw(sb);
                }

                //Render all bullets
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullets[i].Draw(sb);
                }

                //Relic Mod Scenemanger Draw
                foreach (Relic rel in ActiveRelics)
                {
                    if (rel.ModifiesDraw)
                    {
                        rel.ModDraw(sb);
                    }
                }
            }
            else // Results screen
            {
                sb.Draw(Textures["ResultScreen"], new Rectangle(0, 0, 192, 108), new Rectangle(0, 0, 192, 108), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.5f);
                if (CurrentLevel == 0)
                {
                    if (Math.Floor(GameTime) > 0)
                    {
                        if (Cheated)
                        {
                            sb.Draw(Textures["WhitePixel"], new Vector2(0, 0), Color.Purple);
                            sb.Draw(Textures["WhitePixel"], new Vector2(50, 50), Color.DarkBlue);
                            sb.Draw(Textures["WhitePixel"], new Vector2(76, 13), Color.DarkRed);
                        }
                        sb.DrawString(Pico8, "YOU LASTED: " + Math.Floor(GameTime).ToString() + " SECONDS", new Vector2(31, 26), Color.DeepPink, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                        sb.DrawString(Pico8, "YOU LASTED: " + Math.Floor(GameTime).ToString() + " SECONDS", new Vector2(31, 25), Color.Pink, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                    }
                }
                else
                {
                    sb.DrawString(Pico8, "ENEMIES KILLED: " + EnemiesKilled.ToString(), new Vector2(62, 28), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                    sb.DrawString(Pico8, "ENEMIES KILLED: " + EnemiesKilled.ToString(), new Vector2(62, 27), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                    sb.DrawString(Pico8, "TIME TAKEN: " + TimeTaken.ToString() + " SECONDS", new Vector2(62, 38), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                    sb.DrawString(Pico8, "TIME TAKEN: " + TimeTaken.ToString() + " SECONDS", new Vector2(62, 37), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                    sb.DrawString(Pico8, "DAMAGE TAKEN: " + DamageTaken.ToString() + " HP", new Vector2(62, 48), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                    sb.DrawString(Pico8, "DAMAGE TAKEN: " + DamageTaken.ToString() + " HP", new Vector2(62, 47), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                    sb.DrawString(Pico8, "SIDE OBJECTIVE: " + SideObjective.ToString(), new Vector2(62, 58), Color.Gray, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.02f);
                    sb.DrawString(Pico8, "SIDE OBJECTIVE: " + SideObjective.ToString(), new Vector2(62, 57), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                }
            }
        }

        public void UpdateAll(GameTime GT) // this is used for stuff that will be in all scenes
        {
            TotalTime += GT.ElapsedGameTime.TotalSeconds;
            //Update all particles
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update(GT);
            }
            Particles.RemoveAll(I => I.Pos.Y > 225);
            //deletewhenoutside

            //Update all buttons
            //foreach (Button butt in Buttons)
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Update(GT);
            }
            foreach (Button butt in Buttons)
            {
                butt.BlockInput = false;
            }
            if (Buttons.Count > 0) Buttons.RemoveAll(Butt => Butt.NeedsDeletionQ == true); // removes unwanted buttons

            //Menu particle creator
            //init creation
            if (Particles.Count == 0)
            {
                for (int i = 0; i < 300; i++)
                {
                    if (rand.Next(1, 25) == 5)
                    {
                        Particles.Add(new StarParticle(new Vector2(rand.Next(0, 384), rand.Next(-64, 168)), this));// star particles
                    }
                    else
                    {
                        Particles.Add(new UpwardsParticle(new Vector2(rand.Next(0, 384), rand.Next(-64, 168)), this));//upwards particles
                    }
                }
            }
            //constant creation
            COUNTER += 1;
            if (rand.Next(1, 1200) == 1)
            {
                Particles.Add(new StarParticle(new Vector2(rand.Next(0, 384), rand.Next(-1, -1)), this));// star particles
            }
            else
            {
                Particles.Add(new UpwardsParticle(new Vector2(rand.Next(0, 384), rand.Next(-64, -1)), this));//upwards particles
            }

            //Debug stuff
            if (state.Buttons.Back == ButtonState.Pressed & !(LastState.Buttons.Back == ButtonState.Pressed))
            {
                DebugOn = !DebugOn;
                if (DebugOn)
                {
                    Buttons.Add(new Button(new Vector2(0, 73), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            s.Enemies.Add(new BasicEnemy(new Vector2(s.rand.Next(0, 288), 0), s));
                        }
                    }, "+25ene", this));
                    Buttons.Add(new Button(new Vector2(0, 80), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        s.Enemies.Add(new BasicEnemy(new Vector2(s.rand.Next(0, 288), 0), s));
                        for (int i = 0; i < 50; i++)
                        {
                            s.EnemyBullets.Add(new EnemyBasicShot(new Vector2(144, 40), new Vector2(0, 0), Enemies[^1], s));
                        }
                    }, "+50enebul", this));
                    Buttons.Add(new Button(new Vector2(0, 87), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        foreach (Player play in s.Players)
                        {
                            play.Health += 10;
                        }
                    }, "+10HP", this));
                    Buttons.Add(new Button(new Vector2(0, 94), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        s.Enemies.Clear();
                        Cheated = true;
                    }, "ClearEne", this));
                    Buttons.Add(new Button(new Vector2(0, 101), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        s.EnemyBullets.Clear();
                        Cheated = true;
                    }, "ClearBul", this));
                    Buttons.Add(new Button(new Vector2(0, 108), new Rectangle(0, 0, 0, 0), (s, b) =>
                    {
                        Cheated = true;
                        if (s.ActiveRelics.Count < 6)
                        {
                            s.ActiveRelics.Clear();
                        }
                        foreach(Player play in s.Players)
                        {
                            s.ActiveRelics.Add(new Crimson(3, s, play));
                            s.ActiveRelics.Add(new Magic(3, s, play));
                            s.ActiveRelics.Add(new Corruption(3, s, play));
                            s.ActiveRelics.Add(new Living(3, s, play));
                            s.ActiveRelics.Add(new Chronology(3, s, play));
                            s.ActiveRelics.Add(new Soul(3, s, play));
                            s.ActiveRelics.Add(new Energy(3, s, play));
                            s.ActiveRelics.Add(new Future(3, s, play));
                            s.ActiveRelics.Add(new Essence(3, s, play));
                        }

                    }, "+AllRel", this));
                }
                else
                {
                    Buttons.Clear();
                }
            }
        }

        public void DrawAll(SpriteBatch sb)
        {
            //Render all particles
            foreach (Particle part in Particles)
            {
                part.Draw(sb);
            }

            //Draws all buttons
            foreach (Button butt in Buttons)
            {
                butt.Draw(sb);
            }

            //Debug Menu
            if (DebugOn)
            {
                sb.DrawString(game.debug, "| Debug Menu |", new Vector2(0, 28), Color.Yellow, 0f, new Vector2(0, 0), 0.1f, SpriteEffects.None, 0f);
                sb.Draw(Textures["WhitePixel"], new Rectangle(0, 36, 40, 1), new Rectangle(0, 0, 1, 1), Color.Yellow, 0, new Vector2(0, 0), SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Bullets: " + Bullets.Count, new Vector2(0, 36), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Enemy Bullets: " + EnemyBullets.Count, new Vector2(0, 40), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Enemies: " + Enemies.Count, new Vector2(0, 44), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Relics: " + ActiveRelics.Count, new Vector2(0, 48), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Players: " + Players.Count, new Vector2(0, 52), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "RunTime: " + TimeSinceStart, new Vector2(0, 56), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Particles: " + Particles.Count, new Vector2(0, 60), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "LevelTimer: " + GameTime, new Vector2(0, 64), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "Allies: " + Allies.Count, new Vector2(0, 68), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
                sb.DrawString(game.debug, "CurrentLevel " + CurrentLevel, new Vector2(0, 72), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
            }

            //sb.DrawString(game.debug, System.IO.Directory.GetCurrentDirectory() + "\\Levels\\Level1.json", new Vector2(0, 68), Color.Yellow, 0f, new Vector2(0, 0), 0.075f, SpriteEffects.None, 0f);
        }

        public void Refreshlevels()
        {
            //IF IT CRASHES IN THE FUTURE THEN CHANGE THE PROPERTIES
            Levels.Clear();
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\EndlessWorld.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\DictQuickFight.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\DictLongFight.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\Level1.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\Level2.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\Level3.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\Level4.json"))));
            Levels.Add(new Level(JsonConvert.DeserializeObject<object>(File.ReadAllText("Levels\\Level5.json"))));
        }

        public void ResetAllNumbers()
        {
            foreach (Player play in Players)
            {
                play.Health = 10;
                play.IsDead = false;
                play.LocalRelics.Clear();
                play.HitAniFade = 0;
            }

            Buttons.Add(new Button(new Vector2(109, 80), new Rectangle(109, 80, 19, 28), ButtonHelper.DefaultStartMenu[0], Textures["ResultScreen"], this)); // Back to map
            Buttons.Add(new Button(new Vector2(129, 80), new Rectangle(129, 80, 19, 28), (s, b) =>
            {
                Buttons.Clear();
                Buttons.Add(new Button(new Vector2(88, 35), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[0], "SOLO", this));
                Buttons.Add(new Button(new Vector2(86, 45), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[1], "CO-OP", this));
                Buttons.Add(new Button(new Vector2(84, 55), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[2], "CONFIG", this));
                Buttons.Add(new Button(new Vector2(88, 65), new Rectangle(0, 0, 0, 0), ButtonHelper.DefaultStartMenu[3], "EXIT", this));
                game.Scene = 0;
                AltScene = 0;
                menupos = 0;
            }, Textures["ResultScreen"], this)); // Back to menu

            Buttons.Add(new Button(new Vector2(149, 80), new Rectangle(149, 80, 43, 28), ButtonHelper.DefaultMapMenu[10], Textures["ResultScreen"], this)); // Dictionary


            //DefaultMapMenu[10]


            EndlessSpawner = new EndlessSpawner(this);
            AltScene = 1;
            menupos = 0;
            Enemies.Clear();
            Bullets.Clear();
            Allies.Clear();
            EnemyBullets.Clear();
            Refreshlevels();
            CurrentPlayerShop = 0;
            CurrentPlayerShop = 0;
            ActiveRelics.Clear();
            Refreshlevels();

        }

        public void ResetDicList()
        {
            DicList.Clear();
            DicList.Add(new Button(new Vector2(60, 19), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 0, 25, this) { Tags = new List<string>() { "Page", "Enemy","id1", "Mechanical", "Small", "BasicEnemy" } }); // basic
            DicList.Add(new Button(new Vector2(90, 19), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 1, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id2", "Mechanical", "Medium", "Bouncer" } }); // bouncer
            DicList.Add(new Button(new Vector2(120, 19), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 2, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id3", "Mechanical", "Medium", "Curlicue" } });// curlicue
            DicList.Add(new Button(new Vector2(150, 19), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 3, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id4", "Crimson", "Medium", "Exploder" } }); // Exploder
            
            DicList.Add(new Button(new Vector2(60, 60), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 4, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id5", "Mechanical", "Medium", "FlamethrowerEnemy" } }); // Flame
            DicList.Add(new Button(new Vector2(90, 60), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 5, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id6", "Mechanical", "Large", "Guardian" } }); // Guaridan
            DicList.Add(new Button(new Vector2(120, 60), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 6, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id7", "Magical", "Medium", "Jinx" } }); // JInx
            DicList.Add(new Button(new Vector2(150, 60), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 7, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id8", "Mechanical", "Medium", "Kamikaze" } }); // Kamikaze
            
            DicList.Add(new Button(new Vector2(60, 90), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 8, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id9", "Mechanical", "Large", "Lazermancer" } }); // Lazermancer
            DicList.Add(new Button(new Vector2(90, 90), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 9, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id10", "Magical", "Large", "Meteorologist" } }); // Meteorologist
            DicList.Add(new Button(new Vector2(120, 90), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 10, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id11", "Magical", "Paragon", "Nowcaster" } });//Nowcaster
            DicList.Add(new Button(new Vector2(150, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 11, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id12", "Mechanical", "Paragon", "Overseer" } }); // Overseer
            
            DicList.Add(new Button(new Vector2(60, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 12, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id13", "Magical", "Large", "Pulsar" } });//pulsar
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 13, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id14", "Mechanical", "Medium", "Retaliator" } }); // Retaliator
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 14, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id15", "Mechanical", "Medium", "Scanner" } }); // Scanner
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 15, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id16", "Mechanical", "Small", "Sizzler" } }); // Sizzler
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 16, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id17", "Mechanical", "Medium", "Sniper" } }); // Sniper
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 17, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id18", "Crimson", "Large", "Spiker" } }); // Spiker
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 18, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id19", "Mechanical", "Large", "Watchdog" } }); //Watchdog
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyList"], 19, 25, this) { Tags = new List<string>() { "Page", "Enemy", "id20", "Magical", "Large", "Mirage" } }); //Mirage
                                                                                                                                                                                                                                                                                     //Enemy Relics
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyRelicList"], 0, 25, this) { Tags = new List<string>() { "Page", "Relic", "Warp" } });
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyRelicList"], 1, 25, this) { Tags = new List<string>() { "Page", "Relic", "Target" } });
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyRelicList"], 2, 25, this) { Tags = new List<string>() { "Page", "Relic", "Health" } });
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyRelicList"], 3, 25, this) { Tags = new List<string>() { "Page", "Relic", "Curse" } }); // Spiker
            DicList.Add(new Button(new Vector2(90, 120), new Rectangle(0, 0, 25, 26), ButtonHelper.ShipDictionary[15], Textures["DictionaryParticle"], Textures["EnemyRelicList"], 4, 25, this) { Tags = new List<string>() { "Page", "Relic", "Shield" } }); //Watchdog
        }
    }
}