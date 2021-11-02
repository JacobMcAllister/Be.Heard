# Be.Heard
Be Heard is an application designed to assist in the rehabilitation and development of successful speech patterns for those who struggle due to injury or from otherwise similar speech defects.  Be Heard will utilize effective methods as proposed by speech pathologists to strengthen the speech patterns of the user and then work within those methods using a sophisticated machine learning algorithm to focus and improve on the areas of speech that the user needs the most help with.  Over time, the users of Be Heard will be more successful in their everyday conversation and can communicate with their community more effectively.  The application Be Heard can be an excellent tool to help people better overcome the difficulties associated with speech problems and then to help people re-enter the world with greater confidence and a stronger ability to communicate with their fellow human.

# Requirements
These are necessary to build and setup the development environment.
- IIS
- Visual Studio 2017/2019
- Node.js (npm)
- .NET Core 3.1
- Web Compiler (Visual Studio Extension)
- Bundler & Minifier (Visual Studio Extension)

## IIS
Turn on and configure the IIS feature in Windows 10.

## Visual Studio
IDE to use. It may be a bit strict since the app is using extension configs to build sass and javascript.

## Node.js
The app has node_modules dependencies. Install Node and npm.

## Web Compiler
Web Compiler is a Visual Studio extension that helps in the compilation of many css and js frameworks. Usage is heavily geared towards compilation of Sass files.

## Bundler & Minifier
Anther Visual Studio extension. This extension bundles and minifies css and js files. The project uses the extension to bundle js into the app.

# Build the App
There are a few steps to consider before building the app. Once the requirements are fulfilled, start by navigating into the 'ResourcePackages' folder and run 'npm install'. This will install the node_modules dependencies. In the solution window, right-click the 'bundleconfig.json' and navigate to the 'Bundler & Minifier' context window. Enable the bundle on build feature. There will be a prompt to install a NuGet package dependency for the feature. Accept and install. In the topic of NuGet packages, Check and make sure that there are no NuGet dependencies missing for the project. This shouldn't be a problem, but just in case install if there are missing. A prompt will ask you to install.

The initial bundles and sass compilation should be commited into git. This means that you can just run build. The behavior of the extension is to compile and bundle on save and build. If you run into issues of code not showing up or making any changes, this could be the reason.

# TO-DO
- [ ] Include config files schema into README