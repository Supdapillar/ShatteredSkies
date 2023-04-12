using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Animation
    {
        private readonly Texture2D TextureSheet;
        private readonly double AnimationSpeed;
        private double AnimationProgress = 0;
        private readonly int ChoppedWidth;
        private readonly bool Looping;
        private readonly bool NormalProgression;


        //Defualt with or without looping
        public Animation(Texture2D sheet, double speed, int choppedwidth, bool looping)
        {
            TextureSheet = sheet;
            AnimationSpeed = speed;
            ChoppedWidth = choppedwidth;
            Looping = looping;
            NormalProgression = true;
        }
        //Progress scales with an int
        public Animation(Texture2D sheet, int choppedwidth)
        {
            TextureSheet = sheet;
            ChoppedWidth = choppedwidth;
            NormalProgression = false;
        }

        public void Update(GameTime GT)
        {
            if (NormalProgression)
            {
                if (Math.Floor(AnimationProgress) < (TextureSheet.Width / ChoppedWidth))
                {
                    AnimationProgress += AnimationSpeed * GT.ElapsedGameTime.TotalSeconds;
                }
                else if (Looping)
                {
                    AnimationProgress = 0f;
                }
            }
        }

        public void Update(int Progression)
        {
            if (!NormalProgression)
            {
                AnimationProgress = Progression;
            }
        }

        public void Draw(SpriteBatch sb, Vector2 pos, float Layer, SceneManager Sceneman)
        {
            if (Math.Floor(AnimationProgress)== TextureSheet.Width / ChoppedWidth)
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((TextureSheet.Width / ChoppedWidth-1) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, Layer);
            }
            else
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((int)Math.Floor(AnimationProgress) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, Layer);
            }
        }

        public void Draw(SpriteBatch sb, Vector2 pos, float Layer,Color col,SpriteEffects spriteeffect, SceneManager Sceneman)
        {
            if (Math.Floor(AnimationProgress) == TextureSheet.Width / ChoppedWidth)
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((TextureSheet.Width / ChoppedWidth - 1) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), col, 0f, new Vector2(0, 0), spriteeffect, Layer);
            }
            else
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((int)Math.Floor(AnimationProgress) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), col, 0f, new Vector2(0, 0), spriteeffect, Layer);
            }
        }
        public void Draw(SpriteBatch sb, Vector2 pos, float Layer, Color col, SpriteEffects spriteeffect,float rotation, SceneManager Sceneman)
        {
            if (Math.Floor(AnimationProgress) == TextureSheet.Width / ChoppedWidth)
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((TextureSheet.Width / ChoppedWidth - 1) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), col, rotation, new Vector2(0, 0), spriteeffect, Layer);
            }
            else
            {
                sb.Draw(TextureSheet, new Rectangle((int)(pos.X), (int)(pos.Y), ChoppedWidth, TextureSheet.Height), new Rectangle((int)Math.Floor(AnimationProgress) * ChoppedWidth, 0, ChoppedWidth, TextureSheet.Height), col, rotation, new Vector2(0, 0), spriteeffect, Layer);
            }
        }
    }
}
