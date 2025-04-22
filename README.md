# Mixed-Reality Zombie Shooter for Meta Quest

A mixed-reality VR shooter demo for Meta Quest, built with Unity and Meta XR SDKs.

## Features

- Real-world passthrough using Meta Quest camera
- Animated zombie AI with NavMesh navigation
- Revolver mechanics with projectile bullets
- Runtime NavMesh building on room-scale anchors

## Prerequisites

- Unity 2021.3 or later
- Meta Quest 3
- Meta XR All-in-One SDK
- Meta XR Utility Kit

## Getting Started

1. Clone the repo:
   ```bash
   git clone https://github.com/atiq-sm/ZombieGame
   ```
2. Open the project in Unity.
3. Switch build target to Android and set package name in Player Settings.
4. Import Meta XR All-in-One SDK and Meta XR Utility Kit.
5. Connect Quest via USB-C or use Quest Link to test in-editor.
6. Build and run on the headset.

## Usage

- Use the VR controllers to aim and shoot the revolver.
- Zombies spawn on detected surfaces and chase the player.
- Fend off zombies to avoid game over.
