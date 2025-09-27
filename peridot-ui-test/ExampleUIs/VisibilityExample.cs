using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot.UI;

public class VisibilityExample : IExample
{
    private IUIElement _rootElement;

    private Label _label;
    private Slider _slider;
    private TextInput _textInput;
    private string _textInputText;

    public void Initialize(SpriteFont font)
    {
        var layout = new VerticalLayoutGroup(new Rectangle(50, 50, 300, 400), 10);
        _label = new Label(new Rectangle(0, 0, 300, 50), "Click the button to toggle visibility",
            font, Color.Black, Color.LightGray);

        _slider = new Slider(new Rectangle(0, 0, 200, 50), 0, 100, 0, 1, true, Color.Gray, Color.DarkGray, Color.LightGray);

        _textInput = new TextInput(new Rectangle(0, 0, 200, 50), font, "Hey buddy");
        _textInput.OnTextChanged += (text) => _textInputText = text;

        var button = new Button(new Rectangle(0, 0, 200, 50), "Toggle Label Visibility", font, Color.DarkSlateGray, Color.LightGray, Color.White, () =>
        {
            Console.WriteLine("foiawefiojwef");
            _label.SetVisibility(!_label.IsVisible());
            _slider.SetVisibility(!_slider.IsVisible());
            _textInput.SetVisibility(!_textInput.IsVisible());
        });

        layout.AddChild(_label);
        layout.AddChild(_slider);
        layout.AddChild(_textInput);
        layout.AddChild(button);
        _rootElement = layout;
    }

    public IUIElement GetRootElement()
    {
        return _rootElement;
    }
}