from dataclasses import dataclass
from typing import Any, Dict, List, Tuple

import gym
import gym.spaces
import numpy as np
from numpy.typing import NDArray

from peaceful_pie import ray_results_helper, unity_comms


@dataclass
class PlayerObservation:
    IAmAlive: bool
    IHaveAKey: bool
    rayResults: ray_results_helper.RayResults


@dataclass
class RLResult:
    reward: float
    episodeFinished: bool
    playerObservations: List[PlayerObservation]


class MyUnityEnv(gym.Env):
    def __init__(
        self,
        comms: unity_comms.UnityComms,
    ):
        self.comms = comms

        self.action_space = gym.spaces.MultiDiscrete(
            [4] * 3
        )  # turn left/right look up/down forward
        obs = self.reset()
        print("obs.shape", obs.shape)
        self.observation_space = gym.spaces.Box(
            low=0, high=1, shape=obs.shape, dtype=np.float32
        )

    def r