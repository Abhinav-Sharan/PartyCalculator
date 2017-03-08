using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExpenseReport : Page
    {
        private MainPage mainPage;
        private List<Expense> expenses;

        public List<Expense> Expenses
        {
            get
            {
                return expenses;
            }
        }

        public ExpenseReport()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = e.Parameter as MainPage;
            base.OnNavigatedTo(e);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock.Text += mainPage.Owner.userName;
            expenses = new List<Expense>();
            var result = PartyCalculatorWebServiceWrapper.PartyCalculatorService.GetAllExpenses(mainPage.WorkflowId.Value);
            
            if (result == null) return;
            foreach (var item in result)
            {
                Expenses.Add(new Expense(item.UserName, item.ExpenseType, item.Expensedescription, item.Amount));
            }
            gridView.ItemsSource = expenses;
        }

    }
}
