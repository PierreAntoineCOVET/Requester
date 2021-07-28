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

namespace RequesterGui.Components
{
	/// <summary>
	/// Interaction logic for TextboxWatermarkAbleControl.xaml
	/// </summary>
	public partial class TextboxWatermarkAbleControl : UserControl
	{
		public TextboxWatermarkAbleControl()
		{
			DataContext = this;
			InitializeComponent();
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				"Text",
				typeof(string),
				typeof(TextboxWatermarkAbleControl),
				new PropertyMetadata(string.Empty));

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly DependencyProperty WaterMarkProperty =
			DependencyProperty.Register(
				"WaterMark",
				typeof(string),
				typeof(TextboxWatermarkAbleControl),
				new PropertyMetadata(string.Empty));

		public string WaterMark
		{
			get => (string)GetValue(WaterMarkProperty);
			set => SetValue(WaterMarkProperty, value);
		}
	}
}
