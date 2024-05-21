using System;
using System.Data;
using System.Collections.Generic;

using MySqlConnector;
using Core.System.Data.Model;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace Core
{
    internal class UpgradeManager
    {
        internal MySql.Data.MySqlClient.MySqlConnection connection;
        private string connectionString;


        /// <summary>
        /// This constructor initializes the UpgradeManager object with default connection parameters.
        /// </summary>
        /// <returns>None</returns>
        public UpgradeManager()
        {
            string server = "localhost";
            string userid = "root";
            string password = "";
            string database = "dbjanmos";
            this.connectionString = String.Format("server={0};database={1};userid={2};password={3};",
                server, database, userid, password);
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
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
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
        /// <returns>True if the query execution is successful, otherwise false.</returns>
        /// <exception cref="Exception">It catches any exceptions that occur during the execution of the query and rethrows them with a new exception containing the error message.</exception>
        public bool ExecuteQuery(string query, Dictionary<string, string> parameters)
        {
            try
            {
                this.Connect();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, this.connection))
                {
                    foreach (KeyValuePair<string, string> kvp in parameters)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
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
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, this.connection))
                {
                    if (param != null)
                    {
                        foreach (KeyValuePair<string, string> kvp in param)
                        {
                            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                        }
                    }
                    using (MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        return dt;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
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
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.connection))
                {
                    using (MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader())
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
            catch (MySql.Data.MySqlClient.MySqlException ex)
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

        public int GetTotalCount(string query)
        {
            int totalRecords = 0;
            try
            {
                this.Connect();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, this.connection))
                {
                    totalRecords = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return totalRecords;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception($"Error executing query: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }

        public void SaveBackup(int buttonValue, string strPath) //for saving
        {
            try
            {
                this.Connect();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(null, this.connection))
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        if (buttonValue == 0)
                        {
                            mb.ExportToFile(strPath);
                            MessageBox.Show("Backup Successfully.", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if (buttonValue == 1)
                        {
                            mb.ImportFromFile(strPath);
                            MessageBox.Show("Restore Successfully.", "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show($"Error executing: {ex.Message}", "Save Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Save Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { this.connection.Close(); }
        }

        public string OpenBackup(int value) //for reading
        {
            string file = "";
            try
            {
                this.Connect();
                using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(null, this.connection))
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        if (value == 1)
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.Filter = "SQL Source File | *.sql";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                file = sfd.FileName;
                                return file;
                            }
                        }
                        if (value == 2)
                        {
                            OpenFileDialog ofd = new OpenFileDialog();
                            ofd.Filter = "SQL Source File | *.sql";
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                file = ofd.FileName;
                                return file;
                            }
                        }
                        return file;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception($"Error executing: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unexpected error: {e.Message}");
            }
            finally { this.connection.Close(); }
        }
    }
}