using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot.UI;

public class ModalExample : IExample
{
    Modal _modal;
    Button _showModalButton;
    IUIElement _rootElement;
    public void Initialize(SpriteFont font)
    {
        var layout = new Canvas(new Rectangle(50, 50, 300, 400));
        _showModalButton = new Button(new Rectangle(0, 0, 200, 50), "Show Modal", font, Color.DarkSlateGray, Color.LightGray, Color.White, () =>
        {
            _modal.SetVisibility(true);
        });

        layout.AddChild(_showModalButton);

        _modal = new Modal(new Rectangle(0, 0, 800, 600), new Rectangle(200, 150, 400, 300), "Example Modal", font);
        var contentLayout = new VerticalLayoutGroup(new Rectangle(0, 0, 400, 300), 10);
        contentLayout.AddChild(new Label(new Rectangle(0, 0, 400, 50), "This is a modal dialog", font, Color.Black, Color.LightGray));
        contentLayout.AddChild(new Button(new Rectangle(0, 0, 200, 50), "Close Modal", font, Color.DarkSlateGray, Color.LightGray, Color.White, () =>
        {
            _modal.SetVisibility(false);
        }));    
        _modal.AddContentElement(contentLayout);

        _modal.SetVisibility(false);

        layout.AddChild(_modal);

        _rootElement = layout;
    }
    public IUIElement GetRootElement()
    {
        return _rootElement;
    }

}