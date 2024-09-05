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
        [SerializeField] private PolygonCollider2D polygonCollider;
        [SerializeField] private Vector2 colliderOffset;

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
                //lineRenderer.BakeMesh();
                UpdatePolygonCollider();

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

        private void UpdatePolygonCollider()
        {
            if (polygonCollider == null)
            {
                Debug.LogWarning("could not update collider: collider not present");
                return;
            }

            polygonCollider.enabled = true;
            Vector2 startPoint = Camera.main.WorldToScreenPoint(lineRenderer.GetPosition(0));
            Vector2 endPoint = Camera.main.WorldToScreenPoint(lineRenderer.GetPosition(1));
            //Vector2 startPoint = Camera.main.WorldToViewportPoint(lineRenderer.GetPosition(0));
            //Vector2 endPoint = Camera.main.WorldToViewportPoint(lineRenderer.GetPosition(1));

            Vector2 direction = (endPoint - startPoint).normalized;
            direction = new Vector2(direction.y, direction.x);

            Vector2[] points = new Vector2[4]
            {
                startPoint - (direction * wireHeight / 4) + colliderOffset,
                startPoint + (direction * wireHeight / 4) + colliderOffset,
                endPoint + (direction * wireHeight / 4) + colliderOffset,
                endPoint - (direction * wireHeight / 4) + colliderOffset
            };

            polygonCollider.points = points;
        }

        public void SetWireConnection(WireInputOutput input, WireInputOutput output)
        {
            wireInput = input;
            wireOutput = output;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Debug.Log("hi");
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