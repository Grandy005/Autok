using MySql.Data.MySqlClient;

class Program
{
    //Method to get and verify input
    static string GetInput(string inputText, bool canBeNull = false)
    {
        string input = "";
        do
        {
            Console.Write(inputText);
            input = Console.ReadLine();

            if (!canBeNull)
            {
                if (input.Trim().Length > 0)
                {
                    return input.Trim();
                }
                else
                {
                    Console.WriteLine("HIBÁS ADAT!");
                }
            }
            else if (canBeNull)
            {
                return input.Trim();
            }

        } while (true);
    }

    static void GetBrands(MySqlConnection conn)
    {
        //Writing the query
        string query = "SELECT * FROM cars";
        MySqlCommand cmd = new MySqlCommand(query, conn);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            Console.WriteLine("Autó azonosítója    Autó márkája");
            while (reader.Read())
            {
                //Writing every cars id and brand to console
                Console.WriteLine($"{reader.GetInt32(0)}                   {reader.GetString(1)}");
            }
            reader.Close();
        }
    }

    //Method to insert new car into the list
    static void AddNewCar(MySqlConnection conn)
    {
        string id =  GetInput("Add meg az autó azonosítóját: ");
        string brand = GetInput("Add meg az autó márkáját: ");
        string type = GetInput("Add meg az autó típusát: ");
        string license = GetInput("Add meg az autó rendszámát: ");
        string date = GetInput("Add meg a gyártási dátumot (YYYY-MM--DD): ");

        string query = $"INSERT INTO Cars (id, Brand, Type, License, Date) VALUES (@id, @brand, @type, @license, @date);\r\n";

        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        {
            Console.WriteLine(cmd.ExecuteNonQuery() > 0 ? "A query sikeres volt!\n" : "A query sikertelen volt!\n");
            cmd.Parameters.AddWithValue("@id", id);

        }

    }

    //Method to modify the 23th car manufacture date
    static void ModifyDate(MySqlConnection conn)
    {
        string date = GetInput("Add meg az új gy");
    }

    static void Main()
    {
        string connectionString = "Server=localhost;Database=auto;Uid=root;Pwd=;";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                //Opening the connection
                conn.Open();
                Console.WriteLine("Sikeres kapcsolódás!\n");

                GetBrands(conn);

                AddNewCar(conn);
                
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HIBA: " + ex.Message);
            }
        }
    }
}
