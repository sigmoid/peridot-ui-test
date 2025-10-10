using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot.UI;
using Peridot.UI.Builder;

/// <summary>
/// Example usage of the UI markup parser and builder system
/// </summary>
public class UIBuilderExample : IExample
{
    private SpriteFont _font;
    private UIBuilder _builder;
    private UIElement _rootElement;

    public void Initialize(SpriteFont font)
    {
        Console.WriteLine("UIBuilderExample: Initialize started");
        _font = font;

        _builder = new UIBuilder(_font);
        RegisterEventHandlers();
        BuildExampleUI();
    }

    private void RegisterEventHandlers()
    {
        // Register button click handlers
        _builder.RegisterEventHandler("ShowHelpDialog", _ => ShowHelpDialog());
        _builder.RegisterEventHandler("ShowSettingsDialog", _ => ShowSettingsDialog());
        _builder.RegisterEventHandler("TestAction", _ => TestAction());
        _builder.RegisterEventHandler("PrintTextInputValue", _ => PrintTextInputValue());
    }

    private void BuildExampleUI()
    {
        var markup = @"
<canvas bounds=""0,0,1200,900"" backgroundColor=""#2c3e50"" clipToBounds=""true"" name=""mainCanvas"">
    
    <!-- Simple Test Layout -->
    <div bounds=""50,50,1100,800"" spacing=""20"" direction=""vertical"" name=""mainContainer"">
        
        <label bounds=""0,0,400,60"" 
               text=""UI Builder Test"" 
               textColor=""#ffffff""
               name=""titleLabel"" />
        
        <button bounds=""0,0,200,50"" 
                text=""Test Button"" 
                textColor=""#ffffff"" 
                backgroundColor=""#27ae60"" 
                hoverColor=""#2ecc71"" 
                onClick=""TestAction""
                name=""testButton"" />
                
        <button bounds=""0,0,200,50"" 
                text=""Help"" 
                textColor=""#ffffff"" 
                backgroundColor=""#3498db"" 
                hoverColor=""#2980b9"" 
                onClick=""ShowHelpDialog""
                name=""helpButton"" />
                
        <!-- Additional nested container to test recursive search -->
        <div bounds=""0,0,500,200"" spacing=""10"" direction=""horizontal"" name=""nestedContainer"">
            
            <label bounds=""0,0,150,30"" 
                   text=""Nested Label"" 
                   textColor=""#ffffff""
                   name=""nestedLabel"" />
                   
            <button bounds=""0,0,120,30"" 
                    text=""Nested Button"" 
                    textColor=""#ffffff"" 
                    backgroundColor=""#e74c3c"" 
                    hoverColor=""#c0392b"" 
                    onClick=""TestAction""
                    name=""nestedButton"" />
                    
        </div>
        
        <!-- Text Input Test Section -->
        <div bounds=""0,0,600,120"" spacing=""10"" direction=""vertical"" name=""textInputTestSection"">
            
            <label bounds=""0,0,300,30"" 
                   text=""Text Input Search Test:"" 
                   textColor=""#ffffff""
                   name=""textInputLabel"" />
            
            <input bounds=""0,0,400,35"" 
                   placeholder=""Type something here..."" 
                   backgroundColor=""#ffffff"" 
                   textColor=""#000000"" 
                   borderColor=""#cccccc"" 
                   focusedBorderColor=""#3498db"" 
                   padding=""8"" 
                   borderWidth=""2""
                   name=""testTextInput"" />
                   
            <button bounds=""0,0,200,40"" 
                    text=""Print Text to Console"" 
                    textColor=""#ffffff"" 
                    backgroundColor=""#9b59b6"" 
                    hoverColor=""#8e44ad"" 
                    onClick=""PrintTextInputValue""
                    name=""printTextButton"" />
                    
        </div>
                
    </div>
    
</canvas>";

        try 
        {
            _rootElement = _builder.BuildFromMarkup(markup);
            Console.WriteLine("UI markup parsed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse UI markup: {ex.Message}");
            
            // Create fallback UI
            _rootElement = CreateFallbackUI();
        }
    }

    private UIElement CreateFallbackUI()
    {
        var canvas = new Canvas(new Rectangle(0, 0, 1200, 900), Color.Red);
        var label = new Label(new Rectangle(50, 50, 500, 100), 
            "Failed to parse markup! Check console for errors.", 
            _font, Color.White, Color.Red);
        canvas.AddChild(label);
        return canvas;
    }

