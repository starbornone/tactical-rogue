# Tactical Rogue

Tactical Rogue is a tactical game where characters engage in simultaneous, grid-based combat and explore procedurally generated environments. This will use a flexible, component-oriented system to encourage emergent gameplay. The aim is to allow characters to interact with both enemies and the terrain in creative ways.

## Features

(Everything is a TODO at this stage.)

### Combat

- **Simultaneous Turns:** Actions take a specific number of time-units (TUs), allowing strategic play where faster actions can sometimes outpace slower ones.
- **Flexible Action System:** Characters can perform actions independent of rigid context; attack empty tiles, move through dangerous areas, or swing at enemies with dynamic action possibilities.
- **Dice-Pool Combat:** Melee attacks and dodges are determined using a dice-pool system. Characters roll for successes, with additional mechanics for dodging, armor penetration, and damage calculation.
- **Conditions and Complications:** Combat includes a system for adding conditions (such as "off-guard" or "clumsy") to characters, with possibilities for critical failures leading to more dire consequences.
- **Auto-Explore Feature:** An auto-explore system powered by the A* algorithm enables characters to navigate efficiently through levels.

### Movement and Terrain

- **Grid-Based Movement:** Navigate across a tactical grid with freedom to interact with the environment in a meaningful way.
- **Layered Terrain:** Terrain generation features stacked "slabs" that represent geological layers, creating diverse battle maps.
- **Emergent Tile Interactions:** Tiles have material properties, allowing for environmental effects like melting ice or flowing water, opening possibilities for emergent tactical scenarios.

### UI and Exploration

- **Combat and Exploration UI:** Characters can select from a variety of actions in combat, with future plans for complex combat maneuvers.
- **In-Combat Detection:** Systems differentiate between combat and non-combat states, affecting actions like health regeneration.
- **Paper-Doll Equipment System:** A detailed equipment system that reflects player gear both mechanically and visually in-game.

### Tile Generation

- **Slab-Based Tile System:** Terrain is generated using layered slabs instead of cubic tiles, enabling more natural-looking environments and dynamic elevation changes.
- **Environmental Properties:** Each tile prefab has material properties, opening up the potential for environmental interactions like melting snow or collapsing terrain.

## Roadmap

- Implement turn-based time-unit system for action prioritization.
- Expand the dice-pool combat system with additional skills, critical successes, and failures.
- Introduce equipment management and paper-doll UI.
- Develop additional emergent terrain interactions, including environmental hazards and effects.
- Add more terrain generation features such as inclines and diverse biomes.
