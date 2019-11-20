using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    private float direction;

    private AnimationManager animationManager;
    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<AnimationManager>();

        direction = Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (direction > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

           
                animationManager.startWalking();


            GetComponent<Transform>().Translate(GetComponent<Transform>().right * 10.05f * Time.deltaTime * 1.0f * 1.0f);
        }

        else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

            
                animationManager.startWalking();

            GetComponent<Transform>().Translate(GetComponent<Transform>().right * (-10.05f) * Time.deltaTime * 1.0f * 1.0f);
        }



        if (direction == 0)
        {
                animationManager.setIdle();
        }
    }
}
