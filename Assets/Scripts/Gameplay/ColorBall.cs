using UnityEngine;
using System.Collections;

namespace VideogamesMoveis{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ColorBall : MonoBehaviour {
	
		Rigidbody2D myRigidbody;
		float ballVelocity, oldTime; 
		
		public bool isGoingUp, isGreen;
		public TrailRenderer trail;
	
		void Start(){
			trail = transform.GetChild(0).GetComponent<TrailRenderer>();
			myRigidbody = GetComponent<Rigidbody2D>();
			myRigidbody.isKinematic = true;
			oldTime = trail.time;
		}
		
		void Update(){
		ballVelocity = GameStateManager.sharedInstance.ballVelocity;
			if(isGoingUp){
				transform.Translate(Vector3.up * ballVelocity * Time.deltaTime);
			} else {
				transform.Translate(Vector3.down * ballVelocity * Time.deltaTime);
			}
		}
		
		public IEnumerator TrailRenderDestroy(){
			trail.time = 0;
			yield return new WaitForEndOfFrame();
			trail.time = oldTime;
		}
		
	}
}
