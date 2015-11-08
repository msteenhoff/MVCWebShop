namespace WebShopPage.Models
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;


    public partial class WebshopContext : DbContext
    {
        public WebshopContext()
            : base("name=DB")
        {
            //Database.CreateIfNotExists();
        }
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Produkt> Produkter { get; set; }
        public DbSet<Ordre> OrdreDB { get; set; }
        public DbSet<OrdreLinje> Ordrelinjer { get; set; }
        public DbSet<Handlekurv> Handlekurver { get; set; }
        public DbSet<KurvProdukt> KurvProdukter { get; set; }
        //public DbSet<Poststed> Poststeder { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}