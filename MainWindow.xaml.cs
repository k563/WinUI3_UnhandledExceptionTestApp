//
// MainWindow.cs
//

namespace WinUI3_UnhandledExceptionTestApp
{

  public sealed partial class MainWindow : Microsoft.UI.Xaml.Window
  {

    private int m_nActivationsSeen = 0 ;

    public MainWindow ( )
    {
      this.InitializeComponent() ;

      if ( App.ThrowExceptionInMainWindowConstructor ) 
      { 
        System.Diagnostics.Debug.WriteLine("Throwing exception in MainWindow constructor") ;
        throw new System.ApplicationException("Thrown in MainWindow constructor") ; 
      }

      this.Activated += (s,e) => {
        System.Diagnostics.Debug.WriteLine($"MainWindow Activated #{m_nActivationsSeen} {e.WindowActivationState}") ;
        if ( e.WindowActivationState == Microsoft.UI.Xaml.WindowActivationState.CodeActivated )
        {
          m_nActivationsSeen++ ;
          if ( m_nActivationsSeen == 1 )
          {
            // Throw an exception only on the FIRST activation,
            // otherwise we get into an infinite loop ...
            if ( App.ThrowExceptionInMainWindowActivatedHandler) 
            { 
              System.Diagnostics.Debug.WriteLine("Throwing exception in MainWindow Activated handler") ;
              throw new System.ApplicationException("Thrown in MainWindow Activated handler") ; 
            }
          }
        }
      } ;
    }

    private void myButton_Click ( object sender, Microsoft.UI.Xaml.RoutedEventArgs e )
    {
      System.Diagnostics.Debug.WriteLine("myButton_Click") ;
      myButton.Content = "Clicked" ;
    }

    private void myThrowButton_Click ( object sender, Microsoft.UI.Xaml.RoutedEventArgs e )
    {
      System.Diagnostics.Debug.WriteLine("myThrowButton_Click") ;
      if ( App.ThrowExceptionInMainWindowClickHandler ) 
      { 
        System.Diagnostics.Debug.WriteLine("Throwing exception in MainWindow myThrowButton_Click") ;
        throw new System.ApplicationException("Thrown in MainWindow myThrowButton_Click") ; 
      }
    }

  }

}
