#!/bin/bash

# ----------------------
# KUDU Deployment Script
# Version: 0.2.2
# ----------------------

# Helpers
# -------

pause() {
  if [ "$HOSTNAME" = "COSYGLOW" ]; then
    read -p "$*"
  fi
}

exitWithMessageOnError () {
  if [ ! $? -eq 0 ]; then
    echo "An error has occurred during web site deployment."
    echo $1
    exit 1
  fi
}

# Prerequisites
# -------------

# Verify node.js installed
hash node 2>/dev/null
exitWithMessageOnError "Missing node.js executable, please install node.js, if already installed make sure it can be reached from current environment."

# Setup
# -----

# see https://github.com/projectkudu/kudu/wiki/Deployment-hooks
# pwd is initially the root of the repo when batch file is executing.
# DEPLOYMENT_SOURCE == the root of the repo
# DEPLOYMENT_TARGET == the wwwroot folder
# DEPLOYMENT_TEMP   == temporary folder for storing artifacts for the current build. Deleted after cmd is run.
# MSBUILD_PATH      == Path to msbuild executable

#SCRIPT_DIR="${BASH_SOURCE[0]%\\*}"
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
echo "SCRIPT_DIR: $SCRIPT_DIR"

#SCRIPT_DIR="${SCRIPT_DIR%/*}"
#echo "SCRIPT_DIR: $SCRIPT_DIR"

ARTIFACTS=$SCRIPT_DIR/../artifacts
KUDU_SYNC_CMD=${KUDU_SYNC_CMD}

if [[ ! -n "$DEPLOYMENT_SOURCE" ]]; then
  DEPLOYMENT_SOURCE=$SCRIPT_DIR
fi

if [[ ! -n "$NEXT_MANIFEST_PATH" ]]; then
  NEXT_MANIFEST_PATH=$ARTIFACTS/manifest

  if [[ ! -n "$PREVIOUS_MANIFEST_PATH" ]]; then
    PREVIOUS_MANIFEST_PATH=$NEXT_MANIFEST_PATH
  fi
fi

if [[ ! -n "$DEPLOYMENT_TARGET" ]]; then
  DEPLOYMENT_TARGET=$ARTIFACTS/wwwroot
else
  KUDU_SERVICE=true
fi

if [[ ! -n "$DEPLOYMENT_TEMP" ]]; then
  DEPLOYMENT_TEMP=$temp\___deployTemp$random
  CLEAN_LOCAL_DEPLOYMENT_TEMP=true
fi

if [[ -n "$CLEAN_LOCAL_DEPLOYMENT_TEMP" ]]; then
  if [ -d "$DEPLOYMENT_TEMP" ]; then
    rm -rf "$DEPLOYMENT_TEMP"
  fi
  mkdir "$DEPLOYMENT_TEMP"
fi

if [[ ! -n "$MSBUILD_PATH" ]]; then
  # location on my machine...
  MSBUILD_PATH="/c/Program Files/MSBuild/14.0/Bin/MSBuild.exe"
fi

# Node Helpers
# ------------

selectNodeVersion () {
  if [[ -n "$KUDU_SELECT_NODE_VERSION_CMD" ]]; then
    SELECT_NODE_VERSION="$KUDU_SELECT_NODE_VERSION_CMD \"$DEPLOYMENT_SOURCE\" \"$DEPLOYMENT_TARGET\" \"$DEPLOYMENT_TEMP\""
    eval $SELECT_NODE_VERSION
    exitWithMessageOnError "select node version failed"

    if [[ -e "$DEPLOYMENT_TEMP/__nodeVersion.tmp" ]]; then
      NODE_EXE=`cat "$DEPLOYMENT_TEMP/__nodeVersion.tmp"`
      exitWithMessageOnError "getting node version failed"
    fi

    if [[ -e "$DEPLOYMENT_TEMP/.tmp" ]]; then
      NPM_JS_PATH=`cat "$DEPLOYMENT_TEMP/__npmVersion.tmp"`
      exitWithMessageOnError "getting npm version failed"
    fi

    if [[ ! -n "$NODE_EXE" ]]; then
      NODE_EXE=node
    fi

    NPM_CMD="\"$NODE_EXE\" \"$NPM_JS_PATH\""
  else
    NPM_CMD=npm
    NODE_EXE=node
  fi
}

##################################################################################################################################
# Deployment
# ----------

printf "\n"
printf "\n"
#pause "press ENTER to restore NUGET packages"

# Restore NuGet packages
if [ ! -f "$DEPLOYMENT_SOURCE"/gellmvc.sln ]; then
  echo "Could not find the solution file."
else
  nuget restore "$DEPLOYMENT_SOURCE"/gellmvc.sln
fi

printf "\n"

if [ ! -f "$DEPLOYMENT_SOURCE/gellmvc/gellmvc.csproj" ]; then
  echo "Could not find the csproj"
else
  echo "Found csproj: $DEPLOYMENT_SOURCE/gellmvc/gellmvc.csproj"
