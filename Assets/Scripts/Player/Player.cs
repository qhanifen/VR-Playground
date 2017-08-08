using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player> {
    

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                GameOver();
            }
        }
    }

    int _health = 100;
    
    // Use this for initialization
	void Start () {
		
	}

    void GameOver()
    {
        PlayerHud.instance.GameOver();
    }
}
