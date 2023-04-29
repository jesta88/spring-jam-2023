using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest;
using MonoGameTest.Rendering.Sprites;
using MonoGameTest.UI;
using Spring.UI;
using System.Diagnostics;

namespace Spring
{
    public class SpringGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _basicBackground;
        private UIElement _testMenu;

        public SpringGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            Input.Initialize(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("UI/BasicFont");
            _basicBackground = Content.Load<Texture2D>("UI/BasicPanel");

            // Example of how UI works. When a parent is specified, positions are relative to the parent.
            _testMenu = new UIElement(
                new Rectangle(50, 50, 1000, 450),
                new SlicedSprite(_basicBackground, 32, 32),
                null);
            UIElement innerBox = new UIElement(
                new Rectangle(50, 50, 900, 350),
                new SlicedSprite(_basicBackground, 32, 32),
                _testMenu);
            UIButton button = new UIButton(
                new Rectangle(400, 125, 100, 100),
                new SlicedSprite(_basicBackground, 32, 32),
                innerBox);
            UILabel buttonLabel = new UILabel(
                "This is a button.",
                _font,
                new Rectangle(10, 100, 80, 20),
                button);
            button.OnHover += () => { buttonLabel.UpdateText("Hovering!"); };
            button.OnClick += () => { buttonLabel.UpdateText("Clicked!"); };
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _testMenu.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}