## Input and output format
- Your Program would recieve game information as an arguments upon its start. The given information consists of ```YOUR_ID```, ```OPPONENT_ID``` and ```EMPTY_SPACE_ID``` followed by 2D-array as a series of numbers describes the actual game board.
- The information order as follows:
1. ```YOUR_ID```.
2. ```OPPONENT_ID```.
3. ```EMPTY_SPACE_ID```.
4. A series of (6*7) **42** numbers describes game board.

- Every two consecutive numbers are seperated by a space.
- Your program is expected to output one number between **0 and 6 (inclusive)** indicates the chosen column index (0-indexed) via standard output stream.
- The program should **terminate** after outputting desired column index, or it will be timed-out.
- Your program is **NOT** expected to use standard input stream.

## Examples:
- **Input example:**

If ```YOUR_ID=1```, ```OPPONENT_ID=0```, ```EMPTY_SPACE_ID=2``` and the current board looks like this:

![](https://i.imgur.com/0Mf9hjj.png)

The 2D-array would be
```
2 2 2 2 2 2 2
2 2 2 2 2 0 2
2 2 2 2 2 0 2
2 2 1 2 2 1 1
2 2 0 2 1 1 0
0 2 0 1 0 1 0
```

And the whole input argument as one line would be like this:
```
1 0 2 2 2 2 2 2 2 2 2 2 2 2 2 0 2 2 2 2 2 2 0 2 2 2 1 2 2 1 1 2 2 0 2 1 1 0 0 2 0 1 0 1 0
```
First 3 numbers describes ```YOUR_ID```, ```OPPONENT_ID``` and ```EMPTY_SPACE_ID```. Next 7 numbers describes first row of the board (the top row), next 7 numbers describes second row, and so on.

- This is [**code example**](https://github.com/AhmedOS/Connect-4-Ultimate/blob/master/doc/ai_agent_example.cpp#L223) for an agent written in C++.
- Take a look at [ExternalAI.cs](https://github.com/AhmedOS/Connect-4-Ultimate/blob/master/src/connect4/ExternalAI.cs) class for more information.
