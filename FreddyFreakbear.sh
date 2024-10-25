# Check if the script is being run as Administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Error "This script must be run as Administrator."
    exit 1
}

# Set the new password for the Administrator account
$NewPassword = "tempchange"
$AdminUser = "Administrator"

# Change the password using the 'net user' command
net user $AdminUser $NewPassword

# Output the result
Write-Output "Password for user '$AdminUser' has been changed to 'tempchange'."
