using System;
using _Project.Code.Data.Static.Booster;

namespace _Project.Code.Gameplay.Boosters.Configs
{
    [Serializable]
    public struct BoosterConfig
    {
        public BoosterType Type;
        public BoosterAssetConfig Asset;
        
        public BoosterId Id => new(Type.ToString());
    }
}
