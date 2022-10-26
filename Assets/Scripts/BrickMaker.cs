using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BrickMaker : MonoBehaviour
{
	[SerializeField] private GameObject[] bricks;
	[SerializeField] private InputActionReference[] makeBrickActionReferences;
	[SerializeField] private Transform leftController, rightController;

	public int BrickType { get; set; }
	public int BrickColor { get; set; }
	public Material[] materials;

	public UnityEvent brickColorChanged;

	private bool _makingBrick;
	private GameObject _generatedBrick;
	private List<GameObject> _generatedBricks;

	// Start is called before the first frame update
	void Start()
	{
		_makingBrick = false;
		_generatedBricks = new List<GameObject>();

		foreach (var action in makeBrickActionReferences)
		{
			action.action.started += arg => MakeBrick();
			action.action.canceled += arg => DropBrick();
		}
	}

	/*
	void Update()
	{
		if (!_makingBrick) return;
		FormatGeneratedBrick();
	}

	private void FormatGeneratedBrick()
	{
		var rightControllerPosition = rightController.position;
		var leftControllerPosition = leftController.position;

		_generatedBrick.transform.position = Vector3.Lerp(rightControllerPosition, leftControllerPosition, 0.5f);
		_generatedBrick.transform.rotation = Quaternion.LookRotation(rightControllerPosition - leftControllerPosition, transform.up);
		_generatedBrick.transform.localScale = Vector3.Distance(rightControllerPosition, leftControllerPosition) * 0.75f * Vector3.one;
	}
	*/

	private void MakeBrick()
	{
		var rightControllerPosition = rightController.position;
		var leftControllerPosition = leftController.position;

		_makingBrick = true;
		_generatedBrick = Instantiate(bricks[BrickType],
			Vector3.Lerp(rightControllerPosition, leftControllerPosition, 0.5f),
			Quaternion.LookRotation(rightControllerPosition - leftControllerPosition, transform.up));
		_generatedBrick.GetComponent<Renderer>().material = materials[BrickColor];
		_generatedBricks.Add(_generatedBrick);
	}

	private void DropBrick()
	{
		if (!_makingBrick) return;

		_generatedBrick.GetComponent<Rigidbody>().isKinematic = false;
		_makingBrick = false;
	}

	public void ChangeBrickColor(int brickColor)
	{
		BrickColor = brickColor;
		brickColorChanged.Invoke();
	}
}