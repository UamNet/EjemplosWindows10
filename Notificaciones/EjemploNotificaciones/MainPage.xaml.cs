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

using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EjemploNotificaciones
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            this.InitializeComponent();
        }

        private void notify_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"
<toast launch=""toriel-clicked"">

  <visual>
    <binding template = ""ToastGeneric"">
          <image src = ""Assets/Toriel.png""/>
          <text hint-style = ""base"" >I can't let you go</text>
            <text hint-style = ""captionSubtle"" >my child</text>
    </binding>
  </visual>

  <actions>
    <action content=""FIGHT"" arguments=""FIGHT""/>
    <action content=""MERCY"" arguments=""MERCY""/>
  </actions>

  <audio src=""ms-winsoundevent:Notification.Reminder""/>

</toast>");
            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }



        private void liveTilefy_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"
<tile>
  <visual branding=""none"">

    <binding template = ""TileMedium"" hint-textStacking = ""center"">
      <image src = ""Assets/Flowey.jpg"" placement = ""peek"" hint-crop = ""circle""/>
      <text hint-style = ""base"" hint-align = ""center"">Howdy!</text>
      <text hint-style = ""captionSubtle"" hint-align = ""center"">I'm FLOWEY!</text>
    </binding>

    <binding template = ""TileWide"">
      <group>
        <subgroup hint-weight = ""33"">
          <image src = ""Assets/Flowey2.png"" hint-crop = ""circle""/>
        </subgroup>
        <subgroup hint-textStacking = ""center"">
          <text hint-style = ""subtitle"">In this world</text>
          <text hint-style = ""subtitle"">it's KILL</text>
          <text hint-style = ""subtitle"">or be KILLED </text>
        </subgroup>
      </group>
    </binding>

    <binding template = ""TileLarge"" hint-textStacking = ""center"">
      <group>
        <subgroup hint-weight = ""1""/>
        <subgroup hint-weight = ""2"">
          <image src = ""Assets/Flowey.jpg"" hint-crop = ""circle""/>
        </subgroup>
        <subgroup hint-weight = ""1""/>
      </group>

      <text hint-style = ""title"" hint-align = ""center""> Howdy,</text>
      <text hint-style = ""subtitleSubtle"" hint-align = ""center""> I'm FLOWEY </text>
    </binding>


  </visual>
</tile>");

            var notification = new TileNotification(doc);
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddMinutes(10);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
