﻿namespace MyApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ExportArt), typeof(ExportArt));
            Routing.RegisterRoute(nameof(AddArt), typeof(AddArt));
        }
    }
}
