using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;

namespace _Project.Code.Data.Dynamic.PlayerProgress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Coins = 0;
        public LevelInfo Level = LevelInfo.Default;
        
        public List<SerializableKeyValuePair<BoosterId, int>> MenuBoosterInventory = new();
        public List<SerializableKeyValuePair<BoosterId, int>> GameplayBoosterInventory = new();
    }
    
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    
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