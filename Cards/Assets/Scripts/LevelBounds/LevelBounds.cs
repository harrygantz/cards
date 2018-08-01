using System.Collections;
using System.Collections.Generic;
using DEAL;
using UnityEngine;
using DEAL.Tools;

public class LevelBounds : MonoBehaviour
{
    public string acceptedCardType = "";

    // This array will hold all of our level bounds.
    public static List<GameObject> bounds = new List<GameObject>();

    public GameObject topBound;
    public GameObject rightBound;
    public GameObject bottomBound;
    public GameObject leftBound;
    
    
    /// <summary>
    /// Get the bounds of the level and store them in an array of bounds. The bounds should always start as bounds[0] =
    /// top, bounds[1] = right, bounds[2] = bottom, and bounds[3] = left. This is based on their position in the
    /// hierarchy. 
    /// </summary>
    private void Start()
    {
        if (transform.childCount != 4)
        {
            Debug.LogWarning("LEVELBOUNDS Start: ERROR The number of bounds is incorrect!");
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            bounds.Add(transform.GetChild(i).gameObject);
            bounds[i].GetComponent<Bound>().acceptedCardType = "Clear";
        }

        bounds[0] = topBound;
        bounds[1] = rightBound;
        bounds[2] = bottomBound;
        bounds[3] = leftBound;
    }

    /// <summary>
    /// Changes the type of card accepted in the boundry 
    /// </summary>
    /// <param name="boundIndex"></param> this is the index of the bound you wish to change
    /// <param name="cardType"></param> this is the card type a.k.a. the tag assigned to the card
    public static void UpdateAcceptedCardType(int boundIndex, string cardType)
    {
        bounds[boundIndex].GetComponent<Bound>().acceptedCardType = cardType;
    }
}
