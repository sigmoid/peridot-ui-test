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

            // === ADDITIONAL UI ELEMENTS OUTSIDE SCROLLAREA FOR TESTING ===
            
            // TextInput example
            var textInputLabel = new Label(
                new Rectangle(0, 0, 800, 25),
                "TextInput (outside ScrollArea):",
                font,
                Color.Cyan
            );
            
            var textInput = new TextInput(
                new Rectangle(0, 0, 300, 35),
                font,
                "Type here...",
                Color.White,
                Color.Black,
                Color.Gray,
                Color.Blue,
                8, 2
            );
            textInput.OnTextChanged += (text) => UpdateStatus($"TextInput: {text}");

            // TextArea example (read-only)
            var textAreaLabel = new Label(
                new Rectangle(0, 0, 800, 25),
                "TextArea (read-only, outside ScrollArea):",
                font,
                Color.Orange
            );
            
            var textArea = new TextArea(
                new Rectangle(0, 0, 380, 80),
                font,
                wordWrap: true,
                readOnly: true
            );
            textArea.Text = "This is a multi-line TextArea outside the ScrollArea.\nIt demonstrates that other UI elements work properly alongside the ScrollArea.\nThis one is read-only for display purposes.";

            // Slider example
            var sliderLabel = new Label(
                new Rectangle(0, 0, 400, 25),
                "Slider (outside ScrollArea):",
                font,
                Color.Lime
            );
            
            var slider = new Slider(
                new Rectangle(0, 0, 200, 30),
                minValue: 0f,
                maxValue: 100f,
                initialValue: 50f,
                trackColor: Color.DarkGray,
                fillColor: Color.Green,
                handleColor: Color.White
            );
            slider.OnValueChanged += (value) => UpdateStatus($"Slider value: {value:F1}");

            // Toast and Modal buttons
            var controlButtonsLayout = new HorizontalLayoutGroup(new Rectangle(0, 0, 780, 40), 10);
            
            var showToastButton = new Button(
                new Rectangle(0, 0, 120, 35),
                "Show Toast",
                font,
                Color.DarkOrange,
                Color.Orange,
                Color.White,
                () => ShowToast(font)
            );
            
            var showModalButton = new Button(
                new Rectangle(0, 0, 120, 35),
                "Show Modal",
                font,
                Color.DarkRed,
                Color.Red,
                Color.White,
                () => ShowModal(font)
            );

            var clearStatusButton = new Button(
                new Rectangle(0, 0, 120, 35),
                "Clear Status",
                font,
                Color.DarkBlue,
                Color.Blue,
                Color.White,
                () => UpdateStatus("Status cleared")
            );

            controlButtonsLayout.AddChild(showToastButton);
            controlButtonsLayout.AddChild(showModalButton);
            controlButtonsLayout.AddChild(clearStatusButton);

            // Add all elements to main layout
            mainLayout.AddChild(titleLabel);
            mainLayout.AddChild(_statusLabel);
            mainLayout.AddChild(_scrollArea);
            mainLayout.AddChild(buttonLayout);
            mainLayout.AddChild(instructionLabel1);
            mainLayout.AddChild(instructionLabel2);
            mainLayout.AddChild(instructionLabel3);
            mainLayout.AddChild(instructionLabel4);
            
            // Add the additional UI elements
            mainLayout.AddChild(textInputLabel);
            mainLayout.AddChild(textInput);
            mainLayout.AddChild(textAreaLabel);
            mainLayout.AddChild(textArea);
            mainLayout.AddChild(sliderLabel);
            mainLayout.AddChild(slider);
            mainLayout.AddChild(controlButtonsLayout);

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
        // Create a comprehensive test with ALL types of UI elements
        int currentY = 10;
        int spacing = 15;

        // Section 1: Labels with different styles
        var sectionLabel1 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 1: LABELS ===", 
            font, Color.Yellow, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel1);
        currentY += 40;

        for (int i = 0; i < 8; i++)
        {
            var label = new Label(
                new Rectangle(10 + (i % 4) * 180, currentY + (i / 4) * 40, 170, 35),
                $"Label {i + 1}\nMultiline text\nClip test!",
                font,
                Color.White,
                GetPositionColor(i, 0)
            );
            _scrollArea.AddChild(label);
        }
        currentY += 100;

        // Section 2: Regular Buttons
        var sectionLabel2 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 2: REGULAR BUTTONS ===", 
            font, Color.Cyan, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel2);
        currentY += 40;

        for (int i = 0; i < 6; i++)
        {
            var button = new Button(
                new Rectangle(10 + (i % 3) * 200, currentY + (i / 3) * 50, 190, 45),
                $"Button {i + 1}",
                font,
                Color.DarkGreen,
                Color.Green,
                Color.White,
                () => UpdateStatus($"Regular Button {i + 1} clicked!")
            );
            _scrollArea.AddChild(button);
        }
        currentY += 120;

        // Section 3: Image Buttons
        var sectionLabel3 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 3: IMAGE BUTTONS ===", 
            font, Color.Orange, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel3);
        currentY += 40;

        for (int i = 0; i < 8; i++)
        {
            var texture = (i % 4) switch
            {
                0 => texture1,
                1 => texture2,
                2 => texture3,
                _ => texture4
            };

            var imageButton = new ImageButton(
                new Rectangle(10 + (i % 4) * 150, currentY + (i / 4) * 80, 140, 70),
                texture,
                () => UpdateStatus($"Image Button {i + 1} clicked!"),
                tintColor: Color.White,
                hoverTintColor: Color.Yellow,
                drawBackground: true,
                backgroundColor: GetPositionColor(i, 1),
                hoverBackgroundColor: Color.LightGray
            );
            _scrollArea.AddChild(imageButton);
        }
        currentY += 180;

        // Section 4: UIImages
        var sectionLabel4 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 4: UI IMAGES ===", 
            font, Color.Lime, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel4);
        currentY += 40;

        for (int i = 0; i < 12; i++)
        {
            var texture = (i % 4) switch
            {
                0 => texture1,
                1 => texture2,
                2 => texture3,
                _ => texture4
            };

            var image = new UIImage(
                texture,
                new Rectangle(10 + (i % 6) * 120, currentY + (i / 6) * 90, 110, 80)
            );
            _scrollArea.AddChild(image);
        }
        currentY += 200;

        // Section 5: Mixed Layout Groups (if they exist in scroll area)
        var sectionLabel5 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 5: LAYOUT GROUPS ===", 
            font, Color.Pink, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel5);
        currentY += 40;

        // Create a horizontal layout group inside the scroll area
        var horizontalGroup = new HorizontalLayoutGroup(new Rectangle(10, currentY, 700, 60), 10);
        for (int i = 0; i < 5; i++)
        {
            var groupButton = new Button(
                new Rectangle(0, 0, 120, 50),
                $"Group {i + 1}",
                font,
                Color.DarkBlue,
                Color.Blue,
                Color.White,
                () => UpdateStatus($"Layout Group Button {i + 1} clicked!")
            );
            horizontalGroup.AddChild(groupButton);
        }
        _scrollArea.AddChild(horizontalGroup);
        currentY += 80;

        // Create a vertical layout group
        var verticalGroup = new VerticalLayoutGroup(new Rectangle(10, currentY, 300, 200), 5);
        for (int i = 0; i < 4; i++)
        {
            var groupLabel = new Label(
                new Rectangle(0, 0, 280, 45),
                $"Vertical Group Item {i + 1}",
                font,
                Color.Black,
                GetPositionColor(i, 2)
            );
            verticalGroup.AddChild(groupLabel);
        }
        _scrollArea.AddChild(verticalGroup);
        currentY += 220;

        // Section 6: Grid Layout
        var sectionLabel6 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 6: GRID LAYOUT ===", 
            font, Color.Violet, Color.DarkBlue
        );
        _scrollArea.AddChild(sectionLabel6);
        currentY += 40;

        var gridGroup = new GridLayoutGroup(new Rectangle(10, currentY, 600, 300), 4, 3, 10);
        for (int i = 0; i < 12; i++)
        {
            var gridItem = new Label(
                new Rectangle(0, 0, 140, 90),
                $"Grid {i + 1}\nRow {i / 4}\nCol {i % 4}",
                font,
                Color.White,
                GetPositionColor(i, 3)
            );
            gridGroup.AddChild(gridItem);
        }
        _scrollArea.AddChild(gridGroup);
        currentY += 320;

        // Section 7: Extreme positions for clipping test
        var sectionLabel7 = new Label(
            new Rectangle(10, currentY, 600, 30),
            "=== SECTION 7: EXTREME POSITIONS ===", 
            font, Color.Red, Color.Yellow
        );
        _scrollArea.AddChild(sectionLabel7);
        currentY += 40;

        // Elements that extend way beyond normal bounds
        var extremeRight = new Label(
            new Rectangle(1500, currentY, 300, 50),
            "EXTREME RIGHT - Should clip until horizontal scroll!",
            font, Color.White, Color.Red
        );
        _scrollArea.AddChild(extremeRight);

        var extremeDown = new Label(
            new Rectangle(10, currentY + 500, 400, 50),
            "EXTREME DOWN - Test vertical clipping!",
            font, Color.White, Color.Blue
        );
        _scrollArea.AddChild(extremeDown);

        // Far corner test
        var extremeCorner = new ImageButton(
            new Rectangle(1200, currentY + 400, 200, 100),
            texture1,
            () => UpdateStatus("Extreme corner button clicked! Scrolling works!"),
            tintColor: Color.White,
            hoverTintColor: Color.Yellow,
            drawBackground: true,
            backgroundColor: Color.Purple,
            hoverBackgroundColor: Color.Magenta
        );
        _scrollArea.AddChild(extremeCorner);

        // Test negative positions (should be clipped)
        var negativePos = new Label(
            new Rectangle(-100, currentY, 200, 50),
            "NEGATIVE X - Should be clipped!",
            font, Color.White, Color.Orange
        );
        _scrollArea.AddChild(negativePos);
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

    private void ShowToast(SpriteFont font)
    {
        try
        {
            var toast = Toast.Show(
                "This is a toast notification! 🎉", 
                font, 
                new Rectangle(0, 0, 1200, 900),
                Toast.ToastType.Info,
                3.0f
            );
            Core.UISystem.AddElement(toast);
            UpdateStatus("Toast notification shown!");
        }
        catch (System.Exception ex)
        {
            UpdateStatus($"Error showing toast: {ex.Message}");
        }
    }

    private void ShowModal(SpriteFont font)
    {
        try
        {
            var modal = new Modal(
                new Rectangle(0, 0, 1200, 900),
                new Rectangle(300, 200, 600, 400),
                "Example Modal Dialog",
                font,
                font,
                Color.Black * 0.5f, // Semi-transparent overlay
                Color.White,
                Color.DarkBlue,
                () => UpdateStatus("Modal closed")
            );

            // Add some content to the modal
            var modalLabel = new Label(
                new Rectangle(20, 20, 560, 100),
                "This is a modal dialog example!\n\nIt demonstrates that modals work alongside ScrollAreas.\n\nClick the X or outside to close.",
                font,
                Color.Black,
                Color.White
            );
            modal.AddContentElement(modalLabel);

            var modalButton = new Button(
                new Rectangle(20, 140, 150, 40),
                "Modal Button",
                font,
                Color.DarkGreen,
                Color.Green,
                Color.White,
                () => UpdateStatus("Modal button clicked!")
            );
            modal.AddContentElement(modalButton);

            Core.UISystem.AddElement(modal);
            UpdateStatus("Modal dialog shown!");
        }
        catch (System.Exception ex)
        {
            UpdateStatus($"Error showing modal: {ex.Message}");
        }
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}
