# Pooling Utility for Unity

## Introduction
The `Pooling` utility, part of the `com.Klazapp.Utility` namespace, is designed for Unity projects to manage the instantiation and reuse of game objects, primarily to improve performance in scenarios where frequent creation and destruction of objects are needed. The primary goals of object pooling are efficiency, optimization, and reducing the overhead associated with object creation and destruction.

## Features
- The ability to pre-allocate a pool of objects during initialization.
- Minimization of the performance cost associated with instantiating new objects.
- Minimization of memory fragmentation and efficient use of resources.

## Dependencies
To use `Pooling`, certain dependencies are required. Ensure these are included in your Unity project.
- **Unity Version**: Minimum Unity 2020.3 LTS.
- **Repository**: [LogMessage Unity Logger](https://github.com/klazapp/Unity-Logger-Public.git)
- **Repository**: [LogMessage Unity Singleton](https://github.com/klazapp/Unity-Singleton-Public.git)
- **Repository**: [LogMessage Unity Inspector](https://github.com/klazapp/Unity-Inspector-Public.git)
- Unity Mathematics dll

## Compatibility
| Compatibility        | URP | BRP | HDRP |
|----------------------|-----|-----|------|
| Compatible           | ✔️  | ✔️  | ✔️   |

## Installation
1. Open the Unity Package Manager (`Window` > `Package Manager`).
2. Click `+`, select `Add package from git URL...`, and enter `https://github.com/klazapp/Unity-Logger-Public.git`.
3. Click `+`, select `Add package from git URL...`, and enter `https://github.com/klazapp/Unity-Singleton-Public.git`.
4. Click `+`, select `Add package from git URL...`, and enter `https://github.com/klazapp/Unity-Inspector-Public.git`.
5. Unity will download and make the package available in your project.

## Usage

To use the `ObjectPooler` script, attach it to any GameObject in the Unity hierarchy. Once attached, the script will automatically draw gizmos around the elements in the Unity Editor, reflecting the current raycast padding settings. These gizmos are only visible in the Unity Editor and do not affect the game's runtime behavior.

## To-Do List (Future Features)

- [ ] Support for non-rectangular UI components.
- [ ] Runtime visualization options for debugging purposes.


## To-Do List (Future Features)
- 

## License
This utility is released under the [MIT License](LICENSE).
