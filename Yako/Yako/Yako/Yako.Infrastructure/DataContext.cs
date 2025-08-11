using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yako.Infrastructure.Entities;

namespace Yako.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser,AppRole, Guid>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusinessRating>()
        .HasIndex(r => new { r.BusinessId, r.UserId })
        .IsUnique();

            var categoryRestoran = Guid.NewGuid();
            var categoryPark = Guid.NewGuid();
            var categoryMuze = Guid.NewGuid();
            var categoryDoga = Guid.NewGuid();
            var categoryCafe = Guid.NewGuid();
            var categoryKutuphane = Guid.NewGuid();

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = categoryRestoran, Name = "Restoran" },
                new Category { Id = categoryPark, Name = "Park" },
                new Category { Id = categoryMuze, Name = "Müze" },
                new Category { Id = categoryDoga, Name = "Doğa" },
                new Category { Id = categoryCafe, Name = "Cafe" },
                new Category { Id = categoryKutuphane, Name = "Kütüphane" }
            );

            modelBuilder.Entity<Business>().HasData(
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Lezzet Durağı",
                    Description = "Eşsiz tatlar sunan bu restoran, taze malzemeleri ve samimi atmosferiyle öne çıkıyor. Aile yemekleri ve özel günler için ideal.",
                    Address = "ABC Adres 23/AB Beylikdüzü / İstanbul",
                    Location = "Ab",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d96374.75531637031!2d28.490740386946495!3d40.98780644834785!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14b55fc19deb0b3b%3A0xdf4ea093f30983c6!2zQmV5bGlrZMO8esO8L8Swc3RhbmJ1bA!5e0!3m2!1str!2str!4v1747503585138!5m2!1str!2str",
                    ImageUrl = "~/img/restaurant-interior.jpg",
                    CategoryId = categoryRestoran
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Tarihi Kahve Evi",
                    Description = "Geleneksel Türk kahvesi ve nostaljik atmosferiyle ziyaretçilerine huzurlu bir deneyim sunuyor.",
                    Address = "Cumhuriyet Mah. 15. Sokak No:12 Üsküdar / İstanbul",
                    Location = "Üsküdar",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48152.71064922306!2d28.9698491500249!3d41.03522219759064!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14cac826d524c9f1%3A0xc14f7612337b7f38!2zw5xza8O8ZGFyL8Swc3RhbmJ1bA!5e0!3m2!1str!2str!4v1747503358041!5m2!1str!2str",
                    ImageUrl = "~/img/indir1.jpg",
                    CategoryId = categoryRestoran
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Doğa Parkı",
                    Description = "Yeşillikler içinde yürüyüş yolları, gölet ve piknik alanları ile şehirden uzaklaşmak isteyenler için birebir.",
                    Address = "Doğa Yolu Cad. No:1 Sapanca / Sakarya",
                    Location = "Sapanca",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48403.895557168966!2d30.20122487339016!3d40.690635177290815!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14ccaf3818a3c281%3A0xa16aa914be62c628!2sSapanca%2C%20Sakarya!5e0!3m2!1str!2str!4v1747503650083!5m2!1str!2str",
                    ImageUrl = "~/img/indir2.jpg",
                    CategoryId = categoryPark
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Sanat Galerisi Modern",
                    Description = "Modern ve çağdaş sanat eserlerine ev sahipliği yapan bu galeri, sanatseverlerin uğrak noktası.",
                    Address = "İstiklal Cad. No:145 Beyoğlu / İstanbul",
                    Location = "Beyoğlu",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24073.493575970937!2d28.94677986181159!3d41.043046166088175!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14cab75ad870363d%3A0x25bc05b50533efb0!2zQmV5b8SfbHUvxLBzdGFuYnVs!5e0!3m2!1str!2str!4v1747503715772!5m2!1str!2str",
                    ImageUrl = "~/img/indir.jpg",
                    CategoryId = categoryMuze
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Kampçılar Vadisi",
                    Description = "Doğa severler için kamp, yürüyüş ve bisiklet yolları ile ideal bir kaçış noktası.",
                    Address = "Vadi Mevkii No:7 Fethiye / Muğla",
                    Location = "Fethiye",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d51215.09398848661!2d29.08067032055296!3d36.651812855882454!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14c0439b0db6b13b%3A0x87150704244f70d7!2zRmV0aGl5ZSwgTXXEn2xh!5e0!3m2!1str!2str!4v1747503802157!5m2!1str!2str",
                    ImageUrl = "~/img/indir3.jpg",
                    CategoryId = categoryDoga
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Lezzet Bahçesi",
                    Description = "Organik ürünlerle hazırlanan yemekleri ve bahçe atmosferiyle dikkat çeken bir aile restoranı.",
                    Address = "Bahçelievler Mah. 7. Cad. No:45 Çankaya / Ankara",
                    Location = "Çankaya",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d196186.0921243584!2d32.71829758380999!3d39.79784598125841!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14d345ad9f357261%3A0x4b01c691eefd6f1d!2zw4dhbmtheWEvQW5rYXJh!5e0!3m2!1str!2str!4v1747503859235!5m2!1str!2str",
                    ImageUrl = "~/img/indir4.jpg",
                    CategoryId = categoryCafe
                },
                new Business
                {
                    Id = Guid.NewGuid(),
                    Title = "Kültür Kütüphanesi",
                    Description = "Geniş kitap arşivi, sessiz çalışma alanları ve ücretsiz internet hizmetiyle öğrenci dostu bir ortam sunar.",
                    Address = "Kültür Cad. No:89 Konak / İzmir",
                    Location = "Konak",
                    MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d50013.5428788959!2d27.08795479314152!3d38.4219137380358!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd8e2fece48eb%3A0xafa58b890c33632a!2zS29uYWsvxLB6bWly!5e0!3m2!1str!2str!4v1747503918461!5m2!1str!2str",
                    ImageUrl = "~/img/indir5.jpg",
                    CategoryId = categoryKutuphane
                }
            );
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BusinessRating> BusinessRatings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageToo> MessageToos { get; set; }
    }
}
