using UnityEngine;

public class MenuModel : MonoBehaviour
{
	private Vector3 _axis = new (0, 1, 0);
	private float _speed = 5;
	private BrickMaker _brickMaker;

	private void Start()
	{
		_brickMaker = FindObjectOfType<BrickMaker>();
		_brickMaker.brickColorChanged.AddListener(ChangeMaterial);
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(_axis, _speed*Time.deltaTime);
	}

	private void ChangeMaterial()
	{
		gameObject.GetComponent<Renderer>().material = _brickMaker.materials[_brickMaker.BrickColor];
	}
}
