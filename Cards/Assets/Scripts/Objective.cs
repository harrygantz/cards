using System.Collections;
using System.Collections.Generic;
using UnityEngine;



	//This requires that the component be attached to some sort of collider
	[RequireComponent(typeof(Collider))]  
	public class Objective : MonoBehaviour
	{
		// tag to identify the player
		[SerializeField]
		private string _cardTag = "Player";

		// is the objective complete?
		private bool _isComplete;
		public bool IsComplete { get { return _isComplete; } }

		// set the objective to complete
		public void CompleteObjective()
		{
			_isComplete = true;
		}

		// when the player touches the trigger...
		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == _cardTag)
			{
				CompleteObjective();
			}
		}

	}
