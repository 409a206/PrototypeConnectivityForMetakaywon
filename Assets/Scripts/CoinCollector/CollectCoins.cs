using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CollectCoins : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI CoinCollectorText;

    [SerializeField]
    private CoinCounter coinCounter;

    private void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            coinCounter.coinsCollected += 1;
            CoinCollectorText.text = "Coins Collected = " + coinCounter.coinsCollected;
            // = "Coins Collected = ";

            coinCounter.gameObject.GetComponent<AudioSource>().PlayOneShot(coinCounter.snd_collect_coin);
            Destroy(this.gameObject);
        }
    }
}
