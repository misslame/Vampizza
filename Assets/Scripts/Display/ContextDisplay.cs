/*
 * ContextDisplay.cs
 * Controls context display popup menu (right clicking structures)
 * Handles pointer interactions with context menu (leaving menu, clicking on menu elements)
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ContextDisplay : MonoBehaviour
{
    [SerializeField] private Text Title;
    [SerializeField] private Text Desc;
    [SerializeField] private GameObject ButtonGroup;
    [SerializeField] private CanvasGroup Bottom;
    [SerializeField] private Canvas canvas;
    Dictionary<string, Color> textHighlighting = new Dictionary<string, Color>{
        {"Remove", new Color(1f, 0f, 0f, 1f)},
        {"harvest", new Color(1f,0f,0f,1f) },
        {"Upgrade", new Color(0f, 1f, 0f, 1f)},
        {"None", new Color(1f, 1f, 1f, 1f)}
    };
    private Button[] ContextButtons;
    private Vector3Int CurrentSelectedCoord;

    void Awake(){
        ContextButtons = ButtonGroup.GetComponentsInChildren<Button>();

        // Apply pointer event triggers for each button
        foreach(Button button in ContextButtons){
            EventTrigger trigger = button.GetComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => {
                OnPointerEnterDelegate((PointerEventData) data, button.name);
            });

            EventTrigger.Entry entryLeave = new EventTrigger.Entry();
            entryLeave.eventID = EventTriggerType.PointerExit;
            entryLeave.callback.AddListener((data) => {
                OnPointerLeaveDelegate((PointerEventData) data);
            });

            EventTrigger.Entry entryPress = new EventTrigger.Entry();
            entryPress.eventID = EventTriggerType.PointerClick;
            entryPress.callback.AddListener((data) => {
                OnPointerClickDelegate((PointerEventData) data, button.name);
            });

            trigger.triggers.Add( entryEnter );
            trigger.triggers.Add( entryLeave );
            trigger.triggers.Add( entryPress );
        }
        HideDescription();
    }

    /// <summary>
    /// Callback function for pointer entry
    /// </summary>
    private void OnPointerEnterDelegate(PointerEventData data, string name){
        if (textHighlighting.ContainsKey(name)){
            Debug.Log(name);
            ShowDescription(name, textHighlighting[name]);
        } else {
            ShowDescription(name);
        }
    }

    /// <summary>
    /// Callback function for pointer leave
    /// </summary>
    private void OnPointerLeaveDelegate(PointerEventData data){
        HideDescription();
    }

    /// <summary>
    /// Callback function for pointer click
    /// </summary>
    private void OnPointerClickDelegate(PointerEventData data, string name){
        switch(name){
            case "Remove":
                StructureTileManager.Instance.PutAwayStructure(CurrentSelectedCoord);
                break;
            case "Upgrade":
                Debug.Log("<color=red>not implemented yet :( </color>");
                break;
            case "Harvest":
                StructureTileManager.Instance.TryHarvest(CurrentSelectedCoord);
                break;
            default:
                break;
        }
        HideMenu();
    }

    
    /// <summary>
    /// Shows bottom description section
    /// </summary>
    private void ShowDescription(string n, Color c){
        Desc.text = n;
        Desc.color = c;
        Bottom.alpha = 1;
    }

    /// <summary>
    /// Shows bottom description section
    /// </summary>
    private void ShowDescription(string n){
        Desc.text = n;
        Desc.color = textHighlighting["None"];
        Bottom.alpha = 1;
    }

    
    /// <summary>
    /// Hide bottom description section via 0 alpha
    /// </summary>
    private void HideDescription(){
        Bottom.alpha = 0;
    }


    /// <summary>
    /// Hide bottom description section via 0 alpha
    /// </summary>
    public void ShowMenu(){
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
    }

    /// <summary>
    /// Show entire component
    /// </summary>
    public void ShowMenu(string title){
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        Title.text = title;
    }

    /// <summary>
    /// Hide entire component
    /// </summary>
    public void HideMenu(){
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }


    /// <summary>
    /// Moves component to cursor
    /// </summary>
    public void MoveToCursor(Vector3Int selectedCoord){
        ShowMenu();
        CurrentSelectedCoord = selectedCoord;
        RectTransform CanvasRect=canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition=Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 WorldObject_ScreenPosition=new Vector2(
        ((ViewportPosition.x*CanvasRect.sizeDelta.x)),
        ((ViewportPosition.y*CanvasRect.sizeDelta.y)));
        gameObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }

    /// <summary>
    /// Moves component to cursor
    /// </summary>
    public void MoveToCursor(string title, Vector3Int selectedCoord){
        ShowMenu(title);
        CurrentSelectedCoord = selectedCoord;
        RectTransform CanvasRect=canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition=Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 WorldObject_ScreenPosition=new Vector2(
        ((ViewportPosition.x*CanvasRect.sizeDelta.x)),
        ((ViewportPosition.y*CanvasRect.sizeDelta.y)));
        gameObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }
}
