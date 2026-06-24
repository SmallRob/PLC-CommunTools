# PLC-CommunTools

[English](#english) | [中文](#中文)

---

## English

### Overview

PLC-CommunTools is a modern communication debugging/testing tool designed for Windows industrial PLC hardware communication protocols. Built with C# 12 and .NET 8 LTS, it provides comprehensive protocol support, multiple client interfaces, and server-side capabilities with Docker containerization.

### Key Features

- **Protocol Support**: Modbus TCP/RTU, MQTT, Serial Port Communication
- **Multiple Clients**: WPF, WinForm, CLI Terminal
- **Server-Side**: ASP.NET Core Web API, WebSocket, gRPC
- **Docker Deployment**: Multi-container orchestration with Docker Compose
- **MCP Integration**: AI model integration and remote monitoring support
- **Plugin Architecture**: Extensible protocol plugin system

### Quick Start

#### Prerequisites

- .NET 8 SDK
- Docker (optional, for containerized deployment)

#### Using Docker

```bash
# Clone the repository
git clone https://github.com/SmallRob/PLC-CommunTools.git
cd PLC-CommunTools

# Start with Docker Compose
cd docker
docker-compose up -d
```

#### Local Development

```bash
# Build the solution
dotnet build CommunTools.New.sln

# Run the server
dotnet run --project src/Server/Commun.Server.API/Commun.Server.API.csproj

# Run CLI tool
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- --help
```

### CLI Usage

```bash
# Connect to a Modbus device
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- connect --protocol Modbus --address 192.168.1.100 --port 502

# Read register value
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- read --device <device-id> --register 100

# Write register value
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- write --device <device-id> --register 100 --value 12345

# Monitor register changes
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- monitor --device <device-id> --register 100 --interval 1000
```

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/devices` | Get all devices |
| GET | `/api/devices/{id}` | Get device by ID |
| POST | `/api/devices` | Create new device |
| PUT | `/api/devices/{id}` | Update device |
| DELETE | `/api/devices/{id}` | Delete device |
| GET | `/api/protocol/protocols` | Get available protocols |
| POST | `/api/protocol/{deviceId}/connect` | Connect to device |
| POST | `/api/protocol/{deviceId}/disconnect` | Disconnect from device |
| POST | `/api/protocol/{deviceId}/send` | Send command to device |
| GET | `/api/protocol/{deviceId}/status` | Get connection status |

### Project Structure

```
PLC-CommunTools/
├── src/                          # Source code
│   ├── Core/                     # Core protocol libraries
│   │   ├── Commun.Protocols/     # Protocol abstraction layer
│   │   ├── Commun.Modbus/        # Modbus protocol implementation
│   │   ├── Commun.MQTT/          # MQTT protocol implementation
│   │   └── Commun.Common/        # Common utilities
│   ├── Infrastructure/           # Infrastructure layer
│   │   ├── Commun.Data/          # Data access layer (EF Core + SQLite)
│   │   ├── Commun.Logging/       # Logging services
│   │   └── Commun.Configuration/ # Configuration management
│   ├── Server/                   # Server applications
│   │   └── Commun.Server.API/    # ASP.NET Core Web API
│   └── Clients/                  # Client applications
│       └── Commun.Client.CLI/    # CLI terminal
├── tests/                        # Unit and integration tests
├── docker/                       # Docker configuration
├── docs/                         # Documentation
└── old_version/                  # Legacy code (for reference)
```

### Technology Stack

| Category | Technology |
|----------|------------|
| Runtime | .NET 8 LTS |
| Web Framework | ASP.NET Core 8.0 |
| ORM | Entity Framework Core 8.0 |
| Database | SQLite |
| MQTT Client | MQTTnet 5.x |
| CLI Framework | System.CommandLine |
| Containerization | Docker + Docker Compose |
| Reverse Proxy | Nginx |

### Testing

```bash
# Run all tests
dotnet test CommunTools.New.sln

# Run specific test project
dotnet test tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj

# Run with verbose output
dotnet test CommunTools.New.sln --verbosity normal
```

### Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

### Acknowledgments

- Built with [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- MQTT support via [MQTTnet](https://github.com/dotnet/MQTTnet)
- CLI framework by [System.CommandLine](https://github.com/dotnet/command-line-api)

---

## 中文

### 概述

PLC-CommunTools 是一个现代化的通讯调试/测试工具，专为 Windows 工业 PLC 硬件通讯协议而设计。基于 C# 12 和 .NET 8 LTS 构建，提供全面的协议支持、多种客户端接口和服务端功能，支持 Docker 容器化部署。

### 主要特性

- **协议支持**：Modbus TCP/RTU、MQTT、串口通讯
- **多种客户端**：WPF、WinForm、CLI 终端
- **服务端**：ASP.NET Core Web API、WebSocket、gRPC
- **Docker 部署**：使用 Docker Compose 进行多容器编排
- **MCP 集成**：支持 AI 模型集成和远程监控
- **插件架构**：可扩展的协议插件系统

### 快速开始

#### 环境要求

- .NET 8 SDK
- Docker（可选，用于容器化部署）

#### 使用 Docker

```bash
# 克隆仓库
git clone https://github.com/SmallRob/PLC-CommunTools.git
cd PLC-CommunTools

# 使用 Docker Compose 启动
cd docker
docker-compose up -d
```

#### 本地开发

```bash
# 构建解决方案
dotnet build CommunTools.New.sln

# 运行服务端
dotnet run --project src/Server/Commun.Server.API/Commun.Server.API.csproj

# 运行 CLI 工具
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- --help
```

### CLI 使用

```bash
# 连接 Modbus 设备
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- connect --protocol Modbus --address 192.168.1.100 --port 502

# 读取寄存器值
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- read --device <device-id> --register 100

# 写入寄存器值
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- write --device <device-id> --register 100 --value 12345

# 监控寄存器变化
dotnet run --project src/Clients/Commun.Client.CLI/Commun.Client.CLI.csproj -- monitor --device <device-id> --register 100 --interval 1000
```

### API 接口

| 方法 | 端点 | 描述 |
|------|------|------|
| GET | `/api/devices` | 获取所有设备 |
| GET | `/api/devices/{id}` | 根据 ID 获取设备 |
| POST | `/api/devices` | 创建新设备 |
| PUT | `/api/devices/{id}` | 更新设备 |
| DELETE | `/api/devices/{id}` | 删除设备 |
| GET | `/api/protocol/protocols` | 获取可用协议 |
| POST | `/api/protocol/{deviceId}/connect` | 连接设备 |
| POST | `/api/protocol/{deviceId}/disconnect` | 断开设备连接 |
| POST | `/api/protocol/{deviceId}/send` | 向设备发送命令 |
| GET | `/api/protocol/{deviceId}/status` | 获取连接状态 |

### 项目结构

```
PLC-CommunTools/
├── src/                          # 源代码
│   ├── Core/                     # 核心协议库
│   │   ├── Commun.Protocols/     # 协议抽象层
│   │   ├── Commun.Modbus/        # Modbus 协议实现
│   │   ├── Commun.MQTT/          # MQTT 协议实现
│   │   └── Commun.Common/        # 通用工具
│   ├── Infrastructure/           # 基础设施层
│   │   ├── Commun.Data/          # 数据访问层 (EF Core + SQLite)
│   │   ├── Commun.Logging/       # 日志服务
│   │   └── Commun.Configuration/ # 配置管理
│   ├── Server/                   # 服务端应用
│   │   └── Commun.Server.API/    # ASP.NET Core Web API
│   └── Clients/                  # 客户端应用
│       └── Commun.Client.CLI/    # CLI 终端
├── tests/                        # 单元测试和集成测试
├── docker/                       # Docker 配置
├── docs/                         # 文档
└── old_version/                  # 旧版本代码（供参考）
```

### 技术栈

| 类别 | 技术 |
|------|------|
| 运行时 | .NET 8 LTS |
| Web 框架 | ASP.NET Core 8.0 |
| ORM | Entity Framework Core 8.0 |
| 数据库 | SQLite |
| MQTT 客户端 | MQTTnet 5.x |
| CLI 框架 | System.CommandLine |
| 容器化 | Docker + Docker Compose |
| 反向代理 | Nginx |

### 测试

```bash
# 运行所有测试
dotnet test CommunTools.New.sln

# 运行特定测试项目
dotnet test tests/Commun.Protocols.Tests/Commun.Protocols.Tests.csproj

# 运行并显示详细输出
dotnet test CommunTools.New.sln --verbosity normal
```

### 贡献指南

1. Fork 本仓库
2. 创建特性分支 (`git checkout -b feature/amazing-feature`)
3. 提交更改 (`git commit -m 'Add amazing feature'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 创建 Pull Request

### 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件。

### 致谢

- 使用 [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) 构建
- MQTT 支持 via [MQTTnet](https://github.com/dotnet/MQTTnet)
- CLI 框架 by [System.CommandLine](https://github.com/dotnet/command-line-api)
