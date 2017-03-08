using PartyCalculator.PartyWebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private MainPage mainPage;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainPage = e.Parameter as MainPage;
            base.OnNavigatedTo(e);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var user = new user();
            user.userName = textBox.Text;
            PartyCalculatorWebServiceWrapper.PartyCalculatorService.StartWorkflow(mainPage.WorkflowId.Value, user);
            mainPage.Owner = user;
            mainPage.CurrentUser = user;
            mainPage.Navigate(typeof(AddUser));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            user user = new user();
            user.userName = textBox.Text;
            mainPage.CurrentUser = user;
            mainPage.Navigate(typeof(ExistingWorkflows));
        }
    }
}
