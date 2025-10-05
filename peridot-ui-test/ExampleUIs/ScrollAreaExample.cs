using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;

public class ScrollAreaExample : IExample
{
    private UIElement _rootElement;
    private ScrollArea _scrollArea;
    private Label _statusLabel;

    private Texture2D texture1;
    private Texture2D texture2;
    private Texture2D texture3;
    private Texture2D texture4;

    public void Initialize(SpriteFont font)
    {
        try
        {
            // Load textures
            texture1 = Core.Content.Load<Texture2D>("images/spruce_door_bottom");
            texture2 = Core.Content.Load<Texture2D>("images/spruce_log_top"); 
            texture3 = Core.Content.Load<Texture2D>("images/spruce_log");
            texture4 = Core.Content.Load<Texture2D>("images/spruce_trapdoor");

            // Create main layout
            var mainLayout = new VerticalLayoutGroup(new Rectangle(50, 50, 800, 700), 10);

            // Title
            var titleLabel = new Label(
                new Rectangle(0, 0, 800, 40), 
                "ScrollArea with REAL Clipping - Scissor Test", 
                font, 
                Color.Yellow, 
                Color.DarkBlue
            );

            // Status label
            _statusLabel = new Label(
                new Rectangle(0, 0, 800, 30), 
                "Content that extends outside viewport will be clipped!", 
                font, 
                Color.White, 
                Color.DarkGray
            );

            // Create ScrollArea - THIS WILL ACTUALLY CLIP CONTENT
            _scrollArea = new ScrollArea(new Rectangle(0, 0, 780, 300), scrollbarWidth: 20);
            _scrollArea.ScrollSpeed = 30f;
            _scrollArea.OnScrollChanged += OnScrollChanged;

            // Add content that EXTENDS BEYOND the scroll area to test clipping
            PopulateScrollAreaWithExtendingContent(font);

            // Control buttons
            var buttonLayout = new HorizontalLayoutGroup(new Rectangle(0, 0, 780, 50), 10);
            
            var scrollTopButton = new Button(
                new Rectangle(0, 0, 120, 40),
                "Top",
                font,
                Color.DarkGreen,
                Color.Green,
                Color.White,
                () => _scrollArea.ScrollToTop()
            );

            var scrollBottomButton = new Button(
                new Rectangle(0, 0, 120, 40),
                "Bottom",
                font,
                Color.DarkGreen,
                Color.Green,
                Color.White,
                () => _scrollArea.ScrollToBottom()
            );

            var scrollLeftButton = new Button(
                new Rectangle(0, 0, 120, 40),
                "Left",
                font,
                Color.DarkBlue,
                Color.Blue,
                Color.White,
                () => _scrollArea.ScrollToLeft()
            );

            var scrollRightButton = new Button(
                new Rectangle(0, 0, 120, 40),
                "Right",
                font,
                Color.DarkBlue,
                Color.Blue,
                Color.White,
                () => _scrollArea.ScrollToRight()
            );

            buttonLayout.AddChild(scrollTopButton);
            buttonLayout.AddChild(scrollBottomButton);
            buttonLayout.AddChild(scrollLeftButton);
            buttonLayout.AddChild(scrollRightButton);

            // Instructions that explain the clipping
            var instructionLabel1 = new Label(
                new Rectangle(0, 0, 800, 25), 
                "REAL SCISSOR CLIPPING: Content outside viewport is actually clipped", 
                font, 
                Color.Lime
            );

            var instructionLabel2 = new Label(
                new Rectangle(0, 0, 800, 25), 
                "Grid extends far beyond visible area - only visible portion renders", 
                font, 
                Color.LightGray
            );

            var instructionLabel3 = new Label(
                new Rectangle(0, 0, 800, 25), 
                "Use mouse wheel or scrollbars to navigate", 
                font, 
                Color.LightGray
            );

            var instructionLabel4 = new Label(
                new Rectangle(0, 0, 800, 25), 
                "Content is positioned outside viewport but gets clipped", 
                font, 
                Color.LightGray
            );

            // Add all elements to main layout
            mainLayout.AddChild(titleLabel);
            mainLayout.AddChild(_statusLabel);
            mainLayout.AddChild(_scrollArea);
            mainLayout.AddChild(buttonLayout);
            mainLayout.AddChild(instructionLabel1);
            mainLayout.AddChild(instructionLabel2);
            mainLayout.AddChild(instructionLabel3);
            mainLayout.AddChild(instructionLabel4);

            _rootElement = mainLayout;
        }
        catch (System.Exception ex)
        {
            // Fallback if loading fails
            _rootElement = new Label(new Rectangle(50, 50, 400, 100), 
                $"Error loading ScrollArea example: {ex.Message}", font, Color.Red, Color.Black);
        }
    }

