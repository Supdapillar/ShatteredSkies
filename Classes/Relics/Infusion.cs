using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Infusion : Relic
    {
        public Infusion(int power,SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnHit = true;
        }
        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            if (PowerLevel == 1)
            {
                if (SceneMan.rand.NextDouble() <= (0.50))
                {
                    Infuse(ene, bul);
                }
            }
            else if (PowerLevel > 1)
            {
                Infuse(ene, bul);
            }

        }

        private void Infuse(Enemy ene, Bullet bul)
        {
            SceneMan.Particles.Add(new TextParticle(new Vector2(ene.Pos.X - 8, ene.Pos.Y + ene.WidthHeight.Y + 5), "INFUSED", new Color(0, 0, 128, 255), SceneMan));
            switch (bul.GetType().Name)
            {
                case "BasicShot":
                    ene.Contains.StoredBullets.Add(new BasicShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy){Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1))});
                    break;
                case "BasicShotWeak":
                    ene.Contains.StoredBullets.Add(new BasicShotWeak(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "BloodBullet":
                    ene.Contains.StoredBullets.Add(new BloodBullet(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "BouncyShotAlly":
                    ene.Contains.StoredBullets.Add(new BouncyShotAlly(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), new Vector2((float)((SceneMan.rand.NextDouble()) - 0.5), (float)((SceneMan.rand.NextDouble()) - 0.5)), SceneMan, bul.ShotBy));
                    break;
                case "CryoDetonator":
                    ene.Contains.StoredBullets.Add(new CryoDetonator(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "ExplosiveShot":
                    ene.Contains.StoredBullets.Add(new ExplosiveShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "EnemyMeteorAlly":
                    ene.Contains.StoredBullets.Add(new EnemyMeteorAlly(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), new Vector2((float)((SceneMan.rand.NextDouble()) - 0.5), (float)((SceneMan.rand.NextDouble()) - 0.5)), SceneMan, bul.ShotBy));
                    break;
                case "Flamethrower":
                    ene.Contains.StoredBullets.Add(new Flamethrower(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "GatlingShot":
                    ene.Contains.StoredBullets.Add(new GatlingShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "KamikazeFallAlly":
                    ene.Contains.StoredBullets.Add(new KamikazeFallAlly(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), new Vector2((float)((SceneMan.rand.NextDouble()) - 0.5), (float)((SceneMan.rand.NextDouble()) - 0.5)), SceneMan, bul.ShotBy));
                    break;
                case "LivingThorn":
                    ene.Contains.StoredBullets.Add(new LivingThorn(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "LivingFlower":
                    ene.Contains.StoredBullets.Add(new LivingFlower(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "MagicBullet":
                    ene.Contains.StoredBullets.Add(new MagicBullet(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy, bul.ShotBy) { LifeSpan = (float)(9999 - SceneMan.rand.NextDouble() * 12) });
                    break;
                case "MicroDroneShot":
                    ene.Contains.StoredBullets.Add(new MicroDroneShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "PiercingShot":
                    ene.Contains.StoredBullets.Add(new PiercingShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "RailCannon":
                    ene.Contains.StoredBullets.Add(new RailCannon(bul.SubType, bul.Pos, SceneMan, bul.ShotBy) { /*Origin = bul.Pos - bul.ShotBy.Pos*/});
                    break;
                case "ShatteringShot":
                    ene.Contains.StoredBullets.Add(new ShatteringShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "ShrapnelShot":
                    ene.Contains.StoredBullets.Add(new ShrapnelShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "SummoningShot":
                    ene.Contains.StoredBullets.Add(new SummoningShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "LockOnShot":
                    ene.Contains.StoredBullets.Add(new LockOnShot(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "ExplosiveMine":
                    ene.Contains.StoredBullets.Add(new ExplosiveMine(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
                case "StarCannon":
                    ene.Contains.StoredBullets.Add(new StarCannon(bul.SubType, new Vector2(ene.WidthHeight.X / 2, ene.WidthHeight.Y / 2), SceneMan, bul.ShotBy) { Delta = new Vector2((float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1), (float)(SceneMan.rand.NextDouble() + 0.25) * ((SceneMan.rand.Next(0, 2) * 2) - 1)) });
                    break;
            }
        }
    }
}
