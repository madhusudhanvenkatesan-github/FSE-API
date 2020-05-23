dotnet test --collect:"XPlat Code Coverage" -r "_codeCoverage"
reportgenerator -reports:"_codecoverage/d854582c-adb9-4fb7-b396-e0703f9545a2/coverage.cobertura.xml" -targetdir:"_codecoverage/reports"