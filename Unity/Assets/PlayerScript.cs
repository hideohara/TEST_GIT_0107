using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    private bool isBlock = true;
    private AudioSource audioSource;
    public GameObject bombParticle;
    // �A�j���[�^�[�R���g���[���[
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
        // �Q�[���N���A�ňړ����Ȃ�
        if (GoalScript.isGameClear == true)
        {
            return;
        }





    }

    void Update()
    {
        // �Q�[���N���A�ňړ����Ȃ�
        if (GoalScript.isGameClear == true)
        {
            return;
        }

        // �v���C���[�̉������փ��C���o��
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.8f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);

        float distance = 0.9f;
        isBlock = Physics.Raycast(ray, distance);

        //���C�̕\��
        if (isBlock == true)
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
        }

        // �W�����v�A�j���[�V�����؂�ւ�
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

        // �ړ��ʂ��l��
        Vector3 v = rb.velocity;

        // �E�ֈړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x = moveSpeed;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("walk", true);
        }
        // ���ֈړ�
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -moveSpeed;
            transform.rotation = Quaternion.Euler(0, -90, 0);
            animator.SetBool("walk", true);

        }
        // ������Ȃ���Β�~
        else
        {
            v.x = 0;
            //animator.SetBool("walk", false);

        }



        // �ړ��ʂ�ݒ�
        rb.velocity = v;



        // �ړ��ʂ��l��
        //Vector3 v = rb.velocity;

        // �W�����v
        // ���n���Ă��鎞�̂�
        if (isBlock == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                v.y = jumpSpeed;
            }
        }

        // �ړ��ʂ�ݒ�
        rb.velocity = v;
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "COIN")
        {
            other.gameObject.SetActive(false);
            audioSource.Play();
            GameManagerScript.score += 1;

            // �����p�[�e�B�N������
            Instantiate(bombParticle, transform.position, Quaternion.identity);
        }
    }


}
