#!/bin/bash
set -e

# Update package list and install dependencies
apt update;
apt upgrade -y;
apt install nmap -y;
apt install curl -y;
apt install dirsearch -y;