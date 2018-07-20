using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DEAL.ScriptableObjects;


public class CardDisplay : MonoBehaviour
{
	public Card card;

	public Text nameText;
	public Text descriptionText;

	public Material cardMaterial; 
	void Start ()
	{
		nameText.text = card.name;
		descriptionText.text = card.description;
		cardMaterial = GetComponent<Material>();
		cardMaterial = card.matertial;
	}

}
