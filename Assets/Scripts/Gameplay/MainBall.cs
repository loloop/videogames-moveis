using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace VideogamesMoveis{
	public class MainBall : MonoBehaviour {
		
		float rotationTime = 0.3f;
		bool interactable = true;
		
		public Transform[] greenParticles, yellowParticles;
		
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
			transform.DORotate(newRotation, rotationTime, RotateMode.LocalAxisAdd);
		}
		
		public IEnumerator FireParticles(){
			//store original position and color
			Vector3[] greenOriginalTransforms = new Vector3[greenParticles.Length];
			Transform[] yellowOriginalTransforms = new Transform[yellowParticles.Length];
			Color originalColor = new Color(1,1,1,1);
			
			//animate
			for(int i = 0; i < greenParticles.Length; i++){
				greenOriginalTransforms[i] = greenParticles[i].localPosition;
				greenParticles[i].rotation = Quaternion.Euler(0,0, greenParticles[i].rotation.z + i * 20);
				greenParticles[i].DOLocalMove(new Vector3(0, 2, 0), rotationTime);
				
				//greenParticles[i].GetComponent<SpriteRenderer>().DOFade(0, rotationTime);
			}	
			
			yield return new WaitForSeconds(rotationTime);
			
			//reset everyone to original position
			for(int i = 0; i < greenParticles.Length; i++){
				greenParticles[i].position = greenOriginalTransforms[i];
				greenParticles[i].GetComponent<SpriteRenderer>().color = originalColor;		  
			}
			
			/*
			for(int i = 0; i < yellowParticles.Length; i++){
				yellowParticles[i] = yellowOriginalTransforms[i];
				yellowParticles[i].GetComponent<SpriteRenderer>().color = originalColor;	
			}
			*/
			
		}
		
	}
}