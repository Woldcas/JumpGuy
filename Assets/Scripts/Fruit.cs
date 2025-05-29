using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fruitType {Apple,Banana,Cherry,Kiwi,Melon,Orange,Pineapple,Strawberry}


public class Fruit : MonoBehaviour
{
    [SerializeField] private fruitType fruitType;
    [SerializeField] private GameObject pickupVfx;

    private GameManager gameManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        SetRandomLookIfNeeded();
    }

    private void SetRandomLookIfNeeded()
    {
        if (gameManager.FruitsHaveRandomLook() == false)
        {
            UpdateFruitVisuals();
            return;
        }


        int randomIndex = Random.Range(0, 8); // max value is exclusive, so it will give a number from 0 to 7.
        anim.SetFloat("fruitIndex", randomIndex);
    }

    private void UpdateFruitVisuals() => anim.SetFloat("fruitIndex", (int)fruitType);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            gameManager.AddFruit();
            AudioManager.instance.PlaySFX(8);
            Destroy(gameObject);

            GameObject newFX = Instantiate(pickupVfx, transform.position, Quaternion.identity);
        }

    }
}
