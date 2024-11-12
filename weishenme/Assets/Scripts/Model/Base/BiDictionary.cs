using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BiDictionary<TKey, TValue, TObject>
{
    private readonly Dictionary<TKey, TObject> keyToObject = new Dictionary<TKey, TObject>();
    private readonly Dictionary<TValue, TObject> valueToObject = new Dictionary<TValue, TObject>();

    // �������
    public void Add(TKey key, TValue value, TObject obj)
    {
        if (keyToObject.ContainsKey(key) || valueToObject.ContainsKey(value))
        {
            throw new ArgumentException("Key or Value already exists.");
        }

        keyToObject[key] = obj;
        valueToObject[value] = obj;
    }

    // ����Key��ȡ����
    public TObject GetByKey(TKey key)
    {
        return keyToObject.TryGetValue(key, out TObject obj) ? obj : default;
    }

    // ����Value��ȡ����
    public TObject GetByValue(TValue value)
    {
        return valueToObject.TryGetValue(value, out TObject obj) ? obj : default;
    }

    // �Ƴ�����
    public bool RemoveByKey(TKey key)
    {
        if (keyToObject.TryGetValue(key, out TObject obj))
        {
            keyToObject.Remove(key);

            // ���Ҷ�Ӧ��ֵ���Ƴ�
            foreach (var pair in valueToObject)
            {
                if (pair.Value.Equals(obj))
                {
                    valueToObject.Remove(pair.Key);
                    break;
                }
            }

            return true;
        }

        return false;
    }

    public bool RemoveByValue(TValue value)
    {
        if (valueToObject.TryGetValue(value, out TObject obj))
        {
            valueToObject.Remove(value);

            // ���Ҷ�Ӧ�ļ����Ƴ�
            foreach (var pair in keyToObject)
            {
                if (pair.Value.Equals(obj))
                {
                    keyToObject.Remove(pair.Key);
                    break;
                }
            }

            return true;
        }

        return false;
    }
}
