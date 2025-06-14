using Bardent.Weapons;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [field: SerializeField]
    public QuestSO InventoryQuest { get; private set; }

    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    //[SerializeField]
    //private AudioSource audioSource;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryQuest.Icon;
    }

    public void DestroyQuest()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateQuestPickup());

    }

    private IEnumerator AnimateQuestPickup()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}