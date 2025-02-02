using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhp;
    public List<Sprite> sprites;
    public int hpToSpriteChange;
    private int _currentSpriteIdx;
    private int _hp;
    private SpriteRenderer _spriteRenderer;
    //public RedCornersShaderController redCorners;
    
    
    private void Start()
    {
        _hp = maxhp;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[0];
        _currentSpriteIdx = 0;
        //redCorners = FindFirstObjectByType<RedCornersShaderController>();
        //redCorners.PlayPulseEffect( 3f);
    }

    public void Damage(int amount)
    {
        _hp -= amount;
        if (_hp <= maxhp-(_currentSpriteIdx+1)*hpToSpriteChange && _currentSpriteIdx < sprites.Count-1)
        {
            _currentSpriteIdx++;
            _spriteRenderer.sprite = sprites[_currentSpriteIdx];
        }

        if (_hp <= 0) GameOver();
    }
    
    private void GameOver()
    { 
        //Destroy(gameObject);
    }
    
    public void Heal(int amount)
    {
        _hp = Math.Min(_hp + amount, maxhp);
        transform.localScale = Vector3.one * ((float)_hp / maxhp);
    }
}