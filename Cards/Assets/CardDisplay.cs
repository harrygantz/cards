using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DEAL.ScriptableObjects;


public class CardDisplay : MonoBehaviour
{
	public Card card;
	public TextMesh nameText;
	public TextMesh descriptionText;
	public Color32 cardColor;
	public Renderer cardRenderer;
  

	void Start ()
	{
		nameText.text = card.name;
		descriptionText.text = card.description;
		cardRenderer = gameObject.GetComponent<Renderer>();
		cardRenderer.material = card.matertial;
	}

}
