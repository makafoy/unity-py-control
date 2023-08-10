import argparse
from functools import partial
from os import path
from typing import Optional, Union

import mlflow_logging
import models
import torch
from gym import Env
from my_unity_env import MyUnityEnv
from sbs3_checkpoint_callback import SBS3CheckpointCallback
from stable_baselines3 import PPO
from stable_baselines3.common.monitor import Monitor
from stable_baselines3.common.policies import ActorCriticPolicy
from stable_baselines3.common.vec_env import SubprocVecEnv, VecEnv, VecMonitor

from peaceful_pie.unity_comms import UnityComms


def dump_params_counts(net: torch.nn.Module) -> int:
    total_params = 0
    for key, param in net.named_parameters():
        print(key, param.numel())
        total_params += param.numel()
    print("total_params", total_params)
    return total_params


def run(args: argparse.Namespace) -> None:
    class EnvFactory:
        def __init__(self, port: int, server_executable_path: Optional[str]):
            # I think this gets pickled into a new process
            self.port = port
            self.server_executable_path = server_executable_path
            self.unity_comms = UnityComms(
                port=self.port, server_executable_path=self.server_executable_path
            )

        def __call__(self) -> MyUnityEnv:
            # I think this bit 