using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PartyCalculator.PartyWebServices;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExpense : Page
    {
        private int workflowId;
        
        private MainPage mainPage;

        public AddExpense()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = e.Parameter as MainPage;
            workflowId = mainPage.WorkflowId.Value;
            
            base.OnNavigatedTo(e);
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            expense expense = new expense();
            expense.amountSpecified = true;
            expense.amount = double.Parse(textBox_Copy1.Text);
            expense.expenseType = textBox.Text;
            expense.description = textBox_Copy.Text;
            expense.user = mainPage.CurrentUser;
            PartyCalculatorWebServiceWrapper.PartyCalculatorService.AddExpenseToWorkFlow(workflowId, expense);
            
            textBox_Copy1.Text = "";
            textBox.Text = "";
            textBox_Copy.Text = "";
            mainPage.Navigate(typeof(AddExpense));
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Navigate(typeof(ExpenseReport));
        }
    }
}
