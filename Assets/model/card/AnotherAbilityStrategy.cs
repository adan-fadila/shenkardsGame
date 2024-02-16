
/*************************************/
/*JUST FOR TESTING*/
using UnityEngine;

namespace Card_package
{
    public class AnotherAbilityStrategy : IAbilityStrategy
    {
        public AnotherAbilityStrategy()
        {
        }
        public void ActivateAbility()
        {
            Debug.Log("another ability");
        }


    }
}
