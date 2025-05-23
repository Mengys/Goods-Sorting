using System.Collections.Generic;

namespace _Project.Code.Data.Dynamic.PlayerProgress.SerializableKeyValue
{
    public static class KeyValuePairExtensions
    {
        public static SerializableKeyValuePair<TKey, TValue> ToSerializable<TKey, TValue>(this KeyValuePair<TKey, TValue> pair) =>
            new(pair.Key, pair.Value);
        
        public static KeyValuePair<TKey, TValue> ToDefault<TKey, TValue>(this SerializableKeyValuePair<TKey, TValue> pair) =>
            new(pair.Key, pair.Value);
        
        public static List<KeyValuePair<TKey, TValue>> ToDefault<TKey, TValue>(this List<SerializableKeyValuePair<TKey, TValue>> list) =>
            list.ConvertAll(pair => pair.ToDefault());
        
        public static List<SerializableKeyValuePair<TKey, TValue>> ToSerializable<TKey, TValue>(this List<KeyValuePair<TKey, TValue>> list) =>
            list.ConvertAll(pair => pair.ToSerializable());
    }
}