using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShatteredSkies.Classes
{
    public class FutureCloneParticle : Particle
    {
        //Mostly used by future clone to look pretty gamer

        private Color Col;
        private Player ConnectedPlayer;
        public FutureCloneParticle(Vector2 pos, SceneManager sceneman, Color col, Player player) : base(pos, sceneman)
        {
            Pos = pos;
            SceneMan = sceneman;
            Delta = new Vector2(0, 0);
            Col = col;
            TimeSinceCreation = 0.25f;
            ConnectedPlayer = player;
        }

        public override void Update(GameTime GT)
        {
            TimeSinceCreation -= (float)GT.ElapsedGameTime.TotalSeconds;
            if (TimeSinceCreation < 0)
            {
                Pos.Y = 600;
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].CoreTexture, new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Rectangle(0, 0, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width, ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Height), new Color(Col.R / 255 * (TimeSinceCreation * 4), Col.G / 255 * (TimeSinceCreation * 4), Col.B / 255 * (TimeSinceCreation * 4), Col.A / 255 * (TimeSinceCreation * 4)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X - ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(Col.R / 255 * (TimeSinceCreation * 4), Col.G / 255 * (TimeSinceCreation * 4), Col.B / 255 * (TimeSinceCreation * 4), Col.A / 255 * (TimeSinceCreation * 4)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            sb.Draw(ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].WingTexture, new Rectangle((int)Math.Ceiling(Pos.X + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].Width), (int)Math.Ceiling(Pos.Y + ConnectedPlayer.AllCores[ConnectedPlayer.CurrentShipParts[0]].WingOffset[ConnectedPlayer.CurrentShipParts[1]].Y), ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Rectangle(0, 0, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Width, ConnectedPlayer.AllWings[ConnectedPlayer.CurrentShipParts[1]].Height), new Color(Col.R / 255 * (TimeSinceCreation * 4), Col.G / 255 * (TimeSinceCreation * 4), Col.B / 255 * (TimeSinceCreation * 4), Col.A / 255 * (TimeSinceCreation * 4)), 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.3f);
            //sb.Draw(SceneMan.Textures["Core_Sheet"], new Rectangle((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y), SceneMan.Offsets["CoreWidth_" + SceneMan.CurrentShipParts[0]], SceneMan.Textures["Core_Sheet"].Height), new Rectangle(Helper.GetListSum("CoreWidth_", SceneMan.Offsets, SceneMan.CurrentShipParts[0] + 1) - SceneMan.Offsets["CoreWidth_" + SceneMan.CurrentShipParts[0]], 0, SceneMan.Offsets["CoreWidth_" + SceneMan.CurrentShipParts[0]], SceneMan.Textures["Core_Sheet"].Height), new Color(Col.R / 255 * (TimeSinceCreation * 4), Col.G / 255 * (TimeSinceCreation * 4), Col.B / 255 * (TimeSinceCreation * 4), Col.A / 255 * (TimeSinceCreation * 4)), 0f, new Vector2(0, 0), SpriteEffects.None, 0.3f);
            //Helper.CompactDrawWingsInside(sb, SceneMan, new Vector2((int)Math.Ceiling(Pos.X), (int)Math.Ceiling(Pos.Y)), Delta.X, new Color(Col.R / 255 * (TimeSinceCreation * 4), Col.G /255 * (TimeSinceCreation * 4), Col.B / 255 * (TimeSinceCreation * 4), Col.A / 255 * (TimeSinceCreation * 4)));
        }
    }
}
