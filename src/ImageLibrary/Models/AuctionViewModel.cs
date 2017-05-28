using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ImageLibrary.Helpers;

namespace ImageLibrary.Models
{
    public class AuctionViewModel
    {
        public List<Auction> Auctions { get; set; }
    }

    public class CreateAuctionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Primary Banner")]
        public bool PrimaryBanner { get; set; }

        [Display(Name = "Secondary Banner")]
        public bool SecondaryBanner { get; set; }
    }

    public class EditAuctionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Primary Banner")]
        public bool PrimaryBanner { get; set; }

        [Display(Name = "Secondary Banner")]
        public bool SecondaryBanner { get; set; }
    }

    public class LatestAddedAuctionViewModel
    {
        public Auction CurrentAuction { get; set; }

        public Auction PreviousAuction { get; set; }
        public int imageCount { get; set; }
        public int closedImageCount { get; set; }

        public List<ViewDataUploadFilesResult> CurrentImages { get; set; }
        public List<ViewDataUploadFilesResult> PreviousImages { get; set; }
    }
}