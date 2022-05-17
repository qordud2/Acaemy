using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class의 위쪽에 AddComponentMenu하면 컴포넌트가 생긴다
[AddComponentMenu("Test/StandardEditerEx")]
// StandardEditerEx라는 컴포넌트를 붙이면 BoxCollider가 같이 붙어서 나오고 붙어서 나온건 제거가 불가능하다
// 여러개 한번에 붙일때 필요할듯??
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AnimEvent))]
public class StandardEditerEx : MonoBehaviour
{
    // 인스펙터 창에서 매개변수 숫자 픽셀만큼 띄어진다 
    [Space(100)]
    // 인스펙터 위에 설명을 넣어줄 수 있다.
    [Header("This is my inspecotr.")]   
    // region 구역을 정해서 에디터 상에서 접을 수 있다
    #region INSPECTOR   
    // SerializeField private긴한데 인스펙터 창에서 ReadAndWrite를 할 수 있다
    // 데이터는 은폐시키고 수정하면서 작업 가능
    // public나 protected이런거 쓰려면 변수 자료형 앞에 쓰면 된다
    //[SerializeField]
    //int Value = 0;
    // Range 인스펙터 창에서 슬라이더처럼 표현 할 수 있다.
    // Tooltip - 마우스 올리면 설명이 나오게 가능
    [SerializeField, Range(0, 100), Tooltip("This is model")]
    // ContextMenuItem 인스펙터 창에서 우클릭하면 Create Random라는 메뉴가 생기고 클릭하면 OnRandom이 실행된다
    [ContextMenuItem("Create Random", "OnRandom")]
    int Range = 0;

    // 여러줄의 문자열을 입력할 수 있다
    //[TextArea(3,5)]
    // 스크롤은 안되지만 익스펙터 창에서 옆으로 적을 수 있다
    [Multiline(5)]
    public string myText;
    // first를 false로 하면 a값이 없는 컬러값, 
    // first를 true로 하면 a값이 있는 컬러값을 받는다, second를 true를 하면 HRD칼라값을(더 선명한)받을 수 있게한다
    [ColorUsage(true, true)]
    public Color myColor;
    #endregion

    // 인자값으로 함수 이름을 넣고 컴포넌트를 우클릭 하면 인자값에 있는 함수를 실행시킬 수 있다
    [ContextMenu("OnRandom")]

    void OnRandom()
    {
        Range = Random.Range(0, 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
