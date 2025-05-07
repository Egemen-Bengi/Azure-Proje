import React, { useEffect, useState } from 'react';
import { Button, notification, Pagination } from 'antd';
import { HeartOutlined } from "@ant-design/icons";
import "../cssDosyalari/SeriesForm.css";

const DizilerFormu = () => {
    const [api, contextHolder] = notification.useNotification(); 
    const openNotification = (placement) => {
        api.info({
        
        description: "Bu film favori listenize eklendi.",
        placement,
        });
    };
    const [tvSeries, setTvSeries] = useState([]);
    const [yukleniyor, setYukleniyor] = useState(true);
    const [hata, setHata] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 10;

    useEffect(() => {
        fetch("https://projeapi.azurewebsites.net/api/dizi", {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error("Veri alınamadı");
            }
            return response.json();
          })
          .then((data) => {
            if (!data || !Array.isArray(data)) {
              throw new Error("Beklenmeyen veri formatı");
            }
            setTvSeries(data);
            setYukleniyor(false);
          })
          .catch((err) => {
            setHata(err.message);
            setYukleniyor(false);
          });
      }, []);

    const submitHandler = async (serie) => {
        try {
          const response = await fetch(
            `https://projeapi.azurewebsites.net/api/dizi-kullanici/${serie.id}`,
            {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("token")}`,
              },
            }
          );
          if (!response.ok) {
            throw new Error("Veri eklenemedi");
          }
          const data = await response.json();
          
        } catch (error) {
          console.error("Hata:", error);
        }
      };
    
      if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
      if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
      if (!yukleniyor && tvSeries.length === 0) {
        return <div>Gösterilecek dizi bulunamadı.</div>;
      }
    
      const startIndex = (currentPage - 1) * itemsPerPage;
      const endIndex = startIndex + itemsPerPage;
      const currentSeries = tvSeries.slice(startIndex, endIndex);

    return (
        <div>
            {contextHolder} 
            <ul className="dizi-list">
                {currentSeries.map((serie) => {
                const formattedName = serie.diziAdi.toLowerCase().replace(/\s+/g, '');
                const imagePath = `/tvSeries/${formattedName}.jpg`;

                return (
                    <li key={serie.diziAdi} className="dizi-item">
                    <img src={imagePath} alt={serie.diziAdi} className="dizi-image" />
                    <h3>{serie.diziAdi}</h3>
                    <p>
                        <strong>Yayın Yılı:</strong>{" "}
                        {new Date(serie.tarih).getFullYear()}
                    </p>
                    <p>
                        <strong>Açıklama:</strong> {serie.diziAciklamasi}
                    </p>
                    <p>
                        <strong>Bölüm Süresi:</strong> {serie.sure} Dakika
                    </p>
                    <p>
                        <strong>Sezon sayısı:</strong> {serie.sezonSayisi}
                    </p>
                    <Button
                        icon={<HeartOutlined />}
                        onClick={() => {
                        submitHandler(serie);
                        openNotification("top");
                        }}
                    />
                    </li>
                );
                })}
            </ul>
            <Pagination
                current={currentPage}
                total={tvSeries.length}
                pageSize={itemsPerPage}
                onChange={(page) => setCurrentPage(page)}
                style={{ marginTop: "20px", textAlign: "center" }}
            />
        </div>
)};

export default DizilerFormu;