import React, { useState } from "react";
import { Header } from "antd/es/layout/layout";
import LoginForm from "./Formlar/LoginForm";
import RegisterModal from "./Formlar/RegisterModal";
import { Route } from "react-router-dom";
import { useNavigate } from "react-router-dom";
const GirisEkrani = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const navigate = useNavigate();
  const [token, setToken] = useState(null);

  const onFinish = async (values) => {
    try {
      const response = await fetch("http://localhost:7071/api/account/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          KullaniciAdi: values.kullaniciAdi,
          Parola: values.parola,
        }),
      });

      if (!response.ok) {
        throw new Error("Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
      }

      const data = await response.json();
      console.log("Giriş başarılı:", data);

      // Token varsa localStorage ya da state'e kaydedilebilir
      setToken(data.token);
      localStorage.setItem("token", data.token);

      navigate("/anasayfa"); // giriş başarılıysa yönlendir
    } catch (error) {
      console.error("Hata:", error.message);
      alert(error.message); // veya kullanıcıya uygun bir mesaj gösterin
    }
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  const showRegisterModal = () => {
    setIsModalOpen(true);
  };

  const handleRegister = async (values) => {
    const payload = {
      KullaniciAdi: values.KullaniciAdi,
      Parola: values.Parola
    };
  
    try {
      const response = await fetch("http://localhost:7071/api/account/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
  
      if (!response.ok) {
        throw new Error("Kayıt işlemi başarısız oldu.");
      }
  
      const data = await response.json();
      console.log("Kayıt başarılı:", data);
  
      alert("Kayıt başarılı! Giriş yapabilirsiniz.");
      setIsModalOpen(false); 
    } catch (error) {
      console.error("Kayıt hatası:", error.message);
      alert(error.message);
    }
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
