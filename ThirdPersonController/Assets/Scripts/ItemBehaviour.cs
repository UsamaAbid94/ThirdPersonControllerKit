using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private GameBehaviour gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(this.transform.parent.gameObject);
            gameManager.Items += 1;
            gameManager.PrintLootReport();
        }
            
    }
}
