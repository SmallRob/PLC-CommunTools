# PLC-CommunTools API Documentation

## Overview

PLC-CommunTools provides a RESTful API for managing PLC devices and communication protocols.

## Base URL

```
http://localhost:5000/api
```

## Authentication

Currently, the API does not require authentication. This will be added in future versions.

## Endpoints

### Devices

- `GET /api/devices` - Get all devices
- `GET /api/devices/{id}` - Get device by ID
- `POST /api/devices` - Create new device
- `PUT /api/devices/{id}` - Update device
- `DELETE /api/devices/{id}` - Delete device

### Protocol

- `GET /api/protocol/protocols` - Get available protocols
- `POST /api/protocol/{deviceId}/connect` - Connect to device
- `POST /api/protocol/{deviceId}/disconnect` - Disconnect from device
- `POST /api/protocol/{deviceId}/send` - Send command to device
- `GET /api/protocol/{deviceId}/status` - Get connection status
