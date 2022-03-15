﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class PostUserMapping
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		public Guid PostId { get; set; }
		public Post Post { get; set; }
	}
}
