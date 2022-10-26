using System;
using UnityEngine;

public class BrickBottom : MonoBehaviour
{
	[SerializeField] private GameObject assembly;
	private GameObject currentBrick;

	private void Start()
	{
		currentBrick = gameObject.GetComponentInParent<Brick>().gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.GetComponent<BrickTop>()) return;
		var otherBrick = other.gameObject.GetComponentInParent<Brick>().gameObject;
		SnapPieces(otherBrick);
	}
	
	private void SnapPieces(GameObject other)
	{
		var newAssembly = Instantiate(assembly, other.transform.position, other.transform.rotation);
		other.transform.SetParent(newAssembly.transform);
		currentBrick.transform.SetParent(newAssembly.transform);

		var assembler = newAssembly.GetComponent<Assembler>();
		assembler.AddToAssembly(other);
		assembler.AddToAssembly(currentBrick);
	}

	private int CountAssembly(GameObject other)
	{
		return 0;
	}
	
}