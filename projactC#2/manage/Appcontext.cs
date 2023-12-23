using Microsoft.EntityFrameworkCore;


namespace projactC_2.manage
{
    internal class Appcontext : DbContext
    {

       
        
            public DbSet<Users> User { get; set; }
            public DbSet<Task> Tasks { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {


            optionsBuilder.UseSqlServer("Data Source=LAPTOP-R0QOES6Q\\SQLEXPRESS01; Initial Catalog=Task_Manager; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");


            }

        
    }
}
