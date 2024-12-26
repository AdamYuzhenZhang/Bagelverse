# Bagelverse #

- Unity Version: 2022.3.55.f1
- Basic Project Setup: Following this: https://www.youtube.com/watch?v=-tVfg8Zaqos
  - Used URP for portal material
  - Based on Meta XR All-in-One SDK
  - Added a rig, hand tracking, passthrough, and cube interaction
  - Doing everything in the TestScene

### Bagel Portal ###

Here is the bagel portal (Will turn it into a prefab)

- Bagel Portal (Contains a Rig Mirror that calculates and assigns the mirrored transform for the rig)
  - Bagel (The bagel 3D model)
  - Mirrored Objects (Everything in the reflected world)
    - Rig and Scene
   
### Portal Material ###

This tutorial: https://www.youtube.com/watch?v=BXLRprBFfNo

- Place the Mirrored Objects in Bagel Portal in layer StencilMaskn
- In Settings/URP-High Fidelity-Renderer, add the Stencil Mask n following the tutorial

### Reflection Code ###

- RigTracker.cs is attached to MirrorController.
  - It just takes transforms head and hands of the tracked rig.
  - I want it to also take finger tracking data in next step
- RigMirror.cs attached to BagelPortal.
  - It reads tracking data from the RigTracker
  - Then assigns mirrored transforms to the rig in the portal
  - Add more features here: body, fingers, legs, etc.

### A Fully Rigged Character (In Progress) ###

This tutorial https://www.youtube.com/watch?v=v47lmqfrQ9s

I'm thinking about having a fully rigged character in the reflection. A character is imported in the TestScene but disabled for now.

### Grabbing Bagel (Not Started) ###

Probably will do the following

- Add collision spheres to the reflected hands
- When button down, or pinch, grab donut in reflection.

