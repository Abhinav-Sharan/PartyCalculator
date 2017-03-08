using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddUser : Page
    {
        private MainPage mainPage;
        private int? workflowId;

        public AddUser()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = e.Parameter as MainPage;
            workflowId = mainPage.WorkflowId;
            base.OnNavigatedTo(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var user = new PartyWebServices.user();
            user.userName = textBox.Text;
            PartyCalculatorWebServiceWrapper.PartyCalculatorService.AddUserToWorkflow(user,workflowId.Value );
            
            mainPage.Participants.Add(user);
            textBox.Text = "";
            textBox_Copy.Text = "";
            mainPage.Navigate(typeof(AddUser));

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var user = new PartyWebServices.user();
            user.userName = textBox.Text;
            PartyCalculatorWebServiceWrapper.PartyCalculatorService.AddUserToWorkflow(user, workflowId.Value);
            
            mainPage.Participants.Add(user);
            textBox.Text = "";
            textBox_Copy.Text = "";
            mainPage.Navigate(typeof(AddExpense));

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            mainPage.Navigate(typeof(AddExpense));
        }
    }
}
