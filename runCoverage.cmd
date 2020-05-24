dotnet test --collect:"XPlat Code Coverage" -r "_codeCoverage"
echo "##vso[task.prependpath]$HOME/.dotnet/tools"
reportgenerator -reports:"_codecoverage/35ffd839-2bb9-4c14-9a18-4e1df632357a/coverage.cobertura.xml" -targetdir:"_codecoverage/reports"