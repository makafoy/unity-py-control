using System;
using System.Collections.Generic;
using UnityEngine;

public class RayResults {
	public List<List<float>> rayDistances;
	public List<List<int>> rayHitObjectTypes;
	public int NumObjectTypes;
	public RayResults(
	    List<List<float>> rayDistances,
	    List<List<int>> rayHitObjectTypes,
	    int NumObjectTypes
	) {
		this.rayDistances = rayDistances;
		this.rayHitObjectTypes = rayHitObjectTypes;
		this.NumObjectTypes = NumObjectTypes;
	}
}

public class RayCasts : MonoBehaviour {
	[Tooltip("Consider the rays form a low-resolution image. This is the x-resolution of that image.")]
	[Range(1, 100)]
	public int XResolution = 5;
	[Tooltip("Consider the rays form a low-resolution image. This is the y-resolution of that image.")]
	[Range(1, 100)]
	public int YResolution = 5;
	[Tooltip("What is the total horizontal angle subtended by the rays? (60 is good default)")]
	[Range(0, 360)]
	public int XTotalAngle = 60;
	[Range(0, 360)]
	[Tooltip("What is the total vertical angle subtended by the rays? (60 is good default)")]
	public int YTotalAngle = 60;
	[Tooltip("Length of each ray")]
	public int RayLength = 40;
	[Tooltip("Ray radius. 0 means RayCast, >0 is SphereCast")]
	public float RayRadius = 0;
	public bool ShowRaysInEditor = true;
	public bool ShowRaysInPlayer = false;
	[Tooltip("Mandatory field: list here the tags you want to detect. Each will be given it's own output feature plane.")]
	public List<string> DetectableTags = new List<string>();

	class Ray {
		public Vector3 direction;
		public Ray(Vector3 direction) {
			this.direction = direction;
		}
	}

	Vector3 RayDirection(int xIdx, int yIdx) {
		float x_angle = XResolution > 1 ? XTotalAngle / (XResolution - 1.0f) * xIdx - XTotalAn