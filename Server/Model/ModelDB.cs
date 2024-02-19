namespace Server;

using System.Data.SQLite; 
using System.Collections.Generic; 

public class SqlLiteOrderRepository : IOrderRepository
{
    private readonly string _connectionString;
    private List<Order> orders = new List<Order>();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Orders (
            ID VARCHAR(32) PRIMARY KEY,
            Name TEXT NOT NULL,
            Price REAL NOT NULL
        )";
    public SqlLiteOrderRepository(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
        ReadDataFromDatabase();
    }

    private void ReadDataFromDatabase()
    {
        orders = GetAllOrders();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString); 
        Console.WriteLine("База данных :  " + _connectionString + " создана");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection);
        command.ExecuteNonQuery();
    }

    public List<Order> GetAllOrders()
    {
        List<Order> orders = new List<Order>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order(reader["ID"].ToString(), reader["Name"].ToString().ToUpper(), Convert.ToDouble(reader["Price"]));
                        orders.Add(order);
                    }
                }
            }
        }
        return orders;
    }

    public void AddOrder(Order order)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Orders (ID, Name, Price) VALUES (@ID, @Name, @Price)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", order.ID);
                command.Parameters.AddWithValue("@Name", order.Name);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteOrder(string id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Orders WHERE ID = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}

