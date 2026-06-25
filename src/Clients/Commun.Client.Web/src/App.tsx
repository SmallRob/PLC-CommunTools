import { Routes, Route } from 'react-router-dom'
import { Layout } from 'antd'
import AppHeader from './components/AppHeader'
import AppSidebar from './components/AppSidebar'
import Dashboard from './pages/Dashboard'
import ModbusPage from './pages/ModbusPage'
import MqttPage from './pages/MqttPage'
import SerialPage from './pages/SerialPage'
import OpcUaPage from './pages/OpcUaPage'
import DevicesPage from './pages/DevicesPage'

const { Content } = Layout

function App() {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <AppSidebar />
      <Layout>
        <AppHeader />
        <Content style={{ margin: '16px', padding: '24px', background: '#1e1e1e', borderRadius: '8px' }}>
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/modbus" element={<ModbusPage />} />
            <Route path="/mqtt" element={<MqttPage />} />
            <Route path="/serial" element={<SerialPage />} />
            <Route path="/opcua" element={<OpcUaPage />} />
            <Route path="/devices" element={<DevicesPage />} />
          </Routes>
        </Content>
      </Layout>
    </Layout>
  )
}

export default App
