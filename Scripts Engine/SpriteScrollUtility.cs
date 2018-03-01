using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteScrollUtility {

	public static Vector3 FindNextElementPosition(List<Transform> elementsList,float xMod=0)
    {
        Vector3 pos = Vector3.zero;
        pos.x += xMod;


        if (!isFirstElement(elementsList))
        {

            SpriteRenderer lastElement = elementsList[elementsList.Count - 1].gameObject.GetComponent<SpriteRenderer>();
            pos = lastElement.transform.position;
            pos.x += lastElement.bounds.size.x;

        }
        return pos;
    }

    static bool isFirstElement(List<Transform> elementList)
    {
        if (elementList.Count == 0)
        {


            return true;
        }
        else
        {

            return false;
        }

    }
}
