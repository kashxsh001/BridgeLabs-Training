# Turn-Based Multiplayer Game Manager

## Problem Statement

Build a turn-based multiplayer game manager supporting player turns, moves, matchmaking, replay functionality, player statistics, sorting, and searching.

---

## Features

### 1. Player Turns – Circular Linked List

The game maintains player turns using a Circular Linked List.

Features:

- Maintains circular turn order
- Moves to the next player
- Supports skipping a player
- Supports reversing the direction of turns
- Handles the two-player game scenario

Example:

Alice → Bob → Charlie → David → Alice

---

### 2. Move History – Stack

Moves are stored using a Stack.

Features:

- Stores each move made by a player
- Supports Undo
- Supports Replay/Redo of an undone move
- Handles Undo when no moves are available

---

### 3. Matchmaking – Queue

Players waiting for a match are managed using a Queue.

Features:

- Players join in FIFO order
- The first two available players are matched
- Handles an empty queue
- Handles insufficient players

Example:

Alice → Bob → Charlie → David

Matches:

Alice vs Bob  
Charlie vs David

---

### 4. Game Log – Doubly Linked List

Game events are stored using a Doubly Linked List.

Features:

- Stores game history
- Navigate forward through logs
- Navigate backward through logs
- Supports replay functionality

---

### 5. Player Data – Dictionary

Player data is stored using a Dictionary.

Mapping:

PlayerID → Player / Player Statistics

Features:

- Fast player lookup using Player ID
- Stores player score
- Stores games played
- Stores games won
- Stores player inventory

---

### 6. Leaderboard – Sorting

Players can be sorted based on their score.

The leaderboard displays players from highest score to lowest score.

Example:

1. Charlie - 160  
2. Alice - 150  
3. Bob - 110  
4. David - 90

---

### 7. Player Search – Binary Search

The system supports searching for a player using Binary Search.

Players are sorted by Player ID before performing the search.

---

## Data Structures Used

| Requirement | Data Structure |
| Player Turns | Circular Linked List |
| Move History | Stack |
| Matchmaking | Queue |
| Game Log | Doubly Linked List |
| Player Lookup | Dictionary |
| Leaderboard | List + Sorting |
| Player Search | List + Binary Search |

---

## Main Classes

### GameManager

The central class that integrates all data structures and game operations.

Responsibilities:

- Manage players
- Manage turns
- Make moves
- Undo and replay moves
- Handle matchmaking
- Update player statistics
- Maintain game logs
- Sort leaderboard
- Search players

### Player

Represents a player in the game.

Properties:

- PlayerId
- Name
- Stats

### PlayerStats

Stores player-related statistics.

Properties:

- Score
- GamesPlayed
- GamesWon
- Inventory

### Move

Represents a move made during the game.

Properties:

- Player
- Description
- ScoreChange

### GameLog

Represents an event stored in the game history.

Properties:

- Message
- Time

---

## Edge Cases Handled

- Two-player game
- Skip player
- Reverse direction
- Empty matchmaking queue
- Insufficient players for matchmaking
- Undo when no moves exist
- Replay when no moves exist
- Player not found
- Invalid winner
- Null player validation

---

## Complexity Analysis

| Operation | Data Structure | Time Complexity |
|---|---|---|
| Next Turn | Circular Linked List | O(1) |
| Skip Player | Circular Linked List | O(1) |
| Reverse Direction | Boolean Flag | O(1) |
| Add Move | Stack | O(1) |
| Undo Move | Stack | O(1) |
| Replay Move | Stack | O(1) |
| Join Matchmaking | Queue | O(1) |
| Match Players | Queue | O(1) |
| Player Lookup | Dictionary | O(1) average |
| Add Game Log | Doubly Linked List | O(1) |
| Replay Forward/Backward | Doubly Linked List | O(1) |
| Sort Leaderboard | List | O(n log n) |
| Binary Search Player | Sorted List | O(log n) |

---

## Testing

The project uses NUnit for unit testing.

Test cases cover:

- Adding players
- Circular turn movement
- Skip player
- Reverse direction
- Making moves
- Undo functionality
- Replay functionality
- Matchmaking
- Player lookup
- Sorting leaderboard
- Binary search
- Edge cases

The project contains at least 10 NUnit tests, including integrated workflows and invalid scenarios.

---

## Technologies Used

- C#
- .NET
- NUnit
- Generic Collections
- Circular Linked List
- Doubly Linked List
- Stack
- Queue
- Dictionary

---

## How to Run

1. Clone the repository.

2. Open the solution in Visual Studio.

3. Build the solution.

4. Run the main project.

5. Run unit tests using Test Explorer.

---
