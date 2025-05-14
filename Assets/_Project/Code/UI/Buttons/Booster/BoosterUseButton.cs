using System;
using _Project.Code.Data.Static.Booster;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Buttons.Booster
{
    public class BoosterUseButton : ButtonClickHandler 
    {
        [SerializeField] private BoosterType Id;
        
        public BoosterId BoosterId => new(Id.ToString());
        
        private IBoosterUser _user;

        [Inject]
        public void Construct(IBoosterUser user)
        {
            _user = user;
        }
        
        protected override void OnClicked() => 
            _user.Use(new BoosterId(Id.ToString()));
    }
}