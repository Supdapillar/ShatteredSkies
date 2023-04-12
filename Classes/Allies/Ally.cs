using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Ally
    {
        public Vector2 Pos;
        public Vector2 Delta;
        public Vector2 WidthHeight;
        public float Health = 2;
        public float MaxHealth = 3;

        public Vector2 TopSpeed;

        public Player CreatedBy;

        public List<StatusEffect> StatusEffects;

        //Local storage Relics
        public List<dynamic> LocalRelics = new List<dynamic>();

        public double ShotDelay;

        public SceneManager SceneMan;

        public Ally(Vector2 PS, SceneManager Scenemana, Player createdby)
        {
            Pos = PS;
            SceneMan = Scenemana;
        }
        public virtual void Update(GameTime GT)
        {

        }
        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void CheckCollision(Vector2 WH)
        {
            //collision with bullets
            foreach (EnemyBullet Ebull in SceneMan.EnemyBullets)
            {
                if (Helper.BoxCollision((int)Pos.X, (int)Pos.Y, (int)WH.X, (int)WH.Y, (int)Ebull.Pos.X, (int)Ebull.Pos.Y, (int)Ebull.WidthHeight.X, (int)Ebull.WidthHeight.Y))
                {
                    if (Ebull.Damage - (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyArmor > 0)
                    {
                        Health -= Ebull.Damage - (float)CreatedBy.AllCores[CreatedBy.CurrentShipParts[0]].Stats.AllyArmor;
                    }
                    Ebull.Health -= 1;
                    //Relic Mod Ally OnHit
                    foreach (Relic rel in SceneMan.ActiveRelics)
                    {
                        if (rel.ModifiesAllyOnHit)
                        {
                            rel.ModAllyOnHit(this, Ebull);
                        }
                    }
                }
            }
        }
    }
}
