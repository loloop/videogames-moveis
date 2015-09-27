using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace VideogamesMoveis{
	public class MainBall : MonoBehaviour {
		
		float rotationTime = 0.3f;
		bool interactable = true;
		
		public static void CorrectBallCollision(){
			//increment score
			//fire particles
			//spawn new balls	
		}
		
		public static void WrongBallCollision(){
			//end game
			//boo player
		}
		
		void Update(){
			if(((Input.touchCount >= 1) || (Input.GetMouseButtonDown(0))) && (interactable)){
				RotateBall();
				StartCoroutine(SetGameNotInteractable());
			}
		}
		
		IEnumerator SetGameNotInteractable(){
			interactable = false;
			yield return new WaitForSeconds(rotationTime);
			interactable = true;
		}
		
		void RotateBall(){
			Vector3 newRotation = new Vector3(0, 0, 180);
			transform.DORotate(newRotation, rotationTime, RotateMode.LocalAxisAdd);	//just testing out okay???
		}
		
	}
}