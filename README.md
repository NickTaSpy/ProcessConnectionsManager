# ProcessConnectionsManager
Windows Form Application - Allows you to block remote IP addresses that communicate with a process

# Requirements
You need to install [WinPcap](https://www.winpcap.org/) in order to use the UDP listener.

# Bugs
Bugs may be found, the application works generally speaking but I haven't tried it in real scenarios yet.

# How it works

1. At first you need to specify the process name (or PID for accuracy).
The program runs 'netstat -nao' in a hidden window in order to extract all the open ports, their respective PIDs and other information.

2. UDP never has a remote IP address to show you since it's a connectionless protocol.
To solve that, the second part of the application allows you to open a UDP packet capturer using PcapDotNet (.NET wrapper for WinPcap)
which adds foreign IP addresses associated with the specified port from part 1 as new packets arrive/leave.

3. The third part of the application allows you to block a foreign IP address using the windows firewall API. Basically a new firewall rule
is added using the specified Port, Protocol, Foreign IP and sometimes the process file path.
