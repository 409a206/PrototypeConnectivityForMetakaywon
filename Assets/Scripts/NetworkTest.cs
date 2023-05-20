using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UnityWebRequestGet());
    }

    IEnumerator UnityWebRequestGet() {

        string sellerProductId = "73245642797";

        string url = $"/v2/providers/seller_api/apis/api/v1/marketplace/seller-products/{sellerProductId}/partial";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if(www.error == null) {
            Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log("ERROR");
        }
    }

}
