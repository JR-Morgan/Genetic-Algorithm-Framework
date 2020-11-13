### What is the Travelling salesman problem?
https://en.wikipedia.org/wiki/Travelling_salesman_problem

### Console UI
<img src="https://user-images.githubusercontent.com/45512892/99120792-5a779880-25f3-11eb-82af-0879a582ee93.png" width="50%">.

To run the console user interface simply run. This will load the default graph.
```
ConsoleUI.exe
```
To specify your own graph, simply append the file path to the above command. For example:
```
ConsoleUI.exe "GraphFiles\test.csv"
```

The program will print a list of searches.
```
Select search strategy:
(0): Exhaustive Search
(1): Random Search
(2): Local Search - Random initialisations
(3): Local Search - Greedy
(4): Evolutionary Search - Tournament
```
Enter the prefixed number to start that search.
Note: Exhaustive search is not suitable for graphs over around 10 nodes as it of O(n!) complexity.

To simply evaluate the cost of a route, enter both the file path, and a complete route. For example:
```
ConsoleUI.exe "GraphFiles\test.csv 1 2 3 4 5 6 7 8 9 10 11 12"
```
All returns are in human readable format.

### WPFUI
The WPF UI is functionally similar to the console UI, however, will plot the results on a graph.

Run the `WPFUI.exe`
By default, a random graph of 25 nodes is created.
From the UI you can generate a new graph of any size, or load one from file.

### CSV Format
Graph files can be loaded by either UI option from a CSV format.
Column 1 is the Node ID, Column 2 is the X position, and Column 3 is the Y position.
Lines that do not match this format are ignored.
See provided example files for reference.
