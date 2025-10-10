# Text Input Search Test - UIBuilderExample

## Overview

Added a demonstration in UIBuilderExample that shows how to use the recursive search functionality with a TextInput element.

## What was added:

### 1. New UI Elements in Markup

Added a new section in the UIBuilderExample markup with:
- A label explaining the test ("Text Input Search Test:")  
- A TextInput with the name "testTextInput" 
- A button with the name "printTextButton" that triggers the test

### 2. Event Handler

Added `PrintTextInputValue` event handler that:
- Uses `canvas.FindChildByName("testTextInput")` to locate the text input
- Casts the found element to `TextInput`
- Retrieves the current text using the `Text` property
- Prints the current text content to the console
- Provides helpful messages for empty inputs or errors

### 3. Updated Help Text

Modified the help dialog to mention the new text input functionality.

## How to Use:

1. Run the UIBuilderExample
2. Type some text in the text input field
3. Click "Print Text to Console" button
4. Check the console output to see the retrieved text

## Example Output:

```
=== Text Input Search Test ===
Found text input element: TextInput
Current text in input: 'Hello World!'
=== End Text Input Search Test ===
```

Or if the input is empty:
```
=== Text Input Search Test ===
Found text input element: TextInput
Current text in input: ''
The text input is empty. Try typing something first!
=== End Text Input Test ===
```

## Technical Implementation:

The test demonstrates:
- **Recursive Search**: Finding elements nested deep in layout groups
- **Type Safety**: Proper casting and type checking
- **Error Handling**: Graceful handling of missing elements or wrong types
- **Real-world Usage**: Practical example of retrieving user input by element name

This shows how the search functionality can be used in real applications to locate and interact with specific UI elements by their names, making it easy to retrieve user input or modify element properties dynamically.