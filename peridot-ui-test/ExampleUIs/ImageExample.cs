using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;

public class ImageExample : IExample
{
    private IUIElement _rootElement;

    private Texture2D texture1;
    private Texture2D texture2;
    private Texture2D texture3;
    private Texture2D texture4;

    public void Initialize(SpriteFont font)
    {
        texture1 = Core.Content.Load<Texture2D>("images/spruce_door_bottom");
        texture2 = Core.Content.Load<Texture2D>("images/spruce_log_top"); 
        texture3 = Core.Content.Load<Texture2D>("images/spruce_log");
        texture4 = Core.Content.Load<Texture2D>("images/spruce_trapdoor");

        var layout = new GridLayoutGroup(new Rectangle(50, 50, 400, 400), 2, 2, 5);


        var image1 = new UIImage(texture1, new Rectangle(0, 0, 100, 100));
        var image2 = new UIImage(texture2, new Rectangle(0, 0, 100, 100));
        var image3 = new UIImage(texture3, new Rectangle(0, 0, 100, 100));
        var image4 = new UIImage(texture4, new Rectangle(0, 0, 100, 100));

        layout.AddChild(image1);
        layout.AddChild(image2);
        layout.AddChild(image3);
        layout.AddChild(image4);


        _rootElement = layout;
    }

    public IUIElement GetRootElement()
    {
        return _rootElement;
    }
}