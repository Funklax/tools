#!/bin/bash

# Get the currently signed-in user
CURRENT_USER=$(whoami)

# Define the new password
NEW_PASSWORD="tempchange"

# Use 'echo' with 'passwd' to change the user's password
echo "${CURRENT_USER}:${NEW_PASSWORD}" | sudo chpasswd

# Output the result
echo "Password for user '${CURRENT_USER}' has been changed to 'tempchange'."
