using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    public partial class DBViewModel:BaseViewModel
    {
        public ObservableCollection<UserModel> MyObservableUsers { get; } = new();

        public ObservableCollection<ArtCollectionModel> MyObservableArt { get; } = new();

        DataAccesServices DBService;

        public DBViewModel(DataAccesServices dBService)
        {
            DBService = dBService;

            var user1 = new UserModel
            {
                Email = "use",
             
            };

            var user2 = new UserModel
            {
                Email = "use",
            };

            var user3 = new UserModel
            {
                Email = "use",
            };

            var art1 = new ArtCollectionModel
            {
                Name = "jocande",
                UserId = 1
            };

            var art2 = new ArtCollectionModel
            {
                Name = "jocandee",
                UserId = 1

            };

            var art3 = new ArtCollectionModel
            {
                Name = "jocandeee",
                UserId = 1

            };

            DBService.artCollections.Add(art1);
            DBService.artCollections.Add(art2);
            DBService.artCollections.Add(art3);
            DBService.Users.Add(user1);
            DBService.Users.Add(user2);
            DBService.Users.Add(user3);

            dBService.SaveChanges(); // on enregistre les enregistrements en base de données

            List<ArtCollectionModel> comp = DBService.artCollections.Where(p => p.Name.Contains("joc")).ToList();//ici on fait une recherche dans DBset la où le name continet "comp" (dans ce cas, c'est tout)

            foreach (var item in comp)
            {
                MyObservableArt.Add(item);
            }

            List<UserModel> user = DBService.Users.Where(p => p.Email.Contains("am")).ToList();
            user = DBService.Users.Where(p => p.Password.Contains("am")).ToList();

            foreach (var item in user)
            {
                MyObservableUsers.Add(item);
            }
        }
    }
}