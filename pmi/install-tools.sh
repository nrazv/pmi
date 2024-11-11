#!/bin/bash
set -e

# Update package list and install dependencies
apt update;
apt install nmap -y;
apt install curl -y
