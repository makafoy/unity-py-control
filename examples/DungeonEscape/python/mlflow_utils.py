import datetime
import os
import shutil
import time
from os.path import join
from typing import Optional

import mlflow
import mlflow.entities


class MlflowLoader:
    """
    Utilities for interacting with mlflow

    When a run_name is provided as input, if there are multiple runs with the same name,
    this class uses the run with the longest duration, or if there is a run currently running, with that
    name, then uses that run.

    You can either st MLFLOW_TRACKING_URI envornment variable with the url of your mlflow server, or
    pass the url in through the parameter mlflow_uri.
    """

    def __init__(self, experiment_name: str, mlflow_uri: Optional[str] = None):
        self.experiment_name = experiment_name
        self.cache_dir = ".mlflow_cache"
        if not os.path.exists(self.cache_dir):
            os.makedirs(self.cache_dir)

        if mlflow_uri is None:
            assert "MLFLOW_TRACKING_URI" in os.environ
            print("mlflow tracking uri", mlflow.get_tracking_uri())
            self.mlflow_uri = os.environ["MLFLOW_TRACKING_URI"]
        else:
            self.mlflow_uri = mlflow_uri

    def _get_checkpoint_cache_filename(self, run_name: str, run_iterations: int) -> str:
        return f"{run_name}:{run_iterations}.zip"

    def download_checkpoint(self, run_name: str, run_iterations: int) -> str:
        """
        Downloads a checkpoint from mlflow, given the run_name, and the number of
        iterations of training prior to the checkpoint. For example, to download
        ckp_1024.zip, then set run_iterations=1024.
        """
        cache_filename = self._get