import React, { useState } from "react";
import { Header } from "antd/es/layout/layout";
import LoginForm from "./Formlar/LoginForm";
import RegisterModal from "./Formlar/RegisterModal";
import { Route } from "react-router-dom";
import { useNavigate } from "react-router-dom";
const GirisEkrani = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const navigate = useNavigate();

  const onFinish = (values) => { 
    console.log("Başarılı Giriş:", values);
     navigate("/anasayfa");
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  const showRegisterModal = () => {
    setIsModalOpen(true);
  };

  const handleRegister = (values) => {
    console.log("Kayıt Bilgileri:", values);
    setIsModalOpen(false);
  };

  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
          backgroundImage: "url('/src/images/background.jpg')", 
          backgroundSize: "cover", 
          backgroundRepeat: "no-repeat", 
          backgroundPosition: "center", 
        }}
      >
        <div style={{ width: "100%", maxWidth: 600 }}>
          <h1
            style={{
              textAlign: "center",
              fontSize: "48px", 
              fontWeight: "bold", 
              color: "#ffffff", 
              textShadow: "2px 2px 4px rgba(0, 0, 0, 0.7)",
            }}
          >
            Merhaba
          </h1>
          <LoginForm
            onFinish={onFinish} 
            onFinishFailed={onFinishFailed}
            showRegisterModal={showRegisterModal}
          ></LoginForm>
          <RegisterModal
            isModalOpen={isModalOpen}
            setIsModalOpen={setIsModalOpen}
            handleRegister={handleRegister}
          ></RegisterModal>
        </div>
      </div>
    </div>
  );
};

export default GirisEkrani;
