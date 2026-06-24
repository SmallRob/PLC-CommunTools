import { Card, Form, Input, Button } from 'antd'

export default function OpcUaPage() {
  const [form] = Form.useForm()

  return (
    <div>
      <h1 style={{ marginBottom: '24px', color: '#eeeeee' }}>OPC UA 调试</h1>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46', marginBottom: '16px' }}>
        <Form form={form} layout="inline" initialValues={{ endpoint: 'opc.tcp://localhost:4840', nodeId: 'ns=2;s=Demo' }}>
          <Form.Item name="endpoint" label="端点" rules={[{ required: true }]}>
            <Input style={{ width: 250 }} />
          </Form.Item>
          <Form.Item>
            <Button type="primary">连接</Button>
          </Form.Item>
          <Form.Item name="nodeId" label="节点">
            <Input style={{ width: 200 }} />
          </Form.Item>
          <Form.Item>
            <Button>读取</Button>
          </Form.Item>
        </Form>
      </Card>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
        <p style={{ color: '#999' }}>OPC UA 数据日志将在此显示...</p>
      </Card>
    </div>
  )
}
