using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot;
using Peridot.UI;

public class FontTestExample : IExample
{
    private UIElement _rootElement;
    private TextArea _diagnosticsDisplay;
    private SpriteFont _testFont;

    public void Initialize(SpriteFont font)
    {
        _testFont = font;
        
        // Create main layout
        var mainLayout = new VerticalLayoutGroup(new Rectangle(50, 50, 1100, 800), 10);

        // Title
        var titleLabel = new Label(
            new Rectangle(0, 0, 1100, 40), 
            "Font Rendering Diagnostics and Test", 
            font, 
            Color.Yellow, 
            Color.DarkBlue
        );

        // Run diagnostics
        var diagnostics = FontDebugger.AnalyzeFont(font);
        var diagnosticsReport = FontDebugger.GenerateReport(diagnostics);

        // Display diagnostics in a scrollable text area
        _diagnosticsDisplay = new TextArea(
            new Rectangle(0, 0, 1080, 300),
            font,
            "",
            wordWrap: true,
            readOnly: true,
            backgroundColor: Color.Black,
            textColor: Color.Lime
        );
        _diagnosticsDisplay.Text = diagnosticsReport;

        // Add font test buttons
        var buttonLayout = new HorizontalLayoutGroup(new Rectangle(0, 0, 1080, 50), 10);
        
        var refreshButton = new Button(
            new Rectangle(0, 0, 150, 40),
            "Refresh Diagnostics",
            font,
            Color.DarkBlue,
            Color.Blue,
            Color.White,
            RefreshDiagnostics
        );

        var testKerningButton = new Button(
            new Rectangle(0, 0, 120, 40),
            "Test Kerning",
            font,
            Color.DarkGreen,
            Color.Green,
            Color.White,
            () => TestKerning(font)
        );

        var testSpacingButton = new Button(
            new Rectangle(0, 0, 120, 40),
            "Test Spacing",
            font,
            Color.DarkOrange,
            Color.Orange,
            Color.White,
            () => TestSpacing(font)
        );

        var testUnicodeButton = new Button(
            new Rectangle(0, 0, 120, 40),
            "Test Unicode",
            font,
            Color.DarkRed,
            Color.Red,
            Color.White,
            () => TestUnicode(font)
        );

        buttonLayout.AddChild(refreshButton);
        buttonLayout.AddChild(testKerningButton);
        buttonLayout.AddChild(testSpacingButton);
        buttonLayout.AddChild(testUnicodeButton);

        // Create visual test areas
        var visualTestLabel = new Label(
            new Rectangle(0, 0, 1080, 30),
            "Visual Font Tests (check for kerning/spacing issues):",
            font,
            Color.Cyan
        );

        // Kerning test display
        var kerningTestArea = new TextArea(
            new Rectangle(0, 0, 540, 150),
            font,
            GetKerningTestText(),
            wordWrap: true,
            readOnly: true,
            backgroundColor: Color.White,
            textColor: Color.Black
        );

        // Spacing test display  
        var spacingTestArea = new TextArea(
            new Rectangle(0, 0, 540, 150),
            font,
            GetSpacingTestText(),
            wordWrap: true,
            readOnly: true,
            backgroundColor: Color.LightGray,
            textColor: Color.Black
        );

        var testLayout = new HorizontalLayoutGroup(new Rectangle(0, 0, 1080, 150), 10);
        testLayout.AddChild(kerningTestArea);
        testLayout.AddChild(spacingTestArea);

        // Instructions
        var instructionsArea = new TextArea(
            new Rectangle(0, 0, 1080, 120),
            font,
            GetInstructionsText(),
            wordWrap: true,
            readOnly: true,
            backgroundColor: Color.DarkBlue,
            textColor: Color.White
        );

        // Add all elements to main layout
        mainLayout.AddChild(titleLabel);
        mainLayout.AddChild(_diagnosticsDisplay);
        mainLayout.AddChild(buttonLayout);
        mainLayout.AddChild(visualTestLabel);
        mainLayout.AddChild(testLayout);
        mainLayout.AddChild(instructionsArea);

        _rootElement = mainLayout;
    }

    private void RefreshDiagnostics()
    {
        var diagnostics = FontDebugger.AnalyzeFont(_testFont);
        var report = FontDebugger.GenerateReport(diagnostics);
        _diagnosticsDisplay.Text = report;
    }

    private void TestKerning(SpriteFont font)
    {
        var kerningPairs = new string[] 
        { 
            "AV", "Ta", "To", "Tr", "Tu", "Tw", "Ty", 
            "Ya", "Ye", "Yo", "Yu", "PA", "VA", "WA", 
            "Wa", "We", "Wi", "Wo", "Wu", "FA", "LT", "LY"
        };

        var result = new System.Text.StringBuilder();
        result.AppendLine("=== KERNING TEST RESULTS ===");
        result.AppendLine();

        foreach (var pair in kerningPairs)
        {
            if (pair.All(c => font.Characters.Contains(c)))
            {
                var pairWidth = font.MeasureString(pair).X;
                var char1Width = font.MeasureString(pair[0].ToString()).X;
                var char2Width = font.MeasureString(pair[1].ToString()).X;
                var expectedWidth = char1Width + char2Width;
                var kerningOffset = pairWidth - expectedWidth;
                
                result.AppendLine($"'{pair}': Actual={pairWidth:F2}px, Expected={expectedWidth:F2}px, Kerning={kerningOffset:F2}px");
            }
            else
            {
                result.AppendLine($"'{pair}': Missing characters");
            }
        }

        _diagnosticsDisplay.Text = result.ToString();
    }

