#!/bin/bash

docker build -t ghcr.io/aimrank/aimrank-pod:$1 -f Dockerfile .
