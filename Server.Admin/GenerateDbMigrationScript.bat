dotnet ef migrations script --idempotent --context PersistedGrantDbContext --output ./DbScripts/PersistedGrantDbContextScript.sql
dotnet ef migrations script --idempotent --context ConfigurationDbContext --output ./DbScripts/ConfigurationDbContextScript.sql
dotnet ef migrations script --idempotent --context MyContext --output ./DbScripts/MyContextScript.sql