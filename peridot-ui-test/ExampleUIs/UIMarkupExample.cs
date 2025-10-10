using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Peridot.UI;
using Peridot.UI.Builder;

namespace Peridot.UI.Examples
{
    public class UIMarkupExample : IExample
    {
        private UIElement _rootElement;
        private UIBuilder _builder;
        private string _currentUserName = "";
        private float _masterVolume = 75f;
        private float _musicVolume = 60f;
        private float _sfxVolume = 85f;

        public void Initialize(SpriteFont font)
        {
            _builder = new UIBuilder(font);
            
            // Register all event handlers
            RegisterEventHandlers();
            
            // Build UI from markup
            BuildUI();
        }

        private void RegisterEventHandlers()
        {
            // Main navigation handlers
            _builder.RegisterEventHandler("ShowHelp", _ => ShowMessage("Help", "This is the UI Markup Example!\n\nThis entire interface was built using HTML-like markup syntax and parsed into actual UI elements."));
            _builder.RegisterEventHandler("ShowAbout", _ => ShowMessage("About", "UI Markup Parser v1.0\n\nBuilt with Peridot UI Framework\nDemonstrating declarative UI creation"));
            
            // User input handlers
            _builder.RegisterEventHandler("HandleNameChanged", text => {
                _currentUserName = text;
                Console.WriteLine($"Name changed to: {text}");
            });
            
            _builder.RegisterEventHandler("SubmitForm", _ => {
                if (!string.IsNullOrEmpty(_currentUserName))
                {
                    ShowMessage("Welcome!", $"Hello, {_currentUserName}!\n\nWelcome to the UI Markup Example.");
                }
                else
                {
                    ShowMessage("Error", "Please enter your name first.");
                }
            });
            
            // Volume control handlers
            _builder.RegisterEventHandler("HandleMasterVolume", value => {
                _masterVolume = float.Parse(value);
                Console.WriteLine($"Master Volume: {_masterVolume}%");
                UpdateVolumeDisplay();
            });
            
            _builder.RegisterEventHandler("HandleMusicVolume", value => {
                _musicVolume = float.Parse(value);
                Console.WriteLine($"Music Volume: {_musicVolume}%");
                UpdateVolumeDisplay();
            });
            
            _builder.RegisterEventHandler("HandleSfxVolume", value => {
                _sfxVolume = float.Parse(value);
                Console.WriteLine($"SFX Volume: {_sfxVolume}%");
                UpdateVolumeDisplay();
            });
            
            // Action button handlers
            _builder.RegisterEventHandler("ActionPrimary", _ => ShowMessage("Action", "Primary action executed!"));
            _builder.RegisterEventHandler("ActionSecondary", _ => ShowMessage("Action", "Secondary action executed!"));
            _builder.RegisterEventHandler("ActionSuccess", _ => ShowMessage("Success", "Operation completed successfully!"));
            _builder.RegisterEventHandler("ActionWarning", _ => ShowMessage("Warning", "This is a warning message."));
            _builder.RegisterEventHandler("ActionDanger", _ => ShowMessage("Danger", "Danger zone accessed! Be careful."));
            
            // Demo actions
            _builder.RegisterEventHandler("ResetForm", _ => ResetForm());
            _builder.RegisterEventHandler("RandomizeVolume", _ => RandomizeVolumes());
            _builder.RegisterEventHandler("ShowMarkup", _ => ShowMarkupSource());
        }

