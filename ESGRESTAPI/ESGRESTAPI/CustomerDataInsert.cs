using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ESGRESTAPI
{
    public class CustomerDataInsert
    {
        private MySqlConnection con;
        private MySqlCommand com;

        private void connection()
        {

       //establishing a mysql connection
  
            string connectionString = "SERVER=127.0.0.1;" +"DATABASE=esgdbo;" + "UID=root;" + "PASSWORD=admin;";

            con = new MySqlConnection(connectionString);
            con.Open();

        }

        //inserting records in db 
        public Boolean insertRecord(CustomerDetails customerDetails)
        {
            try
            {
                connection();

                String query = "INSERT INTO esgdbo.custrec (customerref,customername,addressline1,addressline2,town,county,country,postcode) VALUES (@customerRef,@customerName,@addressLine1, @addressLine2,@town,@county,@country,@postcode)";

                com = new MySqlCommand(query, con);
                com.Parameters.AddWithValue("@customerRef", customerDetails.CustomerRef);
                com.Parameters.AddWithValue("@customerName", customerDetails.CustomerName.Trim());
                com.Parameters.AddWithValue("@addressLine1", customerDetails.AddressLine1.Trim());
                com.Parameters.AddWithValue("@addressLine2", customerDetails.AddressLine2.Trim());
                com.Parameters.AddWithValue("@town", customerDetails.Town.Trim());
                com.Parameters.AddWithValue("@county", customerDetails.County.Trim());
                com.Parameters.AddWithValue("@country", customerDetails.Country.Trim());
                com.Parameters.AddWithValue("@postcode", customerDetails.Postcode.Trim());

                int result = com.ExecuteNonQuery();
                // Check Error
                if (result < 0)
                {
                    Console.WriteLine("Error inserting data into Database for customer ref " + customerDetails.CustomerRef);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
             
        }


        //retriving the records from the db
        public CustomerDetails fetchRecord(int id)
        {
            try
            {
                CustomerDetails customerDetails = null;
                connection();

                String query = "SELECT * FROM esgdbo.custrec where customerref=@customerRef";

                com = new MySqlCommand(query, con);
                com.Parameters.AddWithValue("@customerRef", id);
                MySqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customerDetails = new CustomerDetails();
                        // iterate your results here
                        customerDetails.CustomerRef = int.Parse(String.Format("{0}", reader["customerref"]));
                        customerDetails.CustomerName = String.Format("{0}", reader["customername"]);
                        customerDetails.AddressLine1 = String.Format("{0}", reader["addressline1"]);
                        customerDetails.AddressLine2 = String.Format("{0}", reader["addressline2"]);
                        customerDetails.Town = String.Format("{0}", reader["town"]);
                        customerDetails.County = String.Format("{0}", reader["county"]);
                        customerDetails.Country = String.Format("{0}", reader["country"]);
                        customerDetails.Postcode = String.Format("{0}", reader["postcode"]);

                    }

                }
                else
                {
                    Console.WriteLine("No record found for " + id);
                    return null;
                }
              

                 return customerDetails;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                con.Close();
            }



        }
    }
}
