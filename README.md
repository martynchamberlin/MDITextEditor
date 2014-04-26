MDITextEditor
=============

__Overview__: This is a multiple document interface (MDI) text editor for Windows, built in C# as a Windows Form for a 3000-level CS class. The goal was to demonstrate our competence with MDI forms and the `MenuStrip` class, with all the accompanying dropdown abilities with `ToolStripMenuItem` objects.

## Features
- All pertinent dropdown menus that are MDI-specific are disabled until an MDI child is created. Throughout the program’s execution, if a no MDI children exist because previous ones had been closed, the dropdown menus are again disabled. 
- User can change font styling (bold and/or italicized), font family, font size, and text alignment.
- User can create unlimited number of documents and easily flip between them using the View dropdown menu. This menu updates dynamically to increase and decrease as the user adds and removes editor windows from the view.
- __Best of all__, the user can flip between different editor windows and the state of those windows will be reflected in the dropdown formatting settings when the click event for these dropdowns is triggered. 

## Difficulties
- Message passing between the parent form and the MDI children was difficult for me. I ended up creating a property inside my MDI class that stored a reference to the parent form object. This way we had both parents pointing to children, and children pointing to the parents. Since the parent exists for the lifetime of the program, I do not think this reference cycle should be a concern from a memory-management standpoint, but it’s important to be aware of.

## Potential Add-ons

Below are some possible enhancements to this program that would make it better.

- It would be nice to change the color of the forms.
- The backgrounds available for the MDI children look quite garish. They were taken from the [dashboard sticky backgrounds](https://developer.apple.com/library/mac/documentation/AppleApplications/Conceptual/Dashboard_ProgTopics/Art/stickies_front-back_2x.png) for Mac OS X. The reason they look so bad in this app is because they are not gradients. If they were made gradients, they would look much richer. Given how imprecise Windows Forms are by default however, and given that this would need support for retina and non-retina devices, I feel like this is a very bad use of time at present.
- There currently exists no way to save and load previously created documents. This would need to be done either (1) via a database (2) via file management. Either way, there would need to be meta data associated with the file, indicating the font family, size, and styling. 
- There currently exists no way to stylize just a portion of the text. It is either all or none. The ability to style only a portion of a `RichTextBox` object is probably not possible, and this is getting much deeper than the assignment required. Some day I hope to explore exactly how this is possible. I suspect it involves complex matrices and number theory.

## Closing Thoughts

I struggle to think of a time in which one would actually want to create MDI forms. Having more than one window power an application is a very powerful and useful idea — without it, we would hardly have modern computing as we know it. However, I strongly believe that these windows should exist independently, from a visual standpoint, from their parents. The MDI paradigm, at least in the .NET world, dictates that all children belong inside the parent frame. In the HTML world this would be akin to saying that all iFrames must be inside of other iFrames. It’s an interesting idea, but in practical terms it’s abomination. 



