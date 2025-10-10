using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;
using Peridot.UI.Builder;


namespace peridot_ui_test;

public class Game1 : Core
{
    IExample _currentExample;
    SpriteFont _font;
    public Game1() :
        base("Peridot UI Test", 1200, 900, false, "fonts/Default")
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
        Console.WriteLine("Game1: LoadContent started");
        try
        {
            _font = Content.Load<SpriteFont>("fonts/Default");
            Console.WriteLine("Game1: Font loaded successfully");
            
            _currentExample = new UIBuilderExample();
            Console.WriteLine("Game1: UIBuilderExample created");
            
            _currentExample.Initialize(_font);
            Console.WriteLine("Game1: Example initialized");
            
            Core.UISystem.AddElement(_currentExample.GetRootElement());
            Console.WriteLine("Game1: UI element added");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Game1: Error in LoadContent: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        Console.WriteLine("Game1: LoadContent completed");
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
