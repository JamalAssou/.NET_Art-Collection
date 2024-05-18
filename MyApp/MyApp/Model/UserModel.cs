using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Model
{
    public class UserModel
    {
        public int Id_User { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<ArtCollectionModel> artCollections { get; set;}

        internal static object FindFirst(string nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }

    public class ArtCollectionModel
    {
        public int Id_Art { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public string Price { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;

        public int UserId { get; set; }
        public UserModel? MyUser { get; set; }

    }
}