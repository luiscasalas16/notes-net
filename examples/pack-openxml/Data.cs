using System.Data;
using Bogus;

namespace example_net_excel
{
    internal static class Data
    {
        public static void Generate(out List<User> users, out List<Order> orders)
        {
            var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };

            var userIds = 0;
            var testUsers = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.UserId, f => userIds++)
                .RuleFor(u => u.Identification, f => f.Random.Replace("#-####-####"))
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>());
            users = testUsers.GenerateBetween(3, 3);

            var usersIds = users.Select(u => u.UserId).ToList();

            var orderIds = 0;
            var testOrders = new Faker<Order>()
                .StrictMode(true)
                .RuleFor(o => o.UserId, f => f.PickRandom(usersIds))
                .RuleFor(o => o.OrderId, f => orderIds++)
                .RuleFor(o => o.Date, f => f.Date.Between(new DateTime(2010, 1, 1), new DateTime(2020, 12, 31)))
                .RuleFor(o => o.Item, f => f.PickRandom(fruit))
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(o => o.UnitPrice, f => Math.Round(f.Random.Double(100d, 1000d), 2))
                .RuleFor(o => o.TotalPrice, (f, u) => u.Quantity * u.UnitPrice);
            orders = testOrders.GenerateBetween(9, 9);
        }

        /// <summary>
        /// Convert a list of Users to a DataTable.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static DataTable ObjectsToDataTable(List<User> users)
        {
            var dataTable = new DataTable("Users");

            dataTable.Columns.Add("UserId", typeof(int));
            dataTable.Columns.Add("Identification", typeof(string));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));

            foreach (var user in users)
            {
                dataTable.Rows.Add(user.UserId, user.Identification, user.FirstName, user.LastName, user.Email, user.Gender);
            }

            return dataTable;
        }

        /// <summary>
        /// Convert a list of Orders to a DataTable.
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static DataTable ObjectsToDataTable(List<Order> orders)
        {
            var dataTable = new DataTable("Orders");

            dataTable.Columns.Add("UserId", typeof(int));
            dataTable.Columns.Add("OrderId", typeof(int));
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("Item", typeof(string));
            dataTable.Columns.Add("Quantity", typeof(int));
            dataTable.Columns.Add("UnitPrice", typeof(double));
            dataTable.Columns.Add("TotalPrice", typeof(double));

            foreach (var user in orders)
            {
                dataTable.Rows.Add(user.UserId, user.OrderId, user.Date, user.Item, user.Quantity, user.UnitPrice, user.TotalPrice);
            }

            return dataTable;
        }

        /// <summary>
        /// Convert a DataTable to a list of Users.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<User> DataTableToUsersObjects(DataTable table)
        {
            var users = new List<User>(table.Rows.Count);

            foreach (DataRow row in table.Rows)
            {
                users.Add(
                    new User()
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        Identification = Convert.ToString(row["Identification"]),
                        FirstName = Convert.ToString(row["FirstName"]),
                        LastName = Convert.ToString(row["LastName"]),
                        Email = Convert.ToString(row["Email"]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), Convert.ToString(row["Gender"])),
                    }
                );
            }

            return users;
        }

        /// <summary>
        /// Convert a DataTable to a list of Orders.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<Order> DataTableToOrdersObjects(DataTable table)
        {
            var users = new List<Order>(table.Rows.Count);

            foreach (DataRow row in table.Rows)
            {
                users.Add(
                    new Order()
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        OrderId = Convert.ToInt32(row["OrderId"]),
                        Date = Convert.ToDateTime(row["Date"]),
                        Item = Convert.ToString(row["Item"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        UnitPrice = Convert.ToDouble(row["UnitPrice"]),
                        TotalPrice = Convert.ToDouble(row["TotalPrice"])
                    }
                );
            }

            return users;
        }
    }

    internal class User
    {
        public int UserId { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }

    internal enum Gender
    {
        Male,
        Female
    }

    internal class Order
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
