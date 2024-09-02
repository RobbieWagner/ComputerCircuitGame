using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CircuitryGame
{
    public class Node : MonoBehaviour
    {
        [HideInInspector] public List<Node> inputNodes;
        [HideInInspector] public int inputNodeCount;
        [HideInInspector] public List<Node> outputNodes;
        [HideInInspector] public int outputNodeCount;


    }
}