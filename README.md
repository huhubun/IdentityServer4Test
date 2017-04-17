# IdentityServer4Test

## 迁移
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