    // Event handler implementations
    private void ShowHelpDialog()
    {
        Console.WriteLine("Help: This UI was built using the UI Markup Parser!");
        Console.WriteLine("The entire interface is defined with HTML-like markup syntax.");
        Console.WriteLine("All elements have 'name' attributes that can be used for searching.");
        Console.WriteLine("Click the Test Button to see a demonstration of recursive element search!");
        Console.WriteLine("Try typing in the text input and clicking 'Print Text to Console' to see");
        Console.WriteLine("how the search functionality can be used to retrieve text from named inputs!");
    }

    private void ShowSettingsDialog()
    {
        Console.WriteLine("Settings dialog would appear here");
    }

    private void TestAction()
    {
        Console.WriteLine("Test action executed!");
        DemonstrateElementSearch();
        
        // Also run the comprehensive search test
        Console.WriteLine("\nRunning comprehensive search test...");
        Peridot.UI.SearchTest.RunSearchTest(_font);
    }

    private void DemonstrateElementSearch()
    {
        Console.WriteLine("\n=== Demonstrating Element Search ===");
        
        // Try searching from the root element (Canvas)
        if (_rootElement is Canvas canvas)
        {
            // Search for individual elements by name
            Console.WriteLine("Searching for elements by name:");
            
            var titleLabel = canvas.FindChildByName("titleLabel");
            Console.WriteLine($"Found titleLabel: {titleLabel != null} (Type: {titleLabel?.GetType().Name})");
            
            var testButton = canvas.FindChildByName("testButton");
            Console.WriteLine($"Found testButton: {testButton != null} (Type: {testButton?.GetType().Name})");
            
            var nestedLabel = canvas.FindChildByName("nestedLabel");
            Console.WriteLine($"Found nestedLabel (recursive): {nestedLabel != null} (Type: {nestedLabel?.GetType().Name})");
            
            var nestedButton = canvas.FindChildByName("nestedButton");
            Console.WriteLine($"Found nestedButton (recursive): {nestedButton != null} (Type: {nestedButton?.GetType().Name})");
            
            var nonExistent = canvas.FindChildByName("nonExistentElement");
            Console.WriteLine($"Found nonExistentElement: {nonExistent != null}");
            
            // Demonstrate searching for multiple elements (if we had multiple with same name)
            var allButtons = canvas.FindAllChildrenByName("testButton");
            Console.WriteLine($"Found {allButtons.Count} elements with name 'testButton'");
            
            // Try searching from a nested container
            var mainContainer = canvas.FindChildByName("mainContainer");
            if (mainContainer is LayoutGroup layoutGroup)
            {
                Console.WriteLine("\nSearching from nested container:");
                var nestedFromContainer = layoutGroup.FindChildByName("nestedLabel");
                Console.WriteLine($"Found nestedLabel from mainContainer: {nestedFromContainer != null}");
                
                // This should not find the title label as it's at the same level, not a child
                var titleFromNested = layoutGroup.FindChildByName("titleLabel");
                Console.WriteLine($"Found titleLabel from mainContainer: {titleFromNested != null}");
            }
        }
        
        Console.WriteLine("=== End Element Search Demo ===\n");
    }

    private void PrintTextInputValue()
    {
        Console.WriteLine("\n=== Text Input Search Test ===");
        
        if (_rootElement is Canvas canvas)
        {
            // Use the search functionality to find the named text input
            var textInput = canvas.FindChildByName("testTextInput");
            
            if (textInput != null)
            {
                Console.WriteLine($"Found text input element: {textInput.GetType().Name}");
                
                // Cast to TextInput and get the current text value
                if (textInput is TextInput input)
                {
                    string currentText = input.Text;
                    Console.WriteLine($"Current text in input: '{currentText}'");
                    
                    if (string.IsNullOrEmpty(currentText))
                    {
                        Console.WriteLine("The text input is empty. Try typing something first!");
                    }
                }
                else
                {
                    Console.WriteLine("Element found but it's not a TextInput!");
                }
            }
            else
            {
                Console.WriteLine("Could not find text input element with name 'testTextInput'");
            }
        }
        else
        {
            Console.WriteLine("Root element is not a Canvas");
        }
        
        Console.WriteLine("=== End Text Input Test ===\n");
    }

    public UIElement GetRootElement()
    {
        return _rootElement;
    }
}