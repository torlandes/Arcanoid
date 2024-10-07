using UnityEngine;

namespace Arcanoid.Game.PickUps
{
    public class DummyPickUp : PickUp
    {
        protected override void PerformActions()
        {
            base.PerformActions();

            Debug.Log($"DummyPickUp PerformActions");
        }
        
    }
}