using System;
using System.Collections.Generic;
using UnityEngine;

public class RayResults {
	public List<List<float>> rayDistances;
	public List<List<int>> rayHitObjectTypes;
	public int NumObjectTypes;
	public RayResults(
	