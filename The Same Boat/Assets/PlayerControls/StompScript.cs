using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompScript : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] float juice;

    private void Start() {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
           StartCoroutine(KillThatBoi(other));
        }
    }

    private IEnumerator KillThatBoi(Collider2D enemy)
    {
        yield return new WaitForSeconds(1.5f);

         Destroy(enemy.gameObject);
    }
}
