using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CircuitryGame
{
    public class Wire : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float wireHeight = 6;
        public bool on;
        public WireInputOutput wireOutput;
        public WireInputOutput wireInput;
        [SerializeField] private Animator animator;

        public void UpdateWire()
        {
            if(wireOutput != null && wireInput != null)
                return;
            if(wireOutput != null)
            {
                rectTransform.pivot = Vector3.right;
                rectTransform.sizeDelta = new Vector2(Vector2.Distance(Input.mousePosition, wireOutput.transform.position) / 5.5f * 4, wireHeight);
                Debug.Log(rectTransform.sizeDelta);
                rectTransform.rotation = Quaternion.Euler(0, 0, Quaternion.LookRotation(Input.mousePosition - wireOutput.transform.position, Vector3.up).eulerAngles.x);
            }
            if (wireInput != null)
            {
                rectTransform.pivot = Vector3.zero;
                rectTransform.sizeDelta = new Vector2(Vector2.Distance(Input.mousePosition, wireInput.uiTransform.position) / 5.5f * 4, wireHeight);
                Debug.Log(rectTransform.sizeDelta);
                rectTransform.rotation = Quaternion.Euler(0, 0, Quaternion.LookRotation(Input.mousePosition - wireInput.transform.position, Vector3.up).eulerAngles.x); 
            }
            else
                Destroy(gameObject);
        }

        public void SetWireConnection(WireInputOutput input, WireInputOutput output)
        {
            wireInput = input;
            wireOutput = output;
            rectTransform.pivot = Vector3.zero;
            rectTransform.sizeDelta = new Vector2(Vector2.Distance(wireInput.uiTransform.anchoredPosition, wireOutput.uiTransform.position) / 5.5f * 4, wireHeight);
            Debug.Log(rectTransform.sizeDelta);
            rectTransform.rotation = Quaternion.Euler(0, 0, Quaternion.LookRotation(wireOutput.transform.position - wireInput.transform.position, Vector3.up).eulerAngles.x);
        }
    }
}