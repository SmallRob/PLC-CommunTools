# Protocol API

## Get Available Protocols

```
GET /api/protocol/protocols
```

Returns a list of supported communication protocols.

**Response:**
```json
[
  "ModbusTcp",
  "ModbusRtu",
  "Mqtt"
]
```

## Connect to Device

```
POST /api/protocol/{deviceId}/connect
```

**Parameters:**
- `deviceId` (int) - Device ID

**Response:**
```json
{
  "success": true,
  "message": "Connected to device PLC-001"
}
```

## Disconnect from Device

```
POST /api/protocol/{deviceId}/disconnect
```

**Parameters:**
- `deviceId` (int) - Device ID

**Response:**
```json
{
  "success": true,
  "message": "Disconnected from device PLC-001"
}
```

## Send Command to Device

```
POST /api/protocol/{deviceId}/send
```

**Parameters:**
- `deviceId` (int) - Device ID

**Request Body:**
```json
{
  "command": "ReadHoldingRegisters",
  "startAddress": 0,
  "count": 10
}
```

**Response:**
```json
{
  "success": true,
  "data": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}
```

## Get Connection Status

```
GET /api/protocol/{deviceId}/status
```

**Parameters:**
- `deviceId` (int) - Device ID

**Response:**
```json
{
  "deviceId": 1,
  "isConnected": true,
  "protocol": "ModbusTcp",
  "lastActivity": "2026-06-24T12:00:00Z"
}
```
