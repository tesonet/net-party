# partycli.exe

## Workflow

This should store username and password for API authorization in the persistent data store:
```
partycli.exe config --username "YOUR USERNAME" --password "YOUR PASSWORD" 
``` 

This should fetch servers from API, store them in persistent data store and display server names and total number of servers in the console:
```
partycli.exe server_list 
``` 

This should fetch servers from persistent data store and display server names and total number of servers in the console:
```
partycli.exe server_list --local
```

## Structure

NetParty - project that contains console application source

NetParty.Tests - project that contains unit tests for said application

NetParty.IntegrationTest - project that contains full integration tests for whole workflow