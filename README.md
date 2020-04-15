# Bright Animator
Extensions for the Unity Animator. Play audio inside the animator.

![Imgur](https://i.imgur.com/ZMv0kTI.gif)

![Imgur](https://i.imgur.com/UE1GS90.gif)


## Features
* Play audio when entering an animator state.
* Add multiple audio clips to be played.
* Play an audio at a given frequency.
* Play audio when exiting an animator state.

## Prerequisites
Unity 2018.3 and up

## Install

### Unity 2019.3
1. Open the package manager and point to the repo URL

![Imgur](https://i.imgur.com/iYGgINz.png)

### Before Unity 2019.3

#### Option A
1. Open the manifest
2. Add the repo URL either via https or ssh

		{
    		"dependencies": {
        		"com.brightlib.animator": "https://github.com/carreraSilvio/BrightAnimator"
    		}
		}

#### Option B
1. Clone or download the project zip
2. Open your project assets folder
3. Copy the repo there

## Usage

### Play Audio Clip
1. Click your game object with an animator
2. Open the animator window
3. Select the animator state
4. On the inspector click on Add Behaviour>PlayAudioClip
5. Pick the AudioClip you want
6. Hit play

Note: 
- Make sure you have an AudioSource added to your GameObject
