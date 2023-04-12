using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Enemy
    {
        public Vector2 Pos;
        public Vector2 Delta;

        public Vector2 WidthHeight;

        public float TimeSinceCreation = 0;
        public Container Contains = new Container();
        public List<Bullet> AllCollidingBullets = new List<Bullet>();
        public float Health = 3;
        public float MaxHealth = 3;

        //Textures
        public Texture2D SprOutline;
        public Texture2D SprInside;
        public Texture2D SprInside2;

        public List<StatusEffect> StatusEffects;

        //Local storage Relics
        public List<dynamic> LocalRelics = new List<dynamic>();

        //Enemy Relics
        public List<dynamic> EnemyRelics = new List<dynamic>();

        public double ShotDelay;

        public int Size = 0;

        public string Name;

        public SceneManager SceneMan;

        public dynamic LastHitBy;

        public dynamic Killedby;

        public Enemy(Vector2 PS, SceneManager Scenemana)
        {
            Pos = PS;
            SceneMan = Scenemana;
        }

        public virtual void Update(GameTime GT)
        {
            Pos.X += Delta.X;
            Pos.Y += Delta.Y;
            TimeSinceCreation += (float)GT.ElapsedGameTime.TotalSeconds;
        }
        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void CheckCollision(Vector2 WH)
        {
            //collision with bullets
            for (int g = 0; g < SceneMan.Bullets.Count; g++)
            {
                if (SceneMan.Bullets[g].Health > 0)
                {
                    if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WH.X, (int)WH.Y, (int)SceneMan.Bullets[g].Pos.X, (int)SceneMan.Bullets[g].Pos.Y, (int)SceneMan.Bullets[g].WidthHeight.X, (int)SceneMan.Bullets[g].WidthHeight.Y))
                    {
                        if (AllCollidingBullets.Contains(SceneMan.Bullets[g]) == false)
                        {
                            Health -= SceneMan.Bullets[g].Damage;
                            SceneMan.Bullets[g].Health -= 1;
                            AllCollidingBullets.Add(SceneMan.Bullets[g]);
                            LastHitBy = SceneMan.Bullets[g].ShotBy;
                            foreach (KeyValuePair<string, float> Strint in SceneMan.Bullets[g].OnHitEffects)
                            {
                                switch (Strint.Key)
                                {
                                    case "Burning":
                                        StatusEffects[0].EffectAmount += Strint.Value;
                                        break;
                                    case "Bleeding":
                                        StatusEffects[1].EffectAmount += Strint.Value;
                                        break;
                                }
                            }
                            //Relic Mod Enemy OnHit
                            foreach (Relic rel in SceneMan.ActiveRelics)
                            {
                                if (rel.ModifiesEnemyOnHit)
                                {
                                    rel.ModEneOnHit(this, SceneMan.Bullets[g]);
                                }
                            }
                            //Enemy relic On hit
                            foreach (EnemyRelic Erel in EnemyRelics)
                            {
                                Erel.ModEneOnHit(this, SceneMan.Bullets[g]);
                            }
                            if (Health <= 0)
                            {
                                Killedby = SceneMan.Bullets[g].ShotBy;
                                return;
                            }
                        }
                        if (SceneMan.Bullets[g] is ShrapnelShot && SceneMan.Bullets[g].SubType == 0 && Health + SceneMan.Bullets[g].Damage > 0)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Contains.StoredBullets.Add(new ShrapnelShot(1, new Vector2(WidthHeight.X / 2, WidthHeight.Y / 2), SceneMan, SceneMan.Bullets[g].ShotBy));
                            }
                        }
                    }
                }
            }
            //Checks if enemy can collider with the bullet agian
            for (int i = 0; i < AllCollidingBullets.Count; i++)
            {
                if (!Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WidthHeight.X, (int)WidthHeight.Y, (int)AllCollidingBullets[i].Pos.X, (int)AllCollidingBullets[i].Pos.Y, (int)AllCollidingBullets[i].WidthHeight.X, (int)AllCollidingBullets[i].WidthHeight.Y))
                {
                    AllCollidingBullets.Remove(AllCollidingBullets[i]);
                }
            }
        }
        public void Enemy_init()
        {
            StatusEffects = new List<StatusEffect>()
            {
                new Burning(this),
                new Bleeding(this),
                new Freezing(this),
                new Hacked(this)
            };
            //Relic Mod Enemy Contructor
            foreach (Relic rel in SceneMan.ActiveRelics)
            {
                if (rel.ModifiesEnemyConstructor)
                {
                    rel.ModEneCons(this);
                }
            }
            //Enemy relic conrtuctor
            foreach (EnemyRelic Erel in EnemyRelics)
            {
                Erel.ModEneCons(this);
            }
            //For dictionary
            if (!SceneMan.LastFought.Contains(GetType().Name))
            {
                SceneMan.LastFought.Insert(0, GetType().Name);
            }
        }

        public void RenderHealth(SpriteBatch sb)
        {
            sb.Draw(SceneMan.Textures["WhitePixel"], new Rectangle((int)Pos.X, (int)Pos.Y - 3, (int)Math.Ceiling((Health/MaxHealth)*WidthHeight.X), 2), new Rectangle(0, 0, 1, 1), Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
