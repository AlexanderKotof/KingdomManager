using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using KM.UI.CarouselScreens;
using ScreenSystem.Components;

public class EntityButton : WindowComponent, IPointerDownHandler, IPointerUpHandler
{
    public GameEntity linkedEntity { get; protected set; }

    public Image Icon;
    public TMP_Text NameText;
    public TMP_Text CountText;
    public Button Button;

    public static GameObject EntityButtonPrefab => Resources.Load<GameObject>("UI/Components/EntityButton");


    public void SetInfo(GameEntity entity, System.Action<GameEntity> onClick, int count = 1)
    {
        if (entity == null)
            return;

        Icon.sprite = entity.Icon;

        var height = Icon.rectTransform.rect.height;
       // Icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, height);

        NameText.text = entity.name;

        CountText.text = count <= 1 ? "" : count.ToString();

        linkedEntity = entity;

        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(() =>
        {
            onClick?.Invoke(linkedEntity);
        });
    }

    public void SetInfo(GameEntity entity, System.Action<GameEntity> onClick, string counterText)
    {
        if (entity == null)
            return;

        Icon.sprite = entity.Icon;

        var height = Icon.rectTransform.rect.height;
        // Icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, height);

        NameText.text = entity.name;

        CountText.text = counterText;

        linkedEntity = entity;

        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(() =>
        {
            onClick?.Invoke(linkedEntity);
        });
    }

    public void SetBuildingInfo(GameEntity entity, System.Action<GameEntity> onClick, bool builded)
    {
        if (entity == null)
            return;

        Icon.sprite = entity.Icon;

        if (builded)
        Icon.color = Color.white;

        NameText.text = entity.name;

        CountText.text = "";

        linkedEntity = entity;

        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(() =>
        {
            onClick?.Invoke(linkedEntity);
        });
    }

    public void SetCount(int count)
    {
        CountText.text = count <= 0 ? "" : count.ToString();
    }

    public bool DragEnabled = false;
    bool isDraging = false;

    Vector3 offset;
    Transform parent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!DragEnabled)
            return;

        Debug.Log("Onpointerdown");
        isDraging = true;

        offset = Input.mousePosition - transform.position;

        transform.SetParent(CarouselScreen.canvasTransform);
    }

    private void DragStarted()
    {
        
    }

    public Action<Vector3> OnDragEnded;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!DragEnabled)
            return;

        Debug.Log("Onpointerup");
        isDraging = false;

        transform.SetParent(parent);

        OnDragEnded?.Invoke(Input.mousePosition);
    }
    void Start()
    {
        parent = transform.parent;
    }

    public void Update()
    {
        if (isDraging)
        {
            transform.position = offset + Input.mousePosition;
        }
    }
}