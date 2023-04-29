using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Rendering.Sprites;
using QuadTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.UI
{
    public class UIButton : UIElement
    {
        public UIElement control;

        public UIButton(Rectangle position, SlicedSprite background, Color? col, UIElement parent, bool consumeInput = true) : base(position, background, col, parent)
        {
            OnDestroyed += Unregister;
            OnPositionChanged += Refresh;
            this.ConsumeInput = consumeInput;
            Register();
        }

        public event Action OnClick;

        public event Action OnHover;

        public bool IsActive => control.IsVisible;

        public bool ConsumeInput { get; set; }

        public void Register()
        {
            Input.buttons.Insert(this);
        }

        public void Unregister()
        {
            Input.buttons.Remove(this);
        }

        public void Refresh()
        {
            Unregister();
            Register();
        }

        public void Click()
        {
            OnClick.Invoke();
        }

        internal void Hover()
        {
            OnHover.Invoke();
        }
    }
}
