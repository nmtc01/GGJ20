﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextScene;
    public Sprite unlocked;
    public Sprite locked;
    public Sprite open;
    public Sprite lightOn;
    public Sprite lightOff;
    private SpriteRenderer door;
    private SpriteRenderer player1Light;
    private SpriteRenderer player2Light;
    private bool player1, player2;

    public bool isLocked;
    private bool isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        this.door = this.gameObject.GetComponent<SpriteRenderer>();
        this.player1Light = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        this.player2Light = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        if(isLocked) door.sprite = locked;
        else door.sprite = unlocked;
        
        player1 = false;
        player2 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        
        if (collision.gameObject.CompareTag("Player1"))
        {
            player1 = true;
            this.player1Light.sprite = lightOn;
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            this.player2Light.sprite = lightOn;
            player2 = true;
        }

        if(player1 && player2 && !isLocked){
            isOpen = true;
            this.door.sprite = open;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            this.player1Light.sprite = lightOff;
            player1 = false;
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            this.player2Light.sprite = lightOff;
            player2 = false;
        }
        
        isOpen = false;
        if(this.isLocked) 
            this.door.sprite = locked;
        else 
            this.door.sprite = unlocked;
    }

    public void UnlockDoor(){
        isLocked = false;
        this.door.sprite = unlocked;
    }

    public void LockDoor(){
        isLocked = true;
        this.door.sprite = locked;
    }

    public void Enter(){
        if(this.isOpen) {
            SceneManager.LoadScene(nextScene);
        }
    }
}
