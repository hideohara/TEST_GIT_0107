using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    private bool isBlock = true;
    private AudioSource audioSource;
    public GameObject bombParticle;
    // アニメーターコントローラー
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ゲームクリアで移動しない
        if (GoalScript.isGameClear == true)
        {
            return;
        }





    }

    void Update()
    {
        // ゲームクリアで移動しない
        if (GoalScript.isGameClear == true)
        {
            return;
        }

        // プレイヤーの下方向へレイを出す
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.8f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);

        float distance = 0.9f;
        isBlock = Physics.Raycast(ray, distance);

        //レイの表示
        if (isBlock == true)
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
        }

        // ジャンプアニメーション切り替え
        if (isBlock == true)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }


        float jumpSpeed = 8.0f;
        float moveSpeed = 4.0f;

        // 移動量を獲得
        Vector3 v = rb.velocity;

        // 右へ移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x = moveSpeed;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("walk", true);
        }
        // 左へ移動
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -moveSpeed;
            transform.rotation = Quaternion.Euler(0, -90, 0);
            animator.SetBool("walk", true);

        }
        // 押されなければ停止
        else
        {
            v.x = 0;
            //animator.SetBool("walk", false);

        }



        // 移動量を設定
        rb.velocity = v;



        // 移動量を獲得
        //Vector3 v = rb.velocity;

        // ジャンプ
        // 着地している時のみ
        if (isBlock == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                v.y = jumpSpeed;
            }
        }

        // 移動量を設定
        rb.velocity = v;
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "COIN")
        {
            other.gameObject.SetActive(false);
            audioSource.Play();
            GameManagerScript.score += 1;

            // 爆発パーティクル発生
            Instantiate(bombParticle, transform.position, Quaternion.identity);
        }
    }


}
