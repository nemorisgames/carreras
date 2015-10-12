using UnityEngine;

public class RepeatButton : MonoBehaviour
{
	public float interval = 0.25f;
	
	bool mIsPressed = false;
	float mNextClick = 0f;

	Central central;

	public string tipoBoton;

	void Start(){
		central = Camera.main.GetComponent<Central> ();
	}

	void OnPress (bool isPressed) { mIsPressed = isPressed; mNextClick = Time.realtimeSinceStartup + interval; }
	
	void Update ()
	{
		if (mIsPressed && Time.realtimeSinceStartup < mNextClick)
		{
			mNextClick = Time.realtimeSinceStartup + interval;
			print ("touched");
			central.repeatButtonPress(tipoBoton);
			// Do what you need to do, or simply:
			//SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}
}
