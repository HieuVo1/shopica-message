using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class MessageDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=mysql-37282-0.cloudclusters.net;Port=37282;Database=shopica_message;Uid=admin;Pwd=w4CJwIvs;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config one to many
            modelBuilder.Entity<Attachments>()
                .HasOne(a => a.Message)
                .WithMany(m => m.Attachments)
                .HasForeignKey(a => a.Message_id);

            modelBuilder.Entity<Messages>()
              .HasOne(a => a.Conversation)
              .WithMany(m => m.Messages)
              .HasForeignKey(a => a.Conversation_id);

            //config many to many
            modelBuilder.Entity<Participants>()
                .HasKey(p => new { p.Conversation_id, p.User_id });

            modelBuilder.Entity<Participants>()
                .HasOne<Users>(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.User_id);

            modelBuilder.Entity<Participants>()
                .HasOne<Conversations>(c => c.Conversation)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.Conversation_id);
        }

        public DbSet<Messages> Messages { get; set; }
        public DbSet<Conversations> Conversations { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Participants> Participants { get; set; }

        //auto add created_at vs updated_at
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseModel trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.Update_at = DateTime.Now;
                            entry.Property("Created_at").IsModified = false;
                            break;

                        case EntityState.Added:
                            trackable.Created_at = DateTime.Now;
                            trackable.Update_at = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
