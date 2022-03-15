using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityTypeConfigurations
{

	public class PostUsersMappingConfiguration : IEntityTypeConfiguration<PostUserMapping>
	{
		public void Configure(EntityTypeBuilder<PostUserMapping> builder)
		{
			builder.HasKey(a => a.Id);

			builder.HasAlternateKey(uam => new { uam.UserId, uam.PostId });

			builder.HasOne(uam => uam.User)
				.WithMany(u => u.PostUserMappings)
				.HasForeignKey(uam => uam.UserId);

			builder.HasOne(uam => uam.Post)
				.WithMany(a => a.PostUserMappings)
				.HasForeignKey(uam => uam.PostId);
		}
	}
}
