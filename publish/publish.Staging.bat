cd ..
dotnet restore

cd .\Server.Admin
rmdir "./bin/staging-publish" /S /Q
dotnet publish Server.Admin.csproj --configuration Release --output .\bin\staging-publish

copy ..\publish\Dockerfile .\bin\staging-publish\Dockerfile