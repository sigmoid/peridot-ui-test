using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;


namespace peridot_ui_test;

public class Game1 : Core
{
    IExample _currentExample;
    SpriteFont _font;
    public Game1() :
        base("Peridot UI Test", 1200, 900, false)
    {
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _font = Content.Load<SpriteFont>("fonts/JosefinSans");
        _currentExample = new VisibilityExample();
        _currentExample.Initialize(_font);
        Core.UISystem.AddElement(_currentExample.GetRootElement());
    }

    protected override void Update(GameTime gameTime)
    {

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
