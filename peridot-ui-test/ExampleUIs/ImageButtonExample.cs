using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;

public class ImageButtonExample : IExample
{
    private UIElement _rootElement;
    private Label _statusLabel;
    private string _lastAction = "Click any image button!";

    private Texture2D texture1;
    private Texture2D texture2;
    private Texture2D texture3;
    private Texture2D texture4;

    public void Initialize(SpriteFont font)
    {
        // Load the same textures as ImageExample for consistency
        texture1 = Core.Content.Load<Texture2D>("images/spruce_door_bottom");
        texture2 = Core.Content.Load<Texture2D>("images/spruce_log_top"); 
        texture3 = Core.Content.Load<Texture2D>("images/spruce_log");
        texture4 = Core.Content.Load<Texture2D>("images/spruce_trapdoor");

        // Create main layout container
        var mainLayout = new VerticalLayoutGroup(new Rectangle(50, 50, 500, 600), 10);

        // Create title label
        var titleLabel = new Label(
            new Rectangle(0, 0, 500, 40), 
            "Image Button Examples", 
            font, 
            Color.White, 
            Color.DarkBlue
        );

        // Create status label to show which button was clicked
        _statusLabel = new Label(
            new Rectangle(0, 0, 500, 30), 
            _lastAction, 
            font, 
            Color.Yellow, 
            Color.DarkGray
        );

        // Create a grid layout for the image buttons
        var buttonGrid = new GridLayoutGroup(new Rectangle(0, 0, 480, 320), 2, 2, 10);

        // Create different types of image buttons to demonstrate various features

        // 1. Basic image button with hover effects
        var doorButton = new ImageButton(
            new Rectangle(0, 0, 150, 150),
            texture1,
            () => UpdateStatus("Door button clicked!"),
            tintColor: Color.White,
            hoverTintColor: Color.LightBlue,
            pressedTintColor: Color.Blue
        );

        // 2. Image button with background
        var logTopButton = new ImageButton(
            new Rectangle(0, 0, 150, 150),
            texture2,
            () => UpdateStatus("Log Top button clicked!"),
            tintColor: Color.White,
            hoverTintColor: Color.LightGreen,
            pressedTintColor: Color.Green,
            drawBackground: true,
            backgroundColor: Color.DarkGreen,
            hoverBackgroundColor: Color.ForestGreen,
            pressedBackgroundColor: Color.DarkOliveGreen
        );

        // 3. Image button with custom colors and disabled state toggle
        var logButton = new ImageButton(
            new Rectangle(0, 0, 150, 150),
            texture3,
            () => ToggleTrapdoorButton(),
            tintColor: Color.Wheat,
            hoverTintColor: Color.Orange,
            pressedTintColor: Color.DarkOrange,
            disabledTintColor: Color.Gray
        );

        // 4. Image button that can be disabled/enabled by the log button
        var trapdoorButton = new ImageButton(
            new Rectangle(0, 0, 150, 150),
            texture4,
            () => UpdateStatus("Trapdoor button clicked!"),
            tintColor: Color.White,
            hoverTintColor: Color.Pink,
            pressedTintColor: Color.Red,
            drawBackground: true,
            backgroundColor: Color.DarkRed,
            hoverBackgroundColor: Color.Crimson,
            pressedBackgroundColor: Color.Maroon
        );

        // Store reference to trapdoor button for toggling
        _trapdoorButton = trapdoorButton;

        // Add buttons to grid
        buttonGrid.AddChild(doorButton);
        buttonGrid.AddChild(logTopButton);
        buttonGrid.AddChild(logButton);
        buttonGrid.AddChild(trapdoorButton);

        // Create instruction labels
        var instructionLabel1 = new Label(
            new Rectangle(0, 0, 500, 25), 
            "- Door: Basic hover effects", 
            font, 
            Color.LightGray
        );

        var instructionLabel2 = new Label(
            new Rectangle(0, 0, 500, 25), 
            "- Log Top: With colored background", 
            font, 
            Color.LightGray
        );

        var instructionLabel3 = new Label(
            new Rectangle(0, 0, 500, 25), 
            "- Log: Click to toggle trapdoor enabled/disabled", 
            font, 
            Color.LightGray
        );

        var instructionLabel4 = new Label(
            new Rectangle(0, 0, 500, 25), 
            "- Trapdoor: Can be disabled by log button", 
            font, 
            Color.LightGray
        );

        // Add all elements to main layout
        mainLayout.AddChild(titleLabel);
        mainLayout.AddChild(_statusLabel);
        mainLayout.AddChild(buttonGrid);
        mainLayout.AddChild(instructionLabel1);
        mainLayout.AddChild(instructionLabel2);
        mainLayout.AddChild(instructionLabel3);
        mainLayout.AddChild(instructionLabel4);

        _rootElement = mainLayout;
    }

    private ImageButton _trapdoorButton;

    private void UpdateStatus(string message)
    {
        _lastAction = message;
        _statusLabel.SetText(_lastAction);
    }

    private void ToggleTrapdoorButton()
    {
        _trapdoorButton.IsEnabled = !_trapdoorButton.IsEnabled;
        string status = _trapdoorButton.IsEnabled ? "enabled" : "disabled";
        UpdateStatus($"Log clicked! Trapdoor is now {status}");
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}