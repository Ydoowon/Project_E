﻿Shader "Shaderboy/Invisible Shader v2"
{
	Properties
	{
	_Multiply("Multiply Invisible", Float) = 1
	}
	SubShader
	{
		Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
		Cull Off ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			uniform float _Multiply;

			fixed4 frag (v2f i) : SV_Target
			{
				return 0 * _Multiply;
			}
			ENDCG
		}
	}
}
