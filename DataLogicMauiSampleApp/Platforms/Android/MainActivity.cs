using Android.App;
using Android.Content.PM;
using Com.Datalogic.Device;
using Com.Datalogic.Decode;
using Android.OS;

namespace DataLogicMauiSampleApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IReadListener
{

    BarcodeManager decoder = null;

    protected override void OnResume()
    {
        base.OnResume();

        // If the decoder instance is null, create it.
        if (decoder == null)
        {
            // Remember an onPause call will set it to null.
            decoder = new BarcodeManager();
        }

        // From here on, we want to be notified with exceptions in case of errors.
        ErrorManager.EnableExceptions(true);

        try
        {
            // add our class as a listener
            decoder.AddReadListener(this);
        }
        catch (DecodeException e)
        {
            Console.WriteLine("Error while trying to bind a listener to BarcodeManager");
        }
    }

    protected override void OnPause()
    {
        base.OnPause();

        // If we have an instance of BarcodeManager.
        if (decoder != null)
        {
            try
            {
                // Unregister our listener from it and free resources
                decoder.RemoveReadListener(this);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to remove a listener from BarcodeManager");
            }
        }
    }

    void IReadListener.OnRead(IDecodeResult decodeResult)
    {
        // Change the displayed text to the current received result.

        Console.WriteLine(decodeResult.Text);
        MessagingCenter.Send<MainActivity, IDecodeResult>(this, "decodeResult", decodeResult);

    }

}
