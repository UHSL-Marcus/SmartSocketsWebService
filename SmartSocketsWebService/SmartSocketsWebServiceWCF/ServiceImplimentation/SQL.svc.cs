using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartSocketsWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {

        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.Database.ToString());
        //SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseOnline.ToString());

        private List<TYPE> SQl_getEntryByID<TYPE>(int ID)
        {
            Type type = typeof(TYPE);
            return SQL_getData<TYPE>("SELECT * FROM " + type.Name + " WHERE " + type.Name + "ID = '" + ID + "'");
        }

        private List<TYPE> SQL_getAllEntries<TYPE>()
        {
            Type type = typeof(TYPE);
            return SQL_getData<TYPE>("SELECT * FROM " + type.Name);
        }

        private bool SQL_deleteEntryByID<TYPE>(int ID)
        {
            Type type = typeof(TYPE);
            return SQL_doNonQuery("DELETE FROM " + type.Name + " WHERE " + type.Name + "ID = '" + ID + "'");
        }

        private DataTableReader SQL_getDataReader(string sql)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseOnline.ToString())) { 
                
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                adapter.SelectCommand = cmd;
                

                conn.Open();
                adapter.Fill(dataSet);

            }

            return dataSet.CreateDataReader();
        }

        private List<TYPE> SQL_getData<TYPE>(string sql)
        {

            DataTableReader reader = SQL_getDataReader(sql);

            List<TYPE> returnList = new List<TYPE>();

            while (reader.Read())
            {

                TYPE ob = Activator.CreateInstance<TYPE>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string s = reader.GetName(i);
                    FieldInfo field = ob.GetType().GetField(reader.GetName(i));
                    if (field.FieldType == typeof(string[]))
                        field.SetValue(ob, ((string)reader[i]).Split(','));
                    else
                        field.SetValue(ob, reader[i]);
                }

                returnList.Add(ob);
            }

            return returnList;
        }

        private bool SQL_getSingleEntry(string sql, string columnName, out object output)
        {
            DataTableReader reader = SQL_getDataReader(sql);

            bool success = false;

            output = null;

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(columnName)) 
                    {
                        output = reader[i];
                        success = true;
                    }
                }
            }


            return success;

        }

        private bool SQL_doNonQuery(string sql)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseOnline.ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                if (cmd.ExecuteNonQuery() != 0)
                    success = true;

            }

            return success;

        }

        private bool SQL_doInsertReturnID<TYPE>(TYPE ob, out string ID)
        {
            Type type = typeof(TYPE);

            string declaration = "DECLARE @outputTable table(" + type.Name + "ID int NOT NULL)";

            string queryName = " INSERT INTO " + type.Name;
            string queryValues = "";


            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {

                if (!fields[i].Name.Equals(type.Name + "ID"))
                {
                    if (queryValues.Length < 1)
                    {
                        queryName += "(";
                        queryValues += " VALUES(";
                    }

                    queryName += fields[i].Name;

                    Type valueType = fields[i].GetType();
                    object value = fields[i].GetValue(ob);

                    queryValues += "'";

                    if (value is DateTime)
                        queryValues += ((DateTime)value).ToString("yyyy-MM-dd");
                    else if (value is int || value is string)
                        queryValues += value;
                    else if (value is string[])
                        queryValues += string.Join(",", value as string[]);

                    queryValues += "'";

                    if (i + 1 < fields.Length)
                    {
                        queryName += ",";
                        queryValues += ",";
                    }
                }
                
            }
            if (queryValues.Length > 0)
            {
                queryName += ")";
                queryValues += ")";
            }
            else queryValues += " DEFAULT VALUES";

            queryName += " OUTPUT INSERTED." + type.Name + "ID INTO @outputTable";

            string select = "; SELECT " + type.Name + "ID FROM @outputTable;";

            object reply;
            bool success = SQL_getSingleEntry(declaration + queryName + queryValues + select, type.Name + "ID", out reply);

            ID = reply.ToString();

            return success;

        }

        private bool SQL_doUpdate<TYPE>(TYPE ob)
        {
            Type type = typeof(TYPE);

            string queryName = " UPDATE " + type.Name;
            string queryValues = "";
            string queryWhere = " WHERE " + type.Name + "ID='";


            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                object value = fields[i].GetValue(ob);

                if (fields[i].Name.Equals(type.Name + "ID"))
                    queryWhere += value + "'";
                else if (value != null)
                {
                    if (queryValues.Length < 1)
                        queryValues += " SET ";
                    else queryValues += ",";

                    queryValues += fields[i].Name + "='";

                    if (value is DateTime)
                        queryValues += ((DateTime)value).ToString("yyyy-MM-dd");
                    else if (value is int || value is string)
                        queryValues += value;
                    else if (value is string[])
                        queryValues += string.Join(",", value as string[]);

                    queryValues += "'";

                }
            }

            return SQL_doNonQuery(queryName + queryValues + queryWhere);

        }

        /*public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/
    }
}
