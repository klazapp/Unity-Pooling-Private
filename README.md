# Object Pooler for Unity

## Introduction

The Object Pooler package under the `com.Klazapp.Utility` namespace provides an efficient pooling system for Unity, designed to manage and reuse instances of game objects, especially beneficial in games with frequent spawn and destroy actions. This system helps to optimize performance by reducing the overhead associated with creating and destroying objects.

## Features

- **Efficient Object Management:** Reduces the overhead of frequently spawning and destroying objects by reusing existing instances from the pool.
- **Automatic Pool Expansion:** Automatically expands the pool size if the current pool does not meet demand.
- **Singleton Implementation:** Optionally, the object pooler can be implemented as a singleton or persistent singleton, ensuring that only one instance exists throughout the game's lifecycle.
- **Customizable Pooling Components:** Allows for detailed configuration of pooled objects, including initial pool size and object references.

## Dependencies

- **Unity 2017.1 or Newer:** Required for the latest MonoBehaviour, GameObject, and serialization API features.
- **Unity Mathematics Package:** Utilized for mathematical operations within the pooling logic.

## Compatibility

Designed to work universally across all Unity projects, regardless of the rendering pipeline or platform.

| Compatibility | URP | BRP | HDRP |
|---------------|-----|-----|------|
| Compatible    | ✔️   | ✔️   | ✔️    |

## Installation

1. Download the Object Pooler package from the [GitHub repository](https://github.com/klazapp/Unity-Object-Pooler-Public.git) or via the Unity Package Manager.
2. Add the scripts to your Unity project within any scripts directory.

## Usage

To utilize the object pooling system, reference the `ObjectPooler` instance in your scripts. Here's how you can retrieve an object from the pool and return it:

```csharp
var pooledObject = ObjectPooler.Instance.GetPool(somePrefab);
// Use the pooled object in your game

// Once done, return it to the pool
ObjectPooler.Instance.ReturnPool(pooledObject);
```

## To-Do List (Future Features)

- [ ] Implement thread-safe operations to enhance reliability in multi-threaded scenarios.
- [ ] Provide a user interface for managing and monitoring pool states directly within the Unity Editor.
- [ ] Expand the utility to support pooling of non-GameObject resources like textures or audio clips.

## License

This utility is released under the MIT License, allowing for free use, modification, and distribution within your projects.

---
