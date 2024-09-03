using RobbieWagnerGames.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircuitryGame
{
    public class WireInputOutput : CustomUIElement
    {
        public bool isInput;
        public Node parentNode;
        [SerializeField] private Wire wirePrefab;
        List<Wire> connectedWires = new List<Wire>();

        private bool isClicking = false;

        protected override void Awake()
        {
            base.Awake();
        }

        private void AddWireConnection(Wire wire)
        {
            connectedWires.Add(wire);
            wire.OnDestroyWire += RemoveWireConnection;
        }

        private void RemoveWireConnection(Wire wire)
        {
            connectedWires.Remove(wire);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            ConnectionManager.Instance.hoveredInputOutputs.Add(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            ConnectionManager.Instance.hoveredInputOutputs.Remove(this);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if(!isClicking)
            {
                if (ConnectionManager.Instance.currentWire == null)
                {
                    if (isInput && ConnectionManager.Instance.input == null && connectedWires.Count == 0)
                    {
                        ConnectionManager.Instance.input = this;
                        Wire newWire = Instantiate(wirePrefab, ConnectionManager.Instance.canvas.transform);
                        ConnectionManager.Instance.currentWire = newWire;
                        connectedWires.Add(newWire);    
                        ConnectionManager.Instance.currentWire.transform.position = transform.position;
                        ConnectionManager.Instance.currentWire.wireOutput = this;

                    }
                    else if (!isInput && ConnectionManager.Instance.output == null)
                    {
                        ConnectionManager.Instance.output = this;
                        Wire newWire = Instantiate(wirePrefab, ConnectionManager.Instance.canvas.transform);
                        ConnectionManager.Instance.currentWire = newWire;
                        connectedWires.Add(newWire);
                        ConnectionManager.Instance.currentWire.transform.position = transform.position;
                        ConnectionManager.Instance.currentWire.wireInput = this;
                    }
                }
                else if (!isInput && ConnectionManager.Instance.input != null && ConnectionManager.Instance.output == null && ConnectionManager.Instance.input.parentNode != parentNode)
                {
                    Debug.Log("connect to output");
                    ConnectionManager.Instance.output = this;
                    connectedWires.Add(ConnectionManager.Instance.currentWire);
                    ConnectionManager.Instance.SetupWire();
                }
                else if (isInput && ConnectionManager.Instance.output != null && ConnectionManager.Instance.input == null && ConnectionManager.Instance.output.parentNode != parentNode && connectedWires.Count == 0)
                {
                    Debug.Log("connect to input");
                    ConnectionManager.Instance.input = this;
                    connectedWires.Add(ConnectionManager.Instance.currentWire);
                    ConnectionManager.Instance.SetupWire();
                }
            }

            isClicking = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isClicking = false;
        }
    } 
}
