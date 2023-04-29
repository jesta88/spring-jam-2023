using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using QuadTree;
using MonoGameTest.UI;

namespace MonoGameTest
{
    public static class Input
    {
        public static readonly GamePadListener _gamePadListener = new GamePadListener();
        public static readonly KeyboardListener _keyboardListener = new KeyboardListener();
        public static readonly MouseListener _mouseListener = new MouseListener();

        public static QuadTree<UIButton> buttons;

        public static void Initialize(Game game)
        {
            game.Components.Add(new InputListenerComponent(game, _keyboardListener, _gamePadListener, _mouseListener));
            _keyboardListener.KeyPressed += OnKeyPressed;
            _mouseListener.MouseMoved += OnMouseMoved;
            _keyboardListener.KeyReleased += OnKeyReleased;
            _keyboardListener.KeyTyped += OnKeyTyped;
            _mouseListener.MouseClicked += OnMouseClicked;
            _mouseListener.MouseDown += OnMouseDown;
            _mouseListener.MouseUp += OnMouseUp;
            _mouseListener.MouseDrag += OnMouseDrag;
            _mouseListener.MouseWheelMoved += OnMouseWheelMoved;

            buttons = new QuadTree<UIButton>(game.GraphicsDevice.Viewport.Bounds);

        }

        private static void OnMouseWheelMoved(object sender, MouseEventArgs e)
        {

        }

        private static void OnMouseDrag(object sender, MouseEventArgs e)
        {

        }

        private static void OnMouseUp(object sender, MouseEventArgs e)
        {

        }

        private static void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left || e.Button == MouseButton.Right)
            {
                foreach (UIButton button in buttons.Query(e.Position))
                {
                    if (button == null || !button.IsVisible)
                        continue;

                    button.Click();

                    if (button.ConsumeInput)
                        break;
                }
            }
        }
        private static void OnMouseClicked(object sender, MouseEventArgs e)
        {

        }

        private static void OnKeyTyped(object sender, KeyboardEventArgs e)
        {
            
        }

        private static void OnKeyReleased(object sender, KeyboardEventArgs e)
        {

        }

        private static void OnMouseMoved(object sender, MouseEventArgs e)
        {
            foreach (UIButton button in buttons.Query(e.Position))
            {
                button.Hover();
                break;
            }
        }


        private static void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
        }
    }
}
