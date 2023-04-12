using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShatteredSkies.Classes
{
    public class Soul : Relic
    {
        //Local Storage Variables
        public Bullet SoulBullet = null;
        public Bullet SoulBulletBase = null;
        public Soul(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnHit = true;
            ModifiesBulletOnDeath = true;
            ModifiesBulletUpdate = true;
            ModifiesBulletConstructor = true;
            //makes sure all bullets get relic
            foreach (Bullet bull in SceneMan.Bullets)
            {
                bull.LocalRelics.Add(new Soul(PowerLevel, SceneMan, ConnectedPlayer, true));
            }
        }


        //for the local constructor
        public Soul(int power, SceneManager sceneman, Player play, bool local) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;
        }
        public override void ModBulUpdate(Bullet bul, GameTime GT)
        {
            for (int i = 0; i < bul.LocalRelics.Count; i++)
            {
                if (bul.LocalRelics[i] is Soul)
                {
                    if (bul.LocalRelics[i].SoulBulletBase == null) // updates the base bullet
                    {
                        if (bul.LocalRelics[i].SoulBullet != null)
                        {
                            bul.Health = bul.LocalRelics[i].SoulBullet.Health;
                        }
                    }
                    else if (bul.LocalRelics[i].SoulBullet == null) // updates the clone bullet
                    {
                        bul.Pos.Y = bul.LocalRelics[i].SoulBulletBase.Pos.Y + bul.LocalRelics[i].SoulBulletBase.Delta.Y; 
                        bul.Pos.X = 288 - bul.LocalRelics[i].SoulBulletBase.Pos.X + bul.LocalRelics[i].SoulBulletBase.Delta.X; ;
                        bul.LocalRelics[i].SoulBulletBase.Health = bul.Health;
                        bul.ProcChance = bul.LocalRelics[i].SoulBulletBase.ProcChance;
                    }
                }
            }
        }
        public override void ModBulCons(Bullet bul)
        {
            if (bul.ShotBy is Player)
            {
                if (bul.ShotBy == ConnectedPlayer)
                {
                    //adds soul to the bullet
                    if (!bul.LocalRelics.OfType<Soul>().Any())
                    {
                        bul.LocalRelics.Add(new Soul(0, SceneMan, ConnectedPlayer, true));
                    }

                    //creates a new bullet with the bullet's soul modified
                    for (int i = 0; i < bul.LocalRelics.Count; i++)
                    {
                        if (bul.LocalRelics[i] is Soul)
                        {
                            if (bul.LocalRelics[i].SoulBulletBase == null)
                            {//bul.GetType()
                                switch (bul.GetType().Name)
                                {
                                    case "BasicShot":
                                        SceneMan.Bullets.Add(new BasicShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "BasicShotWeak":
                                        SceneMan.Bullets.Add(new BasicShotWeak(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "BloodBullet":
                                        SceneMan.Bullets.Add(new BloodBullet(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "BouncyShotAlly":
                                        SceneMan.Bullets.Add(new BouncyShotAlly(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), new Vector2(0, 0), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "CryoDetonator":
                                        SceneMan.Bullets.Add(new CryoDetonator(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "ExplosiveShot":
                                        SceneMan.Bullets.Add(new ExplosiveShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "EnemyMeteorAlly":
                                        SceneMan.Bullets.Add(new EnemyMeteorAlly(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), new Vector2(0, 0), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "Flamethrower":
                                        SceneMan.Bullets.Add(new Flamethrower(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "GatlingShot":
                                        SceneMan.Bullets.Add(new GatlingShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "KamikazeFallAlly":
                                        SceneMan.Bullets.Add(new KamikazeFallAlly(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), new Vector2(0, 0), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "LivingThorn":
                                        SceneMan.Bullets.Add(new LivingThorn(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "LivingFlower":
                                        SceneMan.Bullets.Add(new LivingFlower(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "MagicBullet":
                                        SceneMan.Bullets.Add(new MagicBullet(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, SceneMan.Players[0], new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "MicroDroneShot":
                                        SceneMan.Bullets.Add(new MicroDroneShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "PiercingShot":
                                        SceneMan.Bullets.Add(new PiercingShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "RailCannon":
                                        SceneMan.Bullets.Add(new RailCannon(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "ShatteringShot":
                                        SceneMan.Bullets.Add(new ShatteringShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "ShrapnelShot":
                                        SceneMan.Bullets.Add(new ShrapnelShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "SummoningShot":
                                        SceneMan.Bullets.Add(new SummoningShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer));
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "LockOnShot":
                                        SceneMan.Bullets.Add(new LockOnShot(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer) { LifeSpan = 5f });
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "ExplosiveMine":
                                        SceneMan.Bullets.Add(new ExplosiveMine(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer) { LifeSpan = 5f });
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                    case "StarCannon":
                                        SceneMan.Bullets.Add(new StarCannon(bul.SubType, new Vector2(288 - bul.Pos.X, bul.Pos.Y), SceneMan, new List<dynamic>() { new Soul(0, SceneMan, ConnectedPlayer, true) { SoulBulletBase = bul } }, ConnectedPlayer) { LifeSpan = 5f });
                                        bul.LocalRelics[i].SoulBullet = SceneMan.Bullets[^1];
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void ModBulOnDeath(Bullet bul)
        {
            if (bul.ShotBy is Player)
            {
                if (bul.ShotBy == ConnectedPlayer)
                {
                    for (int i = 0; i < bul.LocalRelics.Count; i++)
                    {
                        if (bul.LocalRelics[i] is Soul)
                        {
                            if (bul.LocalRelics[i].SoulBulletBase == null) // updates the base bullet
                            {
                                if (bul.LocalRelics[i].SoulBullet != null)
                                {
                                    bul.LocalRelics[i].SoulBullet.Health = 0;

                                }
                            }
                            else if (bul.LocalRelics[i].SoulBullet == null) // updates the clone bullet
                            {
                                bul.LocalRelics[i].SoulBulletBase.Health = 0;
                            }
                        }
                    }
                }
            }
        }
        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            if (bul.ShotBy is Player)
            {
                if (bul.ShotBy == ConnectedPlayer)
                {
                    for (int i = 0; i < bul.LocalRelics.Count; i++)
                    {
                        if (bul.LocalRelics[i] is Soul)
                        {
                            if (bul.LocalRelics[i].SoulBulletBase == null) // updates the base bullet
                            {
                                if (bul.LocalRelics[i].SoulBullet != null)
                                {
                                    bul.LocalRelics[i].SoulBullet.Health -= 1;
                                }
                            }
                            else if (bul.LocalRelics[i].SoulBullet == null) // updates the clone bullet
                            {
                                bul.LocalRelics[i].SoulBulletBase.Health -= 1;
                            }
                        }
                    }
                }
            }
        }
    }
}
