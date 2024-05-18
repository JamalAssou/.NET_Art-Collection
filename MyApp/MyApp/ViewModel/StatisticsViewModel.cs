using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SkiaSharp;


namespace MyApp.ViewModel
{
    public partial class StatisticsViewModel : BaseViewModel
    {

        [ObservableProperty]
        public Chart myObservableChart;

        List<ChartEntry> chartEntries = new List<ChartEntry>();
        Random random = new Random();


        public StatisticsViewModel()
        {
            if (Globals.MyArts != null) addCharacterToChartList();
        }


        private void addCharacterToChartList()
        {
            Dictionary<string, int> yearCounts = new Dictionary<string, int>();

            foreach (Art art in Globals.MyArts)
            {
                string year = art.Year;
                if (yearCounts.ContainsKey(year))
                {
                    yearCounts[year]++;
                }
                else
                {
                    yearCounts[year] = 1;
                }
            }

            List<ChartEntry> chartEntries = new List<ChartEntry>();
            foreach (var id in yearCounts)
            {
                SKColor randomColor = new SKColor((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));
                ChartEntry entry = new ChartEntry(id.Value)
                {
                    Label = id.Key,
                    ValueLabel = id.Value.ToString(),
                    Color = randomColor
                };
                chartEntries.Add(entry);
            }

            MyObservableChart = new BarChart { Entries = chartEntries.ToArray() };

        }
    }
}