    private void PopulateScrollAreaWithExtendingContent(SpriteFont font)
    {
        // Create a LARGE grid that extends way beyond the scroll area viewport
        // This will test real clipping - elements outside viewport should not be visible
        int gridCols = 12;  // Much wider than viewport
        int gridRows = 15;  // Much taller than viewport
        int itemWidth = 100;
        int itemHeight = 80;
        int spacing = 10;

        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridCols; col++)
            {
                int x = col * (itemWidth + spacing) + spacing;
                int y = row * (itemHeight + spacing) + spacing;

                // Alternate between different content types for visual variety
                int contentType = (row * gridCols + col) % 4;
                
                switch (contentType)
                {
                    case 0:
                        // Colored rectangle with position info
                        var colorLabel = new Label(
                            new Rectangle(x, y, itemWidth, itemHeight),
                            $"R{row}C{col}\nPos: {x},{y}\nShould clip!",
                            font,
                            Color.White,
                            GetPositionColor(row, col)
                        );
                        _scrollArea.AddChild(colorLabel);
                        break;

                    case 1:
                        // Image that should be clipped when outside viewport
                        var image = new UIImage(
                            texture1, 
                            new Rectangle(x, y, itemWidth, itemHeight - 20)
                        );
                        _scrollArea.AddChild(image);
                        
                        // Small label below image
                        var imageLabel = new Label(
                            new Rectangle(x, y + itemHeight - 20, itemWidth, 20),
                            $"IMG {row},{col}",
                            font,
                            Color.Black,
                            Color.White
                        );
                        _scrollArea.AddChild(imageLabel);
                        break;

                    case 2:
                        // Interactive button (should only respond when visible)
                        var button = new ImageButton(
                            new Rectangle(x, y, itemWidth, itemHeight - 20),
                            texture2,
                            () => UpdateStatus($"Clicked button at R{row}C{col} - pos {x},{y}"),
                            tintColor: Color.White,
                            hoverTintColor: Color.Yellow,
                            drawBackground: true,
                            backgroundColor: Color.DarkRed,
                            hoverBackgroundColor: Color.Red
                        );
                        _scrollArea.AddChild(button);
                        
                        var buttonLabel = new Label(
                            new Rectangle(x, y + itemHeight - 20, itemWidth, 20),
                            $"BTN {row},{col}",
                            font,
                            Color.White,
                            Color.DarkRed
                        );
                        _scrollArea.AddChild(buttonLabel);
                        break;

                    case 3:
                        // Another image type
                        var image2 = new UIImage(
                            (row + col) % 2 == 0 ? texture3 : texture4, 
                            new Rectangle(x, y, itemWidth, itemHeight - 20)
                        );
                        _scrollArea.AddChild(image2);
                        
                        var image2Label = new Label(
                            new Rectangle(x, y + itemHeight - 20, itemWidth, 20),
                            $"TEX {row},{col}",
                            font,
                            Color.Black,
                            Color.LightBlue
                        );
                        _scrollArea.AddChild(image2Label);
                        break;
                }
            }
        }

        // Add some content that extends to extreme positions to really test clipping
        var extremeLabel1 = new Label(
            new Rectangle(2000, 50, 200, 50),
            "WAY RIGHT - Should be clipped until you scroll!",
            font,
            Color.Red,
            Color.Yellow
        );
        _scrollArea.AddChild(extremeLabel1);

        var extremeLabel2 = new Label(
            new Rectangle(50, 2000, 200, 50),
            "WAY DOWN - Should be clipped until you scroll!",
            font,
            Color.Blue,
            Color.Cyan
        );
        _scrollArea.AddChild(extremeLabel2);
    }

    private Color GetPositionColor(int row, int col)
    {
        // Generate different colors based on position for easy identification
        Color[] colors = {
            Color.DarkRed, Color.DarkGreen, Color.DarkBlue, Color.DarkOrange,
            Color.Purple, Color.DarkCyan, Color.Maroon, Color.Navy,
            Color.DarkOliveGreen, Color.DarkViolet, Color.IndianRed, Color.DarkSlateBlue
        };
        
        int index = (row * 3 + col * 2) % colors.Length;
        return colors[index];
    }

    private void UpdateStatus(string message)
    {
        _statusLabel.SetText(message);
    }

    private void OnScrollChanged(Vector2 scrollOffset)
    {
        UpdateStatus($"Scroll: X={scrollOffset.X:F0}, Y={scrollOffset.Y:F0} - Content clipped by scissor test!");
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}
