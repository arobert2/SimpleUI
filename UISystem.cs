using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace SimpleUI
{
    public class UISystem
    {
        private MouseState OldState { get; set; }
        private IUIElement ClickFocus { get; set; }
        private GraphicsDevice Graphics { get; set; }
        private TimeSpan LastClick { get; set; }
        private MouseState CurrentState { get; set; }
        private bool TitleBarClickBounds { get; set; }

        /// <summary>
        /// Generate a new graphics system.
        /// </summary>
        /// <param name="graphics">GraphicDevice game will use</param>
        /// <param name="font">Default Font information</param>
        public UISystem(GraphicsDevice graphics, UIFont font)
        {
            Graphics = graphics;
            WindowElements = new List<UIWindow>();
            Toolbars = new List<UIToolbar>();
            TextureGen gen = new TextureGen(Graphics);

            SetWindowDefaults(gen.GenerateSolidColor(Color.DarkBlue), gen.GenerateSolidColor(Color.Gray), gen.GenerateSolidColor(Color.LightGray), font, 16);
            SetButtonDefaults(gen.GenerateSolidColor(Color.Gray), gen.GenerateSolidColor(Color.DarkGray), font);
            SetToolbarDefaults(gen.GenerateSolidColor(Color.Gray), font);
            SetTextBoxDefaults(font);
        }
        /// <summary>
        /// Create a new window with default settings
        /// </summary>
        /// <param name="t">Title text</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <returns>UIWindow</returns>
        public UIWindow NewWindow(string t, int w, int h, int x, int y)
        {
            UIWindow win = new UIWindow(t, WindowTitleFont, DefaultTitleBarTexture, DefaultNonSelectedWindow, DefaultTitleBarWidth, DefaultBorderTexture, x, y, w, h);
            UIButton CloseButton = NewButton("X", DefaultTitleBarWidth, DefaultTitleBarWidth,w - DefaultTitleBarWidth, 0);
            CloseButton.LeftClickRelease += CloseWindow;
            win.Buttons.Add(CloseButton);
            return win;
        }
        /// <summary>
        /// Create a new button
        /// </summary>
        /// <param name="t">Button text</param>
        /// <param name="w">Width</param>
        /// <param name="h">height</param>
        /// <param name="x">X in relation to container</param>
        /// <param name="y">Y in relation to container</param>
        /// <returns>UIButton</returns>
        public UIButton NewButton(string t, int w, int h, int x, int y)
        {
            UIButton button = new UIButton(t, DefaultButtonFont, DefaultButtonTexture, DefaultButtonClickTexture, x, y, w, h);
            return button;
        }
        /// <summary>
        /// Create a new toolbar.
        /// </summary>
        /// <param name="t">Title of toolbar</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        public UIToolbar NewToolbar(string t,int w, int h, int x, int y)
        {
            UIToolbar toolbar = new UIToolbar(t, x, y, DefaultToolbarPanel, DefaultToolbarFont, w, h);
            return toolbar;
        }
        /// <summary>
        /// Create a new textbox
        /// </summary>
        /// <param name="t">text in box</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="x">x in relation to draw plane.</param>
        /// <param name="y">y in relation to draw plane.</param>
        /// <returns></returns>
        public UITextBox NewTextbox(string t, int w, int h, int x, int y)
        {
            UITextBox text = new UITextBox(t, x, y, DefaultTextBoxFont, w, h);
            return text;
        }

        #region standard click events.
        private void CloseWindow(Object EventArgs)
        {
            foreach (UIWindow win in WindowElements.ToList<UIWindow>())
                if (win.Buttons.Contains(ClickFocus))
                    WindowElements.Remove(win);
            if(WindowElements.Count > 0)
                WindowElements[0].TopWindow = true;
        }

        private void WindowLeftClick(object EventArgs)
        {
            MoveWindowToTop((UIWindow)ClickFocus);
        }

        private void WindowLeftClickHold(object EventArgs)
        {
            if(WindowTitleBarBounds(CurrentState.Position.X, CurrentState.Position.Y))
                MoveUIElement(CurrentState.Position.X, CurrentState.Position.Y);
        }
        #endregion

        #region UIWindow Tools and Properties.
        /// <summary>
        /// Default Title Bar Color
        /// </summary>
        public Texture2D DefaultTitleBarTexture { get; set; }
        /// <summary>
        /// Color of window title bar that is not selected.
        /// </summary>
        public Texture2D DefaultNonSelectedWindow { get; set; }
        /// <summary>
        /// Default Border Color
        /// </summary>
        public Texture2D DefaultBorderTexture { get; set; }
        /// <summary>
        /// Default window title font. UIFont type.
        /// </summary>
        public UIFont WindowTitleFont { get; set; }
        /// <summary>
        /// Bottom window in window stack.
        /// </summary>
        public List<UIWindow> WindowElements { get; set; }
        /// <summary>
        /// Default titlebar width
        /// </summary>
        public int DefaultTitleBarWidth { get; set; }

        /// <summary>
        /// Add a new window to the top of the window queue.
        /// </summary>
        /// <param name="window"></param>
        public void AddWindow (UIWindow window)
        {
            WindowElements.Add(window);
            MoveWindowToTop(window);
        }
        /// <summary>
        /// Move a window to the top.
        /// </summary>
        /// <param name="window"></param>
        public bool MoveWindowToTop(UIWindow window)
        {
            if (window == WindowElements[0])
                return true;

            WindowElements.RemoveAt(WindowElements.IndexOf(window));
            List<UIWindow> holder = WindowElements;
            WindowElements = new List<UIWindow>();
            WindowElements.Add(window);
            WindowElements[0].TopWindow = true;
            WindowElements.AddRange(holder);
            WindowElements[1].TopWindow = false;
            return false;
        }
        /// <summary>
        /// Set defaults for windows.
        /// </summary>
        /// <param name="tbt">Title bar texture</param>
        /// <param name="bt">Border texture</param>
        /// <param name="tbw">Title bar width</param>
        /// <param name="bm">Border margin</param>
        public void SetWindowDefaults(Texture2D tbt, Texture2D ntbt, Texture2D bt, UIFont wtf, int tbw)
        {
            DefaultTitleBarTexture = tbt;
            DefaultBorderTexture = bt;
            DefaultTitleBarWidth = tbw;
            WindowTitleFont = wtf;
            DefaultNonSelectedWindow = ntbt;
        }
        /// <summary>
        /// Set defaults for windows.
        /// </summary>
        /// <param name="tbt">Title bar color/param>
        /// <param name="ntbt">Unselected title bar.</param>
        /// <param name="bt">Border color</param>
        /// <param name="tbw">Title bar width</param>
        /// <param name="bm">Border margin</param>
        public void SetWindowDefaults(Color tbt, Color ntbt, Color bt, UIFont wtf, int tbw)
        {
            TextureGen tex = new TextureGen(Graphics);
            DefaultTitleBarTexture = tex.GenerateSolidColor(tbt);
            DefaultBorderTexture = tex.GenerateSolidColor(bt);
            DefaultTitleBarWidth = tbw;
            WindowTitleFont = wtf;
            DefaultNonSelectedWindow = tex.GenerateSolidColor(ntbt);
        }
        #endregion

        #region UIButton Tools and Properties
        /// <summary>
        /// Default button font.
        /// </summary>
        public UIFont DefaultButtonFont { get; set; }
        /// <summary>
        /// Button texture to display
        /// </summary>
        public Texture2D DefaultButtonTexture { get; set; }
        /// <summary>
        /// button texture to display when clicked
        /// </summary>
        public Texture2D DefaultButtonClickTexture { get; set; }
        /// <summary>
        /// Create a new button
        /// </summary>
        /// <param name="t">text</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="AddToTopWindow">Add to the top UIWindow in list.</param>
        /// <returns></returns>
        public UIButton NewButton(string t, int w, int h, int x, int y, bool AddToTopWindow)
        {
            if(DefaultButtonFont == null || DefaultButtonTexture == null || DefaultButtonClickTexture == null)
            {
                System.Diagnostics.Debug.WriteLine("Button defaults not set. Call UISystem.SetButtonDefaults()");
            }

            UIButton button = new UIButton(t, DefaultButtonFont, DefaultButtonTexture, DefaultButtonClickTexture, x, y, w, h);

            if (AddToTopWindow)
                AddButton(button);
            return button;
        }
        /// <summary>
        /// Add UIButton to top UIWindow
        /// </summary>
        /// <param name="b"></param>
        public void AddButton(UIButton b)
        {
            //WindowElements[0].Buttons.Add(b);
        }
        /// <summary>
        /// Set default button data
        /// </summary>
        /// <param name="dbt">Default Button Texture</param>
        /// <param name="dbct">Default Button Clicked Texture</param>
        /// <param name="dbf">Default Button Font</param>
        public void SetButtonDefaults(Texture2D dbt, Texture2D dbct, UIFont dbf)
        {
            DefaultButtonTexture = dbt;
            DefaultButtonClickTexture = dbct;
            DefaultButtonFont = dbf;
        }
        #endregion

        #region UIToolbar tools and properties
        /// <summary>
        /// Default Toolbar texture
        /// </summary>
        public Texture2D DefaultToolbarPanel { get; set; }
        /// <summary>
        /// default toolbar font
        /// </summary>
        public UIFont DefaultToolbarFont { get; set; }
        /// <summary>
        /// Toolbars to draw.
        /// </summary>
        public List<UIToolbar> Toolbars { get; set; }
        /// <summary>
        /// Set Toolbar defaults
        /// </summary>
        /// <param name="dtp">Default Toolbar Texture</param>
        /// <param name="dtf">Default toolbar font</param>
        public void SetToolbarDefaults (Texture2D dtp, UIFont dtf)
        {
            DefaultToolbarFont = dtf;
            DefaultToolbarPanel = dtp;
        }
        /// <summary>
        /// Add a toolbar to the UI
        /// </summary>
        /// <param name="t">UIToolbar object to add</param>
        public void AddToolbar (UIToolbar t)
        {
            Toolbars.Add(t);
        }
        #endregion

        #region UITextBox tools and properties
        public UIFont DefaultTextBoxFont { get; set; }
        /// <summary>
        /// set textbox defaults
        /// </summary>
        /// <param name="Font">default toolbar font</param>
        public void SetTextBoxDefaults(UIFont Font)
        {
            DefaultTextBoxFont = Font;
        }
        #endregion

        /// <summary>
        /// Draw UI
        /// </summary>
        /// <param name="spritebatch">SpriteBatch you will be drawing to.</param>
        public void DrawUI(SpriteBatch spritebatch)
        {
            for (int i = WindowElements.Count - 1; i >= 0; i--)
                WindowElements[i].Draw(spritebatch);
            for (int i = 0; i < Toolbars.Count; i++)
                Toolbars[i].Draw(spritebatch);
        }
        /// <summary>
        /// Check to see which part of the UI is clicked.
        /// </summary>
        /// <param name="x">mouse x</param>
        /// <param name="y">mouse y</param>
        /// <returns></returns>
        private IUIElement ClickedObject(int x, int y)
        {

            for (int i = 0; i < Toolbars.Count; i++)
                if (x > Toolbars[i].X && x < Toolbars[i].X + Toolbars[i].Width && y > Toolbars[i].Y && y < Toolbars[i].Y + Toolbars[i].Height)
                {
                    IUIElement iuie = Toolbars[0].GetButton(x, y);
                    return iuie;
                }
            for (int i = 0; i < WindowElements.Count; i++)
                if (x > WindowElements[i].X && x < WindowElements[i].X + WindowElements[i].Width && y > WindowElements[i].Y && y < WindowElements[i].Y + WindowElements[i].Height)
                {
                    MoveWindowToTop(WindowElements[i]);
                    IUIElement iuie = WindowElements[0].GetButton(x, y);
                    if (iuie != null)
                        return iuie;
                    return WindowElements[0];
                }    
            
            System.Diagnostics.Debug.WriteLine("WARNING! ClickedObject return null.");
            return null;
        }
        /// <summary>
        /// Click on UI
        /// </summary>
        /// <param name="mouse">Mouse state</param>
        /// <returns>Whether an object was found.</returns>
        public bool ClickUI(MouseState mouse, GameTime gametime)
        {
            CurrentState = mouse;
            //on click
            if (mouse.LeftButton == ButtonState.Pressed && OldState.LeftButton == ButtonState.Released)
            {
                System.Diagnostics.Debug.WriteLine("Click");
                LastClick = gametime.ElapsedGameTime;

                //if nothing if focused       
                ClickFocus = ClickedObject(mouse.Position.X, mouse.Position.Y);
                if (ClickFocus == null)
                {
                    OldState = mouse;
                    return false;
                }
                //if focused is UIWindow
                if (ClickFocus.GetType() == typeof(SimpleUI.UIWindow))
                    MoveWindowToTop((UIWindow)ClickFocus);
                //if focused is UIButton
                if (ClickFocus.GetType() == typeof(SimpleUI.UIButton))
                    ((UIButton)ClickFocus).Clicked = true;
                //add for each IUIElement.*/

                OldState = mouse;
                return true;
            }
            //on hold
            if (mouse.LeftButton == ButtonState.Pressed && OldState.LeftButton == ButtonState.Pressed)
            {
                System.Diagnostics.Debug.WriteLine("Hold");
                //if nothing is focused
                if (ClickFocus == null)
                {
                    OldState = mouse;
                    return false;
                }
                //if focused is UIWindow
                if (ClickFocus.GetType() == typeof(UIWindow) && WindowTitleBarBounds(OldState.Position.X, OldState.Position.Y))
                    MoveUIElement(mouse.Position.X, mouse.Position.Y);
                //add for each UIUElement
                
                OldState = mouse;
                return true;
            }
            //on release
            if (mouse.LeftButton == ButtonState.Released && OldState.LeftButton == ButtonState.Pressed)
            {
                System.Diagnostics.Debug.WriteLine("Release");
                //if nothing is focused.
                if (ClickFocus == null)
                {
                    OldState = mouse;
                    return false;
                }

                //if focused is UIButton
                if (ClickFocus.GetType() == typeof(UIButton))
                {
                    ((UIButton)ClickFocus).Clicked = false;
                    if (ClickFocus == ClickedObject(mouse.Position.X, mouse.Position.Y))
                    {
                        ((UIButton)ClickFocus).LeftClickRelease?.Invoke(ClickFocus);
                    }
                }
                ClickFocus = null;
                OldState = mouse;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Move a IUIElement. Mostly for UIWindows.
        /// </summary>
        /// <param name="x">X mouse location</param>
        /// <param name="y">Y mouse location</param>
        public void MoveUIElement (int x, int y)
        {
            ClickFocus.X = ClickFocus.X + (x - OldState.Position.X);
            ClickFocus.Y = ClickFocus.Y + (y - OldState.Position.Y);
        }
        /// <summary>
        /// Window Title Boarder check
        /// </summary>
        /// <param name="x">X click</param>
        /// <param name="y">Y click</param>
        /// <returns></returns>
        private bool WindowTitleBarBounds(int x, int y)
        {
            if (x > ClickFocus.X && x < ClickFocus.X + ClickFocus.Width && y > ClickFocus.Y && y < ClickFocus.Y + ((UIWindow)ClickFocus).TitleBarWidth)
                return true;
            return false;
        }
    }


    //public enum UIElementType { UIWindow, UIButton, UIUtilityBar, UIContentPanel }

    public delegate void UIClickEvent(object EventArgs);

    /// <summary>
    /// IUIElement interface. All UI elements inherit this interface.
    /// </summary>
    public interface IUIElement
    {
        /// <summary>
        /// Text to display
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// Width of UIEleemnt
        /// </summary>
        int Width { get; set; }
        /// <summary>
        /// Height of UI Element
        /// </summary>
        int Height { get; set; }
        /// <summary>
        /// X Location of UI element.
        /// </summary>
        int X { get; set; }
        /// <summary>
        /// X Location of UI Element.
        /// </summary>
        int Y { get; set; }
        /// <summary>
        /// Left click event
        /// </summary>
        UIClickEvent LeftClick { get; set; }
        /// <summary>
        /// Left double click event
        /// </summary>
        UIClickEvent LeftDoubleClick { get; set; }
        /// <summary>
        /// Holding left click
        /// </summary>
        UIClickEvent LeftHoldClick { get; set; }
        /// <summary>
        /// Release Left Click
        /// </summary>
        UIClickEvent LeftClickRelease { get; set; }
        /// <summary>
        /// Richt Click
        /// </summary>
        UIClickEvent RightClick { get; set; }
        /// <summary>
        /// Right double click event
        /// </summary>
        UIClickEvent RightDoubleClick { get; set; }
        /// <summary>
        /// Holding Right click
        /// </summary>
        UIClickEvent RightHoldClick { get; set; }
        /// <summary>
        /// Release Right Click
        /// </summary>
        UIClickEvent RightClickRelease { get; set; }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="spritebatch">SpriteBatch you want to draw to.</param>
        void Draw(SpriteBatch spritebatch);
    }
    public enum FontAlignment { Left, Right, Center };

    /// <summary>
    /// UIFont information. Used for setting fonts in text.
    /// </summary>
    public class UIFont
    {
        public SpriteFont Font { get; set; }
        public Color FontColor { get; set; }
        public FontAlignment Align { get; set; }
        /// <summary>
        /// Create a UIFont that includes it's font, color, and alignment.
        /// </summary>
        /// <param name="f">font</param>
        /// <param name="c">color</param>
        /// <param name="a">alignment in window.</param>
        public UIFont(SpriteFont f, Color c, FontAlignment a)
        {
            Font = f;
            FontColor = c;
            Align = a;
        }
    }
    public class TextureGen
    {
        public GraphicsDevice Graphics { get; set; }
        /// <summary>
        /// Create a TextureGenerator
        /// </summary>
        /// <param name="graphics">GraphicsDevice</param>
        public TextureGen(GraphicsDevice graphics)
        {
            Graphics = graphics;
        }
        /// <summary>
        /// Generate solid color texture
        /// </summary>
        /// <param name="c">Color of texture</param>
        /// <returns>Texture2D</returns>
        public Texture2D GenerateSolidColor(Color c)
        {
            Texture2D texture = new Texture2D(Graphics, 1, 1);
            texture.SetData(new[] { c });
            return texture;
        }
    }
}

