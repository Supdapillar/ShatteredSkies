using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
        public class Input
    {
        public ButtonState Left { get; set; }
        public ButtonState Right { get; set; }
        public ButtonState Up { get; set; }
        public ButtonState Down { get; set; }
        public ButtonState Shoot { get; set; }
        public ButtonState Ability { get; set; }
        public ButtonState LeftBumper { get; set; }
        public ButtonState RightBumper { get; set; }
        public float LeftTrigger { get; set; }
        public float RightTrigger { get; set; }
        public float LeftX { get; set; }
        public float LeftY { get; set; }
    }
}
