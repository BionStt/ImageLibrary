﻿using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ImageLibrary.Utilities;

namespace ImageLibrary.Models
{
    public class Auction
    {
        public Auction()
        {
            Id = GuidComb.GenerateComb();
        }

        public Auction(string name) : this()
        {
            this.Name = name;
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public bool CurrentBanner { get; set; }
        public bool PreviousBanner { get; set; }
    }
}