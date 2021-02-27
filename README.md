## What is Genetic Algorithm Framework
This software is a basic framework for creating genetic, evolutionary, local search, and other heuristic optimisation algorithms.
This project was created as part of my Undergraduate Computational Intelligence  module.

The project demonstrates Genetic, Evolutional, and Local search algorithms for the Travelling Salesman Problem (TSP), and for 1D Multiple-Stock Size Cutting Stock Problem (CSP). 

What is the Travelling salesman problem? https://en.wikipedia.org/wiki/Travelling_salesman_problem

What is the Cutting Stock Problem? https://en.wikipedia.org/wiki/Cutting_stock_problem

This framework can be used to implement new algorithms for TSP or CSP, or extended to other optimisation problems.

## How to run the algorithms
### TSP

To run the console user interface simply run. This will load the default graph.
```
TSP_CLI.exe
```
To specify your own graph, simply append the file path to the above command. For example:
```
TSP_CLI.exe "GraphFiles\test.csv"
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
TSP_CLI.exe "GraphFiles\test.csv" 1 2 3 4 5 6 7 8 9 10 11 12
```
All returns are in human readable format.

### TSP Graph
<img src="https://user-images.githubusercontent.com/45512892/99120792-5a779880-25f3-11eb-82af-0879a582ee93.png" width="50%">

The TSP Graph is functionally similar to the CLI, however, will plot the results on a graph.

Run the `TSP_WPF.exe`
By default, a random graph of 25 nodes is created.
From the UI you can generate a new graph of any size, or load one from file.

### CSV Format for TSP
Graph files can be loaded by either UI option from a CSV format.
Column 1 is the Node ID, Column 2 is the X position, and Column 3 is the Y position.
Lines that do not match this format are ignored.
See provided example files for reference.

### CSP
The process for running CSP is similar to TSP (however there is no graph UI available as of yet for CSP)

Run CSP.exe (optional first argument specifies the problem file path)
From there select an algorithm to run.
There is also a method of performing parametric optimisation however is only currently setup to work for the island population genetic algorithm.
