using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Transform m_transform;
    private Rigidbody rigidbody;
    private Animator ani;

    //移动速度与旋转速度 
    private float transloatSpeed=30f;   
    private float rotateSpeed=15;

    private Vector3 targetPos;
    private float Blend;
	//获取前面添加的组件
    private void Start()
    {
        m_transform = this.transform;
        rigidbody = this.GetComponent<Rigidbody>();
        ani = this.GetComponent<Animator>();
    }  
    private void Update()
    {
        //引入动画参数
        ani.SetFloat("Blend", Blend);
        
        //通过监控用户输入，判定角色移动向量，并在FixUpdate中控制角色移动到目标位置
        Vector3 v3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        targetPos = v3 * Time.deltaTime * transloatSpeed * Blend;

        //通过角色移动目标向量与角色自身的正方向插值决定旋转角度，并使用rotation进行旋转
        Vector3 targetDir = Vector3.Slerp(m_transform.forward, v3, rotateSpeed * Time.deltaTime);
        m_transform.rotation = Quaternion.LookRotation(targetDir);

        //监控用户输入，控制动画参数
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {            
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Blend = Mathf.Lerp(Blend, 1f, 0.03f);
                return;
            }
            Blend = Mathf.Lerp(Blend, 0.5f, 0.03f);
        }
        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            Blend = Mathf.Lerp(Blend, 0, 0.03f);
        }

        //添加交互操作
        if (!(Input.GetKey(KeyCode.E) ))
        {
         
        }
    }
    private void FixedUpdate()
    {
        //通过目标向量与自身位置控制角色移动
        rigidbody.MovePosition(targetPos + m_transform.position);
    }
}
