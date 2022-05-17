using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.VoteService
{
	public interface IVoteService
	{
		bool HasVoted(string userId, Guid postId);
	}
}
