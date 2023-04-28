using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEditor;

public class BallMovement : MonoBehaviour
{
    public float speed = 1.3f;
	public Transform target;
	private int count = 0;
	private int point = 0;

    // Start is called before the first frame update
    void Start(){
        GetComponent<Rigidbody2D>().velocity =
            Vector2.right * speed;
    }

	void OnGUI()
    {
        GUI.Label(new Rect(450, 20, 3000, 600), "Pontuação: " + this.point);
    }

	void Update(){
		if(this.count == 0){
			var newColor = new Color(0, 0, 0);
			Camera.main.backgroundColor = newColor;
		}else if(this.count == 1){
			var newColor = new Color(1, 1, 1);
			Camera.main.backgroundColor = newColor;
			this.count++;
		}else{
			Thread.Sleep(400);
			this.count = 0;
			transform.position = new Vector3(0, 0);
			target.position = new Vector3(target.position.x, 0);
		}
	}

    float hitFactor(Vector2 bolaPos, Vector2 jogPos, float tam){
			
	    return (bolaPos.y - jogPos.y)/tam;
    }

    void OnCollisionEnter2D(Collision2D colisao){
				if (colisao.gameObject.name == "raqueteL"){
						float y = hitFactor(transform.position, colisao.transform.position, colisao.collider.bounds.size.y);

				Vector2 dir = new Vector2(1,y).normalized;
			
				GetComponent<Rigidbody2D>().velocity = dir*speed;
				}
				if (colisao.gameObject.name == "paredeDir"){
					this.count = 1;
					this.point ++;
				}else if(colisao.gameObject.name == "paredeEsq"){
					this.count = 1;
					this.point --;
				}

				if (colisao.gameObject.name == "raqueteR"){
						float y = hitFactor(transform.position, colisao.transform.position, colisao.collider.bounds.size.y);


				Vector2 dir = new Vector2(-1,y).normalized;
			
				GetComponent<Rigidbody2D>().velocity = dir*speed;
				}


		}
}
