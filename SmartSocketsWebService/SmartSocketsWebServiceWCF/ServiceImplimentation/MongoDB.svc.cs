using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;


namespace SmartSocketsWebService
{

    
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartSocketsWebService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartSocketsWebService.svc or SmartSocketsWebService.svc.cs at the Solution Explorer and start debugging.
    public partial class SmartSocketsWebService : ISmartSocketsWebService
    {

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        private void MongoDB_Start()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Data))) 
                BsonClassMap.RegisterClassMap<Data>();
            if (!BsonClassMap.IsClassMapRegistered(typeof(DataEntry)))
                BsonClassMap.RegisterClassMap<DataEntry>();

            _client = new MongoClient();
            _database = _client.GetDatabase("SmartSocketData");
        }


        private bool MongoDB_InsertDocument<TYPE>(TYPE ob, string collectionName)
        {
            try {

                var collection = _database.GetCollection<TYPE>(collectionName);
                collection.DeleteMany(Builders<TYPE>.Filter.Empty);
                collection.InsertOne(ob);
                return true;

            } catch (Exception e)
            {
                return false;
            }

        }

        private List<TYPE> MongoDB_GetDocuments<TYPE>(string collectionName, FilterDefinition<TYPE> filterOptions)
        {
            var collection = _database.GetCollection<TYPE>(collectionName);
            return collection.Find(filterOptions).ToList();
   
        }
        

        private List<TYPE> MongoDB_GetDocumentFromID<TYPE>(string collection, object ID)
        {

            var filter = Builders<TYPE>.Filter.Eq("_id", BsonValue.Create(ID));

            return MongoDB_GetDocuments(collection, filter);
        }

        private List<TYPE> MongoDB_GetAllDocuments<TYPE>(string collection)
        {
            return MongoDB_GetDocuments(collection, Builders<TYPE>.Filter.Empty);
        }



        /*private void MongoDB_GetDocuments(string collectionName)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            DateTime dt = new DateTime(2016, 4, 27, 11, 14, 0, DateTimeKind.Utc);
            string s1 = string.Format("{0:dd/MM/yyyy-HH:mm:ss}", dt);
            BsonValue bv = BsonValue.Create(dt);
            List<BsonDocument> list = collection.Find(Builders<BsonDocument>.Filter.Eq("_id", BsonValue.Create(dt))).ToList();

            string s = "";

            foreach (BsonDocument b in list)
                s += "\n" + b.ToJson();

            s += "\n";

            list = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

            s = "";

            foreach (BsonDocument b in list)
                s += "\n" + b.ToJson();

            s += "\n";

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

        private object MongoDB_ConvertFromBsonValue(BsonValue bsonVal, Type type)
        {
           
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
                    list.Add(MongoDB_ConvertFromBsonValue(bval, type.GetElementType()));
                

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
                    object convertedVal = MongoDB_ConvertFromBsonValue(val, obVal.GetType());
                    if (obVal.GetType().Equals(convertedVal.GetType()))
                        fields[i].SetValue(ob, convertedVal);
                }

             
            }

                

            return ob;
        }*/
    }
}
