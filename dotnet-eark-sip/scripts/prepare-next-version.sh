#!/bin/bash

# Version
NEXT_VERSION=$1

function syntax {
  echo "Syntax:  $1 NEXT_VERSION"
  echo "Example: $1 2.3.0"
}

if [[ -z "$NEXT_VERSION" ]]; then
  syntax $0
  exit 1
fi

cat << EOF
################################
# Prepare for next version
################################
EOF

# Updating .NET project version in .csproj file with next version SNAPSHOT
echo "Updating .NET project version to $NEXT_VERSION..."
for file in $(find . -name "*.csproj"); do
  echo "Updating $file..."
  sed -i "s|<Version>.*</Version>|<Version>$NEXT_VERSION</Version>|g" "$file"
done

# Commit Maven version update
git add -u
git commit -m "Setting version $NEXT_VERSION-SNAPSHOT"

# Push commits
git push
