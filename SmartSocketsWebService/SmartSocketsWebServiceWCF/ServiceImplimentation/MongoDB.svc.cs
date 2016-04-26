using MongoDB.Bson;
using MongoDB.Driver;
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

        protected static IMongoClient _client = new MongoClient();
        protected static IMongoDatabase _database = _client.GetDatabase("SmartSocketData");

        private bool MongoDB_InsertDocument<TYPE>(TYPE ob, string collectionName, string id = null)
        {
            try {

                var collection = _database.GetCollection<BsonDocument>(collectionName);
                var document = MongoDB_BuildDocument(ob, id);
                collection.InsertOne(document);
                return true;

            } catch (Exception e)
            {
                return false;
            }

        }

        private List<BsonDocument> MongoDB_GetDocuments(string collectionName, FilterDefinition<BsonDocument> filterOptions)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filterOptions).ToList();
   
        }

        private List<TYPE> MongoDB_GetDocumentFromID<TYPE>(string collection, string ID)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", MongoDB_convertToBsonValue(ID));

            List<TYPE> objects = new List<TYPE>();

            foreach (BsonDocument doc in MongoDB_GetDocuments(collection, filter))
                objects.Add(MongoDB_BsonDocumentToObject<TYPE>(doc));
            
            return objects;
        }

        private List<TYPE> MongoDB_GetDocuments<TYPE>(string collection)
        {
           
            List<TYPE> objects = new List<TYPE>();

            foreach (BsonDocument doc in MongoDB_GetDocuments(collection, Builders<BsonDocument>.Filter.Empty))
                objects.Add(MongoDB_BsonDocumentToObject<TYPE>(doc));

            return objects;
        }


        private BsonDocument MongoDB_BuildDocument<TYPE>(TYPE ob, string id = null)
        {
            Type type = typeof(TYPE);
            var document = new BsonDocument();
            if (id != null)
                document.Add("_id", new BsonString(id));

            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {

                string nm = fields[i].Name;
                Type valueType = fields[i].GetType();
                object value = fields[i].GetValue(ob);

                BsonValue bVal = MongoDB_convertToBsonValue(value);

                document.Add(fields[i].Name, bVal, bVal != null);


            }

            return document;

        }

        private BsonValue MongoDB_convertToBsonValue(object value)
        {
            BsonValue bVal = null;

            if (value is DateTime)
                bVal = new BsonDateTime((DateTime)value);
            else if (value is int)
                bVal = new BsonInt32((int)value);
            else if (value is string)
                bVal = new BsonString((string)value);
            else if (value is object[])
            {
                bVal = new BsonArray();
                foreach (object obj in value as object[])
                {
                    if (obj.GetType().GetFields().Length > 0)
                        ((BsonArray)bVal).Add(MongoDB_BuildDocument(obj));
                    else
                        ((BsonArray)bVal).Add(MongoDB_convertToBsonValue(obj));
                }
            }
            else if (value.GetType().GetFields().Length > 0)
            {
                bVal = MongoDB_BuildDocument(value);
            }

            return bVal;
        }

        private object MongoDB_ConvertFromBsonValue<TYPE>(BsonValue bsonVal)
        {
           Type type = typeof(TYPE);

            if (bsonVal.IsBsonDateTime)
                return bsonVal.ToUniversalTime();
            else if (bsonVal.IsInt32)
                return bsonVal.AsInt32;
            else if (bsonVal.IsString)
                return bsonVal.AsString;
            else if (bsonVal.IsBsonArray)
            {
                List<object> list = new List<object>();
                foreach(BsonValue bval in ((BsonArray)bsonVal).ToArray())
                {
                    type.GetElementType().MakeGenericType()
                    


                    list.Add(MongoDB_ConvertFromBsonValue(bval));
                }

                return list.ToArray();

            }
            else if (bsonVal.IsBsonDocument)
            {
                return null;//MongoDB_BsonDocumentToObject(bsonVal.AsBsonDocument);
            }
            return null;
        }

        private TYPE MongoDB_BsonDocumentToObject<TYPE>(BsonDocument doc)
        {
            TYPE ob = Activator.CreateInstance<TYPE>();
            Type type = typeof(TYPE);

            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                string nm = fields[i].Name;
                object obVal = fields[i].GetValue(ob);
                BsonValue val;
                if (doc.TryGetValue(nm, out val))
                {
                    object convertedVal = MongoDB_ConvertFromBsonValue<>(val);
                    if (obVal.GetType().Equals(convertedVal.GetType()))
                        fields[i].SetValue(ob, convertedVal);
                }

             
            }

                

            return ob;
        }
    }
}
