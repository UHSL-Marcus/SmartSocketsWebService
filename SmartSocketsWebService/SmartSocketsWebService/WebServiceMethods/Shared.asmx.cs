using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Services;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using SmartSockets.DocumentDB_Objects;

namespace SmartSockets
{
    /// <summary>
    /// Summary description for SmartSocketsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public partial class SmartSocketsService : System.Web.Services.WebService
    {
        SqlConnection sqlConnStateful = new SqlConnection(Properties.Settings.Default.DatabaseStateful.ToString());

        protected static IMongoClient _client = new MongoClient();
        protected static IMongoDatabase _database = _client.GetDatabase("SmartSocketData");



        

       

        private object SQl_getEntryByID(int ID, object ob)
        {
            string name = ob.GetType().Name;
            return SQL_getData("SELECT * FROM " + name + " WHERE " + name + "ID = '" + ID + "'", ob);
        }

        private object SQL_getData(string sql, object ob)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = sqlConnStateful.CreateCommand();
            cmd.CommandText = sql;
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();

            sqlConnStateful.Open();
            adapter.Fill(dataSet);
            sqlConnStateful.Close();

            DataTableReader reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    FieldInfo field = ob.GetType().GetField(reader.GetName(i));
                    field.SetValue(ob, reader[i]);
                }
            }

            return ob;

            
        }

        private bool SQL_doNonQuery(string sql)
        {
            bool success = false;

            sqlConnStateful.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConnStateful);

            if (cmd.ExecuteNonQuery() != 0)
                success = true;

            sqlConnStateful.Close();

            return success;

        }

        private bool SQL_doInsert(object ob)
        {
            Type type = ob.GetType();

            string queryName = "INSERT INTO " + type.Name + "(";
            string queryValues = "values(";


            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if (!fields[i].Name.Equals(type.Name + "ID"))
                {

                    queryName += fields[i].Name;

                    Type valueType = fields[i].GetType();
                    object value = fields[i].GetValue(ob);

                    queryValues += "'";

                    if (value is DateTime)
                        queryValues += ((DateTime)value).ToString("yyyy-MM-dd");
                    else if (value is int || value is string)
                        queryValues += value;

                    queryValues += "'";

                    if (i + 1 < fields.Length)
                    {
                        queryName += ",";
                        queryValues += ",";
                    }
                }
                
            }
            queryName += ")";
            queryValues += ")";

            return SQL_doNonQuery(queryName + queryValues);

        }

        
    }
}
