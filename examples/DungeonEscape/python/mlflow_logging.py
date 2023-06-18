import argparse
import os
import socket
import subprocess
import sys
from typing import Any, Dict, Tuple, Union

import mlflow
import numpy as np
from stable_baselines3.common.logger import HumanOutputFormat, KVWriter, Logger


class MLFlowLogger(KVWriter):
    """
    Dumps key/value pairs into MLflow's numeric format.
    """

    def __init__(
        self, experiment_name: str, run_name: str, params_to_log: Di