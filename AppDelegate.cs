using System.Text;
using CoreFoundation;
using SkiaSharp;

namespace cattest;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController ();
		vc.View!.AddSubview (new UILabel (Window!.Frame) {
			BackgroundColor = UIColor.SystemBackground,
			TextAlignment = UITextAlignment.Center,
			Text = "Hello, Mac Catalyst!",
			AutoresizingMask = UIViewAutoresizing.All,
		});
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();

        DispatchQueue.MainQueue.DispatchAfter(new DispatchTime(DispatchTime.Now, (long)1000 * 1000000), UseSkia);

        return true;
	}

	private void UseSkia()
	{
        var buffer = new HarfBuzzSharp.Buffer()
        {
            ContentType = HarfBuzzSharp.ContentType.Unicode,
        };

        buffer.AddUtf8(Encoding.UTF8.GetBytes("hello"));
        buffer.GuessSegmentProperties();

        if (buffer != null)
        {
            SkiaSharp.Views.iOS.SKCanvasView view = new SkiaSharp.Views.iOS.SKCanvasView(new CGRect(0, 0, 20, 20));

            if (view.Frame.Width < 0)
                Console.WriteLine("111");
        }

        var result = SKFontManager.Default.MatchFamily("Helvetica");

        if (result == null)
            Console.WriteLine("222");

        UIAlertController c = UIAlertController.Create("Test", result?.FamilyName, UIAlertControllerStyle.Alert);
        UIApplication.SharedApplication.KeyWindow?.RootViewController?.PresentViewController(c, true, null);

    }
}
