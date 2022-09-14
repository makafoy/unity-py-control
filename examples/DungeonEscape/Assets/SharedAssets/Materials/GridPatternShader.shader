
﻿// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "ML-Agents/GridPattern" {
	Properties {
		_LineColor ("Line Color", Color) = (1,1,1,1)
		_CellColor ("Cell Color", Color) = (0,0,0,0)
		// _SelectedColor ("Selected Color", Color) = (1,0,0,1)
		[PerRendererData] _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[IntRange] _GridSize("Grid Size", Range(1,100)) = 10
		_LineSize("Line Size", Range(0,1)) = 0.15
		// [FloatRange] _LineOffset("Line Offset", Range(0,1)) = 0
		[IntRange] _DrawU("Draw U Toggle ( 0 = False , 1 = True )", Range(0,1)) = 1
		[IntRange] _DrawV("Draw V Toggle ( 0 = False , 1 = True )", Range(0,1)) = 1
		// [IntRange] _SelectCell("Select Cell Toggle ( 0 = False , 1 = True )", Range(0,1)) = 0.0
		// [IntRange] _SelectedCellX("Selected Cell X", Range(0,100)) = 0.0
		// [IntRange] _SelectedCellY("Selected Cell Y", Range(0,100)) = 0.0
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
		LOD 200
	

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness = 0.0;
		half _Metallic = 0.0;