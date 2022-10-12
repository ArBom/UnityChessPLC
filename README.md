# UnityChessPLC
Implementation of popular game at Unity programming environment.
The project aims to familiarize one with the Unity environment and to teach one the simple communication between PLC and C# program.

![View of scene](https://db5pap001files.storage.live.com/y4msCMl5ai7r8vyRBozCZU3J-4n8_CDYnh5AdBJpnwl4UdG4CwprKVCoKJR7FsVzpDk8sMNrKmZmfzQz3-VJfsSeYqpZ5J5RvJgTWyvoifnKmDsVUuDG7AOiP5UysabpfeY_54VWHrj2JVBGy-sr7GRFp0uLhYzEg2kRp6JHryhfcxQujecRie4jy_EjQcaBFNQ?width=1861&height=967&cropmode=none)

![View of pawn's promotion win](https://db5pap001files.storage.live.com/y4mnoP4Fwr3km9hiY-U5-I-f5g_Ffk163EQOfejuk5JFCskp5HqFbW-70pUqeEI94hk2Cszk-bk0WySXkh7ex-mERvplFea0AKuwMQ_KP04eGcKYu2QVZc9QHMQyAOp1kMsgHdmsTAQGnyQ5U6Gsi43LSmZlEXQzSUEJyfwzzN9Lhp07eX2hdgGIA072E5G3_ky?width=1909&height=1033&cropmode=none)

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
![View of squeres number](https://db5pap001files.storage.live.com/y4mQODA2IoaUHPxUKx6PpMuLSDKHu8kffW1dii1P6RssqlkNwuZTdXvctREkWD9sWGsK79Uw00ob8itVo1AUJW-_iPO9VUI5B1zoaDnoiJoX4S4EjQxMFbG4Y6fMXQdGP570RZ0UVsMQgJ3oDD7cmMDFtBrsciKECGeUlJ41dqRFXKpaLsjZnoBqU-mM02gwXk1?width=1436&height=719&cropmode=none) | ![View of PLC chessmans array](https://db5pap001files.storage.live.com/y4m2eZR_LiEdzNC2zgoxVA9Rahii0iEfdJxWTS-leRbZuT6AWFyzM4KTOjrAjnwAuxsbzt4AtITXlEGylnM_kQ4xw32ovg6VeYOcU0MD6bv1f9e694JWWIgIjZi9GdLzRBHX1UB3y37e-axpSD6Qd2HE1LqshQ4dOAd-LETwf6iDYhI3IhFLAgwzVcHcrgWk64X?width=739&height=527&cropmode=none)

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

## Status of project:
In the course of implementation.

### Most important To-Do:
* ~~moving chessmans~~ 
  - ~~*en passant* to add~~
* ~~turn-based game system~~
* counterpart at PLC
* communication with PLC
