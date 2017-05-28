using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ImageLibrary.Helpers;
using ImageLibrary.Models;


namespace ImageLibrary.Controllers
{
    public class FileUploadController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        FilesHelper filesHelper;
        String tempPath;
        String serverMapPath;
        private string StorageRoot;
        private string UrlBase;
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public FileUploadController()
        {
            tempPath = "~/somefiles/";
            serverMapPath = "~/Files/";
            StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
            UrlBase = "/Files/";
            //filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot2, UrlBase, tempPath, serverMapPath);
        }

        [HttpGet]
        public ActionResult Index(Guid id)
        {
            AuctionFilesViewModel viewModel = new AuctionFilesViewModel();
            var auction = _db.Auctions.First(a => a.Id == id);
            viewModel.Id = auction.Id;
            viewModel.Name = auction.Name;
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Upload(Guid id)
        {
            serverMapPath = "~/Files/" + id + "/";
            StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
            UrlBase = "/Files/" + id + "/";
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }

        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFileList(Guid id)
        {
            serverMapPath = "~/Files/" + id + "/";
            StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
            UrlBase = "/Files/" + id + "/";
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Show(Guid id)
        {
            serverMapPath = "~/Files/" + id + "/";
            StorageRoot = Path.Combine(HostingEnvironment.MapPath(serverMapPath));
            UrlBase = "/Files/" + id + "/";
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            AuctionFilesViewModel viewModel = new AuctionFilesViewModel();
            var auction = _db.Auctions.First(a => a.Id == id);
            viewModel.Id = auction.Id;
            viewModel.Name = auction.Name;
            return View(viewModel);
        }
    }
}