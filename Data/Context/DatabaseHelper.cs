using System;
using System.Data;
using System.Data.SqlClient;



namespace OrganizationChartMIS.Data.Context
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OrgMISConnection")!;
        }

        public SqlConnection EstablishConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        // retrieves data set and stores it in datatable
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = EstablishConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var entry in parameters)
                        {
                            command.Parameters.AddWithValue(entry.Key, entry.Value ?? DBNull.Value);
                        }
                    }

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        // updates specified table/field
        public int ExecuteUpdate(string commandText, Dictionary<string, object> parameters = null)
        {
            using (var connection = EstablishConnection())
            {
                using (var command = new SqlCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var entry in parameters)
                        {
                            command.Parameters.AddWithValue(entry.Key, entry.Value);
                        }
                    }

                    int affectedRows = command.ExecuteNonQuery();
                    Console.WriteLine($"{affectedRows} rows were updated.");

                    return affectedRows;
                }
            }
        }
    }
}
