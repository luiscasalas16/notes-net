using System.Data;

namespace example_net_excel
{
    class Program
    {
        public static void Main()
        {
            Data.Generate(out List<User> users, out List<Order> orders);

            Console.WriteLine($"original users count {users.Count}");
            Console.WriteLine($"original orders count {orders.Count}");
            Console.WriteLine();

            string filePath = Environment.CurrentDirectory + "..\\..\\..\\..\\test.xlsx";

            //create file

            var dtDataUsers = Data.ObjectsToDataTable(users);
            var dtDatatOrders = Data.ObjectsToDataTable(orders);

            byte[] fileContent;

            using (MemoryStream stream = new MemoryStream())
            {
                using (var document = Helper.CreateDocument(stream))
                {
                    Helper.InsertSheet(document, dtDataUsers);
                    Helper.InsertSheet(document, dtDatatOrders);
                }

                fileContent = Helper.GetDocumentBytes(stream);
            }

            if (File.Exists(filePath))
                File.Delete(filePath);

            File.WriteAllBytes(filePath, fileContent);

            //read file

            DataTable usersDataTable = null;
            DataTable ordersDataTable = null;

            using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(filePath)))
            {
                using (var document = Helper.OpenDocument(stream))
                {
                    usersDataTable = Helper.GetSheet(document, "Users");
                    ordersDataTable = Helper.GetSheet(document, "Orders");
                }
            }

            List<User> lHelperUsers = Data.DataTableToUsersObjects(usersDataTable);
            List<Order> lHelperOrders = Data.DataTableToOrdersObjects(ordersDataTable);

            Console.WriteLine($"file users count {lHelperUsers.Count}");
            Console.WriteLine($"file orders count {lHelperOrders.Count}");
            Console.WriteLine();

            Console.WriteLine("...");

            Console.ReadLine();
        }
    }
}


