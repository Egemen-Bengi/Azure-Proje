import React, { useState } from "react";
import { Button, Checkbox, Form, Input, Modal, AutoComplete } from "antd";
import "antd/dist/reset.css";


const LoginForm = ({onFinish, onFinishFailed, showRegisterModal}) => {
 
  return (

      <div style={{ width: "100%", maxWidth: 600 }}>
        {/* Giriş Formu */}
        <Form
          name="basic"
          layout="vertical"
          style={{
            display: "flex",
            flexDirection: "column",
            gap: "16px",
            padding: "24px",
            backgroundColor: "white",
            borderRadius: "8px",
            boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
          }}
          initialValues={{ remember: true }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
        >
          <Form.Item
            label="Kullanıcı Adı"
            name="KullaniciAdi"
            rules={[{ required: true, message: "Lütfen kullanıcı adı girin!" }]}
          >
            <Input placeholder="Kullanıcı Adı" />
          </Form.Item>

          <Form.Item
            label="Şifre"
           name="Parola"
            rules={[{ required: true, message: "Lütfen şifre girin!!" }]}
          >
            <Input.Password placeholder="Şifre" />
          </Form.Item>

          <Form.Item name="remember" valuePropName="checked">
            <Checkbox>Beni Hatırla</Checkbox>
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" block>
              Giriş Yap
            </Button>
          </Form.Item>

          <Button type="link" onClick={showRegisterModal} block>
            Kayıt Ol
          </Button>
        </Form>
      </div>
  );
};

export default LoginForm;