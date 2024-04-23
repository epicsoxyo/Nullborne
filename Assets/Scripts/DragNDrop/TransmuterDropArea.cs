using UnityEngine;
using UnityEngine.EventSystems;



namespace Nullborne.UI
{

    public class TransmuterDropArea : DropArea, IDropHandler
    {
        
        private Transform endGlyph;



        public void OnDrop(PointerEventData eventData)
        {
            GameObject.FindWithTag("EndGlyph").transform.SetAsLastSibling();
        }

    }

}