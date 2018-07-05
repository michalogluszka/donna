using Donna.Core.AzureSecurity;
using Donna.Core.AzureSecurity.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Donna.UI.WPF.DonnaTestTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _textToSpeechEndpoint = "https://westus.tts.speech.microsoft.com/cognitiveservices/v1";

        private const string _issueTokenEndpoint = "https://westus.api.cognitive.microsoft.com/sts/v1.0/issuetoken";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var authenticaton = new CognitiveServicesAuthentication("");

            Uri issueTokenUri = new Uri("https://westus.api.cognitive.microsoft.com/sts/v1.0/issuetoken");

            ISubscriptionKeyProvider provider = new SubscriptionKeyEnviromentVariableProvider("SpeechServiceSubscriptionKey");
            var auth = new AzureAuthToken(provider, issueTokenUri);

            string token = auth.GetAccessToken();

            Debug.WriteLine(token);



        }
    }
}
