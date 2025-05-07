import React from "react";
import { Button, Form, Input, Modal } from "antd";

const RegisterModal = ({ isModalOpen, setIsModalOpen, handleRegister }) => {
  const [form] = Form.useForm(); 

  const handleCancel = () => {
    form.resetFields(); 
    setIsModalOpen(false); 
  };

  return (
    <Modal
      title="Kayıt Ol"
      open={isModalOpen}
      onCancel={handleCancel} 
      footer={null}
    >
      <Form form={form} layout="vertical" onFinish={handleRegister}>
        <Form.Item
          label="E-posta"
          name="Email"
          rules={[{ required: true, message: "Lütfen e-posta girin!" }]}
        >
          <Input placeholder="E-Postanızı giriniz" />
        </Form.Item>
        <Form.Item
          label="Kullanıcı Adı"
          name="KullaniciAdi"
          rules={[{ required: true, message: "Lütfen kullanıcı adı girin!" }]}
        >
          <Input placeholder="Kullanıcı adınızı girin" />
        </Form.Item>

        <Form.Item
          label="Şifre"
          name="Parola"
          rules={[{ required: true, message: "Lütfen şifre girin!" }]}
        >
          <Input.Password placeholder="Şifrenizi girin" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" block>
            Kayıt Ol
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default RegisterModal;