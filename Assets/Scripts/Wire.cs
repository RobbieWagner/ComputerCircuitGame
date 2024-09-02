using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircuitryGame
{
    public class Wire : MonoBehaviour
    {
        public bool on;
        public WireInputOutput input;
        public WireInputOutput output;
        [SerializeField] private Animator animator;

        public void UpdateWire()
        {
            //Stretch from input to output, update animator base don value of "on"
        }
    }
}