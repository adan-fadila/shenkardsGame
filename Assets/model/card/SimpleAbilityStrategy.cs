
/*************************************/
/*JUST FOR TESTING*/
using UnityEngine;

namespace Card_package
{
    public class SimpleAbilityStrategy : IAbilityStrategy
    {
        public SimpleAbilityStrategy()
        {
        }
        public void ActivateAbility()
        {
            Debug.Log("simple ability");
        }


    }
}
