using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Brick : MonoBehaviour
{
	[SerializeField] private GameObject buildPreview;
	[SerializeField] private AudioSource snapSound;
	[SerializeField] private Transform centerTransform;

	private bool _isGrabbed, _positionOk;
	private XRGrabInteractable _grabInteractable;
	private Rigidbody _rb;


	private BoxCollider _previewCollider;
	private Vector3 placePosition;
	private Quaternion placeRotation;

	[SerializeField] private bool unevenX, unevenZ;

	void Start()
	{
		_isGrabbed = false;
		_grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		_rb = gameObject.GetComponent<Rigidbody>();

		_previewCollider = buildPreview.GetComponent<BoxCollider>();

		if (!centerTransform) centerTransform = transform;

		_grabInteractable.selectEntered.AddListener(ActivateBuildMode);
		_grabInteractable.selectExited.AddListener(SnapBrick);
	}

	private void ActivateBuildMode(SelectEnterEventArgs arg0)
	{
		_isGrabbed = true;
	}

	private void SnapBrick(SelectExitEventArgs arg0)
	{
		_isGrabbed = false;
		if (!_positionOk)
		{
			_rb.isKinematic = false;
			return;
		}

		_rb.isKinematic = true;
		transform.position = placePosition;
		transform.rotation = placeRotation;
		snapSound.Play();

		buildPreview.SetActive(false);
	}

	void Update()
	{
		if (!_isGrabbed) return;
		CheckBrickPlacement();
	}

	private void CheckBrickPlacement()
	{
		if (!Physics.BoxCast(transform.position, _previewCollider.size / 2, -transform.up, out var hitInfo, transform.rotation, 0.08f, LegoLogic.LayerMaskLego))
		{
			_positionOk = false;
			buildPreview.SetActive(false);
			return;
		}

		var brickPos = transform.position;
		placePosition = LegoLogic.SnapPosition(new Vector3(brickPos.x, hitInfo.point.y, brickPos.z));
		placeRotation = LegoLogic.SnapRotation(transform.rotation);
		buildPreview.transform.position = placePosition;
		buildPreview.transform.rotation = placeRotation;

		_positionOk = false;
		var colliders = Physics.OverlapBox(placePosition + placeRotation * _previewCollider.center, _previewCollider.size / 2, placeRotation, LegoLogic.LayerMaskLego);
		var otherColliders = colliders.Where(c => c.gameObject != gameObject).ToArray();
		_positionOk = otherColliders.Length == 0;

		buildPreview.SetActive(_positionOk);
	}
}