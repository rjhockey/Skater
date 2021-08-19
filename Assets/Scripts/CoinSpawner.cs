using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public int maxCoin = 5;
    public float chanceToSpawn = 0.5f;
    public bool forceSpawnAll = false;

    private GameObject[] coins;

    private void Awake()
    {
        //grab all the children and place into coin array
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
        }

       // OnDisable();
    }

    private void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
            return; //doesn't meet criteria, don't spawn

        if (forceSpawnAll)
            for (int i = 0; i < maxCoin; i++)
                coins[i].SetActive(true);
        else
        {
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
            {
                coins[i].SetActive(true);
            }
        }    
    }

    private void OnDisable()
    {
        foreach (GameObject go in coins)
            go.SetActive(false);
    }
}
