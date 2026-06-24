# PLC-CommunTools 重构设计文档

## [S1] 项目概述

### 项目背景
PLC-CommunTools 是一个用于 Windows 工业 PLC 硬件通讯协议及基础通讯的调试/测试工具。当前项目基于 .NET Framework 4.8，包含 WinForm 和 WPF 两个客户端版本。

### 重构目标
1. **协议层现代化**：将协议层封装为独立服务库，供 WPF 和 WinForm 引用
2. **客户端现代化**：创建全新现代化客户端，支持 WPF 和 WinForm
3. **服务端功能**：创建独立服务端，支持远程通讯和数据管理
4. **Docker 容器化**：支持多容器编排部署
5. **CLI 终端**：新增独立 CLI 终端，支持设备调试、服务管理、数据采集
6. **MCP 集成**：支持 AI 模型集成和远程监控

### 技术栈
- **目标框架**：.NET 8 (LTS)
- **客户端 UI**：WPF (推荐) + WinForm
- **服务端架构**：混合架构 (ASP.NET Core Web API + WebSocket + gRPC)
- **数据存储**：SQLite (轻量)
- **通讯协议**：全部协议支持，插件化协议配置和加载
- **部署方式**：Docker 多容器编排

## [S2] 架构设计

### 整体架构图
```
┌─────────────────────────────────────────────────────────────┐
│                    客户端层 (Clients)                        │
├─────────────┬─────────────┬─────────────┬──────────────────┤
│  WPF Client │ WinForm CLI │  CLI Tool   │   MCP Client     │
│  (.NET 8)   │  (.NET 8)   │  (.NET 8)   │   (.NET 8)       │
└──────┬──────┴──────┬──────┴──────┬──────┴────────┬─────────┘
       │             │             │               │
       └─────────────┴──────┬──────┴───────────────┘
                            │
                    ┌───────▼───────┐
                    │   协议层      │
                    │  (Protocols)  │
                    └───────┬───────┘
                            │
┌───────────────────────────┼─────────────────────────────────┐
│                    服务端层 (Server)                         │
├─────────────┬─────────────┼─────────────┬──────────────────┤
│  Web API    │  WebSocket  │    gRPC     │   MCP Server     │
│  (RESTful)  │  (Real-time)│  (Internal) │   (AI/Monitor)   │
└──────┬──────┴──────┬──────┴──────┬──────┴────────┬─────────┘
       │             │             │               │
       └─────────────┴──────┬──────┴───────────────┘
                            │
                    ┌───────▼───────┐
                    │   数据层      │
                    │  (SQLite)     │
                    └───────────────┘
```

### 分层架构

#### 1. 核心层 (Core)
- **Commun.Protocols**：协议抽象层，定义协议接口
- **Commun.Modbus**：Modbus 协议实现
- **Commun.MQTT**：MQTT 协议实现
- **Commun.Serial**：串口通讯实现
- **Commun.Common**：通用工具类

#### 2. 客户端层 (Clients)
- **Commun.Client.WPF**：WPF 客户端，MVVM 架构
- **Commun.Client.WinForms**：WinForm 客户端
- **Commun.Client.CLI**：命令行终端工具

#### 3. 服务端层 (Server)
- **Commun.Server.Core**：服务端核心逻辑
- **Commun.Server.API**：ASP.NET Core Web API
- **Commun.Server.WebSocket**：WebSocket 服务
- **Commun.Server.gRPC**：gRPC 服务（内部通讯）

#### 4. MCP 集成层
- **Commun.MCP.Server**：MCP 服务器实现
- **Commun.MCP.Client**：MCP 客户端库

#### 5. 基础设施层 (Infrastructure)
- **Commun.Data**：数据访问层（SQLite）
- **Commun.Logging**：日志服务
- **Commun.Configuration**：配置管理

## [S3] 协议层设计

### 协议抽象接口
```csharp
public interface IProtocol : IDisposable
{
    string Name { get; }
    string Version { get; }
    bool IsConnected { get; }
    
    Task<bool> ConnectAsync(ProtocolConfig config);
    Task DisconnectAsync();
    Task<ProtocolResponse> SendAsync(ProtocolRequest request);
    
    event EventHandler<ProtocolEventArgs> DataReceived;
    event EventHandler<ProtocolErrorEventArgs> ErrorOccurred;
}

public abstract class ProtocolBase : IProtocol
{
    // 基础实现
}
```

