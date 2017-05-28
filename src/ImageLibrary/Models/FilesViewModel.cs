using ImageLibrary.Helpers;
using System;

namespace ImageLibrary.Models
{
    public class FilesViewModel
    {
        public ViewDataUploadFilesResult[] Files { get; set; }
    }

    public class AuctionFilesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}