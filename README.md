# PLC-CommunTools

> * 此工具针对于Windows工业 **PLC** 硬件通讯协议及基础通讯而编写的通讯调试/测试工具，采用C#8.0语法及.NET 8框架。
> * 此项目支持WinForm、WPF客户端，以及服务端和Docker容器化部署。
> * 本人会把多年从事工业行业的硬件通讯经验体现在工具中，同时有相关行业经验的开发人员也可以提供思路和建议。
> * 如有错误，请不吝赐教。

## 功能特性

- **协议支持**：Modbus TCP/RTU、MQTT、串口通讯等
- **客户端**：WPF、WinForm、CLI终端
- **服务端**：ASP.NET Core Web API、WebSocket、gRPC
- **Docker部署**：支持多容器编排部署
- **MCP集成**：支持AI模型集成和远程监控

## 快速开始

### 使用 Docker 部署

```bash
cd docker
docker-compose up -d
```

### 本地开发

```bash
dotnet build CommunTools.New.sln
dotnet run --project src/Server/Commun.Server.API/Commun.Server.API.csproj
```

### CLI 使用

```bash
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- connect --protocol Modbus --address 192.168.1.100 --port 502
```

## 项目结构

```
PLC-CommunTools/
├── src/
│   ├── Core/              # 核心协议库
│   ├── Clients/           # 客户端应用
│   ├── Server/            # 服务端应用
│   └── Infrastructure/    # 基础设施
├── docker/                # Docker配置
├── tests/                 # 测试项目
└── docs/                  # 文档
```

## 技术栈

- .NET 8 LTS
- ASP.NET Core
- Entity Framework Core + SQLite
- Docker + Docker Compose
- MQTTnet
- System.CommandLine

## 许可证

MIT License
