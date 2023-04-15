using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Todo
    {
        public int id;

        public int userId;

        public string title;

        public bool completed;

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}