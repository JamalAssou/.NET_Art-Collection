using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Service
{
    public class DataAccesServices : DbContext
    {
        #region TABLES
        public DbSet<ArtCollectionModel> artCollections { get; set; }
        public DbSet<UserModel> Users { get; set; }


        #endregion

        #region CONSTRUCTOR
        public DataAccesServices(DbContextOptions<DataAccesServices> options) : base(options)
        {
            Database.EnsureCreated(); // c'est cette ligne de code qui permet de créer notre db
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtCollectionModel>()
                           .HasKey(e => e.Id_Art);
            modelBuilder.Entity<UserModel>()
                           .HasKey(e => e.Id_User);

            modelBuilder.Entity<ArtCollectionModel>()
                .HasOne(e => e.MyUser)
                .WithMany(e => e.artCollections)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id_User); // fait reference à la clé primaire qui concerne la clè etranger

            /* modelBuilder.Entity<UserModel>()
                .HasOne(e => e.MyCompany)
                .WithMany(e => e.MyUsers)
                .HasForeignKey(e => e.CompanyId)
                .HasPrincipalKey(e => e.Id_Comp); // id company*/
        }
        public void DeleteUserArts(int userId)
        {
            var artsToDelete = artCollections.Where(art => art.UserId == userId);
            artCollections.RemoveRange(artsToDelete);
            SaveChanges();
        }
    }
}