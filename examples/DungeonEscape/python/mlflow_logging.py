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
        self, experiment_name: str, run_name: str, params_to_log: Dict[str, Any]
    ):
        self.experiment_name = experiment_name
        self.run_name = run_name
        assert "MLFLOW_TRACKING_URI" in os.environ
        print("mlflow tracking uri", mlflow.get_tracking_uri())
        self.client = mlflow.MlflowClient()
        self.experiment_id = mlflow.get_experiment_by_name(
            experiment_name
        ).experiment_id
        if not self.experiment_id:
            self.experiment_id = mlflow.create_experiment(experiment_name)
        print("experiment id", self.experiment_id)
        self.run = mlflow.start_run(experiment_id=self.experiment_id, run_name=run_name)

        try:
            # check we are in a repo
            subprocess.check_output(["git", "status"], stderr=subproces