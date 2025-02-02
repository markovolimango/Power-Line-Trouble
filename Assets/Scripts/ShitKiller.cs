using DefaultNamespace;
using UnityEngine;

public class ShitKiller : MonoBehaviour
{
    public int shitLayer;
    private Health _carHealth;

    private void Start()
    {
        _carHealth = GameObject.FindGameObjectWithTag("Car").GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shit = collision.gameObject;
        if (shit.layer != shitLayer)
            return;

        _carHealth.Damage(shit.GetComponent<Shit>().damage);
        Destroy(shit);
    }
}