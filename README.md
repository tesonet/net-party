# NetParty CLI

## How to get it running
1. Checkout the repository
2. Build the project (preferably using Visual Studio)
3. Navigate to the build output directory
    * Most likely `src\NetParty.CLI\bin\Release` or `src\NetParty.CLI\bin\Debug` if you're using Visual Studio 
4. Run `partycli.exe --help` to check whether you're at the right place

## Implementation notes
* The solution aims to decouple application logic from anything presentation related. Therefore `Handlers` layer has no knowledge of how it's results are used.
* Logging was implemented using the `Interceptor` approach. This approach helps make our implementations closed for modification and only open for extention.
* Writing to file was picked as the choice for local persistence due to it meeting current requirements and being easy to implement. Does not scale as well if the persisted data gets more complex.
* When persisting configuration user password is not saved  and the authorization token is saved instead. This is done in orer to maintain a reasonable level of security for the implementation costs.
* External API calls could be implemented in a more maintainable way
* Handling of verbs could be refactored to implement the `Chain of Responsibility` pattern as the amound of options and input combinations grows. This would help isolate each case and make it easier to extend in the future.
