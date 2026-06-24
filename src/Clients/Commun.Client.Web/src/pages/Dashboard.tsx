import { Card, Row, Col, Statistic, Tag } from 'antd'
import {
  ApiOutlined,
  CloudOutlined,
  UsbOutlined,
  NodeIndexOutlined,
  CloseCircleOutlined,
} from '@ant-design/icons'

export default function Dashboard() {
  return (
    <div>
      <h1 style={{ marginBottom: '24px', color: '#eeeeee' }}>通讯概览</h1>
      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12} lg={6}>
          <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
            <Statistic
              title="Modbus TCP"
              value={0}
              prefix={<ApiOutlined />}
              suffix="连接"
            />
            <Tag icon={<CloseCircleOutlined />} color="error" style={{ marginTop: '8px' }}>未连接</Tag>
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
            <Statistic
              title="MQTT"
              value={0}
              prefix={<CloudOutlined />}
              suffix="连接"
            />
            <Tag icon={<CloseCircleOutlined />} color="error" style={{ marginTop: '8px' }}>未连接</Tag>
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
            <Statistic
              title="串口"
              value={0}
              prefix={<UsbOutlined />}
              suffix="连接"
            />
            <Tag icon={<CloseCircleOutlined />} color="error" style={{ marginTop: '8px' }}>未连接</Tag>
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
            <Statistic
              title="OPC UA"
              value={0}
              prefix={<NodeIndexOutlined />}
              suffix="连接"
            />
            <Tag icon={<CloseCircleOutlined />} color="error" style={{ marginTop: '8px' }}>未连接</Tag>
          </Card>
        </Col>
      </Row>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46', marginTop: '24px' }}>
        <h3 style={{ color: '#eeeeee', marginBottom: '16px' }}>快速开始</h3>
        <p style={{ color: '#999' }}>选择左侧菜单开始通讯调试：</p>
        <ul style={{ color: '#999', paddingLeft: '20px', marginTop: '8px' }}>
          <li><strong>Modbus TCP</strong> - 工业 PLC 通讯协议</li>
          <li><strong>MQTT</strong> - 物联网消息协议</li>
          <li><strong>串口通讯</strong> - RS232/RS485 通讯</li>
          <li><strong>OPC UA</strong> - 工业自动化协议</li>
        </ul>
      </Card>
    </div>
  )
}
