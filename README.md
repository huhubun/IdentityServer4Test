# IdentityServer4Test

## 迁移
### dotnet ef 命令
为了能在 Visual Studio 的“程序包管理器控制台”之外进行迁移，需要使用 `dotnet ef` 命令，使用该命令需要在项目文件（`.csproj`）中添加包 `Microsoft.EntityFrameworkCore.Tools.DotNet` 的引用：
```xml
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.1" />
  </ItemGroup>
```
此后，在 cmd 或 Powershell 中，处在与 `Server.Admin.csproj` 文件同级的目录下，就可以使用 `dotnet ef` 命令了。

### IdentityServer
IdentityServer4 添加迁移，参见官方文档 [Using EntityFramework Core for configuration data](https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html)，节选如下：
```powershell
Add-Migration InitialIdentityServerPersistedGrantDbMigration -Context PersistedGrantDbContext -OutputDir Migrations/IdentityServer/PersistedGrantDb
Add-Migration InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext -OutputDir Migrations/IdentityServer/ConfigurationDb
```
IdentityServer 带有两个 DbContext，需要通过 `-Context` 进行指定，`-OutputDir` 用于指定迁移文件生成的目录。  
如果是使用 `dotnet ef` 命令部分参数有所不同，例如是通过 `--context` 来指定 DbContext。

### 其他 DbContext
如果除了 IdentityServer 之外，还有自己的 DbContext，使用方式完全没有区别，试验了 ASP.NET Core Identity，默认配置好后为手动迁移
```powershell
Add-Migration ImportASPNETCoreIdentity -Context MyContext -OutputDir Migrations/MyContextEntity
Update-Database -Context MyContext
```

### 迁移脚本
通过 `dotnet ef` 命令可以生成迁移脚本，增加参数 `--idempotent` 以导出适用于各个不同迁移版本数据库的 SQL 脚本（例如：SQL 脚本会增加判断该表是否存在）
```powershell
dotnet ef migrations script --idempotent --context MyContext --output ./DbScripts/DbScript.sql
```
可参见 `Server.Admin` 项目下的 `GenerateDbMigrationScript.bat` 批处理文件。

## Authentication 认证

## Authorization 授权
IdentityServer4 Document: [Token Endpoint](https://identityserver4.readthedocs.io/en/release/endpoints/token.html)  
**GET** */.well-known/openid-configuration* 以获得支持的操作  

### Client Credentials Grant
1. 启动 Server 项目（默认为 http://localhost:5000）
2. **POST** */connect/token* 
   * Content-Type: application/x-www-form-urlencoded
   * client_id
   * client_secret
   * grant_type = client_credentials
   * [scope]
```
POST http://localhost:5000/connect/token HTTP/1.1
Content-Type: application/x-www-form-urlencoded
Accept: */*
Host: localhost:5000

client_id=client&client_secret=secret&grant_type=client_credentials&scope=api1
```
3. 获得响应
```json
{
  "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZhMGE5OTU2NzE0NTU4NmJlMzI3YjVkNWNhN2MwMWUxIiwidHlwIjoiSldUIn0.eyJuYmYiOjE0OTMyMTc5ODQsImV4cCI6MTQ5MzIyMTU4NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6ImNsaWVudCIsInNjb3BlIjpbImFwaTEiXX0.mm1U4E633tEfuxYZrC-MKg-XYIqJHeKWRRV5TbBlGnwK6pPmgmPbjzG4LCwpOQEhTHi_sYJwrBOM-2b2JM49VwO82ZOlKivyTLj4JuiuK4tcg71w4-QJod4vVgp6CE2T99sRWQen7utuxAyh56JW2GpwnmYZhvwT957BXcxdVzvW9Cq66bjMZErwPKQIK0stG0DqgeuTATCn3-vm0oGp_KJVDqGvPZruayjgHoG4XCRm15ds69gAtvMhMpXDtN_WMgZvA5xZLltz8ercZw7CAtBOvenqLY2kly05KU8fsQmN1EYueaNP9oxNyPB3FRVv8l2ZH4f9FNpjgsVZLHwGaA",
  "expires_in": 3600,
  "token_type": "Bearer"
}
```
