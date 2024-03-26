from dataclasses import dataclass
from typing import List

import numpy as np
from numpy.typing import NDArray


@dataclass
class RayResults:
    rayDistances: List[List[float]]
    rayHitObjectTypes: List[List[int]]
    NumObjectTypes: int


def ray_results_to_feature_np(
    ray_results: RayResults,
) -> NDArray[np.float32]:
    """
    Takes in the ray results, i.e. hit distances and object types,
    and return a single numpy array, of same shape as each of distances and object types,
    but with an additional 0th dimension of length object_types

    Conceptually, if a num