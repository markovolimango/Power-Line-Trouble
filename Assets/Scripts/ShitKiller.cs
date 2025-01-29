using UnityEngine;

public class ShitKiller : MonoBehaviour
{
    public int shitLayer;
    private GameObject _car;

    private void Start()
    {
        _car = GameObject.FindGameObjectWithTag("Car");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shit = collision.gameObject;
        if (shit.layer != shitLayer)
            return;

        _car.transform.localScale *= 0.9f;
        Destroy(shit);
    }
}