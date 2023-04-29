using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Rendering.Sprites;
using QuadTree;
using System;
using System.Collections.Generic;


namespace MonoGameTest.UI
{
    public class UIElement:IHasRect
    {
        public event Action<SpriteBatch> OnDraw;
        public event Action OnPositionChanged;
        public event Action OnDestroyed;

        public SlicedSprite background;

        public Color col;

        public List<UIElement> children = new List<UIElement>();
        public bool IsVisible 
        { 
            get { return Parent == null ? _isVisible : Parent._isVisible && _isVisible; } 
        }
        private bool _isVisible = true;

        public Rectangle AbsoluteRect
        {
            get
            {
                if (Parent == null) return LocalRect;
                else return new Rectangle(LocalRect.Location + Parent.AbsoluteRect.Location, LocalRect.Size);
            }
        }

        public Rectangle GetRect => AbsoluteRect;
        public Rectangle LocalRect 
        {
            get
            {
                return internal_LocalRect;
            }
            set
            {
                internal_LocalRect = value;
                OnPositionChanged.Invoke();
            }
        }
        private Rectangle internal_LocalRect;
        public UIElement Parent { get; }

        public UIElement(Rectangle rect, SlicedSprite background, UIElement parent)
        {
            this.internal_LocalRect = rect;
            this.background = background;
            this.Parent = parent;
            this.Parent?.children.Add(this);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            if (background != null)
                background.Draw(spritebatch, AbsoluteRect);

            OnDraw?.Invoke(spritebatch);

            foreach (UIElement child in children)
            {
                child.Draw(spritebatch);
            }
            
        }

        public void DestroyPanel()
        {
            foreach (UIElement child in children)
                child.DestroyPanel();
            OnDestroyed.Invoke();
        }

        public void ToggleVisibility(VisibilityToggleMode mode = VisibilityToggleMode.Toggle)
        {
            switch (mode)
            {
                case VisibilityToggleMode.Toggle:
                    _isVisible = !_isVisible;
                    break;
                case VisibilityToggleMode.ForceTrue:
                    _isVisible = true;
                    break;
                case VisibilityToggleMode.ForceFalse:
                    _isVisible = false;
                    break;
            }
        }
        public enum VisibilityToggleMode { Toggle = 0, ForceTrue = 1, ForceFalse = 2 }
    }
}
