using PartyCalculator.PartyWebServices;
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
    public sealed partial class ExistingWorkflows : Page
    {
        private MainPage mainPage;
        private List<ExistingWorkFlow> workflows;

        public ExistingWorkflows()
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
            var result = PartyCalculatorWebServiceWrapper.PartyCalculatorService.GetWorkFlows();
            
            if (result == null) return;
            
            workflows = new List<ExistingWorkFlow>();

            foreach (var item in result)
            {
                if (IsUserSame(item.Owner.ToServiceUser(), mainPage.Owner))
                {
                    var w = new ExistingWorkFlow(item.WorkflowId, item.Owner.ToServiceUser(), "Owner");
                    ListViewItem temp = new ListViewItem();
                    temp.Content = w.WorkflowId + " " + w.UserName.userName + " " + w.Role;
                    temp.Tag = w;
                    workflows.Add(w);
                    listView.Items.Add(temp);
                }

                if (item.Participants == null) continue;
                foreach (var subItem in item.Participants)
                {
                    var user = new user();
                    user.userName = subItem;
                    if (IsUserSame(user, mainPage.Owner))
                    {
                        var w = new ExistingWorkFlow(item.WorkflowId, user, "Participant");
                        ListViewItem temp = new ListViewItem();
                        temp.Content = w.WorkflowId + " " + w.UserName.userName + " " + w.Role;
                        temp.Tag = w;
                        workflows.Add(w);
                    }
                }
             }
            
        }

        private bool IsUserSame(user user1,user user2)
        {
            return user1.userName.Equals(user2.userName);
        }

        private bool DoesContainUser(user[] user1, user user2)
        {
            foreach (var u in user1)
            {
                if (u.userName.Equals(user2.userName)) return true;
            }
            return false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem wf = listView.SelectedItem as ListViewItem;
            var ew = wf.Tag as ExistingWorkFlow;
            if (ew == null) return;

            mainPage.WorkflowId = ew.WorkflowId;
            mainPage.CurrentUser = ew.UserName;
            
         }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem wf = listView.SelectedItem as ListViewItem;
            ExistingWorkFlow ew = wf.Tag as ExistingWorkFlow;
            PartyCalculatorWebServiceWrapper.PartyCalculatorService.EndWorkflow(ew.WorkflowId);
            
            workflows.Remove(ew);
            listView.Items.Remove(wf);
            mainPage.Navigate(typeof(ExistingWorkFlow));
            
        }
    }
}
