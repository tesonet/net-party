# Great task for Great .NET Developer

If you found this task it means we are looking for you!

## Few simple steps

1. Fork this repo
2. Do your best
3. Prepare pull request and let us know that you are done

## Simple specification

- Build console app ```partycli.exe``` that will show and save servers received from API:

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

- ```partycli.exe``` for now is a simple console app but it will grow in the near future into enterprise grade cli monster:
1. There might be more parameters for the app.
2. Persistent data store provider/storage type/libraries might change.
3. Servers might be displayed differently in the console or even displayed with colors.
4. Different API might be choosen.

- It should be fairly easy to adapt current app code to the upcoming requirements. So choose your architecture wisely!

How to get servers from API?
- Send authorization request (POST) to http://playground.tesonet.lt/v1/tokens to generate token with body: `{"username": "tesonet", "password": "partyanimal"}`. (Don't forget Content-Type)
- Get servers list from http://playground.tesonet.lt/v1/servers. Add header to this request: `Authorization: Bearer <token>`

## Few simple requirements
- Use C# 7.0+ and .NET 4.6.1+
- Write high quality, beautiful code
- Follow modern .NET development practices:  
  Use dependency injection pattern and use IoC container  
  Use async APIs if available, don't block on async code  
- Implement logging in your app
- Maybe You have an idea how it should interact with users? Do it! Its on you!
- Have fun!

## Few simple recommendations:
- Don't reinvent the wheel! If you find a nice library/framework that can make your life easier use it!
- TDD can be realy useful for this app!

## Bonus points:
- Write unit/integration tests
- Make use of AOP
