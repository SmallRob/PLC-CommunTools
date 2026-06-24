import axios from 'axios'

const client = axios.create({
  baseURL: '/api',
  timeout: 10000,
})

export const api = {
  async connectModbus(host: string, port: number) {
    try {
      const response = await client.post('/protocol/modbus/connect', { host, port })
      return response.data
    } catch {
      return { success: false, message: '连接失败' }
    }
  },

  async readModbus(register: number, length: number) {
    try {
      const response = await client.post('/protocol/modbus/read', { register, length })
      return response.data
    } catch {
      return { success: false, message: '读取失败' }
    }
  },

  async getDevices() {
    try {
      const response = await client.get('/devices')
      return response.data
    } catch {
      return []
    }
  },
}
