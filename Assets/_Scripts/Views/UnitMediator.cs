using _Scripts.Models;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Scripts.Views
{
    public class UnitMediator : Mediator
    {
        [Inject] public UnitView View { get; set; }
        [Inject] public UnitModel _model { get; set; }

        public override void OnRegister()
        {
            Debug.Log("Mediator active");
        }

        public void Setup(int id)
        {
            
        }
    }
}