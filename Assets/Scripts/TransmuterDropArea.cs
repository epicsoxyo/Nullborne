using Nullborne.GlyphCode;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransmuterDropArea : DropArea, IDropHandler
{
    
    private Transform endGlyph;



    private void Start()
    {
        endGlyph = transform.GetChild(0);
    }



    public void OnDrop(PointerEventData eventData)
    {
        endGlyph.SetAsLastSibling();
    }

}