    private void TestSpacing(SpriteFont font)
    {
        var testChars = new char[] { 'i', 'l', 'I', '1', '|', 'W', 'M', ' ', '.', ',' };
        
        var result = new System.Text.StringBuilder();
        result.AppendLine("=== SPACING TEST RESULTS ===");
        result.AppendLine();

        foreach (var c in testChars)
        {
            if (font.Characters.Contains(c))
            {
                var width = font.MeasureString(c.ToString()).X;
                var displayChar = c == ' ' ? "SPACE" : c.ToString();
                result.AppendLine($"'{displayChar}': {width:F2}px");
            }
            else
            {
                result.AppendLine($"'{c}': Not available in font");
            }
        }

        result.AppendLine();
        result.AppendLine("CHARACTER CONSISTENCY CHECK:");
        
        // Test repeated characters for consistency
        var testRepeated = "iiiii lllll IIIII 11111 |||||";
        var repeatedWidth = font.MeasureString(testRepeated).X;
        var singleWidths = testRepeated.GroupBy(c => c)
            .ToDictionary(g => g.Key, g => font.MeasureString(g.Key.ToString()).X * g.Count());
        
        result.AppendLine($"Repeated string '{testRepeated}' width: {repeatedWidth:F2}px");
        foreach (var kvp in singleWidths)
        {
            var displayChar = kvp.Key == ' ' ? "SPACE" : kvp.Key.ToString();
            result.AppendLine($"  {displayChar} × count: {kvp.Value:F2}px");
        }

        _diagnosticsDisplay.Text = result.ToString();
    }

    private void TestUnicode(SpriteFont font)
    {
        var unicodeTests = new (char character, string name)[]
        {
            ('\u2013', "En Dash"),
            ('\u2014', "Em Dash"), 
            ('\u2018', "Left Single Quote"),
            ('\u2019', "Right Single Quote"),
            ('\u201C', "Left Double Quote"),
            ('\u201D', "Right Double Quote"),
            ('\u2022', "Bullet Point"),
            ('\u2026', "Ellipsis"),
            ('\u00A9', "Copyright"),
            ('\u00AE', "Registered"),
            ('\u2122', "Trademark")
        };

        var result = new System.Text.StringBuilder();
        result.AppendLine("=== UNICODE CHARACTER TEST ===");
        result.AppendLine();

        foreach (var (character, name) in unicodeTests)
        {
            if (font.Characters.Contains(character))
            {
                var width = font.MeasureString(character.ToString()).X;
                result.AppendLine($"✓ {name} (U+{((int)character):X4}): '{character}' - {width:F2}px");
            }
            else
            {
                result.AppendLine($"✗ {name} (U+{((int)character):X4}): Missing from font");
            }
        }

        result.AppendLine();
        result.AppendLine("DEFAULT CHARACTER TEST:");
        if (font.DefaultCharacter.HasValue)
        {
            result.AppendLine($"Default character: '{font.DefaultCharacter}' (U+{((int)font.DefaultCharacter.Value):X4})");
            
            // Test what happens with an unsupported character
            try
            {
                var testText = "Test\u2603Text"; // Snowman character (likely unsupported)
                var measurement = font.MeasureString(testText);
                result.AppendLine($"Unsupported char test: '{testText}' measures {measurement.X:F2}px");
            }
            catch (Exception ex)
            {
                result.AppendLine($"Error with unsupported character: {ex.Message}");
            }
        }
        else
        {
            result.AppendLine("No default character set - unsupported characters may cause errors!");
        }

        _diagnosticsDisplay.Text = result.ToString();
    }

    private string GetKerningTestText()
    {
        return "KERNING TEST PAIRS:\n\n" +
               "AV Ta To Tr Tu Tw Ty\n" +
               "Ya Ye Yo Yu PA VA WA\n" +
               "Wa We Wi Wo Wu FA LT LY\n\n" +
               "Common kerning issues:\n" +
               "AVATAR WATER TOWER AWAY\n" +
               "Typography Kerning Test\n" +
               "ff fi fl ffi ffl";
    }

    private string GetSpacingTestText()
    {
        return "SPACING TEST:\n\n" +
               "Narrow chars: i l | 1 I\n" +
               "Wide chars: W M @ # %\n" +
               "Medium chars: n o e a s\n\n" +
               "Spacing consistency:\n" +
               "iiiii lllll IIIII\n" +
               "WWWWW MMMMM @@@@@\n" +
               "The quick brown fox\n" +
               "jumps over lazy dog.";
    }

    private string GetInstructionsText()
    {
        return "FONT RENDERING ISSUE DIAGNOSIS:\n\n" +
               "1. Check the diagnostics report above for font configuration issues\n" +
               "2. Look at the visual tests for kerning and spacing problems\n" +
               "3. Use the test buttons to run specific checks\n" +
               "4. Common fixes: Change TextureFormat from Compressed to Color, ensure UseKerning=true, add DefaultCharacter\n" +
               "5. If characters look blurry, check SpriteBatch SamplerState (use LinearClamp for fonts)\n" +
               "6. For missing characters, expand CharacterRegions in .spritefont files";
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}