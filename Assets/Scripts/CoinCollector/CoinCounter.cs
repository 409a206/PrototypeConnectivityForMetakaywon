using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : Singleton<CoinCounter>
{   

    public int coinsCollected;
    public AudioClip snd_collect_coin;

    public CoinCounter() {

    }
}
