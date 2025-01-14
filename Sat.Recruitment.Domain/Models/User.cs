﻿using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Models;

public class User
{
    [Key]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string UserType { get; set; }

    [Required]
    public decimal Money { get; set; }
}
