using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class CoronaOperations
    {
        public List<Datum> GetTheRecords(string sqlQuery)
        {
            SqlConnection connDB;
            List<Datum> theReply = new List<Datum>();
            using (connDB = new SqlConnection("Data Source=localhost;Initial Catalog=Corona;Persist Security Info=True;Integrated Security=true;"))
            {
                try
                {
                    connDB.Open();
                    var sqlCmd = new SqlCommand(sqlQuery, connDB);
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int index = 0;
                        Datum newData = new Datum();
                        newData.countrycode = reader.GetString(index++);
                        newData.date = reader.GetDateTime(index++).ToString();
                        newData.cases = reader.GetInt32(index++).ToString();
                        newData.deaths = reader.GetInt32(index++).ToString();
                        newData.recovered = reader.GetInt32(index++).ToString();
                        theReply.Add(newData);
                    }
                    reader.Close();
                    connDB.Close();
                }
                catch (SqlException ex)
                {
                    return (theReply);
                }

            }
            return (theReply);
        }


        public bool insertRecord(Datum datum)
        {
            SqlConnection connDB;
            using (connDB = new SqlConnection("Data Source=localhost;Initial Catalog=Corona;Persist Security Info=True;Integrated Security=true;"))
            {
                try
                {
                    string countrycode = datum.countrycode;
                    string date = datum.date;
                    int cases = Convert.ToInt32(datum.cases);
                    string deaths = datum.deaths;
                    string recovered = datum.recovered;

                    connDB.Open();
                    string sqlString = "Insert into theStats values(@countrycode, @date, @cases, @deaths, @recovered)";

                    var sqlCmd = new SqlCommand(sqlString, connDB);

                    sqlCmd.Parameters.Add("countrycode", SqlDbType.VarChar).Value = countrycode;
                    sqlCmd.Parameters.Add("date", SqlDbType.SmallDateTime).Value = date;
                    sqlCmd.Parameters.Add("cases", SqlDbType.Int).Value = cases;
                    sqlCmd.Parameters.Add("deaths", SqlDbType.Int).Value = deaths;
                    sqlCmd.Parameters.Add("recovered", SqlDbType.Int).Value = recovered;
                    sqlCmd.ExecuteNonQuery();
                    connDB.Close();
                    return true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                    return false;
                   
                }
            }
        }
    }
}