using RobbieWagnerGames.FirstPerson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CircuitryGame
{
    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance { get; private set; }

        public Canvas canvas;
        public WireInputOutput input;
        public WireInputOutput output;
        [HideInInspector] public HashSet<WireInputOutput> hoveredInputOutputs = new HashSet<WireInputOutput>();

        public Wire currentWire;

        [HideInInspector] public bool isControlHeld = false;
        private UIControls controls;

        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            controls = new UIControls();
            controls.Enable();
            controls.UI.Click.performed += ClickHandler;
            controls.UI.ControlButton.performed += ToggleControlOn;
            controls.UI.ControlButton.canceled += ToggleControlOff;
        }

        private void ClickHandler(InputAction.CallbackContext context)
        {
            if (currentWire != null && hoveredInputOutputs.Count == 0)
            {
               ClearSelection();
            }
        }

        public void ClearSelection(bool deleteWire = true)
        {
            if(deleteWire)
                Destroy(currentWire.gameObject);
            currentWire = null;
            input = null;
            output = null;
        }

        public void SetupWire()
        {
            currentWire.wireOutput = input;
            currentWire.wireInput = output;
            currentWire.UpdateWire();
            currentWire.SetWireConnection(input, output);
            ClearSelection(false);
        }

        private void ToggleControlOn(InputAction.CallbackContext context)
        {
            isControlHeld = true;
            Debug.Log(isControlHeld);
        }

        private void ToggleControlOff(InputAction.CallbackContext context)
        {
            isControlHeld = false;
            Debug.Log(isControlHeld);
        }
    }
}