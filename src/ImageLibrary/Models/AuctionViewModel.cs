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
    }

    public class EditAuctionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class LatestAddedAuctionViewModel
    {
        public Auction auction { get; set; }

        public Auction closedAuction { get; set; }
        public int imageCount { get; set; }
        public int closedImageCount { get; set; }

        public List<ViewDataUploadFilesResult> Images { get; set; }
        public List<ViewDataUploadFilesResult> ClosedImages { get; set; }
    }
}