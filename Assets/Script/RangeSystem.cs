using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidDelVoid();
//public delegate bool BollDelvoid();

public class RangeSystem : MonoBehaviour
{
    //public string Enemy = "Player";
    //public event VoidDelVoid DelAction; 
    // event 외부에서 실행x 
    public VoidDelVoid battle;  // battle는 함수의 주소를 받을수있다
    public Transform Target = null;


    void Start()
    {
        //DelAction?.Invoke();    // 외부에서 Invoke(실행)이 안됨
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Target = other.transform;
            //GetComponent<Monster>().OnChangeState(Monster.STATE.BATTLE);
            battle?.Invoke();    // battle이 있는지 없는지 검사한다
            // if(battle != null)
        }

        
        //if(LayerMask.LayerToName(other.gameObject.layer) == "Player")  
        //    // layer는 Integer값으로 받아서 ToName으로 String형으로 바꿔서 받았다(정수로 받아도댐)
        //{
        //  게임이 복잡해지면 태그와 레이어 둘다 구분해야 할일이 생긴다고함
        //}

    }
}
