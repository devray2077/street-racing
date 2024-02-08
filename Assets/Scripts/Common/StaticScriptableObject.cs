
using UnityEngine;

namespace StreetRacing
{
    public abstract class StaticScriptableObject<T> : ScriptableObject where T : Object
    {
        
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var path = "Parameters/" + typeof(T).Name;
                    instance = Resources.Load<T>(path);
                }
                return instance;
            }
        }
    }
}
