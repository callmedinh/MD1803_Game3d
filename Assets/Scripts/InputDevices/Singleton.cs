using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.InputManager 
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                _instance = (T)FindObjectOfType<T>();
                if (_instance == null )
                {
                    GameObject gameObject = new GameObject();
                    _instance = gameObject.AddComponent<T>();
                    gameObject.name = typeof(T).ToString();

                }
                return _instance;
            }
        }
    }
}
