using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfiguration
{
    public class GigConfiguration: EntityTypeConfiguration<Gig>
    {        
        public GigConfiguration()
        {
            Property(g => g.ArtistId)
                .IsRequired();            

            Property(g => g.Venue)
                    .HasMaxLength(255)
                    .IsRequired();
            
            Property(g => g.GenreId)
                    .IsRequired();

            HasMany(g => g.Attendances)
                .WithRequired(a => a.Gig)
                .WillCascadeOnDelete(false);
        }                  
    }
}