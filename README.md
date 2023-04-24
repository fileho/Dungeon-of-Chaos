# Dungeon-of-Chaos

Dungeon of Chaos is a 2D dungeon crawler game developed in Unity using C#.
The player controls a character that moves through dungeons fighting enemies
and trying to get stronger. The game focuses on three gameplay mechanics —
combat, character progression through statistics, and exploration. The combat is
the core mechanic and the remaining two are supplementing it.

The goal of the game is to finish all dungeons. In order to fulfill the goal,
the player needs to explore new areas and fight the enemies they will encounter.
Killing enemies gives the player experience and the player can strengthen their
character.

## Exploration

The player needs to explore the dungeon in order to progress. They must find
the boss room and since the dungeon is non-linear, it is
impossible to do without exploring. Moreover, the player needs to search
for checkpoints to save their progress and level up if possible.

## Combat

The biggest emphasis is placed on combat. We were trying to create an experience
that requires skill from the player but at the same time keeps the action. It should
be easy to pick up but quite hard to master.
We wanted to achieve skillfulness by giving the player more options on what
to do during combat and giving him at least some time to decide and react, as
opposed to combats that require fast reflexes and speed from the player.

Telegraphing the attacks is done by showing an indicator of the area of effect
in the case of melee attacks or by simple animations in the case of ranged attacks
(such as a slowly appearing projectile). This way is far more readable in 2D where it is hard to show both
the direction and type of the attack by rotating the weapon. Therefore, we are
showing the direction by weapon rotation (both the player and the enemies can
attack in all directions) and the type of attack by the indicator or the simple
animation (this is shown only for the enemies).

## Character Progression

The last part of the core loop is the character progression through statistics, as
is typical for the dungeon crawler genre. In our game, the player can progress
in two systems — stats (such as damage, hp, mana...) and skills (such as a new
type of attack, spell, or a passive skill).
The player can collect consumables that immediately replenish hp, mana, or
stamina, experience points, or items that can be used for resetting points invested
into skills.


The project uses Unity version 2020.3.19f1 LTS
