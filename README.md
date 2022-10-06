# WinUI3_UnhandledExceptionTestApp

A simple WinUI-3 app that tests out the support for App.UnhandledException in the WindowsAppSDK, and demonstrates that it really doesn't work well enough even in the 1.2-preview.

https://github.com/microsoft/microsoft-ui-xaml/issues/5221 explains that even if you install a handler for the 'UnhandledException' event, in the App class, that handler doesn't get invoked when an exception is thrown and not otherwise caught.

That post (June 2021) mentions this not working when the exception is thrown from an async method, but my experiments indicate that the problem is more general than that. Even when an exception is thrown from the main thread (eg in a click handler) not only is the handler never called, but the app silently terminates. This surely is completely unacceptable. 

See also https://github.com/microsoft/microsoft-ui-xaml/issues/5221#issuecomment-1166554263 (June 2022). 'Here's another version, and as minimal as one can imagine. It is synchronous, so, the problem is not only in async methods.'

This killer issue is reported to have been fixed in SDK 1.2-preview2 (https://github.com/microsoft/microsoft-ui-xaml/issues/5221#issuecomment-1267744807). Very good, exceptions thrown on the UI thread eg in a click handler are now caught as expected. However in many other situations the handler still doesn't catch anything, and the app just goes into a weird state. There's surely no way one can publish an app that could behave like that. And given that WinUI-3 is also the foundation for Uno and MAUI-desktop apps, I'm flabbergasted that this hasn't received proper attention.

This little app demonstrates the outstanding problems. See the code in App.xaml.cs, where you can set some flags that cause exceptions to be thrown (and never caught) in various situations, eg on app startup and in Window.Activated.

Maybe there's time before SDK 1.2 is released, for the WinUI team to have another go at cracking the problem ? The app provides an easy way to see whether or not it's resolved.






