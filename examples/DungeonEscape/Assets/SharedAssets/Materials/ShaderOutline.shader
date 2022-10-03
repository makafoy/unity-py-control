// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline and ScreenSpace texture"
{
	Properties
	{
		[Header(Outline)]
		_OutlineVal ("Outline value", Range(0., 2