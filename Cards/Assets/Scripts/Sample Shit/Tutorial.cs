using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject[] Deck;
    private int currentDeckIndex;
    private Vector2 touchOrigin = -Vector2.one; //initalizes the touch to off screen (i.e. false).
    
    private int horizontal;
    private int vertical;


    private void Start()
    {
        //By doing this the position of the cards in the deck matter as they will be chosen based on their position
        //in the array.
        Deck = new GameObject[transform.childCount];
        
        //Populate the array with chosen card types. In the tutorial we just have two cards.
        for (int i = 0; i < transform.childCount; i++)
        {
            Deck[i] = transform.GetChild(i).gameObject;
        }

        currentDeckIndex = 0;
    }

    private void Update()
    {

        #region Player Input  
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        horizontal = (int) (Input.GetAxisRaw("Horizontal"));
        vertical = (int) (Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }
#endif
        #endregion
        
        
    }
}
