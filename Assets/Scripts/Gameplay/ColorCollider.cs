using UnityEngine;
using System.Collections;

namespace VideogamesMoveis{
	public class ColorCollider : MonoBehaviour {
		
		public bool isGreenCollider;
		MainBall parent;
		
		void Start(){
			parent = transform.parent.GetComponent<MainBall>();
		}
		
		void OnTriggerEnter2D(Collider2D collider){
			//check the collision guy and fire the adequate method
			bool isColliderGreen = collider.GetComponent<ColorBall>().isGreen;
			if(isGreenCollider && isColliderGreen){
				GameStateManager.sharedInstance.ScoreUp();
				StartCoroutine(parent.FireParticles());
			} else if (isGreenCollider && !isColliderGreen) {
				GameStateManager.sharedInstance.GameFail();
			}
		}
		
		
	}
}
