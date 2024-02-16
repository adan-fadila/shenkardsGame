using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card_package
{
   public interface IAbilityFactory {
   public IAbilityStrategy generate(string name); 
}

}
