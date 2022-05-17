using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class study
{
/*
    Navigation
Agent Radius - Object에 네비 폭을 설정한다

항상 설정을 변경하면 Bake를 해서 네비게이션도 변경해준다

Object에 NavMesh Obstacle Component를 붙여주면 동적인 장애물이 된다
(단 실시간으로 연산이 되어 무거움)

Nav Mesh Agent - 네비 Componnet
설정 한번보기
(Open Mesh 라는걸로 길이 없는 곳에 포탈같이 이동 되는것도 있다)

인터페이스(항상 public로)는 다중상속 가능하고 다른 스크립트에서 쓰려면 다시 재정의 해야한다

네비게이션(길이 끊어진 곳을 이동할 수 있게 해주는 방법)

1. Object에 Generate OffMeshLinks를 양쪽 Wall에 체크하면 서로 이동 가능
한쪽만 체크되면 그 방향으로만 이동 가능
Bake에 Jump Distance를 올려줘야 띄어있는 거리도 이동 가능하다

2. Off Mesh Link(Component)
Bi Directional - 양쪽 방향으로 이동 가능
Activated - 사용 가능 여부
Navigation Area - Areas의 각 요소들의 Cost를 다르게 할 수 있다

3. NavMeshLink(Componnet)
위와 비슷하지만 범위로 설정이 가능하고(범위가 중요 - Width)
따로 검색해서 다운 받아서 Asset에 넣어야한다

vertex 버퍼
index 버퍼  찾아보기
폴리곤(면을 만들기 위해 최소 3개의 점이 필요한데 그 단위가 폴리곤)









*/
}