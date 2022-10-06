# WinUI3_UnhandledExceptionTestApp

A simple WinUI-3 app that tests out the support for App.UnhandledException in the WindowsAppSDK, and demonstrates that it really doesn't work well enough in the 1.2-preview.

Maybe there's time before 1.2 is released, for the WinUI team to have another go at cracking the problem ? The code here provides an easy way to see whether or not it's resolved.

https://github.com/microsoft/microsoft-ui-xaml/issues/5221 (June 2021) explains that even if you install a handler for the 'UnhandledException' event, in the App class, that handler doesn't get invoked when an exception is thrown and not otherwise caught. It mentions this not working when the exception is thrown from an async method, but my experiments have always indicated that the problem is more general than that. Even when an exception is thrown from the main thread (eg in a click handler) not only is the handler never called, but the app silently terminates. 

See also https://github.com/microsoft/microsoft-ui-xaml/issues/5221#issuecomment-1166554263 (June 2022). 'Here's another version, and as minimal as one can imagine. It is synchronous, so, the problem is not only in async methods.'

This killer issue is reported to have been **fixed in SDK 1.2-preview2** (https://github.com/microsoft/microsoft-ui-xaml/issues/5221#issuecomment-1267744807). **Very good, exceptions thrown on the UI thread eg in a click handler are now caught as expected.** However **in many other situations the handler still doesn't catch anything**, and the app just goes into a weird state. There's surely no way one can publish an app that could behave like that. And given that WinUI-3 is also the foundation for Uno and MAUI-desktop apps, I'm flabbergasted that this hasn't received proper attention.

**This little app demonstrates the outstanding problems.** In App.xaml.cs you can set up some flags that enable exceptions to be thrown in various situations, eg on app startup and in Window.Activated. If they are caught as expected, a message is written to the debug output. Doubtless there are other situations to check, but this seems like a useful start.

Built with 17.3.4 Enterprise ; the WindowsAppSDK runtime 1.2 has been installed ; native debugging has been enabled in the project settings. Maybe some other tweaks would make it work properly ?








