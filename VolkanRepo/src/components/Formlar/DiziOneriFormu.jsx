import React, { useState } from "react";
import { Button, Input } from "antd";

const DiziOneriFormu = () => {
  const [inputValue, setInputValue] = useState("");
  const [oneri, setOneri] = useState(null);
  const [yukleniyor, setYukleniyor] = useState(false); 

  const handleInputChange = (event) => {
    setInputValue(event.target.value);
  };

  const handleSubmit = async () => {
    const seriesBody = inputValue.split(",").map((dizi) => dizi.trim());
    console.log("Gönderilen Veri:", seriesBody);

    setYukleniyor(true); 
    try {
      const response = await fetch(
        `https://projeapi.azurewebsites.net/api/oneri/dizi`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
          body: JSON.stringify(seriesBody),
        }
      );

      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(errorMessage);
      }

      const rawData = await response.text();
      const validJson = rawData.replace(/'/g, '"'); 
      const data = JSON.parse(validJson); 

      setOneri(data); 
      console.log("API Response:", data); 
    } catch (error) {
      console.error("Hata:", error.message);
      setOneri({ error: "Bir hata oluştu: " + error.message }); 
    } finally {
      setYukleniyor(false); 
    }
  };

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>; 

  return (
    <div style={{ width: "100%", maxWidth: 600 }}>
      <h2>Dizi Öneri Botu</h2>
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
        <Input
          placeholder="Izlediginiz bir dizinin adini giriniz"
          value={inputValue}
          onChange={handleInputChange}
        />
        <Button type="primary" onClick={handleSubmit}>
          Gonder
        </Button>
      </form>

      {oneri && (
        <div
          style={{
            marginTop: "16px",
            padding: "16px",
            backgroundColor: "#f6f6f6",
            borderRadius: "8px",
            boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
          }}
        >
          <h3>Öneriler:</h3>
          {oneri.error ? (
            <p style={{ color: "red" }}>{oneri.error}</p>
          ) : (
            Object.keys(oneri).map((key) => (
              <div key={key} style={{ marginBottom: "12px" }}>
                <strong>{oneri[key]["dizi adi"]}</strong>
                <p>{oneri[key]["aciklama"]}</p>
              </div>
            ))
          )}
        </div>
      )}
    </div>
  );
};

export default DiziOneriFormu;