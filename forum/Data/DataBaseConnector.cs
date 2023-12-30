namespace forum.Data;
using System;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class DatabaseConnector
{
    private string connectionString = "Data Source=localhost;Initial Catalog=Forum;Integrated Security=True";

    public bool TestConnection()
    {
        try
        {
            // Creating a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            // Open the database connection
            connection.Open();

            return true;
        }
        catch (Exception ex)
        {
            // Handle connection errors
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }


    public SqlConnection getConnection()
    {
        try
        {
            // Creating a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            // Open the database connection
            connection.Open();

            return connection;
        }
        catch (Exception ex)
        {
            // Handle connection errors
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}