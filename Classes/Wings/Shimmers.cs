using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShatteredSkies.Classes
{
    public class Shimmers : Wings
    {
        public Shimmers(SceneManager sceneman) : base(sceneman)
        {
            MaxDelay = 10f;
            SceneMan = sceneman;
            Width = 7;
            Height = 10;
            WingTexture = SceneMan.Textures["Shimmers"];
        }
        public override void Activated(Player play, GameTime GT)
        {
            if (play.AbilityDelay < 10)
            {
                play.IFrames = 1;
                play.AbilityDelay += GT.ElapsedGameTime.TotalSeconds*6;
            }
        }
        public override void DrawUI(SpriteBatch sb)
        {

        }
    }
}
