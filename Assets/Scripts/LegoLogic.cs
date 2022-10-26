using UnityEngine;

public static class LegoLogic
{
	private static readonly Vector3 Grid = new(0.05f, 0.08f, 0.05f);
	public static readonly int LayerMaskLegoAttach = LayerMask.GetMask("Floor","Lego");
	public static readonly int LayerMaskLego = LayerMask.GetMask("Floor","Lego");

	public static Vector3 SnapPosition(Vector3 input)
	{
		return new Vector3(Mathf.Round(input.x / Grid.x) * Grid.x,
			Mathf.Round(input.y / Grid.y) * Grid.y + Grid.y/2,
			Mathf.Round(input.z / Grid.z) * Grid.z);
	}

	public static Quaternion SnapRotation(Quaternion input)
	{
		var snapAngle = new Vector3(0,Mathf.Round(input.eulerAngles.y / 90) * 90, 0);
		return Quaternion.Euler(snapAngle);
	}
}