using System;
using System.Data;
using System.Collections.Generic;

using MySqlConnector;
using System.Collections;
using Core.System.Data.Model;

namespace Core
{
    internal class UpgradeManager
    {
        internal MySqlConnection connection;

        private readonly string connectionString;
        private readonly string qryReturnId = "SELECT LAST_INSERT_ID();";

        public int LastInsertedId { get; private set; }

        /// <summary>
        /// This constructor initializes the UpgradeManager object with default connection parameters.
        /// </summary>
        /// <returns>None</returns>
        public UpgradeManager()
        {
            string[] connectionParameters =
            { 
                "localhost",
                "dbjanmos",
                "root",
                ""
            };

            this.connectionString = String.Format("server={0};database={1};userid={2};password={3};", 
                connectionParameters[0], connectionParameters[1], connectionParameters[2], connectionParameters[3]);
        }


        /// <summary>
        /// This constructor allows the user to specify a custom connection string.
        /// </summary>
        /// <param name="connectionString">String represents the connection string</param>
        /// <returns>None</returns>
        public UpgradeManager(string connectionString)
        {
            this.connectionString = connectionString;
        }


        /// <summary>
        /// This method is responsible for establishing a connection to the database using the provided connection string
        /// </summary>
        /// <param>None</param>
        /// <returns>None</returns>
        /// <exception cref="Exception">It catches any exceptions that occur during the execution of the query and rethrows them with a new exception containing the error message.</exception>
        private protected void Connect()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (MySqlException ex)
            {
                // Log or handle specific MySql errors here
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }


        /// <summary>
        /// This method executes a parameterized query and returns the result in the form of boolean.
        /// </summary>
        /// <param name="query">A string representing the SQL query to execute.</param>
        /// <param name="parameters">A dictionary containing parameter names and their corresponding values.</param>
        /// <param name="returnId">A boolean representing if the query should return the last inserted id. Default value is false</param>>
        /// <returns>True if the query execution is successful, otherwise false.</returns>
        /// <exception cref="Exception">It catches any exceptions that occur during the execution of the query and rethrows them with a new exception containing the error message.</exception>
        public bool ExecuteQuery(string query, Dictionary<string, string> parameters, bool returnId=false)
        {
            try
            {
                this.Connect();

                if (returnId)
                    String.Concat(query, this.qryReturnId);
                
                using (MySqlCommand cmd = new MySqlCommand(query, this.connection))
                {
                    foreach (KeyValuePair<string, string> kvp in parameters)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }

                    if (returnId)
                        this.LastInsertedId = Convert.ToInt32(cmd.ExecuteScalar());
                    else
                        cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                // Log or handle specific MySql errors here
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }


        /// <summary>
        /// This method executes a query against the database and returns the result in the form of a DataTable
        /// </summary>
        /// <param name="query">A string representing the SQL query to execute.</param>
        /// <returns>It returns the populated <see cref="System.Data.DataTable"/> containing the result of the query.</returns>
        /// <exception cref="Exception">
        /// It catches any exceptions that occur during the execution of the query and rethrows them with
        /// a new exception containing the error message.</exception>
        public DataTable Load(string query)
        {
            return this.Load(query, null);
        }


        /// <summary>
        /// This method executes a parameterized query against the database and returns the result in the form of a DataTable
        /// </summary>
        /// <param name="query">A string representing the SQL query to execute.</param>
        /// <param name="param">A dictionary containing parameter names and their corresponding values.</param>
        /// <returns>It returns the populated <see cref="System.Data.DataTable"/> containing the result of the query.</returns>
        /// <exception cref="Exception">It catches any exceptions that occur during the execution of the query and rethrows them with
        /// a new exception containing the error message.</exception>
        public DataTable Load(string query, Dictionary<string, string> param)
        {
            try
            {
                this.Connect();
                DataTable dt;
                using (MySqlCommand cmd = new MySqlCommand(query, this.connection))
                {
                    if (param != null)
                    {
                        foreach (KeyValuePair<string, string> kvp in param)
                        {
                            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                        }
                    }
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log or handle specific MySql errors here
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }


        /// <summary>
        /// This method executes a query against the database and populates a list of key-value pairs.
        /// </summary>
        /// <param name="query">A string representing the SQL query to execute.</param>
        /// <returns>A list of key-value pairs.</returns>
        /// <exception cref="Exception">
        /// It catches any exceptions that occur during the execution of the query and rethrows them 
        /// with a new exception containing the error message.
        /// </exception>
        public List<KeyValuePair<int, string>> Populate(string query)
        {
            List<KeyValuePair<int, string>> keyValueList;
            try
            {
                this.Connect();
                UpgradeManager db = new UpgradeManager();
                using (MySqlCommand cmd = new MySqlCommand(query, db.connection))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        keyValueList = new List<KeyValuePair<int, string>>();
                        while (dr.Read())
                        {
                            int Id = dr.GetInt32(0);
                            string Description = dr.GetString(1);

                            KeyValuePair<int, string> category = new KeyValuePair<int, string>(Id, Description);
                            keyValueList.Add(category);
                        }

                        dr.Close();
                        return keyValueList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log or handle specific MySql errors here
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }
        public object ExecuteScalar(string query)
        {
            return this.ExecuteScalar(query, null);
        }
        public object ExecuteScalar(string query, Dictionary<string, string> parameters)
        {
            try
            {
                this.Connect();
                using (MySqlCommand cmd = new MySqlCommand(query, this.connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        return cmd.ExecuteScalar();
                    }
                    else
                    {
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }

        public List<CustomerOrderDetails> GetCustomerOrderProduct(string query, Dictionary<string, string> GCOParams)
        {
            List<CustomerOrderDetails> orders = new List<CustomerOrderDetails>();
            try
            {
                this.Connect();
                using (MySqlCommand cmd = new MySqlCommand(query, this.connection))
                {
                    foreach (var param in GCOParams)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CustomerOrderDetails order = new CustomerOrderDetails
                            {
                                InventoryID = new Core.System.Data.Model.Inventory {
                                    Id = dr.GetInt32(0),
                                    Name = new Core.System.Data.Model.Product
                                    {
                                        Name = dr.GetString(1),
                                    }
                                },
                                Quantity = dr.GetInt32(2),
                                UnitPrice = dr.GetDouble(3),
                                TotalAmount = dr.GetDouble(4)
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
            return orders;
        }
    }
}