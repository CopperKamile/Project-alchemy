using UnityEngine;
using UnityEngine.U2D;

public class CheckColliisionWeightBalance : MonoBehaviour
{
    public bool IsRectTransformOverlaping(RectTransform a, RectTransform b)
    {
        Rect rectA = GetWorldRect(a);
        Rect rectB = GetWorldRect(b);
        return rectA.Overlaps(rectB);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corner = new Vector3[4];
        rt.GetWorldCorners(corner);

        //Debug.Log("World Corners");
        //for (var i = 0; i < 4; i++)
        //{
        //    Debug.Log("World Corner " + i + " : " + v[i]);
        //}

        //corners 
        // [1] --------- [2]
        //  |             |
        // [0] --------- [3]

        float width = Vector3.Distance(corner[0], corner[3]);
        float height = Vector3.Distance(corner[0], corner[1]);

        return new Rect(corner[0], new Vector2(width, height));

    }
}