        private void BuildUI()
        {
            var markup = @"
<canvas bounds=""0,0,1200,900"" backgroundColor=""#34495e"" clipToBounds=""true"">
    
    <!-- Header Section -->
    <div bounds=""0,0,1200,80"" spacing=""15"" direction=""horizontal""
         horizontalAlignment=""Center"" 
         verticalAlignment=""Center"" 
         backgroundColor=""#2c3e50"">
        
        <label bounds=""0,0,300,50"" 
               text=""UI Markup Example"" 
               textColor=""#ecf0f1"" 
               backgroundColor=""#3498db"" />
        
        <button bounds=""0,0,100,40"" 
                text=""Help"" 
                textColor=""#ffffff"" 
                backgroundColor=""#27ae60"" 
                hoverColor=""#2ecc71"" 
                onClick=""ShowHelp"" />
                
        <button bounds=""0,0,100,40"" 
                text=""About"" 
                textColor=""#ffffff"" 
                backgroundColor=""#8e44ad"" 
                hoverColor=""#9b59b6"" 
                onClick=""ShowAbout"" />
                
    </div>

    <!-- Main Content -->
    <div bounds=""20,100,1160,780"" spacing=""20"">
        
        <!-- User Registration Section -->
        <div bounds=""0,0,1160,100"" spacing=""15"" direction=""horizontal""
             horizontalAlignment=""Left"" 
             verticalAlignment=""Center"" 
             backgroundColor=""#ecf0f1"">
            
            <label bounds=""0,0,180,40"" 
                   text=""Enter your name:"" 
                   textColor=""#2c3e50"" />
            
            <input bounds=""0,0,350,40"" 
                   placeholder=""Type your full name here..."" 
                   backgroundColor=""#ffffff"" 
                   textColor=""#2c3e50"" 
                   borderColor=""#bdc3c7"" 
                   focusedBorderColor=""#3498db"" 
                   padding=""10"" 
                   borderWidth=""2"" 
                   maxLength=""100"" 
                   onTextChanged=""HandleNameChanged"" 
                   onEnterPressed=""SubmitForm"" />
            
            <button bounds=""0,0,120,40"" 
                    text=""Submit"" 
                    textColor=""#ffffff"" 
                    backgroundColor=""#e74c3c"" 
                    hoverColor=""#c0392b"" 
                    onClick=""SubmitForm"" />
            
            <button bounds=""0,0,100,40"" 
                    text=""Reset"" 
                    textColor=""#ffffff"" 
                    backgroundColor=""#95a5a6"" 
                    hoverColor=""#7f8c8d"" 
                    onClick=""ResetForm"" />
                    
        </div>

        <!-- Controls Demo Section -->
        <div bounds=""0,0,1160,300"" spacing=""20"" direction=""horizontal"">
            
            <!-- Volume Controls -->
            <div bounds=""0,0,500,300"" spacing=""15"" backgroundColor=""#ffffff"">
                
                <label bounds=""0,0,500,40"" 
                       text=""Audio Volume Controls"" 
                       textColor=""#2c3e50"" 
                       backgroundColor=""#f39c12"" />
                
                <div bounds=""0,0,480,60"" spacing=""10"" direction=""horizontal"" 
                     verticalAlignment=""Center"">
                    <label bounds=""0,0,120,30"" 
                           text=""Master Volume:"" 
                           textColor=""#34495e"" />
                    <slider bounds=""0,0,250,30"" 
                            minValue=""0"" 
                            maxValue=""100"" 
                            initialValue=""75"" 
                            step=""1"" 
                            isHorizontal=""true"" 
                            trackColor=""#bdc3c7"" 
                            fillColor=""#3498db"" 
                            handleColor=""#ffffff"" 
                            handleHoverColor=""#ecf0f1"" 
                            handlePressedColor""#bdc3c7"" 
                            trackHeight=""8"" 
                            handleSize""22"" 
                            onValueChanged=""HandleMasterVolume"" />
                    <label bounds=""0,0,80,30"" 
                           name=""masterVolumeLabel"" 
                           text=""75%"" 
                           textColor""#27ae60"" />
                </div>
                
                <div bounds=""0,0,480,60"" spacing=""10"" direction=""horizontal"" 
                     verticalAlignment=""Center"">
                    <label bounds=""0,0,120,30"" 
                           text=""Music Volume:"" 
                           textColor=""#34495e"" />
                    <slider bounds=""0,0,250,30"" 
                            minValue=""0"" 
                            maxValue=""100"" 
                            initialValue=""60"" 
                            step=""5"" 
                            isHorizontal=""true"" 
                            trackColor""#bdc3c7"" 
                            fillColor=""#e74c3c"" 
                            handleColor""#ffffff"" 
                            onValueChanged=""HandleMusicVolume"" />
                    <label bounds=""0,0,80,30"" 
                           name=""musicVolumeLabel"" 
                           text""60%"" 
                           textColor""#e74c3c"" />
                </div>
                
                <div bounds=""0,0,480,60"" spacing=""10"" direction=""horizontal"" 
                     verticalAlignment""Center"">
                    <label bounds=""0,0,120,30"" 
                           text""SFX Volume:"" 
                           textColor""#34495e"" />
                    <slider bounds""0,0,250,30"" 
                            minValue""0"" 
                            maxValue""100"" 
                            initialValue""85"" 
                            step""1"" 
                            isHorizontal""true"" 
                            trackColor""#bdc3c7"" 
                            fillColor""#f39c12"" 
                            handleColor""#ffffff"" 
                            onValueChanged""HandleSfxVolume"" />
                    <label bounds""0,0,80,30"" 
                           name""sfxVolumeLabel"" 
                           text""85%"" 
                           textColor""#f39c12"" />
                </div>
                
                <button bounds=""0,0,200,40"" 
                        text""Randomize Volumes"" 
                        textColor""#ffffff"" 
                        backgroundColor""#9b59b6"" 
                        hoverColor""#8e44ad"" 
                        onClick""RandomizeVolume"" />
                
            </div>
            
            <!-- Action Buttons -->
            <div bounds""0,0,620,300"" spacing""15"" backgroundColor""#f8f9fa"">
                
                <label bounds""0,0,620,40"" 
                       text""Action Buttons Demo"" 
                       textColor""#495057"" 
                       backgroundColor""#dee2e6"" />
                
                <div bounds""0,0,600,200"" direction""grid""
                     columns""3"" 
                     spacing""15"">
                    
                    <button bounds""0,0,180,60"" 
                            text""Primary Action"" 
                            textColor""#ffffff"" 
                            backgroundColor""#007bff"" 
                            hoverColor""#0056b3"" 
                            onClick""ActionPrimary"" />
                    
                    <button bounds""0,0,180,60"" 
                            text""Secondary"" 
                            textColor""#ffffff"" 
                            backgroundColor""#6c757d"" 
                            hoverColor""#545b62"" 
                            onClick""ActionSecondary"" />
                    
                    <button bounds""0,0,180,60"" 
                            text=""Success"" 
                            textColor=""#ffffff"" 
                            backgroundColor=""#28a745"" 
                            hoverColor=""#1e7e34"" 
                            onClick=""ActionSuccess"" />
                    
                    <button bounds=""0,0,180,60"" 
                            text=""Warning"" 
                            textColor=""#212529"" 
                            backgroundColor=""#ffc107"" 
                            hoverColor=""#d39e00"" 
                            onClick=""ActionWarning"" />
                    
                    <button bounds=""0,0,180,60"" 
                            text=""Danger"" 
                            textColor=""#ffffff"" 
                            backgroundColor=""#dc3545"" 
                            hoverColor=""#bd2130"" 
                            onClick=""ActionDanger"" />
                    
                    <button bounds=""0,0,180,60"" 
                            text=""Show Markup"" 
                            textColor=""#ffffff"" 
                            backgroundColor=""#17a2b8"" 
                            hoverColor=""#117a8b"" 
                            onClick=""ShowMarkup"" />
                            
                </div>
                
            </div>
            
        </div>

        <!-- Status Footer -->
        <div bounds=""0,0,1160,60"" spacing=""20"" direction=""horizontal""
             horizontalAlignment=""Center"" 
             verticalAlignment=""Center"" 
             backgroundColor=""#2c3e50"">
            
            <label bounds=""0,0,400,30"" 
                   text=""Built with UI Markup Parser"" 
                   textColor=""#ecf0f1"" />
            
            <label bounds=""0,0,300,30"" 
                   text=""Peridot UI Framework"" 
                   textColor""#3498db"" />
                   
        </div>
        
    </div>
    
</canvas>";

            try
            {
                _rootElement = _builder.BuildFromMarkup(markup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to build UI from markup: {ex.Message}");
                
                // Create fallback UI
                _rootElement = CreateFallbackUI();
            }
        }

        private UIElement CreateFallbackUI()
        {
            var canvas = new Canvas(new Rectangle(0, 0, 1200, 900), Color.Red);
            var label = new Label(new Rectangle(50, 50, 500, 100), 
                "Failed to parse markup! Check console for errors.", 
                null, Color.White, Color.Red);
            canvas.AddChild(label);
            return canvas;
        }

        private void ShowMessage(string title, string message)
        {
            Console.WriteLine($"[{title}] {message}");
            
            // In a real application, you would show a modal dialog here
            // For now, we'll just log to console
        }

        private void UpdateVolumeDisplay()
        {
            // In a real application, you would find the volume labels by ID and update them
            Console.WriteLine($"Volume Update - Master: {_masterVolume}%, Music: {_musicVolume}%, SFX: {_sfxVolume}%");
        }

        private void ResetForm()
        {
            _currentUserName = "";
            Console.WriteLine("Form reset");
            
            // In a real application, you would clear the input field here
        }

        private void RandomizeVolumes()
        {
            _masterVolume = Random.Next(0, 101);
            _musicVolume = Random.Next(0, 101);
            _sfxVolume = Random.Next(0, 101);

            Console.WriteLine($"Randomized volumes - Master: {_masterVolume}%, Music: {_musicVolume}%, SFX: {_sfxVolume}%");
            
            // In a real application, you would update the slider values here
        }

        private void ShowMarkupSource()
        {
            var sourceInfo = @"This UI was built from HTML-like markup using:

1. UITokenizer - Breaks markup into tokens
2. UIParser - Creates symbolic tree representation  
3. UIBuilder - Converts to actual UI elements
4. Event handlers registered in C# code

The markup syntax supports:
- Standard HTML-like elements (canvas, div, label, button, input, slider)
- Custom attributes for UI properties (bounds, colors, spacing, etc.)
- Event handler binding (onClick, onTextChanged, etc.)
- Nested layouts with automatic positioning

Check the source code to see the actual markup!";

            ShowMessage("Markup Source", sourceInfo);
        }

        public UIElement GetRootElement()
        {
            return _rootElement;
        }
    }
}