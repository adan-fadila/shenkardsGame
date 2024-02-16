using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card_package
{
     public class AbilityFactory :  IAbilityFactory
    {
        public IAbilityStrategy generate(string name)
        {
            if(name.Equals("simple")){
                return new SimpleAbilityStrategy();
            }
            return new SimpleAbilityStrategy();
        }
    }
}
   
