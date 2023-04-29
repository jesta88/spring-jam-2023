using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace MonoGameTest.Rendering.Sprites
{
    public class SlicedSprite
    {
        
        Rectangle[] sourceRectangles;
        int tileWidth;
        int tileHeight;
        Texture2D texture;

        int[] indices = new int[]
            {
                0,2,1,1,2,3,
                2,4,3,3,4,5,
                4,6,5,5,6,7,
                1,3,8,8,3,10,
                3,5,10,10,5,12,
                5,7,12,12,7,14,
                8,10,9,9,10,11,
                10,12,11,11,12,13,
                12,14,13,13,14,15
            };

        public SlicedSprite(Texture2D texture, int tileWidth, int tileHeight)
        {
            this.texture = texture;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            sourceRectangles = new Rectangle[]
            {
                new Rectangle(tileWidth*0,tileHeight*0,tileWidth, tileHeight),
                new Rectangle(tileWidth*1,tileHeight*0,tileWidth, tileHeight),
                new Rectangle(tileWidth*2,tileHeight*0,tileWidth, tileHeight),
                new Rectangle(tileWidth*0,tileHeight*1,tileWidth, tileHeight),
                new Rectangle(tileWidth*1,tileHeight*1,tileWidth, tileHeight),
                new Rectangle(tileWidth*2,tileHeight*1,tileWidth, tileHeight),
                new Rectangle(tileWidth*0,tileHeight*2,tileWidth, tileHeight),
                new Rectangle(tileWidth*1,tileHeight*2,tileWidth, tileHeight),
                new Rectangle(tileWidth*2,tileHeight*2,tileWidth, tileHeight),
            };
        }

        public Point DefaultSize => new(texture.Width, texture.Height);

        public void Draw(SpriteBatch sb, Rectangle destination, Color? col = null, float layerDepth = 0)
        {
            

            BasicEffect effect = new BasicEffect(sb.GraphicsDevice);
            effect.Texture = texture;
            effect.TextureEnabled = true;
            if (col != null)
            {
                effect.DiffuseColor = col.Value.ToVector3();
            }
            

            Rectangle[] destinationRectangles = GetDestinationRectangles(destination);

            VertexPositionTexture[] vertices = new VertexPositionTexture[16];



            int[] rectangles = new int[] { 0, 2, 6, 8 };
            int vertInd = 0;

            foreach (int i in rectangles)
            {
                Vector2 topLeft = new Vector2(
                    ((float)destinationRectangles[i].Left / sb.GraphicsDevice.Viewport.Width) * 2 - 1.0f,
                    (((float)destinationRectangles[i].Top / sb.GraphicsDevice.Viewport.Height) * 2 - 1.0f) * -1
                    );

                Vector2 botRight = new Vector2(
                    ((float)destinationRectangles[i].Right / sb.GraphicsDevice.Viewport.Width) * 2 - 1.0f,
                    (((float)destinationRectangles[i].Bottom / sb.GraphicsDevice.Viewport.Height) * 2 - 1.0f) * -1
                    );

                Vector3[] points = new Vector3[]{
                new Vector3(topLeft, 1f),
                new Vector3(topLeft.X, botRight.Y, 1f),
                new Vector3(botRight.X, topLeft.Y, 1f),
                new Vector3(botRight, 1f) };

                Vector2[] UV = new Vector2[]
                {
                    new Vector2((float)sourceRectangles[i].Left / texture.Width, (float)sourceRectangles[i].Top / texture.Height),
                    new Vector2((float)sourceRectangles[i].Left / texture.Width, (float)sourceRectangles[i].Bottom / texture.Height),
                    new Vector2((float)sourceRectangles[i].Right / texture.Width, (float)sourceRectangles[i].Top / texture.Height),
                    new Vector2((float)sourceRectangles[i].Right / texture.Width, (float)sourceRectangles[i].Bottom / texture.Height),

                };

                vertices[vertInd++] = new VertexPositionTexture(points[0], UV[0]);
                vertices[vertInd++] = new VertexPositionTexture(points[1], UV[1]);
                vertices[vertInd++] = new VertexPositionTexture(points[2], UV[2]);
                vertices[vertInd++] = new VertexPositionTexture(points[3], UV[3]);
            };


            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                sb.GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,  
                    vertices.Length, 
                    indices.ToArray(),
                    0,                          
                    18                         
                );
            }
        }

        private Rectangle[] GetDestinationRectangles(Rectangle destinationRectangle)
        {
            int middleWidth = destinationRectangle.Width - tileWidth * 2;
            int middleHeight = destinationRectangle.Height - tileHeight * 2;

            return new Rectangle[]
            {
                new Rectangle(destinationRectangle.Left, destinationRectangle.Top, tileWidth, tileHeight),
                new Rectangle(destinationRectangle.Left + tileWidth, destinationRectangle.Top, middleWidth, tileHeight),
                new Rectangle(destinationRectangle.Left + tileWidth + middleWidth, destinationRectangle.Top, tileWidth, tileHeight),
                new Rectangle(destinationRectangle.Left, destinationRectangle.Top+tileHeight, tileWidth, middleHeight),
                new Rectangle(destinationRectangle.Left + tileWidth, destinationRectangle.Top + tileHeight, middleWidth, middleHeight),
                new Rectangle(destinationRectangle.Left + tileWidth + middleWidth, destinationRectangle.Top + tileHeight, tileWidth, middleHeight),
                new Rectangle(destinationRectangle.Left, destinationRectangle.Top + tileHeight + middleHeight, tileWidth, tileHeight),
                new Rectangle(destinationRectangle.Left + tileWidth, destinationRectangle.Top + tileHeight + middleHeight, middleWidth, tileHeight),
                new Rectangle(destinationRectangle.Left + tileWidth + middleWidth, destinationRectangle.Top + tileHeight + middleHeight, tileHeight, tileWidth)
            };
        }
    }
        
}