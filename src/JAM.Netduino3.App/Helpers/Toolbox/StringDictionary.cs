using System;
using System.Collections;

namespace JAM.Netduino3.Helpers.Toolbox
{
    public class StringDictionary : IEnumerable
    {
        #region Non-public members

        private readonly Hashtable _values;

        #endregion

        #region Public members

        #region IEnumerable members

        public IEnumerator GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        #endregion

        #region Nested types

        public class DuplicateKeyException : Exception
        {
        }

        #endregion

        public int Count
        {
            get { return _values.Count; }
        }

        public IEnumerable Keys
        {
            get { return _values.Keys; }
        }

        public IEnumerable Values
        {
            get { return _values.Values; }
        }

        public string this[string pKey]
        {
            get { return (string) _values[pKey]; }
            set { _values[pKey] = value; }
        }

        public void Add(string pKey, string pValue)
        {
            if (ContainsKey(pKey))
            {
                throw new DuplicateKeyException();
            }
            this[pKey] = pValue;
        }

        public bool ContainsKey(string pKey)
        {
            if (StringHelper.IsNullOrEmpty(pKey))
            {
                throw new ArgumentNullException("pKey");
            }
            return _values.Contains(pKey);
        }

        public void Clear()
        {
            _values.Clear();
        }

        #endregion

        #region Constructors

        public StringDictionary()
        {
            _values = new Hashtable();
        }

        #endregion
    }
}