Shader "Study/VertexColor"
{
    Properties
    {
        _Alpha("Color Alpha", Range(0.0, 1.0)) = 0.3

    }
        // alpha값 프로퍼티


        SubShader
    {
        Tags{"Queue" = "Transparent"}


        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;  //SV는 screen view의 약자
                float3 color :  COLOR;
            };

            uniform float _Alpha;  // 프로퍼티 설정 이후에 같은 이름으로 선언
            //Vertex Shader
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }
            //Pixel Shader
            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(i.color,_Alpha);
            }
            ENDCG
        }
    }
}
