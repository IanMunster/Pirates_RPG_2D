using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
    public float speed; //speed of the star
	public Vector2 size;
	private Vector2 starPosition;

	//private SpriteRenderer starSprite;
	//private float Alpha;

	// Use this for initialization
	void Awake () {
		//starSprite = SpriteRenderer.FindObjectOfType<SpriteRenderer> ();
		//Alpha = starSprite.color.a;
		starPosition.x = Random.Range (1, 100);
		starPosition.y = Random.Range (1, 100);
		GetaStar ();
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		// top right point of screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // Get the current position of the star
		Vector2 currPos = transform.position;
        // new position of the star
		starPosition = new Vector2(currPos.x, currPos.y + speed * Time.deltaTime);
        // update the position of the star
		transform.position = starPosition;
		//Change the starSize (could be multiplied by random
		transform.localScale = size*Random.Range(0.5f, 2f);
		//Change the star Alpha
		//starSprite.color = new Color(starSprite.color.r,starSprite.color.g,starSprite.color.b,Alpha/100);
		// bottom-left point of the screen
        // position the star randomly between the top left and right side of the screen
        // when the star goes outside the screen
		if(currPos.y < min.y || currPos.x < min.x || currPos.y > max.y|| currPos.x > max.x)
        {
			this.gameObject.SetActive (false);
			starPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.x, max.y));
        }
		GetaStar ();
    }
	// Set a Star in the Star Pool to active
	public void GetaStar(){
		GameObject star = StarGenerator.current.GetPooledStars();
		//unless it didnt get a star back
		if(star == null){
			return;
		} else{
			star.transform.position = starPosition;
			star.transform.rotation = this.transform.rotation;
			star.SetActive(true);
		}
	}
}