fi

if [ ! -f "$MSBUILD_PATH" ]; then
  echo "Could not find MSBuild.exe"
else
  echo "MSBUILD_PATH: $MSBUILD_PATH"
fi

# Build to the temporary path
printf "\n"
printf "\n"
echo "Do MSBuild..."
# Tell MSBuild to build our solution.
# compiles to /gellmvc/gellmvc/___deployTemp/_PublishedWebsites

"$MSBUILD_PATH" "gellmvc.sln" "/property:Configuration=Release;TargetFramework=v4.5.2;OutputPath=$DEPLOYMENT_TEMP"

#"$MSBUILD_PATH" "gellmvc.sln" "/t:pipelinePreDeployCopyAllFilesToOneFolder /property:_PackageTempDir=$DEPLOYMENT_TEMP;AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false;TargetFramework=v4.5.2"


printf "\n"
printf "\n"

echo "NPM, BOWER, GRUNT..."

pause "press ENTER to install NPM stuff"

# Go to repo root.
cd "$DEPLOYMENT_SOURCE"
selectNodeVersion

# Install NPM packages
if [ -e "$DEPLOYMENT_SOURCE/package.json" ]; then
  cd "$DEPLOYMENT_SOURCE"
  eval $NPM_CMD prune
  echo "Do npm install --production"
  eval $NPM_CMD install --production
  exitWithMessageOnError "npm install failed"
  cd - > /dev/null
fi

#pause "press ENTER to install bower stuff"

# Install Bower modules
if [ -e "$DEPLOYMENT_SOURCE/bower.json" ]; then
  cd "$DEPLOYMENT_SOURCE"
  eval rm -rf bower_components
  echo deleted bower components
  eval ./node_modules/.bin/bower install
  exitWithMessageOnError "bower failed"
  cd - > /dev/null
fi

#pause "press ENTER to run grunt tasks"

# Run Grunt Task. This populates dist
if [ -e "$DEPLOYMENT_SOURCE/Gruntfile.js" ]; then
  cd "$DEPLOYMENT_SOURCE"
  #pause "Press [Enter] to run grunt"
  eval ./node_modules/.bin/grunt --no-color --verbose
  exitWithMessageOnError "Grunt failed"
  cd - > /dev/null
fi


pause "press ENTER to copy dist files to ___deployTemp/_PublishedWebsites/gellmvc"
if [ ! -d "$DEPLOYMENT_SOURCE"/gellmvc/___deployTemp/_PublishedWebsites/gellmvc ]; then
  mkdir -p "$DEPLOYMENT_SOURCE"/gellmvc/___deployTemp/_PublishedWebsites/gellmvc
fi
if [ -d "$DEPLOYMENT_SOURCE/gellmvc/dist" ]; then
  cp -R "$DEPLOYMENT_SOURCE"/gellmvc/dist "$DEPLOYMENT_SOURCE"/gellmvc/___deployTemp/_PublishedWebsites/gellmvc
fi

pause "press ENTER to Copy gellmvc/___deployTemp into the Kudu ___deployTemp folder"

# Copy gellmvc/___deployTemp into Kudu's ___deployTemp folder
if [ "$HOSTNAME" = "COSYGLOW" ]; then
  cp -R "$DEPLOYMENT_SOURCE"/gellmvc/___deployTemp/* "$DEPLOYMENT_SOURCE"/___deployTemp/
fi

printf "\n"
printf "\n"
if [[ ! -d "$ARTIFACTS"/wwwroot ]]; then
  pause "press ENTER to create $ARTIFACTS wwwroot"
  pushd "$SCRIPT_DIR"
  cd ..
  pwd
  mkdir "$ARTIFACTS"
  cd "$ARTIFACTS"
  mkdir "wwwroot"
  popd
fi

printf "\n"
printf "\n"
pause "press ENTER to delete $DEPLOYMENT_SOURCE/gellmvc/___deployTemp"
rm -rf "$DEPLOYMENT_SOURCE"/gellmvc/___deployTemp

printf "\n"
printf "\n"
pause "press ENTER to run KuduSync - copy files to artifacts/wwwroot"

# KUDU SYNC deployTemp -> artifacts/wwwroot
echo "DEPLOYMENT_SOURCE == $DEPLOYMENT_SOURCE"
echo "KUDU_SYNC_CMD     == $KUDU_SYNC_CMD"
printf "\n"

# 1. KuduSync
"$KUDU_SYNC_CMD" -v 50 -f "$DEPLOYMENT_SOURCE" -t "$DEPLOYMENT_TARGET" -n "$NEXT_MANIFEST_PATH" -p "$PREVIOUS_MANIFEST_PATH" -i ".git;.hg;.deployment;deploy.sh;deploy.cmd;README.md;package.json;Gruntfile.js;bower.json;.gitignore;.bowerrc;bower_components;gellmvc;gellmvc.sln;packages;.vs"
exitWithMessageOnError "Kudu Sync failed"

printf "\n"
echo "Finished successfully."