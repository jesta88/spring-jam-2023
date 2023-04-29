using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Rendering.Sprites;
using MonoGameTest.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.UI
{
    public class UILabel : UIElement
    {
        string content;
        SpriteFont font;
        public UILabel(String content, SpriteFont font, Rectangle rect, Color? col, UIElement parent) : base(rect, null, col, parent)
        {
            this.font = font;
            this.content = FormatString(content);
        }

        public void UpdateText(string newText)
        {
            content = FormatString(newText);
        }

        private string FormatString(string inputString)
        {
            float totalTextSize = font.MeasureString(inputString).X;
            if (totalTextSize > AbsoluteRect.Width)
            {
                int maxCharPerLine = (int)(AbsoluteRect.Width / totalTextSize * inputString.Length);

                for (int i = maxCharPerLine; i < inputString.Length - maxCharPerLine; i += maxCharPerLine)
                {
                    while (inputString[i] != ' ' && i != 0)
                    {
                        if (inputString[i] == '\n')
                            break;
                        i--;
                    }

                    if (i == 0)
                        break; //We couldn't find a place to split the text.

                    inputString = inputString.Insert(++i, "\n");
                }
            }

            return inputString;
        }

        public Vector2 GetLabelSize()
        {
            return font.MeasureString(content);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);

            

            spritebatch.DrawString(font, content, AbsoluteRect.Location.ToVector2(), col.HasValue?col.Value:Color.Black);
        }
    }
}
