namespace Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    public class PostUsersMappingConfiguration : IEntityTypeConfiguration<PostUsersMapping>
    {
        public void Configure(EntityTypeBuilder<PostUsersMapping> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasAlternateKey(uam => new { uam.UserId, uam.PostId });

            builder.HasOne(uam => uam.User)
                .WithMany(u => u.PostUsersMapping)
                .HasForeignKey(uam => uam.UserId);

            builder.HasOne(uam => uam.Post)
                .WithMany(a => a.PostUsersMappings)
                .HasForeignKey(uam => uam.PostId);
        }
    }
}
