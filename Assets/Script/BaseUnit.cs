using System;
using UnityEngine;

namespace Script
{
    public class BaseUnit: Unit
    {
        
        public void Innit(string race, string weapon, string effect)
        {
            name = race + " " + weapon + " " + effect;
        }
        
        //Special Capacity if it's an active 
        public override void Effect()
        {
            //Debug.Log("effet de l'espéce");
        }
        
    }
}