using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGrid : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [Min(1)] [SerializeField] private int gridCellSize;



    public Vector2Int GetNearestGridCoordinate(Vector2 coordinate)
    {

        return Vector2Int.zero;

    }



    void OnDrawGizmos()
    {

        if(gridCellSize < 1) return;

        Gizmos.color = Color.red;
        
        Vector3 pos0 = new Vector3(0f, 0f, 0f);
        Vector3 pos1 = new Vector3(0f, 0f, 0f);

        Rect r = canvas.pixelRect;

        for (int i = (int)Mathf.Floor(r.xMin); i < (int)Mathf.Floor(r.xMax); i += gridCellSize)
        {
            pos0.x = i;
            pos0.y = r.yMin;
            pos1.x = i;
            pos1.y = r.yMax;
            Gizmos.DrawLine
            (
                pos0,
                pos1
            );
        }

        for (int i = (int)Mathf.Floor(r.yMin); i < (int)Mathf.Floor(r.yMax); i += gridCellSize)
        {
            pos0.x = r.xMin;
            pos0.y = i;
            pos1.x = r.xMax;
            pos1.y = i;
            Gizmos.DrawLine
            (
                pos0,
                pos1
            );

        }  
    }

}