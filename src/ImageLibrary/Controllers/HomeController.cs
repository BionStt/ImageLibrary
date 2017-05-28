﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ImageLibrary.Helpers;
using ImageLibrary.Models;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace ImageLibrary.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        FilesHelper filesHelper;
        String tempPath;
        String serverMapPath;
        private string StorageRoot;
        private string UrlBase;
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";

        //[Authorize]
        public ActionResult Index()
        {
            LatestAddedAuctionViewModel viewModel = new LatestAddedAuctionViewModel();
            var auctions = _db.Auctions;
            if (auctions != null)
            {
                //grab latest auction
                var latestAuctionList = auctions.OrderByDescending(u => u.CreateDate);
                var latestAuction = (from m in latestAuctionList select m).FirstOrDefault();
                var firstClosedAuction = latestAuctionList.Skip(1).FirstOrDefault();
                viewModel.auction = latestAuction;
                //get a count of images
                serverMapPath = "~/Files/" + latestAuction.Id + "/";
                StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
                int fileCount = Directory.GetFiles(StorageRoot, "*", SearchOption.TopDirectoryOnly).Length;
                viewModel.imageCount = fileCount;
                //get the image location
                UrlBase = "/Files/" + latestAuction.Id + "/";
                filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
                var list = filesHelper.FilesList(latestAuction.Id);
                viewModel.Images = list.ToList();
                //get last closed auction
                serverMapPath = "~/Files/" + firstClosedAuction.Id + "/";
                StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
                int fileCount2 = Directory.GetFiles(StorageRoot, "*", SearchOption.TopDirectoryOnly).Length;
                viewModel.closedImageCount = fileCount2;
                UrlBase = "/Files/" + firstClosedAuction.Id + "/";
                filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
                var closedAuction = filesHelper.FilesList(firstClosedAuction.Id);
                viewModel.ClosedImages = closedAuction.ToList();
                viewModel.closedAuction = firstClosedAuction;
            }
            return View(viewModel);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Works()
        {
            return View();
        }

        public string EmailData(EmailDataViewModel viewModel)
        {
            string api_user = "#";  //sendgrid relay user
            string api_key = "#";   //sendgrid relay key
            string toAddress = "#;";
            string toName = "#";
            string subject = "Message submitted from Website.";
            string text = viewModel.Name + " " + viewModel.Message;
            string fromAddress = viewModel.Email;
            string url = "https://sendgrid.com/api/mail.send.json";
            // Create a form encoded string for the request body
            string parameters = "api_user=" + api_user + "&api_key=" + api_key + "&to=" + toAddress +
                                "&toname=" + toName + "&subject=" + subject + "&text=" + text +
                                "&from=" + fromAddress;

            try
            {
                //Create Request
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

                // Create a new write stream for the POST body
                StreamWriter streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream());

                // Write the parameters to the stream
                streamWriter.Write(parameters);
                streamWriter.Flush();
                streamWriter.Close();

                // Get the response
                HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                // Create a new read stream for the response body and read it
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                return "Email sent!  We will be in touch with you soon.";
                // Write the results to the console
                //Console.WriteLine(result);
            }
            catch (WebException ex)
            {
                // Catch any execptions and gather the response
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                // Create a new read stream for the exception body and read it
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
                return "Email failed!";
                // Write the results to the console
                //Console.WriteLine(result);
            }
        }

        public class EmailDataViewModel
        {
            public string Name { get; set; }
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            public string Message { get; set; }
        }
    }
}