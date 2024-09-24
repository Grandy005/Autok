using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=auto;Uid=root;Pwd=;";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                //Opening the connection
                conn.Open();
                Console.WriteLine("Connection successful!");

                //Writing the query
                string query = "SELECT * FROM cars";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Autó azonosítója    Autó márkája");
                    while (reader.Read())
                    {
                        // Example of reading columns (assuming first column is string)
                        Console.WriteLine($"{reader.GetInt32(0)}                   {reader.GetString(1)}");
                    }
                    reader.Close();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HIBA: " + ex.Message);
            }
        }
    }
}
