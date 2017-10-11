 using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace StorageAccountDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount csa = CloudStorageAccount
                                        .Parse(CloudConfigurationManager.GetSetting("StorageAccountKey"));
            CloudQueueClient cqc= csa.CreateCloudQueueClient();

            CloudQueue cq= cqc.GetQueueReference("employeeq");

            cq.CreateIfNotExists();

            cq.AddMessage(new CloudQueueMessage("Hello world"));

            Console.WriteLine("Added Message Now");

            //var n =(int) cq.ApproximateMessageCount;
            foreach (var msg in cq.GetMessages(2, null, null, null))
            {
                Console.WriteLine(msg.AsString);
            }
            Console.WriteLine("now get messagae");
            Console.WriteLine((cq.GetMessage()).AsString);
            Console.WriteLine("now queue is ");
            foreach (var msg in cq.GetMessages(2, null, null, null))
            {
                Console.WriteLine(msg.AsString);
            }
            Console.ReadLine();

        }

       

    }
}
