using clothing_store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


    public class Account
    {
        [Key]
        [Required(ErrorMessage = "AccountId is required.")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email length can't exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        public string Role { get; set; }

        public Basket Basket { get; set; }

        public List<Order> Orders { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 50 characters.")]
        public string Surname { get; set; }

        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        public bool Sex { get; set; }

        [Range(50, 300, ErrorMessage = "Height must be between 50 and 300 cm.")]
        public int Height { get; set; }

        [Range(30, 500, ErrorMessage = "Weight must be between 30 and 500 kg.")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "FavouriteColor is required.")]
        public Color FavouriteColor { get; set; }

        public int? AddressId;
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        public List<SpecialDiscount> Discounts { get; set; }

        public bool CorporateClient { get; set; }

        public bool Newsletter { get; set; }
    }

