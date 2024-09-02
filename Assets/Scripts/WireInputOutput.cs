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

        protected override void Awake()
        {
            base.Awake();
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
            if (ConnectionManager.Instance.currentWire == null)
            {
                if (isInput && ConnectionManager.Instance.input == null)
                {
                    ConnectionManager.Instance.input = this;
                    ConnectionManager.Instance.currentWire = Instantiate(wirePrefab, ConnectionManager.Instance.canvas.transform);
                    ConnectionManager.Instance.currentWire.transform.position = transform.position;
                    ConnectionManager.Instance.currentWire.wireOutput = this;
                }
                else if (!isInput && ConnectionManager.Instance.output == null)
                {
                    ConnectionManager.Instance.output = this;
                    ConnectionManager.Instance.currentWire = Instantiate(wirePrefab, ConnectionManager.Instance.canvas.transform);
                    ConnectionManager.Instance.currentWire.transform.position = transform.position;
                    ConnectionManager.Instance.currentWire.wireInput = this;
                }
            }
            else if (isInput && ConnectionManager.Instance.input != null && ConnectionManager.Instance.output == null && ConnectionManager.Instance.input.parentNode != parentNode)
            {
                ConnectionManager.Instance.output = this;
                ConnectionManager.Instance.SetupWire();
            }
            else if (isInput && ConnectionManager.Instance.output != null && ConnectionManager.Instance.input == null && ConnectionManager.Instance.output.parentNode != parentNode)
            {
                ConnectionManager.Instance.input = this;
                ConnectionManager.Instance.SetupWire();
            }
        }
    } 
}
