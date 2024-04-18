using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[RequireComponent(typeof(RectTransform))]
public class DraggableElement : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {

    }



    public void OnEndDrag(PointerEventData eventData)
    {
        // rectTransform.anchoredPosition = Mathf.Round(rectTransform.anchoredPosition  canvas.pixelRect.size)
    }



    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }



    public void OnPointerDown(PointerEventData eventData)
    {

    }

}