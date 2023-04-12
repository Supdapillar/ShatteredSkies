 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShatteredSkies.Classes
{
    public class Technology : Relic
    {
        public Technology(int power, SceneManager sceneman, Player play) : base(power, sceneman, play)
        {
            PowerLevel = power;
            SceneMan = sceneman;
            ConnectedPlayer = play;

            ModifiesEnemyOnHit = true;
        }

        public override void ModEneOnHit(Enemy ene, Bullet bul)
        {
            if (bul.ShotBy.CreatedBy == ConnectedPlayer)
            {
                if (SceneMan.rand.NextDouble() <= 0.75 * bul.ProcChance)
                {
                    ene.StatusEffects[3].EffectAmount += 2;
                }
            }
        }
    }
}
