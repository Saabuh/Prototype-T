# Worklog


## Bugs
- [ ] dropping items between inventory slots causes multiple raycasts hits(?)
- [ ] adding inventory items doesnt handle full inventories properly (pass by reference as a solution? method overload for 2 versionf of AddItem if we care/dont care about remainder)
- [ ] considering moving the inputReader script onto a seperate InputReader object. reasoning is that we are attaching the UIManager to an instance of the inputReader component on the player. If the player dies (gameObject deleted),
so does the input reader temporarily.
- [x] [dragging an item and then closing the ui bugs out, item freezes on current mouse position on next inventory toggle](https://github.com/Saabuh/Prototype-T/issues/1)
- [x] [Visual duplicate item bug with inventory toggling](https://github.com/Saabuh/Prototype-T/issues/2)

# log

## 07-08-2025
- [x] refactor inventory to hold item instances instead of itemData scriptableObjects (scriptableObjects should be immutable data containers, thus not a runtime asset that is modified in game)
    - not as much work as it sounds like, most logic is based around itemslots, not the itemData. Can just replace itemData with iteminstance and fix itemcontainer/ui logic
- [ ] add collidable walls, spawned using procedural generation
- [ ] implement basic enemy spawning and hit registers

## 04-08-2025
- [x] download sample item assets 
- [ ] show short form tooltip when inventory ui is not toggled
    - have 2 tooltip datas, one short form one long form, display based on onInventoryToggle event

## 01-08-2025
- [x] prevent double inputs, such as clickin performing attacking/pressing ui in game at the same time
- [x] implement item use logic, Action Strategies for different items (mining, weapon, block)

## 21-07-2025
- [x] Replace onInventoryToggle event handler with custom event handler
- [x] seperate inventory and hotbar, make hotbar always displayed vs show inventory on press
- [ ] move the onSelectedSlotChanged listeners from the inventoryUIController to the itemSlotUI
- [ ] move the updateSlotUI listeners from the inventoryUIController to the itemSlotUI, allows for more modularity when building different UI?

## 10-07-2025
- [x] add itemslot selection

## 09-07-2025
- [x] refactor inventory to be instantiated instead of hard coded via ScriptableObjects
- [ ] research more into attack system architecture/implementation
- [ ] map out Custom Event System to understand it better

## 09-07-2025
- [ ] try to get a better understanding on the formula behind island formation using square bump/euclidean
- [x] change starter map into an island

## 07-07-2025
- [x] implement different biome generation based on moisture, temperature

## 04-07-2025
- [x] add new biome tilesets
- [x] implement interactive noise visualizer

## 03-07-2025
- [x] research more into procedural world generation/biome generation
    - looking into cellular automata, and stitching together a perlin noise map with a cellulor automata map (wave function collapse maybe?)

## 02-07-2025
- [x] research more into procedural world generation/biome generation
    - looking into cellular automata, and stitching together a perlin noise map with a cellulor automata map (wave function collapse maybe?)

## 02-07-2025
- [x] research more into procedural world generation/biome generation
    - looking into cellular automata, and stitching together a perlin noise map with a cellulor automata map (wave function collapse maybe?)

## 02-07-2025
- [x] add a custom Debug class for runtime enabling of debugging lines
- [x] fix minor "teleport" bug of ui showing/hiding
- [ ] research more into procedural world generation/biome generation
    - looking into cellular automata, and stitching together a perlin noise map with a cellulor automata map (wave function collapse maybe?)
- [ ] research more into attack system architecture/implementation

## 01-07-2025
- [x] refactor tooltipsystem into a singleton

## 30-06-2025
- [x] refactor tooltipsystem into a singleton

## 29-06-2025
- [x] add item tooltip ui
- [ ] swap the item data, not the actual item slot for less buggy referencing?

## 29-06-2025
- [x] add item tooltip ui

## 28-06-2025
- [x] download terraria, corekeeper for referencing

## 27-06-2025
- [ ] improve DragStateManager's Update() logic for holding detection, setting isHolding to true in update() is prone to bugs
- [x] change dragging out of inventory into click and click again to drop

## 26-06-2025
- [x] [Visual duplicate item bug with inventory toggling](https://github.com/Saabuh/Prototype-T/issues/2)
- [x] [dragging an item and then closing the ui bugs out](https://github.com/Saabuh/Prototype-T/issues/1)

## 25-06-2025
- [x] add pickup to world entities
- [x] add toggling inventory ui
- [x] remove singleton from PlayerController
    - this was instead modified to handle multiplayer players. Will add a PlayerManager later on. 

## 24-06-2025
- [x] add basic drop system
- [x] add item template prefab

## 23-06-2025
- [x] finish itemcontainer logic

## 23-06-2025
- [x] adding inventory visuals/ui
- [x] finish itemcontainer logic

## 21-06-2025
- [ ] try out these games:
    - [ ] Necesse
    - [ ] Tinkerlands
    - [ ] Fields of Mistra
    - [ ] Forsaken Isle
- [x] adding event system
- [x] adding inventory visuals/ui

## 20-06-2025
- [x] adding inventory visuals/ui
- [x] retry inventory system implementation

## 19-06-2025 (Reviving the project)
- [x] retry inventory system implementation

## 31-01-2025
- [x] start inventory system
- [ ] continue with attack strategies
    - any entity (player, enemy, npc, environment) can have an attack strategy
    - should be able to combine any weaponData with any attackStrategy and have it not break(?)

## 29-01-2025
- [x] add tree colliders

## 21-01-2025
- [x] generating sample trees on terrain
- [x] fix tree sorting layers

---

Just a note:
"solve with your fingers"
really like the "f*ck it up all the way and then do it for real" approach to creating stuff
90% of the time it's faster to f*ck up and know why than it is to think about all the ways you might f*ck up
a

100% agree: I code like I write my music. I dont sit and make notes on paper, I sit at the synth and tinker, make some sounds, 
twiddle knobs, play some chords, feel the music, keep it or trash it, get feedback and iterate. totally get your jam. im with you.
