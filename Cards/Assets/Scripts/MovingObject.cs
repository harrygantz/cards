using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

	public float moveTime = 0.1f;
	public LayerMask blockingLyer; //Determines if a space can be moved into

	private BoxCollider _boxCollider;
	private Rigidbody _rigidbody;
	private float _inverseMoveTime;
	
	
	protected virtual void Start ()
	{
		_boxCollider = GetComponent<BoxCollider>();
		_rigidbody = GetComponent<Rigidbody>();
		_inverseMoveTime = 1f / moveTime; // We can multiply instead of dividing which is more efficient computationally
	}

	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2(xDir, yDir);

		_boxCollider.enabled = false;
		hit = Physics2D.Linecast(start, end, blockingLyer);
		_boxCollider.enabled = true;

		if (hit.transform == null)
		{
			StartCoroutine(SmoothMovement(end));
			return true;
		}
		return false;
	}
	
	//This moves units from one space to the next taking in a parameter end to specify where to move to
	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude; //Using square magnitude because it is computationally cheaper than magnitude

		while (sqrRemainingDistance > float.Epsilon)
		{
			//moves in a straight line to a target point
			Vector3 newPosition = Vector3.MoveTowards(_rigidbody.position, end, _inverseMoveTime = Time.deltaTime);
			_rigidbody.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AttemptMove<T>(int xDir, int yDir)
		where T : Component
	{
		RaycastHit2D hit;
		bool canMove = Move(xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T>();
		
		if (!canMove && hitComponent != null)
			OnCantMove((hitComponent));
	}
	protected abstract void OnCantMove<T>(T component)
		where T : Component;
}
