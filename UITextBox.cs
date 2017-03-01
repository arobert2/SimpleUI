using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace SimpleUI
{
    public class UITextBox : IUIElement
    {
        /// <summary>
        /// Text to dipslay in textbox.
        /// </summary>
        public string Text
        {
            get
            {
                string aString = "";
                foreach (string s in _text)
                    aString += _text;
                return aString;
            }
            set
            {
                string t = value;
                string[] sa = t.Split(' ');
                string aString = "";
                foreach (string s in sa)
                {
                    System.Diagnostics.Debug.WriteLine(s);
                    if (TextFont.Font.MeasureString(aString + s + " ").X < Width)
                        aString += s + " ";
                    if (aString.Equals("") && TextFont.Font.MeasureString(aString + s + " ").X > Width)
                        System.Diagnostics.Debug.WriteLine("TEXTBOX ERR: Some words exceede max allowed width.");
                    if (!aString.Equals("") && TextFont.Font.MeasureString(aString + s + " ").X > Width)
                    {
                        System.Diagnostics.Debug.WriteLine(aString);
                        _text.Add(aString);
                        aString = "";
                    }
                }
            }
        }
        /// <summary>
        /// X Location in relation to the object you are drawing in..
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y Location in relation to the MonoGame GameWindow.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Color of title bar.
        /// </summary>
        public Texture2D PanelTexture { get; set; }
        /// <summary>
        /// Font your title will display at.
        /// </summary>
        public UIFont TextFont { get; set; }
        /// <summary>
        /// Width of window
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of window
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Click event for this button.
        /// </summary>
        public UIClickEvent LeftClick { get; set; }
        /// <summary>
        /// Left double click event
        /// </summary>
        public UIClickEvent LeftDoubleClick { get; set; }
        /// <summary>
        /// Holding left click
        /// </summary>
        public UIClickEvent LeftHoldClick { get; set; }
        /// <summary>
        /// Release Left Click
        /// </summary>
        public UIClickEvent LeftClickRelease { get; set; }
        /// <summary>
        /// Richt Click
        /// </summary>
        public UIClickEvent RightClick { get; set; }
        /// <summary>
        /// Right double click event
        /// </summary>
        public UIClickEvent RightDoubleClick { get; set; }
        /// <summary>
        /// Holding Right click
        /// </summary>
        public UIClickEvent RightHoldClick { get; set; }
        /// <summary>
        /// Release Right Click
        /// </summary>
        public UIClickEvent RightClickRelease { get; set; }

        private List<string> _text = new List<string>();

        public UITextBox(string text, int x, int y, UIFont tf, int w, int h)
        {
            TextFont = tf;          
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Text = text;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            int line = 0;
            foreach (string s in _text)
            {
                spritebatch.DrawString(TextFont.Font, s, new Vector2(X, Y + line), TextFont.FontColor);
                line += (int)TextFont.Font.MeasureString(s).Y;
                if (line > Height)
                    break;                
            }
        }
    }
}
