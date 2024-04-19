using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[RequireComponent(typeof(RectTransform))]
public class DropArea : MonoBehaviour, IPointerEnterHandler
{

    public virtual void OnPointerEnter(PointerEventData eventData)
    {

        if(eventData.pointerDrag == null) return;

        DraggableElement draggedItem = eventData.pointerDrag.GetComponent<DraggableElement>();

        if(draggedItem == null) return;

        UpdateDropArea(draggedItem);

    }



    protected void UpdateDropArea(DraggableElement draggedItem)
    {

        draggedItem.transform.SetParent(transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        draggedItem.UpdateMostRecentPosition(); 

    }

}