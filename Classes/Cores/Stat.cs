using System;
using System.Collections.Generic;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Stat
    {
        //Stats
        public double Damage = 1f;
        public double BulletDamage = 1f;
        public double AOEDamage = 1f;

        public double ProcPercent = 1f;

        public double ChargeStartAt = 0f;
        public double ChargeRate = 1f;

        public double AllyDamage = 1f;
        public double AllyFireRate = 1f;
        public double AllyArmor = 0f;
        public double AllySpeed = 1f;

        public double FireRate = 1f;

        public double Speed = 1f;
        public double TopSpeed = 1f;
        public double Accerleration = 1f;
        public double Deaccerleration = 1f;

        public double MaxHealth = 10f;
        public double IncomingDamageMultiplier = 1f;

        public double Accuracy = 1f;

        public double BulletLifeSpan = 1f;

        public Stat()
        {

        }
    }
}
