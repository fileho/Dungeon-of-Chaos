workspace "Dungeon of Chaos workspace" "This workspace documents the architecture of the Dungeon of Chaos software project." {

    model {
        doc = softwareSystem "Dungeon of Chaos"    {
        
            save = container "Save System" "Mechanism for saving the game." {
                saveSlot = component "Save Slot" "Used to determine which save is currently used. Allows multiple saves"
                saveData = component "Save Data" "Saved data." "Represents all the data that needs to be saves"
                savelogic = component "Save Logic" "Handles the saving."
                isavable = component "ISavable" "Savable inferface."
            }
            sound = container "Sound System" "Playing of all sounds." {
                soundpool = component "Sound Pool" "Pool of audio sources for playing sounds. Also contains all sounds for this pool."
                soundlogic = component "Sound System" "Selects the sound and appropriate sound pool."
                sounddef = component "Sound Settings" "Properties based on which we can identify given sound."
                soundtrack = component "Soundtrack" "Plays the soundtrack"
                soundcategories = component "Sound Categories" "All sound categories and sound effects in each of them"
            }
            attack = container "Attacks" "Implements all the possible attack for character and enemies."
            dungeon = container "Dungeon" "The dungeon map using the Hierarchcal Wave Function Collapse algorithm." {
                tilemap = component "Tilemap Generator" "Fills several tilemaps from raw GameObjects."
                group Shadows {
                    wallsShadows = component "Wall Shadows" "Generates shapes around walls for shadows."
                    rockShadows = component "Rock Shadows" "Generates a shadow for each rock."
                }
                idgenerator = component "ID Generator" "Generates a unique id for each savable object."
                group Interactibles {
                    checkpoint = component "Checkpoints" "Serves as a place, where player can increase his stats and save the game."
                    mapFragment = component "Map Fragments" "Reveals part of the minimap."
                    dungeonObjects = component "Dungeon Objects" "Objects that have run-time behavior. Those are traps and torches.
                    trap = component "Traps" "Traps that can hurt the player"
                }
                
            }
            skillsys = container "Skill System" "Implements all characters skills."
            unit = container "Unit" "All entities: character and enemies." {
                unitlogic = component "Unit Logic" "The main logic of the entity."
                movement = component "Movement" "Handles movement and pathfinding."
                stats = component "Stats" "All stats such as HP, mana, and damage."
                bars = component "Bars" "Health bar, mana bar and stanima bar."
                effects = component "Effects" "Effects for damage taken and death."
            }
            hirwfc = container "Hierarchical Wave Function Collapse" "Hierarchical Wave Function Collapse algorithm. Generates raw maps." {
                controller = component "Hierarchical Controller" "Controls the generation, gradually runs each WFC."
                seed = component "Seed" "Generates seeds with a given starting seed."
                layer = component "Layer" "Stores all WFCs in a layer and their dependencies."
                group WFC {
                    wrapper = component "WFC Wrapper" "Unity wrapper around WFC implementations."
                    wfc = component "WFC" "Implementations of wfc"
                    wfcpost = component "WFC Postprocessing" "Custom postprocessing for each WFC."
                    predetermined = component "List of predetermined tiles for the WFC."
                }
                postprocessing = component "Postprocessing" "Any custom postprocessing on the final output"
            }
            
        }
        
        // container dependecies
        unit -> sound "Play SFX"
        unit -> skillsys
        unit -> attack "Possible attacks"
        skillsys -> attack "Some skills are attacks"
        skillsys -> sound
        skillsys -> save "Active skills"
        dungeon -> save "Visited Checkpoints"
        
        // save
        savelogic -> saveSlot
        savelogic -> saveData
        saveData -> isavable
        
        // sounds
        soundlogic -> soundpool "Contains several of them"
        soundlogic -> sounddef "Specifies a sound"
        movement -> soundlogic "Plays SFX"
        attack -> soundlogic "Plays SFX"
        trap -> soundlogic "Plays SFX"
        sounddef -> soundcategories "Picks sound category"
        
        // unit
        unitlogic -> movement
        unitlogic -> stats
        unitlogic -> effects
        movement -> sound
        unitlogic -> attack
        movement -> stats "Gets movement speed"
        stats -> bars "Update bars"
        
        // dungeon
        tilemap -> controller "Extract tiles"
        rockShadows -> tilemap
        wallsShadows -> tilemap
        dungeonObjects -> controller "Extract tiles"
        checkpoint -> soundlogic
        mapFragment -> soundlogic
        dungeonObjects -> soundlogic
        checkpoint -> isavable
        mapFragment -> isavable
        idgenerator -> checkpoint "Assign in-scene unique id"
        idgenerator -> mapFragment "Assign in-scene unique id"
        
        // hwfc
        controller -> seed "Generates new seed for each WFC"
        controller -> layer "Store WFCs"
        layer -> wrapper "Spawns instances of WFC"
        controller -> postprocessing "Export to single layer"
        wrapper -> predetermined 
        wrapper -> wfc "Runs the WFC"
        wrapper -> wfcpost "Runs the Postprocessing on the WFC output"
        predetermined -> wfc "Forwarded to the WFC"
    }
    views   {
        container doc "Dungeon-of-Chaos-container-view" {
            include *
        }

        component unit "Unit-component-view" {
            include *
        }
        
        component dungeon "Dungeon-component-view"{
            include *
        }
        
        component sound "Sound-component-view"{
            include *
        }
        
        component save "Save-component-view"{
            include *
        }
        
        component hirwfc "HWFC-component-view"{
            include *
        }

        theme default
    }

}
