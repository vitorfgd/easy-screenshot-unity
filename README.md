Easy Screenshot - Unity3D Script
===================

This script was developed to easily create screenshots for the AppStore (according to Apple's screenshot size guidelines). It will also help you create your android and desktop screenshots.

If every option is selected, the script should create 6 different iOS screenshots (iPhone 3.5, 4, 4.7, 5.5, iPad and iPad Pro), one android screenshot and one desktop screenshot with given size.


## How to use?

### Easy way:

* Import the unity package located under unitypackage/easyscreenshot.unitypackage;

### Hard way:

* Attach **"Assets/Scripts/screenshot.cs"** to your main camera.
* Create a new folder called **"Editor"**, if it does not exists, under your assets folder. Drag the script **"Assets/Editor/ScreenshotEditor.cs"** to the new folder.
* Configure the script according to your needs.
* Choose portrait or landscape. On android and desktop, you'll have to specify the size and mode you want.
* If you selected the option to create android or desktop screenshots you can also enlarge your dimensions using the slider.
* Select the screenshot shortcut you'd like to use to create screenshots during runtime.
* Run your game in the editor and press your screenshot key.
* The script will save the pictures to a new folder called "screenshots" on your project root.


Usage
-------------

![gif](https://thumbs.gfycat.com/VeneratedReliableAbyssiniancat-size_restricted.gif)


Contributing
-------------

1. Fork the repository on Github;
2. Clone the project into your machine;
3. Commit your changes to your own branch;
4. Push your work to your fork;
5. Submit a pull request for review;


Copyright
-------------

This project is under Apache 2.0 license.