### 插件化协议加载
```csharp
public interface IProtocolPlugin
{
    string ProtocolName { get; }
    IProtocol CreateProtocol();
    ProtocolMetadata GetMetadata();
}

public class ProtocolPluginManager
{
    private readonly Dictionary<string, IProtocolPlugin> _plugins;
    
    public void LoadPlugins(string pluginDirectory);
    public IProtocol CreateProtocol(string protocolName);
    public IEnumerable<ProtocolMetadata> GetAvailableProtocols();
}
```

### Modbus 协议实现
```csharp
public class ModbusProtocol : ProtocolBase
{
    // 保留现有 ModbusTcpHelper 的核心逻辑
    // 重构为异步模式
    // 添加连接池支持
    // 添加配置验证
}
```

## [S4] 客户端设计

### WPF 客户端 (MVVM)
```
Commun.Client.WPF/
├── Views/
│   ├── MainWindow.xaml
│   ├── ConnectionView.xaml
│   ├── DeviceExplorerView.xaml
│   └── DataMonitorView.xaml
├── ViewModels/
│   ├── MainViewModel.cs
│   ├── ConnectionViewModel.cs
│   └── DeviceViewModel.cs
├── Models/
│   ├── Device.cs
│   └── Connection.cs
└── Services/
    ├── NavigationService.cs
    └── DialogService.cs
```

### WinForm 客户端
- 保留现有 UI 风格
- 重构为异步模式
- 统一配置管理

### CLI 终端
```
Commun.Client.CLI/
├── Commands/
│   ├── ConnectCommand.cs
│   ├── ReadCommand.cs
│   ├── WriteCommand.cs
│   └── MonitorCommand.cs
├── Services/
│   ├── DeviceService.cs
│   └── ConfigService.cs
└── Program.cs
```

**CLI 功能**：
- `comm connect <protocol> <address>` - 连接设备
- `comm read <register> <length>` - 读取寄存器
- `comm write <register> <value>` - 写入寄存器
- `comm monitor <register>` - 监控寄存器变化
- `comm server start` - 启动服务端
- `comm config list` - 查看配置

## [S5] 服务端设计

### ASP.NET Core Web API
```csharp
[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Device>> GetDevices();
    
    [HttpPost("{id}/connect")]
    public async Task<IActionResult> ConnectDevice(string id);
    
    [HttpPost("{id}/read")]
    public async Task<ReadResponse> ReadRegister(string id, ReadRequest request);
    
    [HttpPost("{id}/write")]
    public async Task<IActionResult> WriteRegister(string id, WriteRequest request);
}
```

### WebSocket 服务
```csharp
public class DeviceWebSocketMiddleware
{
    public async Task InvokeAsync(HttpContext context);
    
    // 实时数据推送
    // 设备状态监控
    // 告警通知
}
```

### gRPC 服务（内部通讯）
```protobuf
service DeviceService {
    rpc Connect (ConnectRequest) returns (ConnectResponse);
    rpc ReadRegister (ReadRequest) returns (ReadResponse);
    rpc WriteRegister (WriteRequest) returns (WriteResponse);
    rpc StreamData (StreamRequest) returns (stream DataPoint);
}
```

## [S6] MCP 集成设计

### MCP 服务器实现
```csharp
public class CommunMcpServer : IMcpServer
{
    // 实现 MCP 协议
    // 暴露设备操作接口
    // 支持 AI 模型调用
}
```

### MCP 工具定义
```json
{
  "tools": [
    {
      "name": "read_register",
      "description": "读取 PLC 寄存器值",
      "inputSchema": {
        "type": "object",
        "properties": {
          "device_id": { "type": "string" },
          "register": { "type": "integer" },
          "length": { "type": "integer" }
        }
      }
    },
    {
      "name": "write_register",
      "description": "写入 PLC 寄存器值",
      "inputSchema": {
        "type": "object",
        "properties": {
          "device_id": { "type": "string" },
          "register": { "type": "integer" },
          "value": { "type": "integer" }
        }
      }
    }
  ]
}
```

## [S7] 数据存储设计

### SQLite 数据库
```sql
-- 设备配置表
CREATE TABLE Devices (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Protocol TEXT NOT NULL,
    Address TEXT NOT NULL,
    Port INTEGER,
    Config TEXT,  -- JSON 格式配置
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 连接历史表
CREATE TABLE ConnectionHistory (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DeviceId TEXT,
    ConnectedAt DATETIME,
    DisconnectedAt DATETIME,
    Status TEXT,
    FOREIGN KEY (DeviceId) REFERENCES Devices(Id)
);

-- 数据记录表
CREATE TABLE DataRecords (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DeviceId TEXT,
    Register INTEGER,
    Value TEXT,
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (DeviceId) REFERENCES Devices(Id)
);
```

