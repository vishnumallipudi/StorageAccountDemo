using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SharedAccessSignatures.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SharedAccessSignatures.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var storageConnString = CloudConfigurationManager.GetSetting("StorageConnection");
            var storageAccountRef = CloudStorageAccount.Parse(storageConnString);

            CloudBlobClient blobClient = storageAccountRef.CreateCloudBlobClient();

            CloudBlobContainer containerRef = blobClient.GetContainerReference("democontainer");


            var blobs = new List<BlobImageModel>();
            foreach (var blob in containerRef.ListBlobs())
            {
                var sastoken = GetSASToken(storageAccountRef);
                if (blob.GetType()==typeof(CloudBlockBlob))
                {
                    blobs.Add(new BlobImageModel { BlobImageURI = blob.Uri.ToString()+sastoken });
                }
            }
            return View(blobs);
        }
        public static string GetSASToken(CloudStorageAccount sa)
        {
            SharedAccessAccountPolicy saap = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30),
                Protocols = SharedAccessProtocol.HttpsOrHttp
            };
            return sa.GetSharedAccessSignature(saap);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}