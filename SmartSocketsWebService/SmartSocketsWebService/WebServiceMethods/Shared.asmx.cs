using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using SmartSockets.Table_Objects;

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
        SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.Database.ToString());

        private object getEntryByID(int ID, object ob)
        {
            string name = ob.GetType().Name;
            return getData("SELECT * FROM " + name + " WHERE " + name + "ID = '" + ID + "'", ob);
        }

        private object getData(string sql, object ob)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = sqlcon.CreateCommand();
            cmd.CommandText = sql;
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();

            sqlcon.Open();
            adapter.Fill(dataSet);
            sqlcon.Close();

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

        private bool doNonQuery(string sql)
        {
            bool success = false;

            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlcon);

            if (cmd.ExecuteNonQuery() != 0)
                success = true;

            sqlcon.Close();

            return success;

        }

        private bool doSqlInsert(object ob)
        {
            Type type = ob.GetType();

            string queryName = "INSERT INTO " + type.Name + "(";
            string queryValues = "values(";


            System.Reflection.FieldInfo[] fields = type.GetFields();
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

            return doNonQuery(queryName + queryValues);

        }

        
    }
}
