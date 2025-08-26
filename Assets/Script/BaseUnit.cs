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
        public override void Effect()
        {
            Debug.Log("effet de l'espéce");
            weapon.Fire();
        }
    }
}