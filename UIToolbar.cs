using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SimpleUI
{
    public class UIToolbar : IUIElement
    {
        public string Text { get; set; }
        /// <summary>
        /// X Location in relation to the MonoGame GameWindow.
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
        /// Buttons to display.
        /// </summary>
        public List<UIButton> Buttons { get; set; }
        /// <summary>
        /// Textbox to display
        /// </summary>
        public List<UITextBox> TextBoxes { get; set; }
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

        public UIToolbar (string text, int x, int y, Texture2D pt, UIFont tf, int w, int h)
        {
            Buttons = new List<UIButton>();
            TextBoxes = new List<UITextBox>();
            Text = text;
            X = x;
            Y = y;
            PanelTexture = pt;
            TextFont = tf;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Draw the IUIElement
        /// </summary>
        /// <param name="spritebatch">spritebatch you will call darw from.</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(PanelTexture, new Rectangle(X, Y, Width, Height), Color.White);

            int oldx, oldy;

            if (Buttons != null)
                foreach (UIButton b in Buttons)
                {
                    oldx = b.X;
                    oldy = b.Y;

                    b.X = X + b.X;
                    b.Y = Y + b.Y;

                    b.Draw(spritebatch);

                    b.X = oldx;
                    b.Y = oldy;
                }

            if (TextBoxes != null)
                foreach (UITextBox tb in TextBoxes)
                {
                    oldx = tb.X;
                    oldy = tb.Y;

                    tb.X = X + tb.X;
                    tb.Y = Y + tb.Y;

                    tb.Draw(spritebatch);

                    tb.X = oldx;
                    tb.Y = oldy;
                }
        }
        /// <summary>
        /// Check if a button is at X,Y
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        public UIButton GetButton(int x, int y)
        {
            foreach (UIButton b in Buttons)
            {
                Point lowerBounds = new Point(b.X + X, b.Y + Y);
                Point upperBounds = new Point(b.X + X + b.Width, b.Y + Y + b.Height);

                if (x > lowerBounds.X && x < upperBounds.X && y > lowerBounds.Y && y < upperBounds.Y)
                {
                    System.Diagnostics.Debug.WriteLine(b);
                    return b;
                }
            }
            return null;
        }
    }
}
