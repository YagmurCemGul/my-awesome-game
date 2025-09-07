using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float speed = 1.0f; 
    private Rigidbody2D r2d;
    private Animator _animator;
    private Vector3 charpos;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _camera;
    private int sayi;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // caching spriteRenderer
        r2d = GetComponent<Rigidbody2D>(); //caching r2d
        _animator = GetComponent<Animator>(); // caching anim
        charpos = transform.position;
        sayi = 1;

    }

    private void FixedUpdate() // 50 fps
    {
        //Debug.Log("Fixed" + sayi);
        //r2d.velocity = new Vector2(speed, 0f);
        sayi = 2;
    }

    void Update() // per frame ve gerçek fizik hesaplamalarını update kısmında yaptırmıyoruz. Buradan önce fizik hesaplaması yapılır.
    {
        //Debug.Log("Update" + sayi);
        /*if (Input.GetKey(KeyCode.Space))
        {
            speed = 1.0f;
            //Debug.Log(message: "Hız 1.0f");
        }
        else
        {
            speed = 0.0f;
            //Debug.Log(message: "Hız 0.0f");
        }*/
        charpos = new Vector3(charpos.x + (Input.GetAxis("Horizontal") * speed * Time.deltaTime), charpos.y);
        transform.position = charpos; //hesapladığım pozisyon karakterime işlensin
        if (Input.GetAxis("Horizontal") == 0.0f)
        {
            _animator.SetFloat("speed", 0.0f);
        }
        else
        {
            _animator.SetFloat("speed", 1.0f);
        }

        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }else if (Input.GetAxis("Horizontal") < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }
       
        
        
        sayi = 3;
        Debug.Log("Update" + sayi);
    }

    private void LateUpdate()
    {
        //_camera.transform.position = new Vector3(charpos.x,charpos.y,charpos.z -1.0f);
        sayi = 4;
    }
}
