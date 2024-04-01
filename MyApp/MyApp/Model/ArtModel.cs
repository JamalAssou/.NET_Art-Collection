using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Model;
public class Art
{
    public int Id { get; set; }
    public string? Price { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? Year { get; set; }
    public string? Picture { get; set; }

   // [JsonIgnore] // pour ignorer le fait de save l'image dans le JSON car impossible...
   // public ImageSource? SelectedImage { get; set; }


    // public string? ImageFileName { get; set; } 

}

