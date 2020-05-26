# ProcessConnectionsManager
Windows Form Application - Allows you to block remote IP addresses that communicate with a process.

# Requirements
You need to install [NMAP](https://nmap.org/download.html) in order to use the UDP listener.

# Bugs
Bugs may be found. Most of the application should work fine. Post an issue if you need something fixed.

# How it works

1. At first you need to specify the process name (or PID for more accuracy).
The program runs 'netstat -nao' in a hidden window in order to find all the open ports and other information for the process.

2. You can't see an IP address for UDP ports because it's a connectionless protocol.
To solve that, the second part of the application allows you to open a UDP packet capturer using [sharppcap](https://github.com/chmorgan/sharppcap) which adds foreign IP addresses associated with the specified port from part 1 as new packets arrive/leave.

3. The third part of the application allows you to block a foreign IP address using the windows firewall API. Basically a new firewall rule is added using the specified Port, Protocol, Foreign IP and sometimes the process file path.
