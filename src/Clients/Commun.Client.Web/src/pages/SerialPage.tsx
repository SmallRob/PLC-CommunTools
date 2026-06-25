import { Card, Form, Select, Button } from 'antd'

export default function SerialPage() {
  const [form] = Form.useForm()

  return (
    <div>
      <h1 style={{ marginBottom: '24px', color: '#eeeeee' }}>串口通讯</h1>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46', marginBottom: '16px' }}>
        <Form form={form} layout="inline" initialValues={{ port: 'COM1', baudRate: 9600, dataBits: 8, stopBits: 1, parity: 'none' }}>
          <Form.Item name="port" label="串口">
            <Select style={{ width: 100 }} options={[{ value: 'COM1', label: 'COM1' }, { value: 'COM2', label: 'COM2' }, { value: 'COM3', label: 'COM3' }]} />
          </Form.Item>
          <Form.Item name="baudRate" label="波特率">
            <Select style={{ width: 100 }} options={[{ value: 9600, label: '9600' }, { value: 19200, label: '19200' }, { value: 115200, label: '115200' }]} />
          </Form.Item>
          <Form.Item name="dataBits" label="数据位">
            <Select style={{ width: 80 }} options={[{ value: 7, label: '7' }, { value: 8, label: '8' }]} />
          </Form.Item>
          <Form.Item name="stopBits" label="停止位">
            <Select style={{ width: 80 }} options={[{ value: 1, label: '1' }, { value: 2, label: '2' }]} />
          </Form.Item>
          <Form.Item name="parity" label="校验">
            <Select style={{ width: 80 }} options={[{ value: 'none', label: '无' }, { value: 'even', label: '偶' }, { value: 'odd', label: '奇' }]} />
          </Form.Item>
          <Form.Item>
            <Button type="primary">打开串口</Button>
          </Form.Item>
        </Form>
      </Card>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
        <p style={{ color: '#999' }}>串口数据日志将在此显示...</p>
      </Card>
    </div>
  )
}
