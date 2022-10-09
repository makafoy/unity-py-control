Shader "Custom/WireFrame" {
	Properties {
		_LineColor ("LineColor", Color) = (1, 1, 1, 1)
		_MainColor ("_MainColor", Color) = (1, 1, 1, 1)
		_LineWidth ("Line width", Range(0, 1)) = 0.1
		_ParcelSize ("ParcelSize", Range(0, 100)) = 1
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		float4 _LineColor;
		float4 _MainColor;
		fixed _LineWidth;
		float _ParcelSize;

		struct Input {
			f