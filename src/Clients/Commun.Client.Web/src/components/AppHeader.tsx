import { Layout, Typography } from 'antd'
import { GithubOutlined } from '@ant-design/icons'

const { Header } = Layout
const { Title } = Typography

export default function AppHeader() {
  return (
    <Header style={{
      background: '#1e1e1e',
      padding: '0 24px',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      borderBottom: '1px solid #3f3f46'
    }}>
      <Title level={4} style={{ margin: 0, color: '#eeeeee' }}>
        PLC-CommunTools
      </Title>
      <GithubOutlined style={{ fontSize: '20px', color: '#eeeeee', cursor: 'pointer' }} />
    </Header>
  )
}
