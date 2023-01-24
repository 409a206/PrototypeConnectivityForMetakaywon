using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControls : MonoBehaviour
{
    public GameObject minimapCover;
    public GameObject minimapRT;
    private RectTransform minimapRectTransform;

    //맵 카메라
    public Camera mapCamera;
    

    //미니맵 크기
    private float minimapWidth;
    private float minimapHeight;

    //미니맵 위치
    private float minimapPosX;
    private float minimapPosY;

    //큰 맵 위치
    [SerializeField]
    private float mapPosX;
    [SerializeField]
    private float mapPosY;

    //큰 맵 크기
    [SerializeField]
    private float mapWidth;
    [SerializeField]
    private float mapHeight;

    //미니맵 배율
    [SerializeField]
    private float minimapOrthographicCameraSize;
    //큰 맵 배율
    [SerializeField]
    private float mapOrthographicCameraSize;

    //lerp 관련 변수
    [SerializeField]
    private float lerpTime;
    private float currentTime = 0;
    private float normalizedValue;

    private float appliedMapWidth;
    private float appliedMapHeight;
    private float appliedCameraSize;
    private float appliedmapPosX;
    private float appliedmapPosY;

    //확대됐는지 확인하는 플래그 변수
    private bool isExtended = false;

    private void Start() {
        minimapRectTransform = minimapCover.GetComponent<RectTransform>();


        minimapWidth = minimapRectTransform.rect.width;
        minimapHeight = minimapRectTransform.rect.height;
        minimapPosX = minimapRectTransform.localPosition.x;
        minimapPosY = minimapRectTransform.localPosition.y;

        Debug.Log(minimapPosX);

        mapCamera.orthographicSize = minimapOrthographicCameraSize;

    }
    
    // Update is called once per frame
    void Update()
    {
       SizeShiftToggle();
       //Debug.Log(Mathf.Lerp(1, 2, lerpTime * Time.deltaTime));
    }

    void SizeShiftToggle() {
         //M키로 커졌다 작아졌다 로직
        	if (Input.GetKeyDown (KeyCode.M)){
                StartCoroutine(MapToggleCoroutine(isExtended));
                 isExtended = !isExtended;
            }
    }

    IEnumerator MapToggleCoroutine(bool isExtended)
    {
        //작은 상태라면
        if(!isExtended) {
            
            //커지기 로직
            currentTime = 0;
            while(currentTime <= lerpTime) {
                currentTime += Time.deltaTime;
                normalizedValue = currentTime/lerpTime;
                float t = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f);
                //맵 사이즈 설정
                        minimapRT.GetComponent<RectTransform>().sizeDelta = new Vector2(
                            Mathf.Lerp(minimapWidth, mapWidth, t), 
                            Mathf.Lerp(minimapHeight, mapHeight, t));
 
                        minimapCover.GetComponent<RectTransform>().sizeDelta =  new Vector2(
                            Mathf.Lerp(minimapWidth, mapWidth, t),
                             Mathf.Lerp(minimapHeight, mapHeight, t));

                        //맵 위치 조정
                        //lerp로 조정
                        minimapRectTransform.localPosition = Vector2.Lerp(new Vector2(minimapPosX, minimapPosY),
                                                                           new Vector2(mapPosX, mapPosY),t);
                       
                        //카메라 배율 설정
                        mapCamera.orthographicSize = Mathf.Lerp(minimapOrthographicCameraSize,
                        mapOrthographicCameraSize, t);
 
                        yield return null;              
            }
        } 

        //큰 상태라면
        else {
           
            currentTime = 0;
            Debug.Log("else절 들어옴");
            while(currentTime <= lerpTime) {
                currentTime += Time.deltaTime;
                normalizedValue = currentTime/lerpTime;
                float t = Mathf.Sin(normalizedValue * Mathf.PI * 0.5f);
                //작아지기 로직
                //lerp 추가
                minimapRT.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Lerp(mapWidth, minimapWidth, t), 
                    Mathf.Lerp(mapHeight, minimapHeight, t)) ;
                minimapCover.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    Mathf.Lerp(mapWidth, minimapWidth, t), 
                    Mathf.Lerp(mapHeight, minimapHeight, t));

                //위치 설정
                minimapRectTransform.localPosition = Vector2.Lerp(new Vector2(mapPosX, mapPosY),
                                                                   new Vector2(minimapPosX, minimapPosY), t);
                
                //orthographic camera size (=zoom 배율 설정)
                mapCamera.orthographicSize = Mathf.Lerp(mapOrthographicCameraSize,
                minimapOrthographicCameraSize, t);
                Debug.Log("작아지기 실행");
                yield return null;
            }
        }
        
        
        Debug.Log(isExtended);
        yield return null;
       
    }
}
