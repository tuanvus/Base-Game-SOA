using UnityEngine;
using TMPro;

public class PopUpText : BasePopUp
{
	[SerializeField]
	protected TextMeshProUGUI txtTitle, txtMessage;


	public void Init(string message)
	{
		txtMessage.text = message;
	}

	public void Init(string title, string message)
	{
		txtTitle.text = title;
		Init(message);
	}
}

	

