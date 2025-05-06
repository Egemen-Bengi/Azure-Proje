import React, { useEffect, useState } from "react";

const BegendigimFilmlerForm = () => {
  const [begendigimFilmler, setBegendigimFilmler] = useState([]);
  const [yukleniyor, setYukleniyor] = useState(true);
  const [hata, setHata] = useState(null);

  useEffect(() => {
    fetch("http://localhost:5000/api/diziler")
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
        setHata(err.message);
        setYukleniyor(false);
      });
  }, []);

  const deleteHandler = (filmId) => {
    fetch(`http://localhost:5000/api/film-kullanici/${filmId}`, {
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
        // Listeden silinen diziyi çıkar
        setBegendigimFilmler((prev) =>
          prev.filter((film) => film.id !== filmId)
        );
      })
      .catch((error) => {
        console.error("Hata:", error);
      });
  };

  if (yukleniyor) return <div className="loading">Yükleniyor...</div>;
  if (hata) return <div style={{ color: "red" }}>Hata: {hata}</div>;
  if (begendigimFilmler.length === 0) {
    return <div>Gösterilecek film bulunamadı.</div>;
  }

  return (
    <div>
      <h2>Filmler</h2>
      <ul className="film-list">
        {begendigimFilmler.map((film) => (
          <li key={film.filmID} className="film-item">
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
            <button
              onClick={() => deleteHandler(film.filmId)}
              style={{ marginTop: "10px", color: "white", backgroundColor: "red", border: "none", padding: "8px", cursor: "pointer" }}
            >
              Sil
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BegendigimFilmlerForm;
