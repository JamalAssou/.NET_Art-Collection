using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;

namespace MyApp.ViewModel;

public partial class StatisticsViewModel : BaseViewModel
{
    [ObservableProperty]
    string? animalName;

    [ObservableProperty]
    public Chart myObservableChart = new BarChart();

    ChartEntry[] entries = new[]
       {
           new ChartEntry(51)
           {
               Label = "La Joconde",
               ValueLabel="$260m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(28)
           {
               Label = "Les Tournesols",
               ValueLabel= "$14.9m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(35)
           {
               Label = "The Nightmare",
               ValueLabel="$75.9m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(47)
           {
               Label = "The Scream",
               ValueLabel="$119m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(64)
           {
               Label = "Guernica",
               ValueLabel="$200m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(57)
           {
               Label = "La Nuit Etoilée",
               ValueLabel="$119m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(22)
           {
               Label = "Les Demoiselles d'Avignon",
               ValueLabel="$200m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(24)
           {
               Label = "La Persistance de la Mémoire",
               ValueLabel="$90m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(48)
           {
               Label = "Les Amoureux",
               ValueLabel="$80m",
               Color = SKColor.Parse("#b455b6")
           },
           new ChartEntry(87)
           {
               Label = "Abstracktes Bild",
               ValueLabel="$46m",
               Color = SKColor.Parse("#b455b6")
           }
       };

    public StatisticsViewModel()
    {
        Chart myChart = new BarChart
        {
            Entries = entries
        };

        MyObservableChart = myChart;
    }
    
}

