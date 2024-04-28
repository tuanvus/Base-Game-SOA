using LTA.Error;
using SimpleJSON;
using System;

namespace LTA.Data
{

    public class DataExpection : ErrorException
    {
        protected const string ERROR_DATA_MESSAGE = "Error at {0}";
        protected const string DEFAULT_DATA_MESSAGE = "Can not read data {0}";


        protected DataExpection()
        {

        }
        protected string dataName;

        string ErrorMessage => String.Format(ERROR_DATA_MESSAGE, dataName);

        string DefaultMessage => String.Format(DEFAULT_DATA_MESSAGE, dataName);

        public DataExpection(IData data, string message = null)
        {
            dataName = DataHelper.GetDataName(data);
            InitDataMessage(message);
        }

        protected void InitDataMessage(string message = null)
        {
            SetMessage(Init(message));
        }

        protected string Init(string message)
        {
            return String.Format(ErrorMessage + " : {0}", message ?? DefaultMessage);
        }


        protected override ProblemData DefaultProblemData
        {
            get
            {
                return new ProblemData(Severity.Critical, ScopeType.Data);
            }
        }
    }

    public class ArrayDataExpection : DataExpection
    {
        public ArrayDataExpection(IData data, int index, string message = null)
        {
            dataName = DataHelper.GetDataName(data,index);
            InitDataMessage(message);
        }
    }

    public class MapDataExpection<K> : DataExpection
    {
        public MapDataExpection(IData data, K key, string message = null)
        {
            dataName = DataHelper.GetDataName<K>(data,key);
            InitDataMessage(message);
        }
    }

    public class JSONObjectMissingKeyException : WarningException
    {
        protected const string ERROR_MISSING_KEY_MESSAGE = "JSONObject is missing key";

        protected virtual string GetMessage(string key = null)
        {
            string Message = ERROR_MISSING_KEY_MESSAGE;
            if (key != null)
                Message = String.Format(Message + " = {0}", key);
            return Message;
        }

        public JSONObjectMissingKeyException()
        {
            SetMessage(GetMessage());
        }

        public JSONObjectMissingKeyException(string key,JSONNode node = null)
        {
            if (node != null)
                SetMessage(GetMessage(key) + "\n data is " + node.ToString());
            else
                SetMessage(GetMessage(key));
        }
    }
    public class JSONObjectMissingKeyException<T> : JSONObjectMissingKeyException
    {
        protected override string GetMessage(string key = null)
        {
            return base.GetMessage(key)+ " " + String.Format("To Convert Object Type {0}",typeof(T).Name);
        }

        public JSONObjectMissingKeyException() : base()
        {

        }

        public JSONObjectMissingKeyException(string key,JSONNode node = null) : base(key,node)
        {

        }
    }

    public class JSONDataIsNotObject : Exception
    {
        protected const string ERROR_JSON_DATA_IS_NOT_OBJECT = "JSONData is not Object";

        public JSONDataIsNotObject() : base(ERROR_JSON_DATA_IS_NOT_OBJECT)
        {

        }

        public JSONDataIsNotObject(JSONNode node) : base(ERROR_JSON_DATA_IS_NOT_OBJECT + "\n data is " + node.ToString())
        {

        }
    }

    public class JSONDataIsNotArray : Exception
    {
        protected const string ERROR_JSON_DATA_IS_NOT_ARRAY = "JSONData is not Array";

        public JSONDataIsNotArray() : base(ERROR_JSON_DATA_IS_NOT_ARRAY)
        {

        }

        public JSONDataIsNotArray(JSONNode node) : base(ERROR_JSON_DATA_IS_NOT_ARRAY + "\n data is " + node.ToString())
        {

        }
    }
}
