using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuadroNegro", menuName = "ScriptableObjects/Quadro Negro")]
public class QuadroNegroSO : ScriptableObject
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public void SetValue(string key, object value)
    {
        if (data.ContainsKey(key))
        {
            data[key] = value;
        }
        else
        {
            data.Add(key, value);
        }
    }

    public T GetValue<T>(string key)
    {
        if (data.ContainsKey(key))
        {
            return (T)data[key];
        }
        return default;
    }

    public int GetIntValue(string key)
    {
        if (data.ContainsKey(key))
        {
            return (int)data[key];
        }
        return default;
    }
}
