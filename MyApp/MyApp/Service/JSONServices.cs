using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp.Service;

internal class JSONServices
{
    internal async Task GetArts()
    {     
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Arts.json");
        
        try
        {
            using var stream = File.Open(filePath, FileMode.Open);

            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            Globals.MyArts = JsonSerializer.Deserialize<List<Art>>(contents);

        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("JSON load Error!", ex.Message, "OK");
        }
    }

    internal async Task  SetArts()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Arts.json");
        
        try
        {
            if (File.Exists(filePath)) File.Delete(filePath); 
            using FileStream fileStream = File.Create(filePath);

            await JsonSerializer.SerializeAsync(fileStream, Globals.MyArts);
            await fileStream.DisposeAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("JSON save Error!", ex.Message, "OK");
        }
    }
}
