using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhp;
    private int _hp;

    private void Start()
    {
        _hp = maxhp;
    }

    public void Damage(int amount)
    {
        _hp -= amount;
        if (_hp <= 0) transform.localScale *= 20;
        else transform.localScale = Vector3.one * ((float)_hp / maxhp);
    }

    public void Heal(int amount)
    {
        _hp = Math.Min(_hp + amount, maxhp);
        transform.localScale = Vector3.one * ((float)_hp / maxhp);
    }
}