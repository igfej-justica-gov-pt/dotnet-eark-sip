param (
    [string]$NEXT_VERSION
)

# Syntax function
function Show-Syntax {
    Write-Host "Syntax: $($MyInvocation.MyCommand.Name) NEXT_VERSION"
    Write-Host "Example: $($MyInvocation.MyCommand.Name) 2.3.0"
}

# Check if NEXT_VERSION is empty
if (-not $NEXT_VERSION) {
    Show-Syntax
    exit 1
}

# Prepare for next version
Write-Host "################################"
Write-Host "# Prepare for next version"
Write-Host "################################"

# Updating .NET project version in .csproj file with next version SNAPSHOT
Write-Host "Updating .NET project version to $NEXT_VERSION..."
Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
    Write-Host "Updating $($_.FullName)..."
    (Get-Content $_.FullName) | ForEach-Object {
        $_ -replace '<Version>.*</Version>', "<Version>$NEXT_VERSION</Version>"
    } | Set-Content $_.FullName
}

# Commit changes and push
git add -u
git commit -m "Setting version $NEXT_VERSION-SNAPSHOT"
git push
