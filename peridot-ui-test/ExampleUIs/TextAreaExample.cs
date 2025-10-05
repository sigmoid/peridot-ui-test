using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot.UI;

public class TextAreaExample : IExample
{
    private UIElement _rootElement;

    public void Initialize(SpriteFont font)
    {
        var textArea = new TextArea(
            bounds: new Rectangle(50, 50, 400, 200),
            font: font,
            placeholder: "Enter your text here...\nSupports multiple lines!",
            wordWrap: true,
            readOnly: true
        );

        textArea.Text = "lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        _rootElement = textArea;
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}