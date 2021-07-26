using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("You pressed the pointer down!");
    }

    // Event listener for beginning of drag
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("You are now dragging the pointer!");
    }

    // Event listener for drag, triggers for every frame that drag occurs
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("You dragged for a frame!");
    }

    // Event listener for end of drag
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("You have stopped dragging the pointer!");
    }
}
