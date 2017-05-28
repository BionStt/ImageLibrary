using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ImageLibrary.Helpers;
using ImageLibrary.Models;
using System.Threading.Tasks;

namespace ImageLibrary.Controllers
{
    public class AuctionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            AuctionViewModel viewModel = new AuctionViewModel();
            var auctions = _db.Auctions;
            viewModel.Auctions = auctions.OrderByDescending(u => u.CreateDate).ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateAuctionViewModel viewModel = new CreateAuctionViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel viewModel)
        {
            var auction = new Auction();
            auction.Name = viewModel.Name;
            auction.Description = viewModel.Description;
            auction.CreateDate = DateTime.Now;
            auction.CurrentBanner = viewModel.PrimaryBanner;
            auction.PreviousBanner = viewModel.SecondaryBanner;
            _db.Entry(auction).State = System.Data.Entity.EntityState.Added;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            EditAuctionViewModel viewModel = new EditAuctionViewModel();
            var auction = _db.Auctions.First(a => a.Id == id);
            viewModel.Name = auction.Name;
            viewModel.Description = auction.Description;
            viewModel.PrimaryBanner = auction.CurrentBanner;
            viewModel.SecondaryBanner = auction.PreviousBanner;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditAuctionViewModel viewModel)
        {
            var auction = _db.Auctions.First(a => a.Id == viewModel.Id);
            auction.Id = viewModel.Id;
            auction.Name = viewModel.Name;
            auction.Description = viewModel.Description;
            auction.CurrentBanner = viewModel.PrimaryBanner;
            auction.PreviousBanner = viewModel.SecondaryBanner;
            _db.Entry(auction).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}