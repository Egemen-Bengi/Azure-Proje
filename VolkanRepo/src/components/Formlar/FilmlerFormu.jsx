import React, { useState, useEffect } from "react";
import { Button, Pagination, notification } from "antd";
import { HeartOutlined } from "@ant-design/icons";
import "../cssDosyalari/FilmlerFormu.css";

const FilmlerFormu = () => {
  const [api, contextHolder] = notification.useNotification(); 
  const openNotification = (placement) => {
    api.info({
      
      description: "Bu film favori listenize eklendi.",
      placement,
    });
  };

  const [filmler, setFilmler] = useState([]);
  const [yukleniyor, setYukleniyor] = useState(true);
  const [hata, setHata] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  const submitHandler = async (film) => {
    try {
      const response = await fetch(
        `https://projeapi.azurewebsites.net/api/film-kullanici/${film.id}`,
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

  useEffect(() => {
    fetch("https://projeapi.azurewebsites.net/api/film", {
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
        setFilmler(data);
        setYukleniyor(false);
      })
      .catch((err) => {
        setHata(err.message);
        setYukleniyor(false);
      });
  }, []);

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
  if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
  if (!yukleniyor && filmler.length === 0) {
    return <div>Gösterilecek film bulunamadı.</div>;
  }

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const currentFilms = filmler.slice(startIndex, endIndex);

  return (
    <div>
      {contextHolder} 
      <ul className="film-list">
        {currentFilms.map((film) => {
          const formattedName = film.filmAdi.trim().replace(/\s+/g, "");
          const imagePath = `/Films/${formattedName}.jpg`;

          return (
            <li key={film.filmId} className="film-item">
              <img src={imagePath} alt={film.filmAdi} className="film-image" />
              <h3>{film.filmAdi}</h3>
              <p>
                <strong>Yayın Yılı:</strong>{" "}
                {new Date(film.tarih).getFullYear()}
              </p>
              <p>
                <strong>Açıklama:</strong> {film.filmAciklamasi}
              </p>
              <p>
                <strong>Bölüm Süresi:</strong> {film.sure} Dakika
              </p>
              <Button
                icon={<HeartOutlined />}
                onClick={() => {
                  submitHandler(film);
                  openNotification("top");
                }}
              />
            </li>
          );
        })}
      </ul>
      <Pagination
        current={currentPage}
        total={filmler.length}
        pageSize={itemsPerPage}
        onChange={(page) => setCurrentPage(page)}
        style={{ marginTop: "20px", textAlign: "center" }}
      />
    </div>
  );
};

export default FilmlerFormu;