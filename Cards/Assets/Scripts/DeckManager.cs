using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour 
{
    // -Need an array or list of cards that will be used in the level.
    // -Whatever data structure we use should allow us to grab a card prefab and place the card in it via the inspector.
    // -Then we want to create a pool using those selected prefabs.
    //     -We need to know how many times each card has apeared
    //     -We need to know the total number of cards that will appear (in order to maximize efficiency we should pool
    //      20 cards or less at a time).
    //     -The pool should stop at a predefined count
    //     -The pool should only add a card after the most recent card was destroyed
    //     -There needs to be a way to set the occurrence of each prefab (for instance one type of card shows up x times
    //      while another type shows up y times)
    //     - /
    // .
}
