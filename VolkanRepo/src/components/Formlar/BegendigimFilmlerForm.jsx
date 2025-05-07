import React, { useEffect, useState } from "react";
import { Button, Pagination, notification } from "antd";
import "../cssDosyalari/FilmlerFormu.css";

const BegendigimFilmlerForm = () => {
  const [begendigimFilmler, setBegendigimFilmler] = useState([]);
  const [yukleniyor, setYukleniyor] = useState(true);
  const [hata, setHata] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;

  const [api, contextHolder] = notification.useNotification();
  const openNotification = (placement) => {
    api.info({
      
      description: "Bu film favori listenizden kaldırıldı.",
      placement,
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      await fetch(
        "https://projeapi.azurewebsites.net/api/film-kullanici/kullanici",
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
          setBegendigimFilmler(data);
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

  const deleteHandler = (filmId) => {
    fetch(`https://projeapi.azurewebsites.net/api/film-kullanici/${filmId}`, {
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
        setBegendigimFilmler(
          (prev) => prev.filter((film) => film.filmId !== filmId) // Corrected from film.filmID to film.filmId
        );
      })
      .catch((error) => {
        console.error("Hata:", error);
      });
  };

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
  if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
  if (!begendigimFilmler || begendigimFilmler.length === 0) {
    return <div>Gösterilecek film bulunamadı.</div>;
  }

  const startIndex = (currentPage - 1) * itemsPerPage;
          const endIndex = startIndex + itemsPerPage;
          const currentFilms = begendigimFilmler.slice(startIndex, endIndex);
          

  return (
    <div>
      {contextHolder}
      <ul className="film-list">
        {currentFilms.map((alinanFilm) => {
          const film = alinanFilm.film; 
          const formattedName = film.filmAdi
            ? film.filmAdi.trim().replace(/\s+/g, "")
            : "default";
          const imagePath = `/Films/${formattedName}.jpg`;

          return (
            <li key={film.filmId} className="film-item">
              <img
                src={imagePath}
                alt={film.filmAdi || "Film Adı Bulunamadı"}
                className="film-image"
              />
              <h3>{film.filmAdi || "Film Adı Bulunamadı"}</h3>
              <p>
                <strong>Yayın Yılı:</strong>{" "}
                {film.tarih ? new Date(film.tarih).getFullYear() : "Bilinmiyor"}
              </p>
              <p>
                <strong>Açıklama:</strong>{" "}
                {film.filmAciklamasi || "Açıklama Yok"}
              </p>
              <p>
                <strong>Bölüm Süresi:</strong>{" "}
                {film.sure ? `${film.sure} Dakika` : "Bilinmiyor"}
              </p>
              <Button onClick={() => {deleteHandler(film.id); openNotification("top");} }>Sil</Button>
            </li>
          );
        })}
      </ul>
      <Pagination
        current={currentPage}
        total={begendigimFilmler.length}
        pageSize={itemsPerPage}
        onChange={(page) => setCurrentPage(page)}
        style={{ marginTop: "20px", textAlign: "center" }}
      />
    </div>
  );
};

export default BegendigimFilmlerForm;
