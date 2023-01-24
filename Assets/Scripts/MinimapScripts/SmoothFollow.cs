using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    
    public Transform Target;
    public float speed = 2.0f;
    
    void Update () {
       FollowPlayerPosition();
       FollowPlayerRotation();
    }

    public void FollowPlayerPosition() {
        float Interpolation = speed * Time.deltaTime;
        Vector3 position = transform.position;

        position.x = Mathf.Lerp(transform.position.x, Target.position.x, Interpolation);
        //position.y = Mathf.Lerp(transform.position.y, Target.position.y, Interpolation);
        position.z = Mathf.Lerp(transform.position.z, Target.position.z, Interpolation);
        
        transform.position = position;
    }

    public void FollowPlayerRotation() {
        transform.rotation = Quaternion.Euler(90f, Target.eulerAngles.y, 0f);
    }

}