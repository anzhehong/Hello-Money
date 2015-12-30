using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace NavigationMenuSample.Views
{
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        void HubPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> sections = new List<string>();

            foreach (HubSection section in AboutUs.Sections)
            {
                if (section.Header != null)
                {
                    sections.Add(section.Header.ToString());
                }
            }

            ZoomedOutList.ItemsSource = sections;
        }


        private void ThemeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = ElementTheme.Light;
        }
        private void ThemeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.RequestedTheme = ElementTheme.Dark;
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp();
            bottomCommandBar.IsOpen = false;
        }
        private void Customer_HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO : Message Box to show help
            var dialog = new MessageDialog("Help Message","This application is used to help you manage your pocket!");
            dialog.Commands.Add(new UICommand("Ok, I've known.", cmd => { }, commandId: 0));
            //dialog.Commands.Add(new UICommand("no", cmd => { }, commandId: 1));

            var result = dialog.ShowAsync();
        }
        private void AppBarButton_Click_ChangeTheme(object sender, RoutedEventArgs e)
        {
            // change theme
            if (this.RequestedTheme.Equals(ElementTheme.Default))
            {
                this.RequestedTheme = ElementTheme.Dark;
            }else if (this.RequestedTheme.Equals(ElementTheme.Dark))
            {
                this.RequestedTheme = ElementTheme.Default;
            }
        }
        private void AppBarButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            // DELETE FILE
            var dialog = new MessageDialog("Warning", "还没做");
            dialog.Commands.Add(new UICommand("Ok, I've known.", cmd => { }, commandId: 0));
        }
        private void AppBarButton_Click_OneDrive(object sender, RoutedEventArgs e)
        {
            // one drive
            var dialog = new MessageDialog("Warning", "还没做");
            dialog.Commands.Add(new UICommand("Ok, I've known.", cmd => { }, commandId: 0));
        }
        private void AppBarButton_Click_SetBuget(object sender, RoutedEventArgs e)
        {
            // one drive
            var dialog = new MessageDialog("Warning", "还没做");
            dialog.Commands.Add(new UICommand("Ok, I've known.", cmd => { }, commandId: 0));
        }

        


        private ControlInfoDataItem item;
        public class ControlInfoDataItem
        {
            public ControlInfoDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
            {
                this.UniqueId = uniqueId;
                this.Title = title;
                this.Subtitle = subtitle;
                this.Description = description;
                this.ImagePath = imagePath;
                this.Content = content;
                this.Docs = new ObservableCollection<ControlInfoDocLink>();
                this.RelatedControls = new ObservableCollection<string>();
            }

            public string UniqueId { get; private set; }
            public string Title { get; private set; }
            public string Subtitle { get; private set; }
            public string Description { get; private set; }
            public string ImagePath { get; private set; }
            public string Content { get; private set; }
            public ObservableCollection<ControlInfoDocLink> Docs { get; private set; }
            public ObservableCollection<string> RelatedControls { get; private set; }

            public override string ToString()
            {
                return this.Title;
            }
        }
        public class ControlInfoDocLink
        {
            public ControlInfoDocLink(string title, string uri)
            {
                this.Title = title;
                this.Uri = uri;
            }
            public string Title { get; private set; }
            public string Uri { get; private set; }
        }

        public ControlInfoDataItem Item
        {
            get { return item; }
            set { item = value; }
        }
        private void ShowHelp()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            string HTMLOpenTags = loader.GetString("HTMLOpenTags");
            string HTMLCloseTags = loader.GetString("HTMLCloseTags");

            contentWebView.NavigateToString(HTMLOpenTags + Item.Content + HTMLCloseTags);

            if (!helpPopup.IsOpen)
            {
                rootPopupBorder.Width = 346;
                rootPopupBorder.Height = this.ActualHeight;
                helpPopup.HorizontalOffset = Window.Current.Bounds.Width - 346;
                helpPopup.IsOpen = true;
            }
        }
        
    }

    
}
