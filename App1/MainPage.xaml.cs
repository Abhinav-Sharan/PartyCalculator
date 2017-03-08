using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PartyCalculator.PartyWebServices;
using PartyCalculator.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PartyCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public const string FEATURE_NAME = "Pivot";
        private int? workflowid;
        private user owner;
        private user currentUser;
        private List<user> participants = new List<user>();

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="Login", ClassType=typeof(LoginPage)},
            new Scenario() { Title="ExistingWorkflows", ClassType=typeof(ExistingWorkflows)},
            new Scenario() { Title="Add User", ClassType=typeof(AddUser)},
            new Scenario() { Title="Add Expense", ClassType=typeof(AddExpense)},
            new Scenario() { Title="Expense Report", ClassType=typeof(ExpenseReport)}
        };

        internal void Navigate(Type type)
        {
            ScenarioControl.SelectedItem = scenarios.Find(s => s.ClassType == type);
        }

        public MainPage()
        {
            this.InitializeComponent();

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
            SampleTitle.Text = FEATURE_NAME;
            ApplyWorkflow();
        }

        private void ApplyWorkflow()
        {
            try
            {
                workflowid = PartyCalculatorWebServiceWrapper.PartyCalculatorService.GetWorkFlowId();
            }
            catch
            {
                ///TO DO 
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = scenarios;
            ScenarioControl.SelectedIndex = 0;
        }

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        public user Owner
        {
            get
            {
                return owner;
            }

            set
            {
                owner = value;
            }
        }

        public user CurrentUser
        {
            get
            {
                return currentUser;
            }

            set
            {
                currentUser = value;
            }
        }

        public int? WorkflowId
        {
            get
            {
                return workflowid;
            }

            set
            {
                workflowid = value;
            }
        }
        public List<user> Participants
        {
            get
            {
                return participants;
            }

         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Footer_Click(object sender, RoutedEventArgs e)
        {


        }

        private void NewWorkflow_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
                
        }


        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the status block when navigating scenarios.
            NotifyUser(String.Empty, NotifyType.StatusMessage);

            ListView scenarioListBox = sender as ListView;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if (s != null)
            {
                object parameter = CreatParameneterBasedOnType(s.ClassType);
                ScenarioFrame.Navigate(s.ClassType, this);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                    StatusBorder.Visibility = Visibility.Collapsed;
                }
            }
        }

        private object CreatParameneterBasedOnType(Type classType)
        {
            if (classType == typeof(AddUser)) return workflowid;
            if (classType == typeof(AddExpense)) return new object[]{ workflowid,owner};
            return null;

        }

        /// <summary>
        /// Used to display messages to the user
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="type"></param>
        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
            }
            StatusBlock.Text = strMessage;

            // Collapse the StatusBlock if it has no text to conserve real estate.
            StatusPanel.Visibility = (StatusBlock.Text != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}






