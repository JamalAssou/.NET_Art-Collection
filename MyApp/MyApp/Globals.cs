global using System;

global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;

global using MyApp.View;
global using MyApp.ViewModel;
global using MyApp.Model;
global using MyApp.Service;

global using Microcharts;
global using Microcharts.Maui;

public class Globals
{
    public static List<Art> MyArts= new();
    public static List<Art> PossibleArtsCollection= new();
}