#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd ${DIR} || exit

echo "Pulling from GitHub"
## Get latest from GitHub
cd ..
git pull origin master
cd ${DIR}

echo "Cleaning up"
## Delete temp directories
rm -rf tmp
rm -rf out_mobile
rm -rf cordova_mobile_tmp

## Create temp directories
mkdir tmp
mkdir cordova_mobile_tmp
mkdir out_mobile
mkdir out_mobile/android
mkdir out_mobile/iOS

## Copy existing source
cd ${DIR}
cd tmp
cp -r ../../src/myProducts.Web/app .
cp -r ../../src/myProducts.Web/appStartup .
cp -r ../../src/myProducts.Web/appServices .
cp -r ../../src/myProducts.Web/mobile .
cp -r ../../src/myProducts.Web/libs .
cp -r ../../src/myProducts.Web/assets .

cp ../node-webkit-sharedsource/* .

## Download generated index.html page
echo "GETting index.html"
curl -k https://windows8vm.local/ngmd/mobile/#/ > ./mobile/index.html
perl -pi -w -e 's/\/ngmd\//..\//g;' ./mobile/index.html

## Create cordova project
cd ${DIR}
cd cordova_mobile_tmp
cordova create myProducts com.tt.ngmd myProducts
rm -rf myProducts/www

## Copy existing application elements
cp -r ../tmp/ myProducts/www
cp -r ../cordova-sharedsource/ myProducts/

echo "Creating cordova projects"

cd myProducts

cordova platform add ios
cordova platform add android

cordova plugin add org.apache.cordova.device
cordova plugin add org.apache.cordova.geolocation
cordova plugin add org.apache.cordova.splashscreen
cordova plugin add org.apache.cordova.statusbar
cordova plugin add org.apache.cordova.console

## Build for iOS
cp -r ../../cordova-ios/ ./platforms/ios/myProducts
cp ./www/config.xml ./platforms/ios/myProducts

echo "Building for iOS"
cordova build ios

cd platforms/ios/build/emulator/
mv myProducts.app "../../../../../../out_mobile/ios/myProducts.app"
cd ../../../..

echo "Building for Android"

# Tweak Android to use Crosswalk
: <<‘QWERTY’
rm -Rf platforms/android/CordovaLib/*
cp -a ../../cordova-android/crosswalk-cordova/framework/* \
    platforms/android/CordovaLib/
cp -a ../../cordova-android/crosswalk-cordova/VERSION platforms/android/

export ANDROID_HOME=$(dirname $(dirname $(which android)))
cd platforms/android/CordovaLib/
android update project --subprojects --path . \
    --target "android-19"
ant debug
cd ../../..
QWERTY

## Finally build Android
cp -r ../../cordova-android/ ./platforms/android/

cordova build android

cd platforms/android/ant-build/
cp myProducts-debug.apk ../../../../../out_mobile/android/

## Remove tmp directory
cd ${DIR}
rm -rf tmp
