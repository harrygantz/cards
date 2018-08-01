using UnityEngine;
using System.Collections;

namespace DEAL.Tools
{	
	public class ReactivateOnSpawn : MonoBehaviour 
	{
		public bool ShouldReactivate=true;	

		public virtual void Reactivate()
		{
			if (ShouldReactivate)
			{
				gameObject.SetActive(true);
			}
		}
	}
}