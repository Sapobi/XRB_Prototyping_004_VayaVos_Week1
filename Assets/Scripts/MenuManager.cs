using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private InputActionReference helpMenuActionReference, brickOptionsActionReference;
	[SerializeField] private GameObject helpMenu, brickOptions;
	[SerializeField] private Outline[] modelOutlines;

	// Start is called before the first frame update
	void Start()
	{
		helpMenuActionReference.action.performed += arg => ToggleHelpMenu();
		brickOptionsActionReference.action.canceled += arg => ToggleBrickOptions();
	}

	private void ToggleHelpMenu()
	{
		helpMenu.SetActive(!helpMenu.activeSelf);
	}

	private void ToggleBrickOptions()
	{
		brickOptions.SetActive(!brickOptions.activeSelf);
	}

	public void SetOutline(int option)
	{
		foreach (var outline in modelOutlines)
		{
			outline.enabled = false;
		}

		modelOutlines[option].enabled = true;
	}
}