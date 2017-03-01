using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleUI
{
    /// <summary>
    /// UIWindow to draw.
    /// </summary>
    public class UIWindow : IUIElement
    {
        /// <summary>
        /// Text to display.
        /// </summary>
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
        public Texture2D TitleBarTexture { get; set; }
        /// <summary>
        /// Color of window title bar that is not selected.
        /// </summary>
        public Texture2D NonSelectedTitleBarTexture { get; set; }
        /// <summary>
        /// Border Color
        /// </summary>
        public Texture2D BorderTexture { get; set; }
        /// <summary>
        /// Title Bar Width
        /// </summary>
        public int TitleBarWidth { get; set; }
        /// <summary>
        /// Font your title will display at.
        /// </summary>
        public UIFont TitleFont { get; set; }
        /// <summary>
        /// Window Buttons. Buttons pretty much only in TitleBar.
        /// </summary>
        public List<UIButton> Buttons { get; set; }
        /// <summary>
        /// Width of window
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of window
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Is this window on top.
        /// </summary>
        public bool TopWindow { get; set; }
        /// <summary>
        /// Textbox on images.
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

        public int TopTitleMargin { get; set; }
        public int RightTitleMargin { get; set; }
        public int LeftTitleMargin { get; set; }

        public UIWindow()
        {
            TitleBarWidth = 16;
            TopTitleMargin = 3;
            LeftTitleMargin = 3;
            RightTitleMargin = 3;
            Buttons = new List<UIButton>();
            TextBoxes = new List<UITextBox>();
        }
        /// <summary>
        /// Create a new window
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="titleBartexture">Color of title bar</param>
        /// <param name="titlebarwidth">Width of titlebar</param>
        /// <param name="bordercolor">Color of border</param>
        /// <param name="bordermargin">Width of border</param>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="font">font</param>
        /// <param name="h">Window height</param>
        /// <param name="w">Window Width</param>
        /// <param name="bordertexture">Border texture</param>
        public UIWindow(string title, UIFont font, Texture2D titleBartexture,Texture2D nonselectedtitlebar, int titlebarwidth, Texture2D bordertexture, int x, int y, int w, int h )
        {
            Text = title;
            TitleBarTexture = titleBartexture;
            NonSelectedTitleBarTexture = nonselectedtitlebar;
            BorderTexture = bordertexture;
            TitleBarWidth = titlebarwidth;
            X = x;
            Y = y;
            TitleFont = font;
            Width = w;
            Height = h;
            Buttons = new List<UIButton>();
            TextBoxes = new List<UITextBox>();
        }
        /// <summary>
        /// Draws the window
        /// </summary>
        /// <param name="spritebatch">spritebatch you will draw to.</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(BorderTexture, new Rectangle(X, Y, Width, Height), Color.White);
            //Call content panel draw Here

            if (TopWindow)
                spritebatch.Draw(TitleBarTexture, new Rectangle(X, Y, Width, TitleBarWidth), Color.White);
            else
                spritebatch.Draw(NonSelectedTitleBarTexture, new Rectangle(X, Y, Width, TitleBarWidth), Color.White);

            spritebatch.DrawString(TitleFont.Font, Text, new Vector2(X + LeftTitleMargin, Y + TopTitleMargin), TitleFont.FontColor);

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

                if(x > lowerBounds.X && x < upperBounds.X && y > lowerBounds.Y && y < upperBounds.Y)
                {
                    System.Diagnostics.Debug.WriteLine(b);
                    return b;
                }
            }
            return null;
        }
    }
}
