using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.VoteService
{
	public class VoteService : IVoteService
	{
		private readonly AppDbContext appDbContext;
		public VoteService(AppDbContext dbContext)
		{
			this.appDbContext = dbContext;
		}

		public bool HasVoted(string userId, Guid postId) => this.appDbContext.PostUserMappings.Any(pu => (pu.UserId == userId) && (pu.PostId == postId));
	}
}
