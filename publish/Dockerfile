# 基于 dotnet core 镜像
FROM microsoft/dotnet:core

# 拷贝当前（.）所有文件到 docker 容器的 publish 文件夹下
COPY . /publish

# 设置 docker 工作目录为 publish 文件夹
WORKDIR /publish

# 设置 docker 容器对外公开 25000 端口
EXPOSE 25000

# 设置 asp.net core 环境为 Staging
ENV ASPNETCORE_ENVIRONMENT Staging

# 使用 dotnet Server.Admin.dll 来启动程序
CMD ["dotnet", "Server.Admin.dll"]