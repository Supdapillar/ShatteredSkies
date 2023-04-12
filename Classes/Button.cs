using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShatteredSkies.Classes
{
    public class Button
    {
        public Vector2 Pos;

        public Color ButtonColor = new Color(255, 255, 255);
        private readonly int ConnectedRelic = -1;
        private readonly Action<SceneManager, Button> action;

        //only allows 1 input in a frame
        public bool BlockInput = false;

        public bool NeedsDeletionQ = false;

        static SceneManager SceneMan;
        private Rectangle Rect;

        private readonly Texture2D ButtonPic;
        private readonly String ButtonText;

        //slider stuff
        private readonly int[] SliderValues;
        public int StaticSlider;
        private readonly int SliderIndex;
        private readonly Texture2D ButtonPicSliderBorder;
        private readonly Texture2D ButtonPicSliderInside1;
        private readonly Texture2D ButtonPicSliderInside2;
        private readonly int ChoppedWidth;

        public Color TextHoverColor = new Color(128,0,255);
        public Color TextColor = new Color(255,255,255);

        public List<string> Tags = new List<string>();

        private Button ClosestButton;
        private double Angle = 0;

        //// Constructors ////
        //Text only button
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, string text, SceneManager man)
        {
            ButtonPic = null;
            SceneMan = man;
            action = act;
            ButtonText = text;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = 5;
            ButtonPicSliderBorder = null;
        }

        //Image button
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = 5;
            ButtonPicSliderBorder = null;
        }

        //imaged button with slider
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, int[] sliderpos, int sliderindex, int choppedwidth, int connectedrelic, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            SliderValues = sliderpos;
            SliderIndex = sliderindex;
            ButtonPicSliderBorder = slider;
            ConnectedRelic = connectedrelic;
        }

        //imaged button with slider and slider inside
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, Texture2D sliderinside1, int[] sliderpos, int sliderindex, int choppedwidth , int connectedrelic, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            SliderValues = sliderpos;
            SliderIndex = sliderindex;
            ButtonPicSliderBorder = slider;
            ButtonPicSliderInside1 = sliderinside1;
            ConnectedRelic = connectedrelic;
        }

        //imaged button with slider and slider inside1 and 2
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, Texture2D sliderinside1, Texture2D sliderinside2, int[] sliderpos, int sliderindex, int choppedwidth, int connectedrelic, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            SliderValues = sliderpos;
            SliderIndex = sliderindex;
            ButtonPicSliderBorder = slider;
            ButtonPicSliderInside1 = sliderinside1;
            ButtonPicSliderInside2 = sliderinside2;
            ConnectedRelic = connectedrelic;
        }

        //imaged button with slider and slider inside1 and 2 and no clickies
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, Texture2D sliderinside1, Texture2D sliderinside2, int staticslider, int choppedwidth, int connectedrelic, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            StaticSlider = staticslider;
            ButtonPicSliderBorder = slider;
            ButtonPicSliderInside1 = sliderinside1;
            ButtonPicSliderInside2 = sliderinside2;
            ConnectedRelic = connectedrelic;
        }

        //Image button with slider Has no relic
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, int[] sliderpos, int sliderindex, int choppedwidth ,SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            SliderValues = sliderpos;
            SliderIndex = sliderindex;
            ButtonPicSliderBorder = slider;
        }

        //Image button with slider Has no relic AND NO CLICKIES
        public Button(Vector2 PS, Rectangle Recta, Action<SceneManager, Button> act, Texture2D tex, Texture2D slider, int staticslider, int choppedwidth, SceneManager man)
        {
            ButtonPic = tex;
            SceneMan = man;
            action = act;
            ButtonText = null;
            Pos = PS;
            Rect = Recta;
            //sluder
            ChoppedWidth = choppedwidth;
            StaticSlider = staticslider;
            ButtonPicSliderBorder = slider;
        }

        //functionz
        public void Update(GameTime GT)
        {
            //button inputs for current button
            if (SceneMan.Buttons[SceneMan.menupos] == this)
            {
                // A button
                if (!BlockInput)
                {
                    if (SceneMan.Players[SceneMan.CurrentPlayerShop].State.Buttons.A == ButtonState.Pressed & !((SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.Buttons.A == ButtonState.Pressed)))
                    {
                        OnPressed();
                    }
                }
                if (SliderValues != null)
                {
                    //LB and RB
                    if (SceneMan.Players[SceneMan.CurrentPlayerShop].State.Buttons.LeftShoulder == ButtonState.Pressed & !(SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.Buttons.LeftShoulder == ButtonState.Pressed) & (SliderValues[SliderIndex] > 0))
                    {
                        SliderValues[SliderIndex]--;
                    }
                    else if ((SceneMan.Players[SceneMan.CurrentPlayerShop].State.Buttons.RightShoulder == ButtonState.Pressed) & !(SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.Buttons.RightShoulder == ButtonState.Pressed) & (SliderValues[SliderIndex] < ((ButtonPicSliderBorder.Width-1)/(ChoppedWidth-1))-1))
                    {
                        SliderValues[SliderIndex]++;
                    }
                }
                //Moving inputs
                if (!BlockInput)
                {//up
                    if (SceneMan.Players[SceneMan.CurrentPlayerShop].State.DPad.Up == ButtonState.Pressed & (SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.DPad.Up != ButtonState.Pressed))// up on dpad
                    {
                        foreach (Button butt in SceneMan.Buttons)
                        {
                            butt.BlockInput = true;

                            if (butt == SceneMan.Buttons[SceneMan.menupos]) continue; //SceneMan.Buttons[SceneMan.menupos].Pos.X - mousePosition.X

                            Angle = Helper.GetAngleOfTwoPoints(butt.Pos, Pos);
                            if (Angle >= 45 && Angle <= 135)//CHECKS IF THE BUTTON IS TO THE LEFT
                            {
                                if (ClosestButton == null)
                                {
                                    ClosestButton = butt;
                                }
                                else if (Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, butt.Pos) < Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, ClosestButton.Pos))
                                {
                                    ClosestButton = butt;
                                }
                            }
                        }
                        for (int i = 0; i < SceneMan.Buttons.Count; i++)
                        {
                            if (SceneMan.Buttons[i] == ClosestButton)
                            {
                                ClosestButton = null;
                                SceneMan.menupos = i;
                                break;
                            }
                        }
                    }//down
                    if (SceneMan.Players[SceneMan.CurrentPlayerShop].State.DPad.Down == ButtonState.Pressed & (SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.DPad.Down != ButtonState.Pressed))// down on dpad
                    {
                        foreach (Button butt in SceneMan.Buttons)
                        {
                            butt.BlockInput = true;

                            if (butt == SceneMan.Buttons[SceneMan.menupos]) continue; //SceneMan.Buttons[SceneMan.menupos].Pos.X - mousePosition.X

                            Angle = Helper.GetAngleOfTwoPoints(butt.Pos, Pos);
                            if (Angle <= -45 && Angle >= -135)//CHECKS IF THE BUTTON IS TO THE LEFT
                            {
                                if (ClosestButton == null)
                                {
                                    ClosestButton = butt;
                                }
                                else if (Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, butt.Pos) < Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, ClosestButton.Pos))
                                {
                                    ClosestButton = butt;
                                }
                            }
                        }
                        for (int i = 0; i < SceneMan.Buttons.Count; i++)
                        {
                            if (SceneMan.Buttons[i] == ClosestButton)
                            {
                                ClosestButton = null;
                                SceneMan.menupos = i;
                                break;
                            }
                        }
                    }//left

                    if ((SceneMan.Players[SceneMan.CurrentPlayerShop].State.DPad.Left == ButtonState.Pressed & (SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.DPad.Left != ButtonState.Pressed)))
                    {
                        foreach (Button butt in SceneMan.Buttons)
                        {
                            butt.BlockInput = true;

                            if (butt == SceneMan.Buttons[SceneMan.menupos]) continue; //SceneMan.Buttons[SceneMan.menupos].Pos.X - mousePosition.X

                            Angle = Helper.GetAngleOfTwoPoints(butt.Pos, Pos);
                            if (Angle <= 45 && Angle >= -45)//CHECKS IF THE BUTTON IS TO THE LEFT
                            {
                                if (ClosestButton == null)
                                {
                                    ClosestButton = butt;
                                }
                                else if (Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, butt.Pos) < Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, ClosestButton.Pos))
                                {
                                    ClosestButton = butt;
                                }
                            }
                        }
                        for (int i = 0; i < SceneMan.Buttons.Count; i++)
                        {
                            if (SceneMan.Buttons[i] == ClosestButton)
                            {
                                ClosestButton = null;
                                SceneMan.menupos = i;
                                break;
                            }
                        }
                    }//right

                    if (SceneMan.Players[SceneMan.CurrentPlayerShop].State.DPad.Right == ButtonState.Pressed & (SceneMan.Players[SceneMan.CurrentPlayerShop].LastState.DPad.Right != ButtonState.Pressed))
                    {
                        foreach (Button butt in SceneMan.Buttons)
                        {
                            butt.BlockInput = true;

                            if (butt == SceneMan.Buttons[SceneMan.menupos]) continue; //SceneMan.Buttons[SceneMan.menupos].Pos.X - mousePosition.X

                            Angle = Helper.GetAngleOfTwoPoints(butt.Pos, Pos);
                            if ((Angle >= 135 && Angle <= 180) || (Angle <= -135 && Angle >= -180))//CHECKS IF THE BUTTON IS TO THE RIGHT
                            {
                                if (ClosestButton == null)
                                {
                                    ClosestButton = butt;
                                }
                                else if (Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, butt.Pos) < Helper.GetDistance(SceneMan.Buttons[SceneMan.menupos].Pos, ClosestButton.Pos))
                                {
                                    ClosestButton = butt;
                                }
                            }
                        }
                        for (int i = 0; i < SceneMan.Buttons.Count; i++)
                        {
                            if (SceneMan.Buttons[i] == ClosestButton)
                            {
                                ClosestButton = null;
                                SceneMan.menupos = i;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            if (ButtonPic != null) // render picture button
            {
                if (SceneMan.Buttons[SceneMan.menupos] == this)
                {
                    sb.Draw(ButtonPic, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), Rect, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                }
                else
                {
                    sb.Draw(ButtonPic, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), Rect, Color.Gray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.1f);
                }
                if (ButtonPicSliderBorder != null)
                {
                    if (SliderValues == null) // for static images like when you click a shop icon
                    {
                        if (ConnectedRelic != -1 && ButtonPicSliderInside1 == null) // relics
                        {
                            sb.Draw(ButtonPicSliderBorder, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors1[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                        }
                        else
                        {
                            sb.Draw(ButtonPicSliderBorder, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                        }
                        if (ButtonPicSliderInside1 != null) // for double layered buttons
                        {
                            if (ConnectedRelic != -1) // relics
                            {
                                sb.Draw(ButtonPicSliderInside1, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors1[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                            else
                            {
                                sb.Draw(ButtonPicSliderInside1, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                        }
                        if (ButtonPicSliderInside2 != null) // for triple layered relics
                        {
                            if (ConnectedRelic != -1)// relics
                            {
                                sb.Draw(ButtonPicSliderInside2, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors2[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                            else
                            {
                                sb.Draw(ButtonPicSliderInside2, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(StaticSlider * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                        }
                    }
                    else
                    {
                        if (ConnectedRelic != -1 && ButtonPicSliderInside1 == null) // relics
                        {
                            sb.Draw(ButtonPicSliderBorder, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors1[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                        }
                        else
                        {
                            sb.Draw(ButtonPicSliderBorder, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.2f);
                        }
                        if (ButtonPicSliderInside1 != null) // for double layered buttons
                        {
                            if (ConnectedRelic != -1) // relics
                            {
                                sb.Draw(ButtonPicSliderInside1, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors1[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                            else
                            {
                                sb.Draw(ButtonPicSliderInside1, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                        }
                        if (ButtonPicSliderInside2 != null) // for triple layered relics
                        {
                            if (ConnectedRelic != -1)// relics
                            {
                                sb.Draw(ButtonPicSliderInside2, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), SceneMan.RelicsColors2[SceneMan.Players[SceneMan.CurrentPlayerShop].CurrentRelics[ConnectedRelic]], 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                            else
                            {
                                sb.Draw(ButtonPicSliderInside2, new Rectangle((int)Pos.X, (int)Pos.Y, Rect.Width, Rect.Height), new Rectangle(SliderValues[SliderIndex] * (ChoppedWidth - 1), 0, ChoppedWidth, ButtonPicSliderBorder.Height), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.19f);
                            }
                        }
                    }
                }
            }
            else // render text button
            {
                if (SceneMan.Buttons[SceneMan.menupos] == this)
                {
                    sb.DrawString(SceneMan.Pico8, ButtonText, new Vector2(Pos.X, Pos.Y + 1), TextHoverColor * 0.5f, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.11f);
                    sb.DrawString(SceneMan.Pico8, ButtonText, Pos, TextHoverColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                }
                else
                {
                    sb.DrawString(SceneMan.Pico8, ButtonText, new Vector2(Pos.X, Pos.Y + 1), TextColor * 0.5f, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.11f);
                    sb.DrawString(SceneMan.Pico8, ButtonText, Pos, TextColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);
                }
            }
        }
        public void OnPressed() => action(SceneMan, this);
    }
}
