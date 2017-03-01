using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleUI
{
    public class UIButton : IUIElement
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
        public Texture2D ButtonTexture { get; set; }
        /// <summary>
        /// Color of window title bar that is not selected.
        /// </summary>
        public Texture2D ButtonSelectedTexture { get; set; }
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
        /// Is it currently clicked.
        /// </summary>
        public bool Clicked { get; set; }
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

        /// <summary>
        /// A Button you can click.
        /// </summary>
        /// <param name="text">Text for the button</param>
        /// <param name="font">Font for the text</param>
        /// <param name="bt">Button texture to be displayed.</param>
        /// <param name="bst">button texture when clicked</param>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        public UIButton(string text, UIFont font, Texture2D bt, Texture2D bst, int x, int y, int w, int h)
        {
            Text = text;
            ButtonTexture = bt;
            ButtonSelectedTexture = bst;
            X = x;
            Y = y;
            TextFont = font;
            Width = w;
            Height = h;
            Clicked = false;
        }
        /// <summary>
        /// A Button you can click.
        /// </summary>
        /// <param name="font">Font for the text</param>
        /// <param name="bt">Button texture to be displayed.</param>
        /// <param name="bst">button texture when clicked</param>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        public UIButton(UIFont font, Texture2D bt, Texture2D bst, int x, int y, int w, int h)
        {
            ButtonTexture = bt;
            ButtonSelectedTexture = bst;
            X = x;
            Y = y;
            TextFont = font;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Draws the Button
        /// </summary>
        /// <param name="spritebatch">spritebatch you will draw to.</param>
        public void Draw(SpriteBatch spritebatch)
        {
            if (Clicked)
                spritebatch.Draw(ButtonTexture, new Rectangle(X, Y, Width, Height), Color.White);
            else
                spritebatch.Draw(ButtonSelectedTexture, new Rectangle(X, Y, Width, Height), Color.White);

            Vector2 size = TextFont.Font.MeasureString(Text);
            Vector2 pos = new Vector2(X + Width / 2, Y + Height / 2);
            Vector2 origin = size / 2;

            if(Text != null)
                spritebatch.DrawString(TextFont.Font, Text, pos, TextFont.FontColor, 0, origin, 1, SpriteEffects.None, 1);
        }
    }
}
