import { Card, Table, Button, Tag, Space } from 'antd'
import { PlusOutlined, DeleteOutlined, EditOutlined } from '@ant-design/icons'

const columns = [
  { title: '名称', dataIndex: 'name', key: 'name' },
  { title: '协议', dataIndex: 'protocol', key: 'protocol', render: (p: string) => <Tag color="blue">{p}</Tag> },
  { title: '地址', dataIndex: 'address', key: 'address' },
  { title: '状态', dataIndex: 'status', key: 'status', render: (s: string) => <Tag color={s === '在线' ? 'green' : 'default'}>{s}</Tag> },
  { title: '操作', key: 'action', render: () => <Space><Button size="small" icon={<EditOutlined />} /><Button size="small" danger icon={<DeleteOutlined />} /></Space> },
]

const data = [
  { key: '1', name: 'PLC-1', protocol: 'Modbus', address: '192.168.1.100:502', status: '离线' },
  { key: '2', name: 'Sensor-Hub', protocol: 'MQTT', address: 'localhost:1883', status: '离线' },
  { key: '3', name: 'OPC-Server', protocol: 'OPC UA', address: 'opc.tcp://localhost:4840', status: '离线' },
]

export default function DevicesPage() {
  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '24px' }}>
        <h1 style={{ color: '#eeeeee', margin: 0 }}>设备管理</h1>
        <Button type="primary" icon={<PlusOutlined />}>添加设备</Button>
      </div>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
        <Table columns={columns} dataSource={data} />
      </Card>
    </div>
  )
}
