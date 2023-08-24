using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public Text time, heart, status;
    private Rigidbody rb;
    public float speed = 1.5f;
    float timeCounter = 15;
    int heartCounter = 3;
    bool gameContinue = true;
    bool gameDone = false;
    // Start is called before the first frame update
    void Start()
    {
        heart.text = heartCounter + "";
        time.text = timeCounter + "";
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (gameContinue && !gameDone)
        {
			timeCounter -= Time.deltaTime;
			time.text = (int)timeCounter + "";
        }
        else if(!gameDone)
        {
            status.text = "Oyun Tamamlanamadi!";
            btn.gameObject.SetActive(true);
        }


		if (timeCounter < 0)
		{
			gameContinue = false;
		}
	}

    void FixedUpdate()
    {
        if (gameContinue && !gameDone)
        {
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			Vector3 vector3 = new Vector3(x, 0, y);
			rb.AddForce(vector3 * speed);
		}
        else
        {
            rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string obj = collision.gameObject.name;

        if(obj == "FinishPlane")
        {
			gameDone = true;
            status.text = "Oyun Tamamlandi Tebrikler!";
			btn.gameObject.SetActive(true);
		}
        else if(obj != "LabyrentPlane")
        {
            heartCounter -= 1;
            heart.text = heartCounter + "";
            if(heartCounter == 0)
            {
				gameContinue = false;
            }
        }
    }
}
