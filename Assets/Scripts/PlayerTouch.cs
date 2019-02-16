using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCustomController))]
public class PlayerTouch : MonoBehaviour
{
	
	[Header("movement data")]
	[Header("MovementController")]
	private CharacterCustomController myController;
	[Header("direction references")]
	public GameObject directionPointer;
	public GameObject directionPoint;
	
	
	[Header("touch input")]
	[Header("touch input - move reference")]
	public LayerMask touchInputLayer;
	public GameObject touchInput;
	private bool moventTouch;
	[Header("touch input - jump reference")] 
	public GameObject jumpDirectionLine;
	private bool jumpTouch;

	[Header("time mechanic")]
	[Header("time reference")]
	private TimeScaleController timeScaleController;
	

	// Use this for initialization
	void Start ()
	{
		timeScaleController = GameObject.FindWithTag("TimeScale").GetComponent<TimeScaleController>();
		myController = GetComponent<CharacterCustomController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
				Vector2.zero, 
				Mathf.Infinity, 
				touchInputLayer);
			if (hitInfo.collider != null)
			{
				//checking were he clicked
				if (hitInfo.collider.tag == "MovementTouchInput")
				{
					moventTouch = true;
					//print("click on movement");
				}
				if (hitInfo.collider.tag == "JumpTouchInput")
				{
					jumpTouch = true;
					jumpDirectionLine.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					//print("click on jump");
				}
			}
		}
		if (Input.GetMouseButton(0))
		{
			if (moventTouch)
			{
				//moving the player using the touch input position
				Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				touchInput.transform.position = new Vector3(mPos.x, mPos.y, touchInput.transform.position.z);
				touchInput.transform.localPosition = Vector3.ClampMagnitude(touchInput.transform.localPosition, .07f);
				Vector2 inputValue = touchInput.transform.localPosition.normalized;
				myController.Move(inputValue.x);

				//moving the pointer using the touch input position
				Vector2 lookInput = inputValue;
				float inputAngle = Mathf.Atan2(-lookInput.x, lookInput.y) * Mathf.Rad2Deg;
				directionPointer.transform.eulerAngles = Vector3.forward * inputAngle;
			}

			//getting starting the jump at the momen the mouse touch the input
			if (!moventTouch && !jumpTouch)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 
					Vector2.zero, 
					Mathf.Infinity, 
					touchInputLayer);
				if (hitInfo.collider != null)
				{
					if (hitInfo.collider.tag == "JumpTouchInput")
					{
						jumpTouch = true;
						jumpDirectionLine.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					}
				}
			}
			if (jumpTouch)
			{
				
				//preparing jump line renderer positions
				Vector3 lineRenTouchStart = new Vector3(
					jumpDirectionLine.transform.position.x, 
					jumpDirectionLine.transform.position.y,
					-9);
				Vector3 lineRenTouchCurrent = new Vector3(
					Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
					Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
					-9);
				
				//setting jump line renderer position
				jumpDirectionLine.GetComponent<LineRenderer>().SetPosition(0, lineRenTouchStart);
				jumpDirectionLine.GetComponent<LineRenderer>().SetPosition(1, lineRenTouchCurrent);
				//getting easy vectors references to the line renderer position
				Vector3 B = jumpDirectionLine.GetComponent<LineRenderer>().GetPosition(0);//destination
				Vector3 A = jumpDirectionLine.GetComponent<LineRenderer>().GetPosition(1);//origin
				
				//moving the pointer using the touch input position
				Vector2 directionInput = (B - A).normalized;
				float inputAngle = Mathf.Atan2(-directionInput.x, directionInput.y) * Mathf.Rad2Deg;
				directionPointer.transform.eulerAngles = Vector3.forward * inputAngle;

				//setting time scale to the jumo
				timeScaleController.SetTimeScale(1 - ((Vector3.Distance(B, A)/4f) * 5f ));
				
				
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			if (jumpTouch)
			{
				//applying jump force using the line renderer direction
				Vector3 B = jumpDirectionLine.GetComponent<LineRenderer>().GetPosition(0);//destination
				Vector3 A = jumpDirectionLine.GetComponent<LineRenderer>().GetPosition(1);//origin
	
				Vector3 direction = (B - A).normalized;
				myController.Jump(direction);
				
				//reseting time scale to normal time
				timeScaleController.ResetTimeScale();
			}
			
			//reseting touch checkers
			moventTouch = false;
			jumpTouch = false;
			
			//reseting gameobject positions
			touchInput.transform.localPosition = Vector3.zero;
			jumpDirectionLine.transform.localPosition = Vector3.zero;
			
			//resetting jump line renderer position
			jumpDirectionLine.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
			jumpDirectionLine.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);

		}
	}
}
