# Devices API

## Get All Devices

```
GET /api/devices
```

Returns a list of all configured devices.

**Response:**
```json
[
  {
    "id": 1,
    "name": "PLC-001",
    "protocol": "ModbusTcp",
    "address": "192.168.1.100",
    "port": 502,
    "isEnabled": true
  }
]
```

## Get Device by ID

```
GET /api/devices/{id}
```

**Parameters:**
- `id` (int) - Device ID

**Response:**
```json
{
  "id": 1,
  "name": "PLC-001",
  "protocol": "ModbusTcp",
  "address": "192.168.1.100",
  "port": 502,
  "isEnabled": true
}
```

## Create Device

```
POST /api/devices
```

**Request Body:**
```json
{
  "name": "PLC-001",
  "protocol": "ModbusTcp",
  "address": "192.168.1.100",
  "port": 502,
  "isEnabled": true
}
```

**Response:** `201 Created`

## Update Device

```
PUT /api/devices/{id}
```

**Parameters:**
- `id` (int) - Device ID

**Request Body:**
```json
{
  "name": "PLC-001-Updated",
  "protocol": "ModbusTcp",
  "address": "192.168.1.100",
  "port": 502,
  "isEnabled": false
}
```

**Response:** `200 OK`

## Delete Device

```
DELETE /api/devices/{id}
```

**Parameters:**
- `id` (int) - Device ID

**Response:** `204 No Content`
