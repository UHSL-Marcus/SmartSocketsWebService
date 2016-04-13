using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SmartSockets.DocumentDB_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Services;

namespace SmartSockets
{

    class templogic
    {
        public string color;
        public string size;
        public string height;

        public templogic (string color, string size, string height)
        {
            this.size = size;
            this.color = color;
            this.height = height;
        }
    }

    public partial class SmartSocketsService : System.Web.Services.WebService
    {

        
        [WebMethod]
        public string logicaltest()
        {
            string s = "";

            List<IConditionTuple> filterOptions = new List<IConditionTuple>();
            filterOptions.Add(new FourTuple("color", "green", ConditionTypes.EQUALS, LogicalOperators.NULL));
            filterOptions.Add(new FourTuple("size", "big", ConditionTypes.EQUALS, LogicalOperators.AND));

            List<IConditionTuple> filterOptions2 = new List<IConditionTuple>
            {
                new FourTuple("height", "tall", ConditionTypes.EQUALS, LogicalOperators.NULL),
                new FourTuple("height", "short", ConditionTypes.EQUALS, LogicalOperators.OR)
            };

            filterOptions.Add(new TwoTuple(filterOptions2.ToArray(), LogicalOperators.AND));

            var filter = Builders<BsonDocument>.Filter.Eq("color", MongoDB_convertToBsonValue("green"));
            filter = filter & Builders<BsonDocument>.Filter.Eq("size", MongoDB_convertToBsonValue("big"));
            filter = filter & ( Builders<BsonDocument>.Filter.Eq("height", MongoDB_convertToBsonValue("tall")) | Builders<BsonDocument>.Filter.Eq("height", MongoDB_convertToBsonValue("short")) );

            var collection = _database.GetCollection<BsonDocument>("templogic");
            var filter2 = MongoDB_BuildFilter(filterOptions.ToArray());
            List<BsonDocument> col = collection.Find(MongoDB_BuildFilter(filterOptions.ToArray())).ToList();
            List<BsonDocument> col2 = collection.Find(filter).ToList();


            foreach (BsonDocument b in col)
                s += "\n" + b.ToJson();

            s += " ********** ";

            foreach (BsonDocument b in col2)
                s += "\n" + b.ToJson();


            return s;
        }


        private void MongoDB_InsertDocument(object ob, string collectionName, string id = null)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var document = MongoDB_BuildDocument(ob, id);
            collection.InsertOne(document);

        }

        private List<BsonDocument> MongoDB_GetDocument(string collectionName, FilterDefinition<BsonDocument> filterOptions)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filterOptions).ToList();
        }

        private List<BsonDocument> MongoDB_GetDocumentFromSingleEquality(string collection, KeyValuePair<string, object> filterOption)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(filterOption.Key, MongoDB_convertToBsonValue(filterOption.Value));
            return MongoDB_GetDocument(collection, filter);
        }

        private FilterDefinition<BsonDocument> MongoDB_BuildFilter(IConditionTuple[] conditions)
        {
            FilterDefinition<BsonDocument> filter = null;
            LogicalOperators logic = LogicalOperators.NULL;

            for (int i = 0; i < conditions.Length; i++)
            {
                FilterDefinition<BsonDocument> tempFilter = null;
                if (conditions[i] is FourTuple )
                {
                    var tuple = (FourTuple)conditions[i];
                    tempFilter = tuple.Item3(tuple.Item1, MongoDB_convertToBsonValue(tuple.Item2));
                    logic = tuple.Item4;
                }
                if (conditions[i] is TwoTuple)
                {
                    var tuple = (TwoTuple)conditions[i];
                    tempFilter = MongoDB_BuildFilter(tuple.Item1);
                    logic = tuple.Item2;
                }

                if (i > 0)
                {
                    switch (logic)
                    {
                        case LogicalOperators.AND:
                            filter = filter & (tempFilter);
                            break;
                        case LogicalOperators.OR:
                            filter = filter | (tempFilter);
                            break;
                        default: break;
                    }
                }
                else if (i == 0)
                    filter = (tempFilter);
            }

            return filter;

        }

        private BsonDocument MongoDB_BuildDocument(object ob, string id = null)
        {
            Type type = ob.GetType();
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
    }

    public interface IConditionTuple { };

    public class FourTuple : Tuple<string, object, Func<string, BsonValue, FilterDefinition<BsonDocument>>, LogicalOperators>, IConditionTuple 
    {
        public FourTuple(string t1, object t2, Func<string, BsonValue, FilterDefinition<BsonDocument>> t3, LogicalOperators t4) : base(t1, t2, t3, t4) { } 
    }

    public class TwoTuple : Tuple<IConditionTuple[], LogicalOperators>, IConditionTuple
    {
        public TwoTuple(IConditionTuple[] t1, LogicalOperators t2) : base(t1, t2) { }
    }

    public class ConditionTypes
    {
        public static Func<string, BsonValue, FilterDefinition<BsonDocument>> EQUALS = delegate (string field, BsonValue value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        };
    }

    public enum LogicalOperators { AND, OR, NULL }
}
