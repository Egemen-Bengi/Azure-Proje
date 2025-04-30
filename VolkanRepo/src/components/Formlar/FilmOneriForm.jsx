import React from "react";
import { Button, Input } from "antd";

const FilmOneriFormu = () => {
  const handleSubmit = (event) => {
    //burada apiye veri gönderme işlemi yapılacak
  };
  return (
    <div style={{ width: "100%", maxWidth: 600 }}>
      <h2>Film Öneri Botu</h2>
      <form
        style={{
          display: "flex",
          flexDirection: "column",
          gap: "16px",
          padding: "24px",
          backgroundColor: "smokewhite",
          borderRadius: "8px",
          boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
        }}
      >
        <Input placeholder="Izlediginiz bir filmin adini giriniz" />
        <Button type="primary" onClick={handleSubmit}>
          Gönder
        </Button>
      </form>
    </div>
  );
};

export default FilmOneriFormu;
