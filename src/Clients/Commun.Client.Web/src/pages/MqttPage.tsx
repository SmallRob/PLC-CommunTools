import { Card, Form, Input, InputNumber, Button } from 'antd'

export default function MqttPage() {
  const [form] = Form.useForm()

  return (
    <div>
      <h1 style={{ marginBottom: '24px', color: '#eeeeee' }}>MQTT 调试</h1>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46', marginBottom: '16px' }}>
        <Form form={form} layout="inline" initialValues={{ broker: 'localhost', port: 1883, topic: 'test/topic', message: 'Hello MQTT' }}>
          <Form.Item name="broker" label="代理" rules={[{ required: true }]}>
            <Input style={{ width: 150 }} />
          </Form.Item>
          <Form.Item name="port" label="端口" rules={[{ required: true }]}>
            <InputNumber style={{ width: 80 }} />
          </Form.Item>
          <Form.Item>
            <Button type="primary">连接</Button>
          </Form.Item>
          <Form.Item name="topic" label="主题">
            <Input style={{ width: 150 }} />
          </Form.Item>
          <Form.Item name="message" label="消息">
            <Input style={{ width: 150 }} />
          </Form.Item>
          <Form.Item>
            <Button>发布</Button>
          </Form.Item>
        </Form>
      </Card>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
        <p style={{ color: '#999' }}>MQTT 消息日志将在此显示...</p>
      </Card>
    </div>
  )
}
