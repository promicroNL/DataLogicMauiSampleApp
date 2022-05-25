using Com.Datalogic.Decode;
namespace DataLogicMauiSampleApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
#if ANDROID
        MessagingCenter.Subscribe<MainActivity, IDecodeResult>(this, "decodeResult", async (sender, arg) =>
        {

            HelloLbl.Text = arg.Text;
            WelcomeLbl.Text = arg.BarcodeID.ToString();

            SemanticScreenReader.Announce(HelloLbl.Text);
            SemanticScreenReader.Announce(WelcomeLbl.Text);
        });

#endif
    }


}

