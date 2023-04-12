using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShatteredSkies.Classes
{
    public class Helper
    {
        public static int GetListSum(string str, Dictionary<string, int> dict, int counter)
        {
            int sum = 0;
            List<int> Convert = new List<int>();

            for (int i = 0;i < dict.Count; i++)
            {
                if (dict.ElementAt(i).Key.Contains(str))
                {
                    Convert.Add(dict.ElementAt(i).Value);
                };
            }

            if (counter !> Convert.Count) { return 0; }
            for (int i = 0; i < counter; i++)
            {
                sum += Convert[i];
            }
            return sum;
        }
        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public static float SumAllPlayerHealth(SceneManager SceneMan)
        {
            float TotalHealth = 0;
            foreach(Player play in SceneMan.Players)
            {
                TotalHealth += play.Health;
            }
            return TotalHealth;
        }

        public static bool BoxCollision(int x, int y, int w, int h, int x2, int y2, int w2, int h2)
        {
            int xw = x + w;
            int yh = y + h;

            int xw2 = x2 + w2;
            int yh2 = y2 + h2;
            bool collision = false;

            //bottem right corner
            if (x2 >= x & x2 <= xw & y2 >= y & y2 <= yh) { collision = true; };
            //bottem right corner
            if (xw2 >= x & xw2 <= xw & y2 >= y & y2 <= yh) { collision = true; };

            //top right corner
            if (x2 >= x & x2 <= xw & yh2 >= y & yh2 <= yh) { collision = true; };

            //top left corner
            if (xw2 >= x & xw2 <= xw & yh2 >= y & yh2 <= yh) { collision = true; };

            //Checks agian for size differences
            //bottem right corner
            if (x >= x2 & x <= xw2 & y >= y2 & y <= yh2) { collision = true; };
            //bottem right corner
            if (xw >= x2 & xw <= xw2 & y >= y2 & y <= yh2) { collision = true; };

            //top right corner
            if (x >= x2 & x <= xw2 & yh >= y2 & yh <= yh2) { collision = true; };

            //top left corner
            if (xw >= x2 & xw <= xw2 & yh >= y2 & yh <= yh2) { collision = true; };
            return collision;
        }

        public static void WrapString(string str, Rectangle rect, Color col, SpriteBatch sb, SceneManager SceneMan)
        {
            string Word = "";
            int StartingPos = 0;
            List<string> Lines = new List<string>();
            //
            for (int i = 1; i <= str.Length; i++)
            {
                if (char.IsWhiteSpace(str[i - 1]))//detects words
                {
                    Word = "";
                    for (int i2 = 1; i2 <= str.Length - i; i2++)
                    {
                        if (char.IsWhiteSpace(str[i - 1 + i2]) == false)
                        {
                            Word += str[i - 1 + i2];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //special skip line boyo
                if (str[i - 1] == '@')
                {
                    Lines.Add(str[StartingPos..(i)]);
                    StartingPos = i;
                    Word = "";
                }
                if (char.IsWhiteSpace(str[i-1]))
                {
                    if (SceneMan.Pico8.MeasureString(str[StartingPos..i]+Word).X >= rect.Width)
                    {
                        Lines.Add(str[StartingPos..i]);
                        StartingPos = i;
                        Word = "";
                    }
                }
                if (i == str.Length)
                {
                    Lines.Add(str[StartingPos..i]);
                }
            }
            for (int i = 0; i < Lines.Count; i++)//renders all the new lines to the screen
            {
                Word = Lines[i].Replace("@","");
                sb.DrawString(SceneMan.Pico8, Word, new Vector2(rect.X+1, rect.Y+(i*7)),col,0f,new Vector2(0,0),1f,SpriteEffects.None,0f);
                sb.DrawString(SceneMan.Pico8, Word, new Vector2(rect.X+1, rect.Y + (i * 7)+1), new Color(col.R/2, col.G / 2, col.B / 2), 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.01f);
                
            }
            //sb.DrawString(SceneMan.Pico8, "LMAO", new Vector2(rect.X, rect.Y), col, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }

        public static string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (word.Contains("\r"))
                {
                    lineWidth = 0f;
                    sb.Append("\r \r");
                }

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }

                else
                {
                    if (size.X > maxLineWidth)
                    {
                        if (sb.ToString() == " ")
                        {
                            sb.Append(WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                        else
                        {
                            sb.Append("\n" + WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }
            }

            return sb.ToString();
        }

        public static int CheckForDuplicates(int[] array, int check)
        {
            int NumOfDuplicates = 0;
            foreach(int I in array)
            {
                if (I == check)
                {
                    NumOfDuplicates += 1;
                }
            }
            return NumOfDuplicates;
        }

        public static double GetAngleOfTwoPoints(Vector2 p1, Vector2 p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            return (Math.Atan2(yDiff, xDiff) * (180 / Math.PI));
        }

        public static double GetRadiansOfTwoPoints(Vector2 p1, Vector2 p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            return (Math.Atan2(yDiff, xDiff));
        }

        public static double GetDistance(Vector2 pos1, Vector2 pos2)
        {
            return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }

        public static Vector2 CenterPlayer(Player play)
        {
            return new Vector2(play.Pos.X + play.AllCores[play.CurrentShipParts[0]].Width / 2, play.Pos.Y + play.AllCores[play.CurrentShipParts[0]].Height / 2);
        }

        public static Vector2 CenterActor(Vector2 vect1, Vector2 vect2)
        {
            return new Vector2(vect1.X+vect2.X/2, vect1.Y + vect2.Y / 2);
        }

        public static bool CheckFlowersForEnemy(Enemy ene,SceneManager SceneMan)
        {
            foreach(dynamic dyn in SceneMan.Bullets)
            {
                if (dyn is LivingFlower)
                {
                    if (dyn.GrabbedEnemy == ene)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
