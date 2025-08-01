﻿using System.ComponentModel.DataAnnotations;

namespace DeliveryLocal.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
