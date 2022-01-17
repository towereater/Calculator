# RPN Calculator
Test project that simulates a RPN calculator.

## Server App
To run the server app, navigate to CalcServer folder and run:
'''bash
dotnet run
'''

The server app waits for incoming requests on port 11000 (localhost) containing a string of code to run throught the Execute function of a RPN calculator class instance. The resulting state of the calculator, X and Y register values in particular, is then returned to the client as a string.

## Client App
To run the client app, navigate to CalcClient folder and run:
'''bash
dotnet run
'''

The client app reads a string of code from the console terminal and sends it to the server listening on port 11000 (localhost). The result of the given line of code is then returned as a string containing the state of the calculator after the operations.

### TODO List
- Async server requests;
- A different calculator for each client with custom initialisation (number of registers);
- More calculator functions;
- JSON formatted data exchanged between client and server;
- IP-port pair as a config or input parameter;