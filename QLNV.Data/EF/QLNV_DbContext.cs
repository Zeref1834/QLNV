using Microsoft.EntityFrameworkCore;
using QLNV.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.Data.EF
{
    public class QLNV_DbContext : DbContext
    {
        public QLNV_DbContext(DbContextOptions<QLNV_DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nhanvien>().HasKey(nv => new { nv.MANV });
        }

        public DbSet<Nhanvien> Nhanviens { get; set; }
    }
}
