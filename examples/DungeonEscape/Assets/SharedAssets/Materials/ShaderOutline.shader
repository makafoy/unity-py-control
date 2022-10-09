// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline and ScreenSpace texture"
{
	Properties
	{
		[Header(Outline)]
		_OutlineVal ("Outline value", Range(0., 2.)) = 1.
		_OutlineCol ("Outline color", color) = (1., 1., 1., 1.)
		[Header(Texture)]
		_MainTex ("Texture", 2D) = "white" {}
		_Zoom ("Zoom", Range(0.5, 20)) = 1
		_SpeedX ("Speed along X", Range(-1, 1)) = 0
		_SpeedY ("Speed along Y", Range(-1, 1)) = 0
	}
	SubShader
	{
		Tags { "Queue"="Geometry" "RenderType"="Opaque" }
		
		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
			};

			float _OutlineVal;

			v2f vert(appdata_base v) {
				v2f o;

				// Convert vertex to clip space
				o.pos = UnityObjectToClipPos(v.vertex);

				// Convert normal to view space (camera space)
				float3 normal = mul((float3x3) UNITY_MATRIX_IT_MV, v.normal);
