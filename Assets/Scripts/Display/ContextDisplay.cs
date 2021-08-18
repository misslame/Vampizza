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
    Dictionary<string, Color> textHighlighting = new Dictionary<string, Color>{
        {"Remove", new Color(1f, 0f, 0f, 1f)},
        {"Upgrade", new Color(0f, 1f, 0f, 1f)},
        {"None", new Color(1f, 1f, 1f, 1f)}
    };
    private Button[] ContextButtons;

    void Awake(){
        ContextButtons = ButtonGroup.GetComponentsInChildren<Button>();
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
                OnPointerClickDelegate((PointerEventData) data);
            });

            trigger.triggers.Add( entryEnter );
            trigger.triggers.Add( entryLeave );
            trigger.triggers.Add( entryPress );
        }
        HideDescription();
    }

    void Start(){
        // here for debug reasons
        // MoveToCursor();
    }

    public void OnPointerEnterDelegate(PointerEventData data, string name){
        if (textHighlighting.ContainsKey(name)){
            Debug.Log(name);
            ShowDescription(name, textHighlighting[name]);
        } else {
            ShowDescription(name);
        }
    }

    public void OnPointerLeaveDelegate(PointerEventData data){
        HideDescription();
    }

    public void OnPointerClickDelegate(PointerEventData data){
        HideMenu();
    }

    public void ShowDescription(string n, Color c){
        Desc.text = n;
        Desc.color = c;
        Bottom.alpha = 1;
    }
    
    public void ShowDescription(string n){
        Desc.text = n;
        Desc.color = textHighlighting["None"];
        Bottom.alpha = 1;
    }

    public void HideDescription(){
        Bottom.alpha = 0;
    }

    void ShowMenu(){
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
    }

    void HideMenu(){
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
    }


    [SerializeField] Canvas canvas;
    public void MoveToCursor(){
        ShowMenu();
        RectTransform CanvasRect=canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition=Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 WorldObject_ScreenPosition=new Vector2(
        ((ViewportPosition.x*CanvasRect.sizeDelta.x)),
        ((ViewportPosition.y*CanvasRect.sizeDelta.y)));
        gameObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }
}
