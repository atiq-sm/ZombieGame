# ZombieGame - Mixed Reality Zombie Shooter
## Comprehensive Project Description

---

## Table of Contents
1. [Project Objective](#project-objective)
2. [Overview](#overview)
3. [Technical Architecture](#technical-architecture)
4. [Core Components](#core-components)
5. [Gameplay Mechanics](#gameplay-mechanics)
6. [Implementation Details](#implementation-details)
7. [Development Environment](#development-environment)
8. [How It Works](#how-it-works)
9. [Future Enhancements](#future-enhancements)

---

## Project Objective

The **ZombieGame** is a cutting-edge **Mixed Reality (MR) VR shooter** designed specifically for the **Meta Quest 3** headset. The primary objective is to create an immersive gaming experience that seamlessly blends the virtual and physical worlds by leveraging Meta's passthrough technology.

### Key Goals:
- **Immersive Mixed Reality Experience**: Utilize real-world environments as the gameplay arena by overlaying virtual zombies onto physical spaces
- **Intuitive VR Interaction**: Provide natural and responsive controller-based shooting mechanics
- **Dynamic AI Navigation**: Implement intelligent zombie pathfinding that adapts to the player's physical environment
- **Room-Scale Gaming**: Leverage room-scale VR to enable free movement while defending against zombie hordes
- **Accessible VR Development**: Demonstrate best practices for mixed reality game development using Unity and Meta XR SDKs

The project serves both as an engaging game and as a technical demonstration of mixed reality capabilities, showcasing how virtual content can interact meaningfully with real-world spaces.

---

## Overview

**ZombieGame** is a first-person mixed reality shooter where players defend themselves against waves of animated zombies that spawn and navigate through their actual physical environment. Using the Meta Quest 3's passthrough cameras, players can see their real surroundings while virtual zombies emerge from walls and surfaces, creating a unique blend of reality and virtual horror.

### What Makes It Unique:
- **Real-World Integration**: Unlike traditional VR games that place you in entirely virtual environments, this game uses your actual room as the game world
- **Spatial Awareness**: Zombies intelligently navigate around your real furniture and room layout using runtime-generated navigation meshes
- **Physical Engagement**: Players must physically move and aim in their space, creating an active gaming experience
- **Meta Quest 3 Optimization**: Built specifically for Meta's latest mixed reality hardware with full passthrough support

---

## Technical Architecture

### Platform Stack
```
┌─────────────────────────────────────┐
│      Meta Quest 3 Hardware          │
│   (Passthrough Cameras + VR)        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│         Unity Engine 6000           │
│        (Unity 6.0 / 2021.3+)        │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Meta XR All-in-One SDK         │
│           (v74.0.3)                 │
│  - OVR Input System                 │
│  - Passthrough API                  │
│  - Room Anchors                     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Meta XR Utility Kit (MRUK)       │
│  - Scene Understanding              │
│  - Surface Detection                │
│  - Anchor Management                │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│       Unity AI Navigation           │
│         (v2.0.5)                    │
│  - NavMesh Surface                  │
│  - NavMesh Agent                    │
│  - Runtime NavMesh Building         │
└─────────────────────────────────────┘
```

### Key Technologies
1. **Unity 6000.0.35f1**: Core game engine
2. **Meta XR SDK v74.0.3**: VR hardware integration and passthrough
3. **Unity AI Navigation v2.0.5**: Pathfinding system for zombie movement
4. **Unity Input System v1.11.2**: Modern input handling
5. **XR Interaction Toolkit v3.0.8**: VR interaction framework
6. **Universal Render Pipeline v17.0.3**: Graphics rendering

---

## Core Components

### 1. **ZombieAI.cs**
**Purpose**: Controls individual zombie behavior and navigation.

**Functionality**:
- Manages NavMeshAgent component for pathfinding
- Continuously tracks and chases the player's camera position
- Configurable movement speed
- Handles zombie death and destruction
- Automatically disables navigation when killed

**Key Features**:
- Real-time target tracking (follows VR headset position)
- Safe destruction handling
- Integration with Unity's NavMesh system

### 2. **Gun.cs**
**Purpose**: Implements the VR revolver weapon mechanics.

**Functionality**:
- Monitors VR controller button input for shooting
- Instantiates bullet projectiles at the gun's muzzle
- Applies physics-based velocity to bullets
- Plays shooting sound effects
- Manages bullet lifecycle and cleanup

**Key Features**:
- OVR input integration for natural VR shooting
- Configurable bullet speed and spawn point
- Automatic bullet destruction after 2 seconds
- Audio feedback for shots
- Attaches collision detection to spawned bullets

### 3. **BulletCollision.cs**
**Purpose**: Handles bullet-zombie collision detection.

**Functionality**:
- Detects collisions with zombies
- Triggers zombie death when hit
- Destroys bullet on impact
- Uses component-based detection for robust hit detection

**Key Features**:
- Parent hierarchy traversal for accurate hit detection
- Immediate feedback on successful hits
- Clean resource management

### 4. **ZombieSpawner.cs**
**Purpose**: Manages periodic zombie spawning in the player's environment.

**Functionality**:
- Timer-based spawning system (default: every 3 seconds)
- Integrates with MRUK (Meta Room Understanding Kit) for spatial awareness
- Finds valid spawn positions on vertical surfaces (walls)
- Ensures zombies spawn at minimum distance from player
- Handles spawn position validation with retry logic

**Key Features**:
- Room-aware spawning (uses detected room anchors)
- Surface-based positioning (spawns on walls with normal offset)
- Configurable spawn intervals and distance constraints
- Robust error handling with retry attempts (up to 1000 tries)

### 5. **RuntimeNavmeshBuilder.cs**
**Purpose**: Dynamically creates navigation meshes based on the player's physical room.

**Functionality**:
- Builds NavMesh at runtime after room scanning completes
- Integrates with MRUK scene loading callbacks
- Uses Unity's NavMeshSurface for navigation data generation
- Waits for frame completion to ensure all geometry is loaded

**Key Features**:
- Event-driven architecture (responds to scene loaded events)
- Coroutine-based async building
- Automatic navigation mesh generation for any room layout

---

## Gameplay Mechanics

### Player Experience Flow

1. **Setup Phase**:
   - Player puts on Meta Quest 3 headset
   - Game initiates passthrough mode, showing real environment
   - MRUK scans the room, detecting walls, floor, and surfaces
   - RuntimeNavmeshBuilder creates walkable navigation mesh
   - Virtual revolver appears in player's hand

2. **Combat Phase**:
   - Zombies begin spawning on walls every 3 seconds
   - Each zombie drops to the ground and navigates toward the player
   - Player aims with VR controllers and shoots by pressing trigger
   - Bullets physically fly through space with realistic physics
   - Hit zombies are instantly destroyed
   - Wave intensity increases with more zombies over time

3. **Movement & Strategy**:
   - Player can physically walk around their room
   - Must manage positioning to avoid being surrounded
   - Real furniture and obstacles affect both player and zombie movement
   - Room layout becomes strategic consideration

### Combat System

#### Shooting Mechanics
- **Input**: OVR controller trigger button
- **Projectile System**: Physics-based bullets (not hitscan)
- **Bullet Behavior**:
  - Spawns at gun muzzle position
  - Travels forward at 20 m/s (configurable)
  - Auto-destroys after 2 seconds
  - Collision detection with zombies
- **Audio Feedback**: Sound effect plays on each shot

#### Enemy AI Behavior
- **Navigation**: Uses Unity NavMesh for pathfinding
- **Target Tracking**: Always moves toward player's camera position
- **Movement Speed**: Configurable (default: 1 unit/second)
- **Death Condition**: Single bullet hit kills zombie
- **Spawning Pattern**: 
  - Random positions on vertical surfaces (walls)
  - Minimum 0.3m from player
  - Offset from wall surface to prevent clipping

---

## Implementation Details

### Mixed Reality Integration

#### Passthrough Setup
The game uses Meta Quest 3's passthrough API to render the real world:
- Camera feed overlay shows physical environment
- Virtual objects rendered on top with proper occlusion
- Maintains spatial registration between real and virtual elements

#### Room Understanding (MRUK)
```csharp
// Scene awareness initialization
MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);

// Finding valid spawn positions
room.GenerateRandomPositionOnSurface(
    MRUK.SurfaceType.VERTICAL,  // Spawn on walls
    minEdgeDistance,             // Stay away from edges
    LabelFilter.Included(spawnLabels),
    out Vector3 position,
    out Vector3 normal
);
```

The game automatically:
- Detects room boundaries and surfaces
- Identifies vertical surfaces for spawning
- Provides position and normal data for placement
- Updates when room changes

#### Dynamic Navigation Mesh
```csharp
// Runtime NavMesh building
navMeshSurface.BuildNavMesh();
```

Process:
1. MRUK completes room scan
2. Callback triggers NavMesh build
3. NavMeshSurface analyzes room geometry
4. Creates walkable navigation graph
5. Zombies can now pathfind around obstacles

### Physics System

#### Bullet Physics
```csharp
bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
```

- Uses Rigidbody for realistic projectile motion
- Gravity affects bullet trajectory
- Collision detection via physics engine
- Automatic cleanup prevents memory leaks

#### Collision Detection
```csharp
// On bullet collision
ZombieAI zombie = collision.transform.GetComponentInParent<ZombieAI>();
if (zombie != null) {
    zombie.Kill();
    Destroy(gameObject);
}
```

- Unity's physics engine handles collision events
- Component-based detection ensures reliable hits
- Parent hierarchy traversal catches hits on child colliders

### AI System

#### Zombie Navigation
```csharp
// Continuous player tracking
Vector3 targetPosition = Camera.main.transform.position;
agent.SetDestination(targetPosition);
agent.speed = speed;
```

Features:
- Frame-by-frame destination updates
- Smooth pathfinding around obstacles
- Speed control for difficulty tuning
- Automatic path recalculation

### VR Input System

#### Controller Integration
```csharp
// Button press detection
if (OVRInput.GetDown(shootingButton)) {
    Shoot();
}
```

- OVRInput provides Meta Quest controller access
- Configurable button mapping in Unity Inspector
- GetDown ensures single shot per button press
- Natural and responsive feel

---

## Development Environment

### Prerequisites

#### Hardware Requirements
- **Meta Quest 3** (required for mixed reality features)
- **Development PC** with:
  - Windows 10/11 or macOS
  - USB-C port for device connection
  - Minimum 8GB RAM (16GB recommended)
  - Graphics card capable of VR development

#### Software Requirements
- **Unity Hub** (latest version)
- **Unity 6000.0.35f1** or **Unity 2021.3+**
- **Android SDK** and **NDK** (for Quest building)
- **Meta Quest Developer Hub** (optional but recommended)
- **Visual Studio 2019+** or **Visual Studio Code** with C# extension

### Project Setup

1. **Clone Repository**:
   ```bash
   git clone https://github.com/atiq-sm/ZombieGame
   ```

2. **Open in Unity**:
   - Launch Unity Hub
   - Add project from disk
   - Open with Unity 6000.0.35f1

3. **Install Dependencies**:
   - Unity Package Manager automatically downloads:
     - Meta XR All-in-One SDK (v74.0.3)
     - Meta XR Utility Kit
     - Unity AI Navigation (v2.0.5)
     - XR Interaction Toolkit (v3.0.8)

4. **Configure Build Settings**:
   - File → Build Settings
   - Switch platform to **Android**
   - Set texture compression to **ASTC**
   - Configure Player Settings:
     - Company Name
     - Product Name
     - Package Name (com.yourcompany.zombiegame)
     - Minimum API Level: Android 10.0 (API 29)

5. **Setup Meta Quest**:
   - Enable Developer Mode in Meta Quest mobile app
   - Connect Quest 3 to PC via USB-C
   - Allow USB debugging on headset
   - Verify connection: `adb devices`

6. **Test in Editor** (optional):
   - Install Oculus Quest Link software
   - Connect Quest 3 via cable or Air Link
   - Press Play in Unity Editor
   - Test with live headset preview

7. **Build and Deploy**:
   - Build Settings → Build
   - Unity generates APK
   - Automatically installs to connected Quest 3
   - Launch app from "Unknown Sources" in Quest library

### Asset Dependencies

The project includes several asset packages:

- **Zombie Animation Pack FREE**: Provides zombie character models and animations
- **Low Poly Weapons LITE**: Contains weapon models including revolver
- **ithappy Weapons FREE**: Additional weapon assets
- **Meta XR SDKs**: Core VR and MR functionality
- **Unity XR packages**: Platform-level VR support

---

## How It Works

### Complete System Flow

#### 1. **Initialization Sequence**
```
Game Start
    ↓
Passthrough Activation
    ↓
MRUK Scene Scan
    ↓
Room Detection & Anchor Creation
    ↓
NavMesh Build Callback Triggered
    ↓
RuntimeNavmeshBuilder.BuildNavmesh()
    ↓
Wait for Frame End
    ↓
NavMeshSurface.BuildNavMesh()
    ↓
Game Ready
```

#### 2. **Game Loop**

**Per Frame (Update Cycle)**:
```
Input Check (Gun.cs)
    ├─ Button Pressed? → Spawn Bullet → Apply Physics
    └─ No Input → Continue

Zombie AI Updates (ZombieAI.cs)
    ├─ Get Player Position
    ├─ Set NavMesh Destination
    └─ Move Along Path

Spawner Timer (ZombieSpawner.cs)
    ├─ Timer >= Interval?
    │   ├─ Find Valid Surface Position
    │   ├─ Calculate Spawn Point
    │   └─ Instantiate Zombie
    └─ Timer < Interval → Increment Timer

Physics Simulation
    ├─ Bullet Movement
    ├─ Collision Detection
    └─ Gravity Effects

Collision Resolution (BulletCollision.cs)
    ├─ Bullet Hits Zombie?
    │   ├─ Call zombie.Kill()
    │   └─ Destroy Bullet
    └─ Bullet Timeout → Destroy
```

#### 3. **Spatial Awareness Flow**

**Room Understanding**:
```
Meta Quest Cameras
    ↓
Computer Vision Processing
    ↓
MRUK Scene Data
    ├─ Wall Surfaces
    ├─ Floor Plane
    ├─ Obstacles
    └─ Room Boundaries
    ↓
Navigation Mesh Generation
    ├─ Walkable Areas
    ├─ Obstacles
    └─ Pathfinding Graph
    ↓
Zombie Spawning & Navigation
```

#### 4. **Combat Interaction Flow**

```
Player Pulls Trigger
    ↓
OVRInput.GetDown(shootingButton)
    ↓
Gun.Shoot() Called
    ↓
├─ Play Audio
├─ Instantiate Bullet Prefab
├─ Add BulletCollision Component
├─ Set Bullet Velocity
└─ Schedule Destruction (2s)
    ↓
Bullet Travels Through Space
    ↓
Physics Engine Detects Collision
    ↓
OnCollisionEnter(Collision)
    ↓
Check for ZombieAI Component
    ↓
├─ Zombie Found
│   ├─ zombie.Kill()
│   │   ├─ Disable NavMeshAgent
│   │   └─ Destroy GameObject
│   └─ Destroy Bullet
└─ No Zombie → Bullet Continues
```

### Data Flow Diagram

```
[Meta Quest Sensors] → [MRUK] → [Room Data]
                                      ↓
[Player Input] → [Gun.cs] → [Bullet Physics] → [Collision]
                                                      ↓
[Spawn Timer] → [ZombieSpawner] → [Zombie Instance] → [NavMeshAgent]
                                                            ↓
                                    [Camera Position] ← [ZombieAI]
```

---

## Future Enhancements

### Potential Improvements

1. **Gameplay Enhancements**:
   - Multiple weapon types (shotgun, machine gun, grenades)
   - Power-ups and health system
   - Score tracking and high scores
   - Progressive difficulty waves
   - Boss zombies with special abilities
   - Multiplayer co-op mode

2. **Technical Improvements**:
   - Object pooling for bullets and zombies (performance)
   - Spatial audio for zombie sounds (immersion)
   - Hand tracking support (controller-free play)
   - Improved zombie animations and variety
   - Better spawn distribution algorithm
   - Room boundary warnings

3. **User Experience**:
   - Tutorial mode for first-time players
   - Customizable difficulty settings
   - In-game UI for ammo and health
   - Pause menu and settings
   - Save/load game state
   - Leaderboards and achievements

4. **Visual & Audio**:
   - Particle effects for bullet impacts
   - Blood splatter effects (optional)
   - Muzzle flash for gun
   - Enhanced zombie death animations
   - Ambient horror soundtrack
   - Dynamic lighting based on room conditions

5. **Mixed Reality Features**:
   - Zombies breaking through actual walls
   - Virtual barricades on real furniture
   - Environmental interaction (throwing objects)
   - Time-of-day lighting integration
   - Persistent game state tied to physical room

---

## Conclusion

**ZombieGame** represents a compelling demonstration of mixed reality gaming on the Meta Quest 3 platform. By seamlessly integrating virtual zombies into real-world environments through advanced spatial understanding and dynamic navigation systems, the project showcases the potential of mixed reality to create uniquely immersive gaming experiences.

The technical implementation leverages industry-standard tools (Unity, Meta XR SDKs) while demonstrating best practices in VR development, AI navigation, and physics-based gameplay. The modular code architecture ensures maintainability and extensibility for future enhancements.

Whether used as an entertaining game or as a learning resource for aspiring VR developers, ZombieGame exemplifies how modern mixed reality technology can blur the lines between physical and virtual worlds, creating engaging experiences that were impossible just a few years ago.

---

## Additional Resources

### Documentation
- [Meta XR SDK Documentation](https://developer.oculus.com/documentation/)
- [Unity XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/)
- [Unity AI Navigation](https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/)

### Community
- GitHub Repository: [atiq-sm/ZombieGame](https://github.com/atiq-sm/ZombieGame)
- Meta Quest Developer Forums
- Unity VR Development Community

### Related Projects
- Meta's MR template projects
- Unity XR sample scenes
- Mixed reality development showcases

---

*This project was developed as a demonstration of mixed reality gaming capabilities for the Meta Quest 3 platform.*
