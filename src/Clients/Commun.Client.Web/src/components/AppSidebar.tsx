import { Layout, Menu } from 'antd'
import {
  DashboardOutlined,
  ApiOutlined,
  CloudOutlined,
  UsbOutlined,
  NodeIndexOutlined,
  DatabaseOutlined,
} from '@ant-design/icons'
import { useNavigate, useLocation } from 'react-router-dom'

const { Sider } = Layout

const menuItems = [
  { key: '/', icon: <DashboardOutlined />, label: '仪表盘' },
  { key: '/modbus', icon: <ApiOutlined />, label: 'Modbus TCP' },
  { key: '/mqtt', icon: <CloudOutlined />, label: 'MQTT' },
  { key: '/serial', icon: <UsbOutlined />, label: '串口通讯' },
  { key: '/opcua', icon: <NodeIndexOutlined />, label: 'OPC UA' },
  { key: '/devices', icon: <DatabaseOutlined />, label: '设备管理' },
]

export default function AppSidebar() {
  const navigate = useNavigate()
  const location = useLocation()

  return (
    <Sider width={220} style={{ background: '#1e1e1e', borderRight: '1px solid #3f3f46' }}>
      <div style={{ padding: '16px', textAlign: 'center' }}>
        <h2 style={{ color: '#0078d7', margin: 0, fontSize: '18px' }}>PLC Tools</h2>
      </div>
      <Menu
        mode="inline"
        selectedKeys={[location.pathname]}
        items={menuItems}
        onClick={({ key }) => navigate(key)}
        style={{ background: 'transparent', borderRight: 'none' }}
      />
    </Sider>
  )
}
