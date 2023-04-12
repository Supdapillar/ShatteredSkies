using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Flashers : Wings
    {
        private float ExplosionSize = 0;
        private bool HasExploded = false;
        public Flashers(SceneManager sceneman) : base(sceneman)
        {
            MaxDelay = 4f;
            SceneMan = sceneman;
            Width = 8;
            Height = 18;
            WingTexture = SceneMan.Textures["Flashers"];
        }
        public override void Activated(Player play, GameTime GT)
        {
            if (play.AbilityDelay <= 0)
            {
                HasExploded = false;
                foreach (Enemy ene in SceneMan.Enemies)
                {
                    ExplosionSize = 0;
                    foreach (StatusEffect status in ene.StatusEffects)
                    {

                        ExplosionSize += status.EffectAmount;
                        status.EffectAmount = 0;
                    }
                    if (ExplosionSize > 0 && ExplosionSize <= 10)
                    {
                        SceneMan.Bullets.Add(new Explosion(0, new Vector2(ene.Pos.X + ene.WidthHeight.X / 2 - 20, ene.Pos.Y + ene.WidthHeight.Y / 2 - 20), SceneMan, ExplosionSize, 20, play));
                        HasExploded = true;
                    }
                    else if (ExplosionSize > 10)
                    {
                        SceneMan.Bullets.Add(new Explosion(0, new Vector2(ene.Pos.X + ene.WidthHeight.X / 2 - 30, ene.Pos.Y + ene.WidthHeight.Y / 2 - 30), SceneMan, ExplosionSize, 30, play));
                        HasExploded = true;
                    }
                }
                if (HasExploded) play.AbilityDelay = 4;
            }
        }
        public override void DrawUI(SpriteBatch sb)
        {

        }
    }
}
