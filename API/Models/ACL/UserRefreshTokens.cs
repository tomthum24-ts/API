﻿using System.ComponentModel.DataAnnotations;

namespace API.Models.ACL
{
    public class UserRefreshTokens
    {
		[Key]
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string RefreshToken { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
