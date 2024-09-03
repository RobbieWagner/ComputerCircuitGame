using RobbieWagnerGames.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace CircuitryGame
{
    public class Wire : CustomUIElement
    {
        public LineRenderer lineRenderer;
        [SerializeField] private float wireHeight = 6;
        public bool on;
        public WireInputOutput wireOutput;
        public WireInputOutput wireInput;
        [SerializeField] private Animator animator;

        protected override void Awake()
        {
            base.Awake();

            lineRenderer.widthMultiplier = wireHeight / 100;
        }

        private void Update()
        {
            UpdateWire();
        }

        

        public void UpdateWire()
        {
            if (wireOutput != null && wireInput != null)
            {
                lineRenderer.SetPosition(0, wireInput.uiTransform.position);
                lineRenderer.SetPosition(1, wireOutput.uiTransform.position);
            }
            else if (wireOutput != null)
            {
                lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lineRenderer.SetPosition(1, wireOutput.uiTransform.position);
            }
            else if (wireInput != null)
            {
                lineRenderer.SetPosition(0, wireInput.uiTransform.position);
                lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else
                Destroy(gameObject);
        }

        public void SetWireConnection(WireInputOutput input, WireInputOutput output)
        {
            wireInput = input;
            wireOutput = output;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (ConnectionManager.Instance.isControlHeld && ConnectionManager.Instance.currentWire != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            OnDestroyWire?.Invoke(this);
        }
        public delegate void DestroyDelegate(Wire wire);
        public event DestroyDelegate OnDestroyWire;
    }
}