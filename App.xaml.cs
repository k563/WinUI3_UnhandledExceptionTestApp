//
// App.xaml.cs
//

namespace WinUI3_UnhandledExceptionTestApp
{

  // NOTE : DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION has been defined
  // in the project settings, so that we won't get a Debug.Break if an exception
  // does get handled. Instead, the unhandled exception will just be handled here. 

  // Build and run the app with Debug/x64 to demonstrate the issues.

  public partial class App : Microsoft.UI.Xaml.Application
  {

    // Set these flags to have exceptions thrown from various places.
    // We expect all of these to be caught in our UnhandledException handlers,
    // and for the application to not silently crash or hang up ...

    // If we set this to true - IT WORKS AS EXPECTED !!!
    // Our click handler throws an exception, and it's caught by the handler.
    // However the stack trace doesn't tell us anything useful.

    public static bool ThrowExceptionInMainWindowClickHandler     = true ; 
    
    // If we set this to true, our handler isn't called ; a couple of message boxes appear
    // telling us that the app will terminate immediately ; however the app continues to run.

    public static bool ThrowExceptionInAppConstructor             = false ;

    // If we set this to true, our handler isn't called ; two message boxes appear
    // telling us that the app will terminate ; the app then goes into a weird state,
    // where the UI doesn't show, and you have to terminate it with 'Stop Debugging'.

    public static bool ThrowExceptionInAppLaunched                = true ; 

    // If we set this to true, our handler isn't called ; two message boxes appear
    // telling us that the app will terminate ; the app then goes into a weird state,
    // where the UI doesn't show, and you have to terminate it with 'Stop Debugging'.

    public static bool ThrowExceptionInMainWindowConstructor      = false ;

    // If we set this to true, our handler isn't called, but the app
    // continues to run normally (once the Activate has somehow been invoked
    // a second time, at which point we didn't throw an exception).

    public static bool ThrowExceptionInMainWindowActivatedHandler = false ;

    public App ( )
    {

      this.InitializeComponent() ;

      // Set up both flavours of 'UnhandledException' event handler.
      // We expect any exceptions throw from anywhere in the app
      // to be caught by one of these ???

      this.UnhandledException += (
        object                                        sender, 
        Microsoft.UI.Xaml.UnhandledExceptionEventArgs unhandledExceptionEventArgs 
      ) => {
        // Setting 'Handled' to true allows the app to continue, ie it won't terminate.
        unhandledExceptionEventArgs.Handled = true ;
        System.Diagnostics.Debug.WriteLine(  
          $"App.UnhandledException : '{unhandledExceptionEventArgs.Exception.Message}'"
        ) ;
        System.Diagnostics.Debug.WriteLine(  
          $"Stack trace : {unhandledExceptionEventArgs.Exception.StackTrace??"??"}"
        ) ;
      } ;

      System.AppDomain.CurrentDomain.UnhandledException += (
        object                             sender, 
        System.UnhandledExceptionEventArgs unhandledExceptionEventArgs
      ) => {
        // We've never seen this be called ...
        System.Diagnostics.Debug.WriteLine(  
          $"AppDomain.CurrentDomain.UnhandledException raised : {unhandledExceptionEventArgs.ExceptionObject} {unhandledExceptionEventArgs.IsTerminating}"
        ) ;
      } ;

      if ( App.ThrowExceptionInAppConstructor ) 
      { 
        System.Diagnostics.Debug.WriteLine("Throwing exception in App constructor") ;
        throw new System.ApplicationException("Thrown in App constructor") ; 
      }

    }

    protected override void OnLaunched ( Microsoft.UI.Xaml.LaunchActivatedEventArgs args )
    {
      if ( App.ThrowExceptionInAppLaunched ) 
      { 
        System.Diagnostics.Debug.WriteLine("Throwing exception in App.OnLaunched") ;
        throw new System.ApplicationException("Thrown in App.OnLaunched") ; 
      }
      m_window = new MainWindow() ;
      m_window.Activate() ;
    }

    private Microsoft.UI.Xaml.Window m_window ;

  }

}
