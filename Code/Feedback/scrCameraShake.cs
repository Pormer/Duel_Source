using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class scrCameraShake : MonoBehaviour
{
    public bool isShake = false;
    Vector3 cameraPos;
    [SerializeField]
    [Range(0.01f, 0.5f)]
    float shakeRange = 0.05f;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float duration = 0.3f;
    float ShakeDelay = 0;
     
    public void Shake()
    {
        isShake = true;
        ShakeDelay = duration;
        cameraPos = transform.position;
        if (gameObject.activeInHierarchy)
        { StartCoroutine(this.Shaking()); }
    }
    IEnumerator Shaking()
    {
        while (ShakeDelay>0)
        {
            ShakeDelay -= 0.01f;
            StartShake();
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        isShake = false;
        transform.position = cameraPos;
    }
    void StartShake()
    { 
        float cameraPosX = Random.Range(-1f,1f) * shakeRange;
        float cameraPosY = Random.Range(-1f, 1f) * shakeRange;
        Vector3 cameraPos = transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        transform.position = cameraPos;
    } 
     
}