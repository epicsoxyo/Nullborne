using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Nullborne.UI
{

    public enum DraggableElementType
    {
        DRAGGABLE_DEFAULT,
        DRAGGABLE_GLYPH,
        DRAGGABLE_LITERAL,

        // add more draggable elements here...
    }

    [RequireComponent(typeof(RectTransform))]
    public class DropArea : MonoBehaviour, IPointerEnterHandler
    {

        [SerializeField] private DraggableElementType draggableElementType_;



        public virtual void OnPointerEnter(PointerEventData eventData)
        {

            if(eventData.pointerDrag == null) return;

            DraggableElement draggedItem = eventData.pointerDrag.GetComponent<DraggableElement>();

            if(draggedItem == null) return;

            if(draggedItem.draggableElementType != DraggableElementType.DRAGGABLE_DEFAULT
            && draggedItem.draggableElementType != draggableElementType_)
                return;

            UpdateDropArea(draggedItem);

        }



        protected void UpdateDropArea(DraggableElement draggedItem)
        {

            draggedItem.transform.SetParent(transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            draggedItem.UpdateMostRecentPosition(); 

        }

    }

}