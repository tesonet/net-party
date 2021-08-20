
cd partycli/partycli
$fileExe = "C:\Evaldas Kalvaitis net-party\partycli\partycli\bin\Debug\partycli.exe"

& $fileExe server_list --local
& $fileExe  config --username "tesonet" --password "partyanimal" 
& $fileExe server_list 
& $fileExe  server_list --local
cd ../..