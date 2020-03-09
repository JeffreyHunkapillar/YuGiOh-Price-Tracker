using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//This is a collection of data points
		public ChartValues<double> xValues { get; set; }
		public MainWindow()
		{
			//Initializes the UI
			InitializeComponent();
		}

		private void CheckPriceBtn_Click(object sender, RoutedEventArgs e)
		{
			//Declare a new collection of xValues
			xValues = new ChartValues<double>();
			string cardName = CardNameTextBox.Text;
			string uriString = "https://yugiohprices.com/price_history/" + cardName;


			WebClient myWebClient = new WebClient();
			// Download home page data. 
			Console.WriteLine("Accessing {0} ...", uriString);
			// Open a stream to point to the data stream coming from the Web resource.
			Stream myStream = myWebClient.OpenRead(uriString);

			Console.WriteLine("\nDisplaying Data :\n");
			StreamReader sr = new StreamReader(myStream);

			string webData = sr.ReadToEnd();
			// Close the stream. 
			myStream.Close();

			var regex = new Regex("{(.*?)}");
			var matches = regex.Matches(webData);
			List<double> prices = new List<double>();
			var priceRegex = new Regex("val:(.*?)}");
			var dateRegex = new Regex("^Date[(0-9)]+$");

			var priceMatches = priceRegex.Matches(webData);
			var dateMatches = dateRegex.Matches(webData);



			foreach (Match match in matches)
			{
				var valueWithoutBrackets = match.Groups[1].Value; 
				var valueWithBrackets = match.Value; 
			}

			List<string> values = new List<string>();

			foreach(Match match in priceMatches)
			{
				var test = match.ToString().Split(':');
				var parsed = test[1].TrimEnd('}');
				xValues.Add(double.Parse(parsed));
			}



			//"MyChart is declared in the xaml file"
			//""
			MyChart.Series.Add(new LineSeries
			{
				Values = xValues,
				Stroke = Brushes.LightPink,
				Fill = new SolidColorBrush
				{
					Color = System.Windows.Media.Color.FromRgb(255, 182, 193),
					Opacity = 0.7
				},
				Title = "YuGiOhPrices"
			});
		}
	}


}