## [S8] Docker 部署设计

### docker-compose.yml
```yaml
version: '3.8'

services:
  server:
    build:
      context: .
      dockerfile: docker/server.Dockerfile
    ports:
      - "5000:5000"   # Web API
      - "5001:5001"   # WebSocket
      - "5002:5002"   # gRPC
    volumes:
      - server-data:/app/data
      - server-logs:/app/logs
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Data Source=/app/data/commun.db
    restart: unless-stopped

  mcp-server:
    build:
      context: .
      dockerfile: docker/mcp.Dockerfile
    ports:
      - "3000:3000"
    depends_on:
      - server
    environment:
      - MCP_SERVER_URL=http://server:5000
    restart: unless-stopped

  nginx:
    image: nginx:alpine
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./docker/nginx/nginx.conf:/etc/nginx/nginx.conf
      - ./docker/nginx/ssl:/etc/nginx/ssl
    depends_on:
      - server
      - mcp-server
    restart: unless-stopped

volumes:
  server-data:
  server-logs:
```

### Dockerfile (Server)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000 5001 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Server/Commun.Server.API/Commun.Server.API.csproj", "Server/Commun.Server.API/"]
RUN dotnet restore "Server/Commun.Server.API/Commun.Server.API.csproj"
COPY src/Server/Commun.Server.API/ .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commun.Server.API.dll"]
```

## [S9] 实施计划

### 阶段一：基础架构搭建（2周）
1. 创建解决方案结构
2. 搭建核心协议库框架
3. 配置 CI/CD 流水线
4. 编写单元测试框架

### 阶段二：协议层迁移（3周）
1. 迁移 Modbus 协议实现
2. 迁移 MQTT 协议实现
3. 迁移串口通讯实现
4. 实现插件化加载机制

### 阶段三：客户端开发（4周）
1. 开发 WPF 客户端
2. 重构 WinForm 客户端
3. 开发 CLI 终端
4. 集成测试

### 阶段四：服务端开发（3周）
1. 开发 ASP.NET Core Web API
2. 开发 WebSocket 服务
3. 开发 gRPC 服务
4. 数据层实现

### 阶段五：MCP 集成（2周）
1. 实现 MCP 服务器
2. 定义 MCP 工具接口
3. 集成测试

### 阶段六：Docker 部署（1周）
1. 编写 Dockerfile
2. 配置 docker-compose
3. 部署测试

### 阶段七：文档和测试（1周）
1. 编写 API 文档
2. 编写用户手册
3. 性能测试
4. 安全测试

## [S10] 风险评估

### 技术风险
1. **协议兼容性**：迁移过程中可能丢失某些协议特性
2. **性能影响**：抽象层可能带来性能开销
3. **插件安全性**：动态加载插件可能引入安全风险

### 缓解措施
1. 充分的单元测试和集成测试
2. 性能基准测试
3. 插件签名验证机制

### 时间风险
1. **范围蔓延**：需求可能不断扩展
2. **技术债务**：遗留代码可能难以迁移

### 缓解措施
1. 严格的范围控制
2. 分阶段交付
3. 定期代码审查

## [S11] 成功标准

### 功能完整性
- [ ] 所有现有协议功能正常工作
- [ ] WPF 客户端功能完整
- [ ] WinForm 客户端功能完整
- [ ] CLI 终端功能完整
- [ ] 服务端 API 功能完整
- [ ] MCP 集成正常工作
- [ ] Docker 部署成功

### 性能指标
- [ ] 协议响应时间 < 100ms
- [ ] 服务端支持 1000+ 并发连接
- [ ] 数据库查询响应时间 < 50ms

### 质量指标
- [ ] 单元测试覆盖率 > 80%
- [ ] 集成测试覆盖率 > 70%
- [ ] 代码审查通过率 100%
- [ ] 安全扫描无高危漏洞

## [S12] 附录

### 参考文档
1. .NET 8 文档
2. ASP.NET Core 文档
3. Docker 文档
4. MCP 协议规范
5. Modbus 协议规范

### 术语表
- **MCP**：Model Context Protocol，模型上下文协议
- **PLC**：Programmable Logic Controller，可编程逻辑控制器
- **Modbus**：工业通讯协议
- **MQTT**：消息队列遥测传输协议
- **gRPC**：Google 远程过程调用框架
- **WebSocket**：全双工通讯协议