using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobbieWagnerGames.UI;
using UnityEngine.EventSystems;
using System;

namespace CircuitryGame
{
    public class Node : CustomUIElement
    {
        [SerializeField] private RectTransform canvasT;
        public List<WireInputOutput> inputs;
        public int inputCount;
        public List<WireInputOutput> outputs;
        public int outputCount;

        private bool dragging = false;
        private Vector2 dragOffset = Vector2.zero;

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Update()
        {
            if(dragging)
                uiTransform.position = (Vector2) Input.mousePosition - dragOffset;

            UpdateInputOutput();
        }

        protected virtual void UpdateInputOutput()
        {
            
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            dragging = true;
            dragOffset = Input.mousePosition - uiTransform.position;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            dragging = false;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            dragging = false;
        }
    }
}