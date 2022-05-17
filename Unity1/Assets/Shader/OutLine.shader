Shader "Study/OutLine"
{
    Properties
    {
        _OutlineColor("Outline color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline width", Range(0.0, 0.2)) = 0.05
        // Shader에서는 ;이 필요없다
    }
    SubShader   // 안쪽 부분은 접근할 수가 없다
    {
        Tags { "Queue" = "Transparent+1" "IgnoreProjector" = "True" }

        Pass    // 이게 렌더링 파이프라인 함수라고 생각하면 댐
        {
            ZWrite Off
            Cull Front

            CGPROGRAM
            #pragma vertex vert     // vertex 쉐이더
            #pragma fragment frag   // fixel 쉐이더

            //CGINCLUDE
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;   // float4 Vector4라고 생각하면 됨
                float3 normal : NORMAL;     // : 은 = 라고 생각하면 됨
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            uniform float _OutlineWidth;    // 이런식으로 재선언 해야됨
            uniform float _OutlineColor;

            

            v2f vert (appdata v)    // vertex 입력값이
            {
                v2f o;
                
                o.pos = UnityObjectToClipPos(v.vertex);

                // mul - multiply, float3x3 - 3x3 Matrix
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * o.pos.z * _OutlineWidth;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target // fixed의 출력값이 된다??
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
