using RequesterGui.Components;
using System;
using System.Collections.Generic;
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

namespace RequesterGui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			LoadHost();
		}

		private void LoadHost()
		{

		}

		private void AddHost_Click(object sender, RoutedEventArgs e)
		{
			_ = HostTabControl.Items.Add(new TabItem
			{
				Header = "test",
				Name = "test",
				Content = new HostControl(),
				IsSelected = true
			});
		}

		private void SaveConfig_Click(object sender, RoutedEventArgs e)
		{
			foreach (var item in HostTabControl.Items)
			{
				if(item is TabItem tabItem && tabItem.Content is HostControl hostControl)
				{
					MessageBox.Show($"{hostControl.HostAlias}, {hostControl.HostUrl}, {hostControl.HostWindowsAuth}, {hostControl.HostJwtAuth}");
				}
			}
		}
	}
}
