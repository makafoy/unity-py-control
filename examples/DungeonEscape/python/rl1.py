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


