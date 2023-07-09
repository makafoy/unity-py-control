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
            assert "MLFLO