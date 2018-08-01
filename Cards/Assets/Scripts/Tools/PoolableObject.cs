using UnityEngine;
using System.Collections;
using DEAL.Tools;
using System;

namespace DEAL.Tools
{	
	/// Add this class to an object that you expect to pool from an objectPooler. 
	/// Note that these objects can't be destroyed by calling Destroy(), they'll just be set inactive (that's the whole point).
	public class PoolableObject : ObjectBounds 
	{
		public delegate void Events();
		public event Events OnSpawnComplete;

		/// The life time, in seconds, of the object. If set to 0 it'll live forever, if set to any positive value it'll be set inactive after that time.
		public float LifeTime = 0f;

		/// Turns the instance inactive, in order to eventually reuse it.
		public virtual void Destroy()
		{
			gameObject.SetActive(false);
		}

		/// Called every frame
		protected virtual void Update()
		{

		}


		/// When the objects get enabled (usually after having been pooled from an ObjectPooler, we initiate its death countdown.
		protected virtual void OnEnable()
		{
			Size = GetBounds().extents * 2;
			if (LifeTime>0)
			{
				Invoke("Destroy", LifeTime);	
			}
		}


		/// When the object gets disabled (maybe it got out of bounds), we cancel its programmed death
		protected virtual void OnDisable()
		{
			CancelInvoke();
		}


		/// Triggers the on spawn complete event
		public void TriggerOnSpawnComplete()
		{
			if(OnSpawnComplete != null)
			{
				OnSpawnComplete();
			}
		}
	}
}
