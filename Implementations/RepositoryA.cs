using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Implementations
{
    public class RepositoryA : IRepository
    {
        private Dictionary<string, string> dataStore = new Dictionary<string, string>();

        public RepositoryA()
        {
            dataStore.Add("repoName", "RepositoryA");
        }

        public void Delete(string Key)
        {
            if (dataStore.ContainsKey(Key))
            {
                dataStore.Remove(Key);
            }
        }

        public string Get(string Key)
        {
            if (dataStore.ContainsKey(Key))
            {
                return dataStore[Key];
            }
            else
            {
                return null;
            }
        }

        public void AddOrUpdate(string key, string value)
        {
            if (dataStore.ContainsKey(key))
            {
                dataStore[key] = value;
            }
            else
            {
                dataStore.Add(key, value);
            }
        }
    }
}
