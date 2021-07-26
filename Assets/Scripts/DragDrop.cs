using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.Animations;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] RectTransform InventoryPanelSlider;
    public Item SlotContent;

    // Event listener for mouse click down
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log(string.Format("You pressed the pointer down on <color=green>{0}</color>!\n" + 
        "This is a <color=purple>{1}</color>", SlotContent, SlotContent.GetType().BaseType));
    }

    // Event listener for beginning of drag
    public void OnBeginDrag(PointerEventData eventData) {
        Animator anim = InventoryPanelSlider.GetComponent<Animator>();
        anim.SetBool("ShowInventory", false);
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
