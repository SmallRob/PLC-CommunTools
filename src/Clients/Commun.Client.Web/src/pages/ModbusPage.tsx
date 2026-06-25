import { useState } from 'react'
import { Card, Form, Input, InputNumber, Button, Table, Tag, Space, message } from 'antd'
import { PlayCircleOutlined, PauseCircleOutlined } from '@ant-design/icons'
import { api } from '../services/api'

export default function ModbusPage() {
  const [form] = Form.useForm()
  const [connected, setConnected] = useState(false)
  const [loading, setLoading] = useState(false)
  const [results, setResults] = useState<any[]>([])

  const handleConnect = async () => {
    try {
      setLoading(true)
      const values = await form.validateFields()
      const response = await api.connectModbus(values.host, values.port)
      if (response.success) {
        setConnected(true)
        message.success('Modbus 连接成功')
      } else {
        message.error(response.message)
      }
    } catch (err) {
      message.error('连接失败')
    } finally {
      setLoading(false)
    }
  }

  const handleRead = async () => {
    try {
      const values = await form.validateFields()
      const response = await api.readModbus(values.register, values.length)
      if (response.success) {
        setResults(prev => [...prev, {
          key: Date.now(),
          time: new Date().toLocaleTimeString(),
          type: '读取',
          address: values.register,
          data: response.data,
          status: 'success'
        }])
      } else {
        message.error(response.message)
      }
    } catch (err) {
      message.error('读取失败')
    }
  }

  const columns = [
    { title: '时间', dataIndex: 'time', key: 'time', width: 120 },
    { title: '操作', dataIndex: 'type', key: 'type', width: 80 },
    { title: '地址', dataIndex: 'address', key: 'address', width: 100 },
    { title: '数据', dataIndex: 'data', key: 'data' },
    { title: '状态', dataIndex: 'status', key: 'status', width: 80, render: (s: string) => <Tag color={s === 'success' ? 'green' : 'red'}>{s === 'success' ? '成功' : '失败'}</Tag> },
  ]

  return (
    <div>
      <h1 style={{ marginBottom: '24px', color: '#eeeeee' }}>Modbus TCP 调试</h1>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46', marginBottom: '16px' }}>
        <Form form={form} layout="inline" initialValues={{ host: '192.168.1.100', port: 502, register: 0, length: 1 }}>
          <Form.Item name="host" label="主机" rules={[{ required: true }]}>
            <Input style={{ width: 150 }} />
          </Form.Item>
          <Form.Item name="port" label="端口" rules={[{ required: true }]}>
            <InputNumber style={{ width: 80 }} />
          </Form.Item>
          <Form.Item>
            <Button type="primary" icon={connected ? <PauseCircleOutlined /> : <PlayCircleOutlined />} onClick={handleConnect} loading={loading}>
              {connected ? '断开' : '连接'}
            </Button>
          </Form.Item>
          <Form.Item name="register" label="地址">
            <InputNumber style={{ width: 80 }} />
          </Form.Item>
          <Form.Item name="length" label="长度">
            <InputNumber style={{ width: 80 }} min={1} />
          </Form.Item>
          <Form.Item>
            <Space>
              <Button onClick={handleRead} disabled={!connected}>读取</Button>
            </Space>
          </Form.Item>
        </Form>
      </Card>
      <Card style={{ background: '#2d2d30', border: '1px solid #3f3f46' }}>
        <Table columns={columns} dataSource={results} size="small" pagination={false} />
      </Card>
    </div>
  )
}
