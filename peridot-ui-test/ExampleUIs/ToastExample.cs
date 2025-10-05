using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;
using System;

public class ToastExample : IExample
{
    private UIElement _rootElement;
    private ToastManager _toastManager;
    private Rectangle _screenBounds;

    public void Initialize(SpriteFont font)
    {
        _screenBounds = new Rectangle(0, 0, 1200, 900);
        
        // Create a canvas to hold all the demo UI elements
        var canvas = new Canvas(new Rectangle(0, 0, 1200, 900));

        // Initialize the ToastManager
        _toastManager = new ToastManager(Core.UISystem, font, _screenBounds);
        _toastManager.MaxToasts = 5;
        _toastManager.AutoDismissOldToasts = true;

        // Create demo UI elements
        CreateDemoInterface(canvas, font);

        _rootElement = canvas;
    }

    private void CreateDemoInterface(Canvas canvas, SpriteFont font)
    {
        // Title
        var titleLabel = new Label(
            new Rectangle(50, 30, 400, 40),
            "Toast Notification Demo",
            font,
            Color.White,
            Color.DarkBlue
        );
        canvas.AddChild(titleLabel);

        // Description
        var descLabel = new Label(
            new Rectangle(50, 80, 600, 30),
            "Click the buttons below to see different types of toast notifications:",
            font,
            Color.Black
        );
        canvas.AddChild(descLabel);

        // Info Toast Button
        var infoButton = new Button(
            new Rectangle(50, 130, 150, 40),
            "Show Info Toast",
            font,
            Color.CornflowerBlue,
            Color.LightBlue,
            Color.White,
            () => _toastManager.ShowInfo("This is an informational message!", 3f)
        );
        canvas.AddChild(infoButton);

        // Success Toast Button
        var successButton = new Button(
            new Rectangle(220, 130, 150, 40),
            "Show Success Toast",
            font,
            Color.Green,
            Color.LightGreen,
            Color.White,
            () => _toastManager.ShowSuccess("Operation completed successfully!", 3f)
        );
        canvas.AddChild(successButton);

        // Warning Toast Button
        var warningButton = new Button(
            new Rectangle(390, 130, 150, 40),
            "Show Warning Toast",
            font,
            Color.Orange,
            Color.Yellow,
            Color.Black,
            () => _toastManager.ShowWarning("This is a warning message!", 4f)
        );
        canvas.AddChild(warningButton);

        // Error Toast Button
        var errorButton = new Button(
            new Rectangle(560, 130, 150, 40),
            "Show Error Toast",
            font,
            Color.Red,
            Color.Pink,
            Color.White,
            () => _toastManager.ShowError("An error has occurred!", 5f)
        );
        canvas.AddChild(errorButton);

        // Position Demo Section
        var positionLabel = new Label(
            new Rectangle(50, 200, 300, 30),
            "Test Different Positions:",
            font,
            Color.Black
        );
        canvas.AddChild(positionLabel);

        // Top positions
        var topLeftButton = new Button(
            new Rectangle(50, 240, 120, 35),
            "Top Left",
            font,
            Color.DarkBlue,
            Color.Blue,
            Color.White,
            () => _toastManager.ShowInfo("Top Left Toast!", 2f, Toast.ToastPosition.TopLeft)
        );
        canvas.AddChild(topLeftButton);

        var topCenterButton = new Button(
            new Rectangle(180, 240, 120, 35),
            "Top Center",
            font,
            Color.DarkBlue,
            Color.Blue,
            Color.White,
            () => _toastManager.ShowInfo("Top Center Toast!", 2f, Toast.ToastPosition.TopCenter)
        );
        canvas.AddChild(topCenterButton);

        var topRightButton = new Button(
            new Rectangle(310, 240, 120, 35),
            "Top Right",
            font,
            Color.DarkBlue,
            Color.Blue,
            Color.White,
            () => _toastManager.ShowInfo("Top Right Toast!", 2f, Toast.ToastPosition.TopRight)
        );
        canvas.AddChild(topRightButton);

        // Middle positions
        var middleLeftButton = new Button(
            new Rectangle(50, 285, 120, 35),
            "Middle Left",
            font,
            Color.Purple,
            Color.Violet,
            Color.White,
            () => _toastManager.ShowInfo("Middle Left Toast!", 2f, Toast.ToastPosition.MiddleLeft)
        );
        canvas.AddChild(middleLeftButton);

        var middleCenterButton = new Button(
            new Rectangle(180, 285, 120, 35),
            "Middle Center",
            font,
            Color.Purple,
            Color.Violet,
            Color.White,
            () => _toastManager.ShowInfo("Middle Center Toast!", 2f, Toast.ToastPosition.MiddleCenter)
        );
        canvas.AddChild(middleCenterButton);

        var middleRightButton = new Button(
            new Rectangle(310, 285, 120, 35),
            "Middle Right",
            font,
            Color.Purple,
            Color.Violet,
            Color.White,
            () => _toastManager.ShowInfo("Middle Right Toast!", 2f, Toast.ToastPosition.MiddleRight)
        );
        canvas.AddChild(middleRightButton);

        // Bottom positions
        var bottomLeftButton = new Button(
            new Rectangle(50, 330, 120, 35),
            "Bottom Left",
            font,
            Color.Brown,
            Color.SandyBrown,
            Color.White,
            () => _toastManager.ShowInfo("Bottom Left Toast!", 2f, Toast.ToastPosition.BottomLeft)
        );
        canvas.AddChild(bottomLeftButton);

        var bottomCenterButton = new Button(
            new Rectangle(180, 330, 120, 35),
            "Bottom Center",
            font,
            Color.Brown,
            Color.SandyBrown,
            Color.White,
            () => _toastManager.ShowInfo("Bottom Center Toast!", 2f, Toast.ToastPosition.BottomCenter)
        );
        canvas.AddChild(bottomCenterButton);

        var bottomRightButton = new Button(
            new Rectangle(310, 330, 120, 35),
            "Bottom Right",
            font,
            Color.Brown,
            Color.SandyBrown,
            Color.White,
            () => _toastManager.ShowInfo("Bottom Right Toast!", 2f, Toast.ToastPosition.BottomRight)
        );
        canvas.AddChild(bottomRightButton);

        // Special Demo Section
        var specialLabel = new Label(
            new Rectangle(50, 390, 300, 30),
            "Special Demos:",
            font,
            Color.Black
        );
        canvas.AddChild(specialLabel);

        // Multiple Toasts Button
        var multipleButton = new Button(
            new Rectangle(50, 430, 180, 40),
            "Show Multiple Toasts",
            font,
            Color.DarkGreen,
            Color.LimeGreen,
            Color.White,
            () => ShowMultipleToasts()
        );
        canvas.AddChild(multipleButton);

        // Sequential Toasts Button  
        var sequentialButton = new Button(
            new Rectangle(250, 430, 180, 40),
            "Sequential Workflow",
            font,
            Color.DarkMagenta,
            Color.Magenta,
            Color.White,
            () => ShowSequentialToasts()
        );
        canvas.AddChild(sequentialButton);

        // Long Message Toast
        var longMessageButton = new Button(
            new Rectangle(450, 430, 180, 40),
            "Long Message Toast",
            font,
            Color.DarkCyan,
            Color.Cyan,
            Color.Black,
            () => _toastManager.ShowWarning("This is a very long toast message that demonstrates how the toast system handles longer text content gracefully!", 6f)
        );
        canvas.AddChild(longMessageButton);

        // Control Section
        var controlLabel = new Label(
            new Rectangle(50, 500, 200, 30),
            "Toast Controls:",
            font,
            Color.Black
        );
        canvas.AddChild(controlLabel);

        // Dismiss All Button
        var dismissAllButton = new Button(
            new Rectangle(50, 540, 140, 35),
            "Dismiss All",
            font,
            Color.DarkRed,
            Color.Red,
            Color.White,
            () => _toastManager.DismissAll()
        );
        canvas.AddChild(dismissAllButton);

        // Hide All Button
        var hideAllButton = new Button(
            new Rectangle(200, 540, 140, 35),
            "Hide All",
            font,
            Color.Black,
            Color.Gray,
            Color.White,
            () => _toastManager.HideAll()
        );
        canvas.AddChild(hideAllButton);

        // Status Information
        var statusLabel = new Label(
            new Rectangle(50, 590, 400, 30),
            "Active Toasts: 0 | Max Toasts: 5",
            font,
            Color.DarkBlue
        );
        canvas.AddChild(statusLabel);

        // Update Status Button
        var updateStatusButton = new Button(
            new Rectangle(50, 630, 150, 30),
            "Update Status",
            font,
            Color.Gray,
            Color.LightGray,
            Color.Black,
            () => statusLabel.Text = $"Active Toasts: {_toastManager.ActiveToastCount} | Max Toasts: {_toastManager.MaxToasts}"
        );
        canvas.AddChild(updateStatusButton);

        // Instructions
        var instructionsTextArea = new TextArea(
            new Rectangle(500, 200, 400, 200),
            font,
            "",
            wordWrap: true,
            readOnly: true,
            backgroundColor: Color.LightGray,
            textColor: Color.DarkBlue
        );
        
        instructionsTextArea.Text = @"Toast Notification Instructions:

- Toasts automatically fade in and out
- Different types have different colors and durations
- Multiple toasts stack without overlapping
- Toasts can be positioned at 9 different locations
- Use 'Dismiss All' to fade out all toasts
- Use 'Hide All' to immediately remove all toasts
- The system prevents too many toasts from accumulating

Try clicking the buttons to see the toast system in action!";

        canvas.AddChild(instructionsTextArea);
    }

    private void ShowMultipleToasts()
    {
        _toastManager.ShowInfo("First toast in sequence");
        _toastManager.ShowSuccess("Second toast - success!");
        _toastManager.ShowWarning("Third toast - warning!");
        _toastManager.ShowError("Fourth toast - error!");
        _toastManager.ShowInfo("Fifth toast - stacking demo");
    }

    private void ShowSequentialToasts()
    {
        var step1 = _toastManager.ShowInfo("Step 1: Initializing process...", 2f);
        
        step1.OnToastFinished += () =>
        {
            var step2 = _toastManager.ShowInfo("Step 2: Loading data...", 2f);
            
            step2.OnToastFinished += () =>
            {
                var step3 = _toastManager.ShowInfo("Step 3: Processing...", 2f);
                
                step3.OnToastFinished += () =>
                {
                    _toastManager.ShowSuccess("Workflow completed successfully!", 3f);
                };
            };
        };
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }

    // Note: In a real implementation, you would call this from your game's Update method
    public void Update(float deltaTime)
    {
        _toastManager?.Update(deltaTime);
    }
}