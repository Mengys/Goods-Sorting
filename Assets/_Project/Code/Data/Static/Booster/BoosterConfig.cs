using System;

namespace _Project.Code.Data.Static.Booster
{
    [Serializable]
    public struct BoosterConfig
    {
        public BoosterType Type;
        public BoosterAssetConfig Asset;
        
        public BoosterId Id => new(Type.ToString());
    }
}
