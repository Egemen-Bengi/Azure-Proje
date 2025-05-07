import React, { useEffect, useState } from "react";
import { Button, Pagination, notification } from "antd";
import "../cssDosyalari/FilmlerFormu.css";

const BegendigimDizilerForm = () => {
  const [begendigimDiziler, setBegendigimDiziler] = useState([]);
  const [yukleniyor, setYukleniyor] = useState(true);
  const [hata, setHata] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  const [api, contextHolder] = notification.useNotification();
  const openNotification = (placement) => {
    api.info({
      
      description: "Bu dizi favori listenizden kaldırıldı.",
      placement,
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      await fetch(
        "https://projeapi.azurewebsites.net/api/dizi-kullanici/kullanici",
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      )
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
          setBegendigimDiziler(data);
          setYukleniyor(false);
        })
        .catch((err) => {
          console.error("Hata:", err.message);
          setHata(err.message);
          setYukleniyor(false);
          
        });
    };
    fetchData();
  }, []);

  const deleteHandler = (diziId) => {
    fetch(`https://projeapi.azurewebsites.net/api/dizi-kullanici/${diziId}`, { 
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Veri silinemedi");
        }
        return response.json();
      })
      .then((data) => {
        console.log("Silinen veri:", data);
        setBegendigimDiziler(
          (prev) => prev.filter((dizi) => dizi.diziId !== diziId) 
        );
      })
      .catch((error) => {
        console.error("Hata:", error);
      });
  };

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
  if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
  if (!begendigimDiziler || begendigimDiziler.length === 0) {
    return <div>Gösterilecek dizi bulunamadı.</div>;
  }

  const startIndex = (currentPage - 1) * itemsPerPage;
          const endIndex = startIndex + itemsPerPage;
          const currentDiziler = begendigimDiziler.slice(startIndex, endIndex);
          

  return (
    <div>
      {contextHolder}
      <ul className="film-list">
        {currentDiziler.map((alinanDizi) => {
          const dizi = alinanDizi.dizi; 
          const formattedName = dizi.diziAdi
            ? dizi.diziAdi.trim().replace(/\s+/g, "")
            : "default";
          const imagePath = `/Films/${formattedName}.jpg`;

          return (
            <li key={dizi.diziId} className="film-item">
              <img
                src={imagePath}
                alt={dizi.diziAdi || "Dizi Adı Bulunamadı"}
                className="film-image"
              />
              <h3>{dizi.diziAdi || "Dizi Adı Bulunamadı"}</h3>
              <p>
                <strong>Yayın Yılı:</strong>{" "}
                {dizi.tarih ? new Date(dizi.tarih).getFullYear() : "Bilinmiyor"}
              </p>
              <p>
                <strong>Açıklama:</strong>{" "}
                {dizi.diziAciklamasi || "Açıklama Yok"}
              </p>
              <p>
                <strong>Bölüm Süresi:</strong>{" "}
                {dizi.sure ? `${dizi.sure} Dakika` : "Bilinmiyor"}
              </p>
              <Button onClick={() => {deleteHandler(dizi.diziId); openNotification("top");} }>Sil</Button>
            </li>
          );
        })}
      </ul>
      <Pagination
        current={currentPage}
        total={begendigimDiziler.length}
        pageSize={itemsPerPage}
        onChange={(page) => setCurrentPage(page)}
        style={{ marginTop: "20px", textAlign: "center" }}
      />
    </div>
  );
};

export default BegendigimDizilerForm;
