using UnityEngine;

public class Coins : MonoBehaviour
{
    int coins = 0;

    void Update()
    {
        if (coins == 8)
        {
            GameObject.Find("Plat (16)").transform.localScale = new Vector3 (4, 1, 1);
            GameObject.Find("Instruction").GetComponent<TextMesh>().text = "Well done!";
            GameObject.Find("TextPumpkin").SetActive(false);
            coins++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
        }
    }
}
