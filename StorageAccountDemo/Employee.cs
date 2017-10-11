using Microsoft.WindowsAzure.Storage.Table;

namespace StorageAccountDemo
{
    class Employee:TableEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }


        public Employee()
        {

        }
        public Employee(string Name,string Email)
        {
            this.Name = Name;
            this.Email = Email;
            this.PartitionKey = "US";
            this.RowKey = Email;
        }

    }
}
