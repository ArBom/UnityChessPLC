# UnityChessPLC
Implementation of popular game at Unity programming environment.
The project aims to familiarize one with the Unity environment and to teach one the simple communication between PLC and C# program.

![View of scene](https://github.com/user-attachments/assets/25094bb0-609c-4ad7-9f69-d30ec8f07379)
![View of pawn's promotion win](https://github.com/user-attachments/assets/dc434c20-fec3-4ad8-b13a-0601dd24c228)
## Used packages and files source:
* Unity Assets:
  - [https://assetstore.unity.com/packages/2d/textures-materials/real-materials-vol-0-free-115597](https://assetstore.unity.com/packages/2d/textures-materials/real-materials-vol-0-free-115597 "Metals' and stones' materials")
  - [https://assetstore.unity.com/packages/2d/textures-materials/9t5-pbr-textures-freebies-171062](https://assetstore.unity.com/packages/2d/textures-materials/9t5-pbr-textures-freebies-171062 "Grass' material")
  - [https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-sky-61217](https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-sky-61217 "Skybox")

* Font:
  - https://www.1001fonts.com/littlelordfontleroy-font.html

* PLC communication .cs file:
  - https://github.com/fbarresi/Sharp7

* Noises:
  -  https://mixkit.co

## Communication
It's work at thin-client architecture. State of game, rules, etc. are implemented and stored in PC. At this moment possible is sending data of disposition. Every square of the chessboard have unique number (It's shown below). After chessman's move update of chessboard array is send to PLC (DB1).

Squares of the chessboard with unique numbers | Fragment of Array of chessmans at PLC
------------- | -------------
![View of squeres number](https://github.com/user-attachments/assets/aaf8a193-bf17-48dd-b8ed-f8001948bb0f) | ![View of PLC chessmans array](https://github.com/user-attachments/assets/c1b1028d-8396-46da-9e85-5f94891d68b0)

For example:  
   <span style="color:gray">white</span> <span style="color:orange">knight</span> at <span style="color:lightgreen">B</span><span style="color:green">1</span> ≙ 16#<span style="color:blue">44</span> at DB1.Chessman[<span style="color:lime">8</span>]  
    squere <span style="color:lightgreen">B</span><span style="color:green">1</span> ≀column №<span style="color:lightgreen">1</span>, row №<span style="color:green">0</span> → <span style="color:lightgreen">1</span>*8+<span style="color:green">0</span>=<span style="color:lime">8</span>≀ ≙ <span style="color:lime">8</span><sup>th</sup> position at array  
    <span style="color:blue">44</span><sub>16</sub>=68<sub>10</sub>=<span style="color:orange">4</span><sub>10</sub>+<span style="color:gray">64</span><sub>10</sub> ≀<span style="color:orange">4</span>≙<span style="color:orange">KNIGHT</span>, <span style="color:gray">64</span>≙<span style="color:gray">White</span>≀

```csharp
    public enum Color
    {
        Black = 0,
        White = 64
    }
    public enum ChessmanType
    {
        EMPTY = 0,
        PAWN = 1,
        ROOK = 2,
        KNIGHT = 4,
        BISHOP = 8,
        QUEEN = 16,
        KING = 32,
    }
```

## Counterpart at PLC
![PLC chessman](https://github.com/user-attachments/assets/6cb9766b-a602-460d-a220-6cdddea38740)

PLC program is implemented in SCL. Data of chessman are  Chessboard could be rotated, depended on actual player turn. In case of rotate of chessboard the list of chessmans is reversed, letters and numbers on board are replaced with an other ones. LastClicked tag is periodically checking by C# app in time of waiting on PLC-game-player move. After read new value of LastClicked tag (after HMI click) it's set on default value, from outside <0; 63> interval.

![Chequer stack](https://github.com/user-attachments/assets/14a9aa3b-b08f-49ad-af8d-b7ac2f8bc85d)


## Status of project:
In the course of implementation.

### Most important To-Do:
* ~~moving chessmans~~ 
  - ~~*en passant* to add~~
* ~~turn-based game system~~
* ~~counterpart at PLC~~
* communication with PLC
