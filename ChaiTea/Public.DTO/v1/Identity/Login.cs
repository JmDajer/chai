﻿using System.ComponentModel.DataAnnotations;

namespace Public.DTO.v1.Identity;

public class Login
{
        [StringLength(128, MinimumLength = 5, ErrorMessage = "Wrong length email")]
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
}