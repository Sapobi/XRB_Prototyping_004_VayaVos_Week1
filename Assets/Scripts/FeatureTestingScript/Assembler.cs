using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Assembler : MonoBehaviour
{
	private Rigidbody _rb;

	[SerializeField] private List<GameObject> _bricks;
	[SerializeField] private List<Rigidbody> _brickRigidbodies;
	[SerializeField] private Transform holdPosition;

	void Awake()
	{
		_bricks = new List<GameObject>();
		_brickRigidbodies = new List<Rigidbody>();
		_rb = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		//if (_bricks.Count > 1) return;
		//RemoveFromAssembly(_bricks[0]);
	}

	private void CalculateCoM()
	{
		var centerOfMass = new Vector3();
		var totalMass = 0f;
		foreach (var part in _brickRigidbodies)
		{
			var mass = part.mass;
			centerOfMass += part.worldCenterOfMass * mass;
			totalMass += mass;
		}

		centerOfMass /= totalMass;
		_rb.centerOfMass = centerOfMass;
	}

	private void RemoveFromAssembly(GameObject brick)
	{
		var index = _bricks.IndexOf(brick);

		var removedObject = _bricks[index];
		removedObject.transform.parent = null;
		removedObject.AddComponent<Rigidbody>();

		var removedObjectRb = removedObject.GetComponent<Rigidbody>();
		removedObjectRb.mass = _brickRigidbodies[index].mass;
		removedObjectRb.centerOfMass = _brickRigidbodies[index].centerOfMass;

		_bricks.RemoveAt(index);
		_brickRigidbodies.RemoveAt(index);

		//CalculateCoM();
	}

	public void AddToAssembly(GameObject brick)
	{
		//var grabInteractable = brick.GetComponent<XRGrabInteractable>();
		//grabInteractable.enabled = false;
		
		_bricks.Add(brick);

		//CalculateCoM();
	}
}